﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basler.Pylon;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;
using Automation.BDaq;
using System.Threading;
using processWeb;
using Npgsql;
using System.IO;
using Newtonsoft.Json;
using SandPaperInspection.classes;

namespace SandPaperInspection
{
    public partial class InspectionPage : Form
    {
        int counterOk = 0;
        int counteDefect = 0;
        int counterTotal = 0;
        ModelData modelData = new ModelData();
        int FrameCount = 0;
        CameraParameters CameraParameters = new CameraParameters();
        Database db = new Database();
        //Class1 algo = new Class1();
        
        private Camera camera1 = null;
        private PixelDataConverter converter1;
        Bitmap imgCam1;
        Bitmap imgCam2;
        private Camera camera2 = null;
        private PixelDataConverter converter2;

        bool captureImages = false;

        bool pink = false;
        bool blue = false;
        bool image1Grabbed = false;
        bool image2Grabbed = false;
        bool doProcess = false;
        private Stopwatch stopWatch = new Stopwatch();
        Thread mergeImagesThread1;
        Thread mergeImagesThread2;
        Thread processThread;
        Thread saveData;
        Bitmap finalImage;
        static Bitmap imageCam1;
        static Bitmap imageCam2;
        int heightAddon = 0;

        List<Bitmap> bitmapsMerge1 = new List<Bitmap>();
        List<Bitmap> bitmapsMerge2 = new List<Bitmap>();
        
        class DefectImageData
        {
            public Rectangle cropRect { get; set; }
            public double defectArea { get; set; }
            public int defectCode { get; set; }
            public string[] defectType = new string[] { "line Marks", "wrinkle", "hole or cut","Tape", "other" };
            public Bitmap image { get; set; }
            public string path { get; set; }
            public DateTime dateTime { get; set; }
            public string modelName { get; set; }
            public Point defLocation { get; set; }
            public Point defSize { get; set; }
            public string finish { get; set; }
            public string operation { get; set; }
            public string rollNum { get; set; }
            public string batchNum { get; set; }

            public DefectImageData(DefectImageData did)
            {
                this.cropRect = did.cropRect;
                this.defectArea = did.defectArea;
                this.defectCode = did.defectCode;
                this.defectType = did.defectType;
                this.image = did.image;
                this.path = did.path;
                this.dateTime = did.dateTime;
                this.modelName = did.modelName;
                this.defLocation = did.defLocation;
                this.defSize = did.defSize;
                this.finish = did.finish;
                this.operation = did.operation;
                this.rollNum = did.rollNum;
                this.batchNum = did.batchNum;

            }
            public DefectImageData()
            {

            }
        }

        class ImageData
        {
            public List<DefectImageData> defectImageDatas = new List<DefectImageData>();
            public Bitmap image;
            public ImageData(ImageData id)
            {
                this.defectImageDatas = id.defectImageDatas;
                this.image = id.image;
            }
            public ImageData()
            {

            }

        }

        List<ImageData> imageDataList = new List<ImageData>();
        //static List<DefectImageData> defectImageDataList = new List<DefectImageData>();

        NpgsqlTypes.NpgsqlPoint loc = new NpgsqlTypes.NpgsqlPoint(1200, 2000);
        Size defSize = new Size(40, 50);

        //---------------------------------Daq Navi--------------------
        int outputCardState = 0;
        int outputCardStatePriv = 0;
        static int outPutSubPC = 0;
        ErrorCode err = ErrorCode.Success;

        private void instantDiCtrl1_ChangeOfState(object sender, DiSnapEventArgs e)
        {
            byte[] portData = e.PortData;
            Console.WriteLine("input evetnt occured =======");
            try
            {
                string valueString = null;
                for (int j = 0; j < portData.Length; j++)
                {
                    valueString += portData[j].ToString("X2");
                    if (j < portData.Length - 1)
                    {
                        valueString += ", ";
                    }
                }
                Console.WriteLine("input data =======" + valueString);
            }
            catch (System.Exception) { }
        }
       

        #region //cameraCode
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnConnectionLost), sender, e);
                return;
            }

            // Close the camera object.
            DestroyCamera(camera1, converter1);
            DestroyCamera(camera2, converter2);
            
            // Because one device is gone, the list needs to be updated.
            // UpdateDeviceList();
        }


        // Occurs when the connection to a camera device is opened.
        private void OnCameraOpened(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraOpened), sender, e);
                return;
            }

            // The image provider is ready to grab. Enable the grab buttons.
        }


        // Occurs when the connection to a camera device is closed.
        private void OnCameraClosed(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraClosed), sender, e);
                return;
            }

            // The camera connection is closed. Disable all buttons.
        }

        // Occurs when a camera starts grabbing.
        private void OnGrabStarted(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnGrabStarted), sender, e);
                return;
            }

            // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed.

            stopWatch.Reset();

            // Do not update the device list while grabbing to reduce jitter. Jitter may occur because the GUI thread is blocked for a short time when enumerating.
            // updateDeviceListTimer.Stop();

            // The camera is grabbing. Disable the grab buttons. Enable the stop button.
        }

        // Occurs when a camera has stopped grabbing.
        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<GrabStopEventArgs>(OnGrabStopped), sender, e);
                return;
            }

            // Reset the stopwatch.
            stopWatch.Reset();

            // Re-enable the updating of the device list.
            //updateDeviceListTimer.Start();

            // The camera stopped grabbing. Enable the grab buttons. Disable the stop button.
            if (e.Reason != GrabStopReason.UserRequest)

            {
                MessageBox.Show("A grab error occured:\n" + e.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        // Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed1(Object sender, ImageGrabbedEventArgs e)
        {
            
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed1), sender, e.Clone());
                return;
            }

            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    // if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    // {
                    stopWatch.Restart();
                    //     Console.WriteLine("1");
                    Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                    // Lock the bits of the bitmap.
                    //   Console.WriteLine("2");
                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    // Place the pointer to the buffer of the bitmap.
                    //   Console.WriteLine("3");
                    converter1.OutputPixelFormat = PixelType.BGRA8packed;
                    //  Console.WriteLine("3.25");
                    IntPtr ptrBmp = bmpData.Scan0;
                    converter1.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                    bitmap.UnlockBits(bmpData);
                    Bitmap bmpNew;
                    try
                    {
                        bmpNew = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error in cloning bitmap ImageGrab1 error: {0}", ex.Message);
                        return;
                    }
                    //pictureBox5.Image = bmpNew;
                    // Assign a temporary variable to dispose the bitmap after assigning the new bitmap to the display control.
                   // Bitmap bitmapOld = pictureBox.Image as Bitmap;
                    // Provide the display control with the new bitmap. This action automatically updates the display.
                    byte[] pixelData = (byte[])e.GrabResult.PixelData;

                    //pictureBox5.Image = bmpNew;
                    if (captureImages == true)
                    {
                        try
                        {
                            bitmapsMerge1.Add(bmpNew.Clone(new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), PixelFormat.Format24bppRgb));
                            heightAddon += bmpNew.Height;
                            imageCam1 = bmpNew;
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine("Error in cloning bitmap ImageGrab1 error: {0}", ex.Message);
                            return;
                        }
                        
                    }
                    image1Grabbed = true;
                    FrameCount++;
                    //if (bitmapOld != null)
                    //{
                    //    // Dispose the bitmap.
                    //    bitmapOld.Dispose();
                    //}

                    //labelDefType.Text = counter1.ToString();

                }
                
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }

        // Closes the camera object and handles exceptions.
        private void DestroyCamera(Camera camera, PixelDataConverter converter)
        {


            // Destroy the camera object.
            try
            {
                if (camera != null)
                {
                    camera.Close();
                    camera.Dispose();
                    camera = null;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Destroy Cam Exxxxx----------------");
                ShowException(exception);
            }

            // Destroy the converter object.
            if (converter != null)
            {
                converter.Dispose();
                converter = null;
            }
        }

        public Bitmap MergeImagesSidewaysOverlap(Image firstImage, Image secondImage, int overlap)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                if (firstImage == null)
                {
                    throw new ArgumentNullException("firstImage");
                }

                if (secondImage == null)
                {
                    throw new ArgumentNullException("secondImage");
                }

                int outputImageWidth = (firstImage.Width - overlap) + secondImage.Width;

                int outputImageHeight = firstImage.Height < secondImage.Height ? firstImage.Height : secondImage.Height;

                Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Bitmap outputImage1 = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                using (Graphics graphics = Graphics.FromImage(outputImage))
                {
                    graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size),
                        new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                    graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width - overlap/2, 0), secondImage.Size),
                        new Rectangle(new Point(overlap/2, 0), secondImage.Size), GraphicsUnit.Pixel);
                    graphics.Dispose();
                }

                outputImage1 = (Bitmap)outputImage.Clone();
                sw.Stop();
                Console.WriteLine("Time taken to merge images {0}", sw.ElapsedMilliseconds);
                return outputImage1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            
        }

        public Bitmap MergeImagesSideways(Image firstImage, Image secondImage)
        {
            if (firstImage == null)
            {
                throw new ArgumentNullException("firstImage");
            }

            if (secondImage == null)
            {
                throw new ArgumentNullException("secondImage");
            }

            int outputImageWidth = firstImage.Width + secondImage.Width;

            int outputImageHeight = firstImage.Height < secondImage.Height ? firstImage.Height : secondImage.Height;

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap outputImage1 = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size),
                    new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width, 0), secondImage.Size),
                    new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                graphics.Dispose();
            }

            outputImage1 = (Bitmap)outputImage.Clone();
            return outputImage1;
        }

        private async Task MergeImagesCam2()
        {
            //mergeImagesThread2 = new Thread(delegate ()
            //{
                Cursor.Current = Cursors.WaitCursor;
                if (bitmapsMerge2.Count > 0)
                {

                    Bitmap bmp = bitmapsMerge2[0];
                    
                    for (int i = 1; i < bitmapsMerge2.Count; i++)
                    {
                        bmp = MergeTwoImages((Bitmap)bmp, bitmapsMerge2[i]);
                    }
                    // System.Threading.Thread.Sleep(4000);
                    bmp.Save(@"D:\image7090.jpg");
                    //pictureBox4.Invoke(new EventHandler(delegate
                    //{
                    //    pictureBox4.Image = bmp;
                    //}));
                    
                    imageCam2 = (Bitmap)bmp.Clone();
                    Console.WriteLine(bitmapsMerge2.Count());
                    Console.WriteLine("Image 2 " + imageCam2.Height);

                    bitmapsMerge2.Clear();
                Console.WriteLine("Image 1 {0} Image 2 {1}", imageCam1.Size, imageCam2.Size);
                    finalImage = MergeImagesSideways((Bitmap)imageCam1, (Bitmap)imageCam2);
                //finalImage = (Bitmap)imageCam1.Clone();
                    finalImage.Save(@"D:\image7010.jpg");


                pictureBox5.Image = (Bitmap)finalImage.Clone();

                Console.WriteLine("task done");
                Cursor.Current = Cursors.Default;

                }
            //});
            //mergeImagesThread2.Start();
        }

        private async Task MergeImagesCam1()
        {
            //mergeImagesThread1 = new Thread(delegate ()
            //{
                Cursor.Current = Cursors.WaitCursor;
                if (bitmapsMerge1.Count > 0)
                {
                    Bitmap bmp = bitmapsMerge1[0];
                    
                    for (int i = 1; i < bitmapsMerge1.Count; i++)
                    {
                        bmp = MergeTwoImages((Bitmap)bmp.Clone(), bitmapsMerge1[i]);
                    }
                    //bmp.Save(@"D:\image7080.jpg");
                    //pictureBox3.Invoke(new EventHandler(delegate
                    //{
                    //    pictureBox3.Image = bmp;
                    //}));
                    //Invoke pictureBox3.Image = bmp;

                    imageCam1 = (Bitmap)bmp.Clone();
                    Console.WriteLine(bitmapsMerge1.Count());
                    bitmapsMerge1.Clear();
                    Console.WriteLine("Image 1 " + imageCam1.Height);
                    Console.WriteLine("task done");
                    Cursor.Current = Cursors.Default;
                }
            //});
           // mergeImagesThread1.Start();
        }

        public Bitmap MergeTwoImages(Image firstImage, Image secondImage)
        {
            if (firstImage == null)
            {
                throw new ArgumentNullException("firstImage");
            }

            if (secondImage == null)
            {
                throw new ArgumentNullException("secondImage");
            }

            int outputImageWidth = firstImage.Width > secondImage.Width ? firstImage.Width : secondImage.Width;

            int outputImageHeight = firstImage.Height + secondImage.Height;

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bitmap outputImage1 = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //BitmapData bmpData = outputImage.LockBits(new Rectangle(0, 0, outputImage.Width, outputImage.Height), ImageLockMode.ReadWrite, outputImage.PixelFormat);
            //outputImage.UnlockBits(bmpData);

            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size),
                    new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                graphics.DrawImage(secondImage, new Rectangle(new Point(0, firstImage.Height), secondImage.Size),
                    new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                graphics.Dispose();
            }

            outputImage1 = (Bitmap)outputImage.Clone();

            return outputImage1;
        }


        // Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed2(Object sender, ImageGrabbedEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed2), sender, e.Clone());
                return;
            }

            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.
                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    // if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    //  {
                    stopWatch.Restart();
                    //      Console.WriteLine("1");
                    Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                    // Lock the bits of the bitmap.
                    //     Console.WriteLine("2");
                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    // Place the pointer to the buffer of the bitmap.
                    //  Console.WriteLine("3");
                    converter2.OutputPixelFormat = PixelType.BGRA8packed;
                    //  Console.WriteLine("3.25");
                    IntPtr ptrBmp = bmpData.Scan0;
                    converter2.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                    bitmap.UnlockBits(bmpData);
                    Bitmap bmpNew = null;
                    try
                    {
                        bmpNew = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);

                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error in cloning bitmap ImageGrabbed2 error: {0}", ex.Message);
                    }

                    //pictureBox5.Image = bmpNew;

                    // Assign a temporary variable to dispose the bitmap after assigning the new bitmap to the display control.
                    //Bitmap bitmapOld = pictureBox.Image as Bitmap;
                    // Provide the display control with the new bitmap. This action automatically updates the display.
                    //Console.WriteLine("SizeX:{0}", e.GrabResult.Width);
                    //Console.WriteLine("SizeY:{0}", e.GrabResult.Height);
                    byte[] pixelData = (byte[])e.GrabResult.PixelData;

                    if (captureImages == true)
                    {
                        try
                        {
                            bitmapsMerge2.Add(bmpNew.Clone(new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), PixelFormat.Format24bppRgb));
                            imageCam2 = bmpNew;
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine("Error in cloning bitmap ImageGrabbed2 error: {0}", ex.Message);
                        }

                    }

                    // Console.WriteLine("Gray value of first pixel:{0}", pixelData[0]);
                    image2Grabbed = true;

                    //labelDefArea.Text = counter2.ToString();

                    //if (bitmapOld != null)
                    //{
                    //    // Dispose the bitmap.
                    //    bitmapOld.Dispose();
                    //}
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }
        private void ShowException(Exception exception)
        {
            // MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine(exception.Message);
        }
        
        public int GetListValueById(string id)
        {
            try
            {
                int index = CameraParameters.List.FindIndex(para => para.nodeName == id);

                int value = (int)CameraParameters.List[index].nodeVal;

                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            

        }

        private void StartCamera1()
        {
            if (camera1 != null)
            {
                DestroyCamera(camera1, converter1);
            }

            // Open the connection to the selected camera device.

            try
            {
                // Create a new camera object.
                camera1 = new Camera("22047158");
                converter1 = new PixelDataConverter();

                //CameraSettings cameraSettings = new CameraSettings();
                //cameraSettings.Focus();
                //cameraSettings.Activate();
                //Console.WriteLine(Form.ActiveForm.Name);
                //Form currentForm = Form.ActiveForm;
                //Console.WriteLine(currentForm.Name);
                //camera1.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                camera1.ConnectionLost += OnConnectionLost;
                camera1.CameraOpened += OnCameraOpened;
                camera1.CameraClosed += OnCameraClosed;
                camera1.StreamGrabber.GrabStarted += OnGrabStarted;
                camera1.StreamGrabber.ImageGrabbed += OnImageGrabbed1;
                camera1.StreamGrabber.GrabStopped += OnGrabStopped;

                // Open the connection to the camera device.
                camera1.Open();

                //camera1.Parameters[PLCamera.Width].SetValue(Convert.ToInt32(8000), IntegerValueCorrection.Nearest);
                camera1.Parameters[PLCamera.UserSetSelector].TrySetValue("UserSetSelector_UserSet1");
                //camera1.Parameters[PLCamera.UserSetSelector].TrySetValue("UserSetSelector_Default");

                camera1.Parameters[PLCamera.UserSetLoad].TryExecute();

                camera1.Parameters[PLCamera.Width].SetValue(8000, IntegerValueCorrection.Nearest);
                camera1.Parameters[PLCamera.OffsetX].TrySetValue(Convert.ToInt32(0));
                camera1.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(ModelData.cam1Expo);
                camera1.Parameters[PLCamera.Height].SetValue(Convert.ToInt32(2600));
                camera1.Parameters[PLCamera.AcquisitionLineRateAbs].TrySetValue(Convert.ToDouble(GetListValueById("LineRateCam1")));
                camera1.Parameters[PLCamera.GainSelector].SetValue("DigitalAll");
                camera1.Parameters[PLCamera.GainRaw].TrySetValue(Convert.ToInt32(256));
                camera1.Parameters[PLCamera.GainSelector].SetValue("AnalogAll");
                camera1.Parameters[PLCamera.GainRaw].TrySetValue(Convert.ToInt32(4));
                //camera1.Parameters[PLCamera.TriggerSource].SetValue("Software");

                //-----------------------------------Uncomment the following code when encoder is connected------------------------------------------//

                //camera1.Parameters[PLCamera.UserSetSelector].TrySetValue("UserSetSelector_UserSet3");
                //camera1.Parameters[PLCamera.UserSetLoad].TryExecute();
                camera1.Parameters[PLCamera.ShaftEncoderModuleLineSelector].ParseAndSetValue("PhaseA");
                camera1.Parameters[PLCamera.ShaftEncoderModuleLineSource].ParseAndSetValue("Line1");
                camera1.Parameters[PLCamera.ShaftEncoderModuleLineSelector].ParseAndSetValue("PhaseB");
                camera1.Parameters[PLCamera.ShaftEncoderModuleLineSource].ParseAndSetValue("Line2");
                camera1.Parameters[PLCamera.FrequencyConverterInputSource].TrySetValue("ShaftEncoderModuleOut");
                camera1.Parameters[PLCamera.TriggerSelector].TrySetValue("FrameStart");
                camera1.Parameters[PLCamera.TriggerMode].SetValue("Off");
                camera1.Parameters[PLCamera.TriggerSelector].TrySetValue("AcquisitionStart");
                camera1.Parameters[PLCamera.TriggerMode].SetValue("Off");
                camera1.Parameters[PLCamera.TriggerSelector].TrySetValue("LineStart");
                camera1.Parameters[PLCamera.TriggerMode].SetValue("On");
                camera1.Parameters[PLCamera.TriggerSource].SetValue("FrequencyConverter");
                camera1.Parameters[PLCamera.LineSelector].SetValue("Out1");
                camera1.Parameters[PLCamera.LineMode].SetValue("Output");
                camera1.Parameters[PLCamera.LineSource].SetValue("ExposureActive");

            }
            catch (Exception exception)
            {
                allCameras.Remove(allCameras[0]);
                MessageBox.Show("Error in starting Camera 1.\nPlease reconnect camera from system and start again.");
                ShowException(exception);
            }
        }

        public void UpdateCameraPara()
        {
            camera1.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(ModelData.cam1Expo);
            camera2.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(ModelData.cam2Expo);
            camera1.Parameters[PLCamera.AcquisitionLineRateAbs].TrySetValue(Convert.ToDouble(GetListValueById("LineRateCam1")));
            camera2.Parameters[PLCamera.AcquisitionLineRateAbs].TrySetValue(Convert.ToDouble(GetListValueById("LineRateCam2")));
            Console.WriteLine(ModelData.cam1Expo);
            button1.BackColor = Color.Red;
        }
        public void ShowStaticImage()
        {
            if (allCameras.Count == 2)
            {
                if (processThread != null)
                {
                    processThread.Abort();

                }
                //camera1.Parameters[PLCamera.TriggerMode].SetValue("On");
                //camera2.Parameters[PLCamera.TriggerMode].SetValue("On");
                doProcess = false;

                Parallel.Invoke(() =>
                {
                    Stop(camera1);

                },
                () =>
                {
                    Stop(camera2);

                });
                captureImages = false;

                //MergeImagesCam1();
                //MergeImagesCam2();
                timer1.Stop();
                Bitmap fullImage = MergeImagesSidewaysOverlap(imageCam1, imageCam2, GetListValueById("Overlap"));
                pictureBox5.Image = fullImage;
            }
        }
        public void ShowLiveImage()
        {
            if (allCameras.Count == 2 && captureImages == false)
            {
                captureImages = true;

                Parallel.Invoke(() =>
                {
                    ContinuousShot(camera1);

                },
                () =>
                {
                    ContinuousShot(camera2);

                });

                timer1.Enabled = true;
                //timer1.Start();
            }
        }
        private void Stop(Camera camera)
        {
            // Stop the grabbing.
            try
            {
                camera.StreamGrabber.Stop();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Stop Exxxxx----------------");
                ShowException(exception);
            }
        }


        private void ContinuousShot(Camera camera)
        {
            try
            {
                // Start the grabbing of images until grabbing is stopped.
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Continuous Shot Exxxxx----------------");
                ShowException(exception);
            }
        }
        private void StartCamera2()
        {
            if (camera2 != null)
            {
                DestroyCamera(camera2, converter2);
            }


            // Open the connection to the selected camera device.


            try
            {
                // Create a new camera object.
                camera2 = new Camera("22162815");
                converter2 = new PixelDataConverter();

                //CameraSettings cameraSettings = new CameraSettings();
                //cameraSettings.Focus();
                //cameraSettings.Activate();
                //Console.WriteLine(Form.ActiveForm.Name);
                //Form currentForm = Form.ActiveForm;
                // Console.WriteLine(currentForm.Name);
                //camera2.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                camera2.ConnectionLost += OnConnectionLost;
                camera2.CameraOpened += OnCameraOpened;
                camera2.CameraClosed += OnCameraClosed;
                camera2.StreamGrabber.GrabStarted += OnGrabStarted;
                camera2.StreamGrabber.ImageGrabbed += OnImageGrabbed2;
                camera2.StreamGrabber.GrabStopped += OnGrabStopped;

                // Open the connection to the camera device.
                camera2.Open();

                camera2.Parameters[PLCamera.UserSetSelector].TrySetValue("UserSetSelector_UserSet1");
                //camera2.Parameters[PLCamera.UserSetSelector].TrySetValue("UserSetSelector_Default");

                camera2.Parameters[PLCamera.UserSetLoad].TryExecute();

                camera2.Parameters[PLCamera.Width].SetValue(Convert.ToInt32(8000), IntegerValueCorrection.Nearest);
                camera2.Parameters[PLCamera.OffsetX].TrySetValue(Convert.ToInt32(0));
                camera2.Parameters[PLCamera.ExposureTime].TrySetValue(ModelData.cam2Expo);
                camera2.Parameters[PLCamera.Height].SetValue(Convert.ToInt32(2600));
                camera2.Parameters[PLCamera.AcquisitionLineRateAbs].TrySetValue(Convert.ToDouble(GetListValueById("LineRateCam1")));
                camera2.Parameters[PLCamera.GainSelector].SetValue("DigitalAll");
                camera2.Parameters[PLCamera.GainRaw].TrySetValue(Convert.ToInt32(256));
                camera2.Parameters[PLCamera.GainSelector].SetValue("AnalogAll");
                camera2.Parameters[PLCamera.GainRaw].TrySetValue(Convert.ToInt32(4));
                //camera2.Parameters[PLCamera.TriggerSelector].SetValue("AcquisitionStart");
                //camera2.Parameters[PLCamera.TriggerMode].SetValue("Off");
                //camera2.Parameters[PLCamera.TriggerSource].SetValue("Software");

                //-----------------------------------Uncomment the following code when encoder is connected------------------------------------------//
                camera2.Parameters[PLCamera.TriggerSelector].SetValue("LineStart");
                camera2.Parameters[PLCamera.TriggerMode].SetValue("On");
                camera2.Parameters[PLCamera.TriggerSource].SetValue("Line1");



                Console.Write("Cam 2 Expo {0}", ModelData.cam2Expo);
            }
            catch (Exception exception)
            {
                allCameras.Remove(allCameras[0]);

                MessageBox.Show("Error in starting Camera 2.\nPlease reconnect camera from system and start again.");

                ShowException(exception);
            }
        }
        //-----------------------------------------------------



        List<ICameraInfo> allCameras = new List<ICameraInfo>();
        #endregion
        public InspectionPage()
        {
            InitializeComponent();
            CommonParameters.saveImages = false;

            allCameras = CameraFinder.Enumerate();
            instantDoCtrl1.SelectedDevice = new DeviceInformation(0);
            instantDiCtrl1.SelectedDevice = new DeviceInformation(0);

        }

        public void ShowAllData()
        {
            using (NpgsqlConnection con = db.GetConnection())
            {
                con.Open();
                string query = @"select * from public.logreport";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                

                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NpgsqlTypes.NpgsqlPoint point = (NpgsqlTypes.NpgsqlPoint)reader[3];
                        chart1.Series[(int)reader[6]].Points.AddXY(point.X / 10, point.Y / 10);
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found for this date and shift");
                }
            }
        }

        public void UpdateLabel()
        {
            if (CommonParameters.saveImages)
            {
                labelSaveImage.Text = "Save Images: ON";
                labelSaveImage.ForeColor = Color.LimeGreen;
            }
            else
            {
                labelSaveImage.Text = "Save Images: OFF";
                labelSaveImage.ForeColor = Color.Red;
            }
        }

        private void InspectionPage_Load(object sender, EventArgs e)
        {
            CameraParameters.updatePara();
            CameraParameters.List = JsonConvert.DeserializeObject<List<Settings>>(File.ReadAllText(string.Format(@"{0}\bin\x64\Release\CamSettings.json", CommonParameters.projectDirectory)));
            modelData = JsonConvert.DeserializeObject<ModelData>(File.ReadAllText(string.Format(@"{0}\Models\{1}\thresholds.json", CommonParameters.projectDirectory, CommonParameters.selectedModel)));
            Console.WriteLine(ModelData.cam1Expo);
            Console.WriteLine(ModelData.webDetect);
            Console.WriteLine(ModelData.blockSize);
            CommonParameters.algo.defMinSizeProp = ModelData.webDetect;
            CommonParameters.algo.defBlockSizeProp = ModelData.blockSize;
            comboBoxOperation.SelectedIndex = 0;
            
            clock.Start();
            string path = string.Format(@"{0}\Models\{1}\DefectImages", CommonParameters.projectDirectory, CommonParameters.selectedModel);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = string.Format(@"{0}\Models\{1}\DefectImagesFullRes", CommonParameters.projectDirectory, CommonParameters.selectedModel);


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (allCameras.Count == 2)
            {
                StartCamera1();
                StartCamera2();
            }
            else
            {
                MessageBox.Show("Cameras not connected. Please connect all cameras and restart the application", "Cameras not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            db.TestConnection();

            //db.InsertRecord(Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")), DateTime.Now.ToString("HH:mm:ss"), "SrNum", loc, "Type1", "ImgPath", 2);
            //ShowAllData();
            //Bitmap bitmap = new Bitmap(@"C:\Users\ADVANTECh\Desktop\tokyo1.jpg");
            //db.InsertImage(bitmap);
            //Bitmap bmp;
            //using (var ms = new MemoryStream(db.GetImageData()))
            //{
            //    bmp = new Bitmap(ms);
            //}

            //pictureBox5.Image = bmp;
            ErrorCode err = ErrorCode.Success;
            DiintChannel[] interruptChans = instantDiCtrl1.DiintChannels;

            Console.WriteLine("Channel Count {0}", instantDiCtrl1.DiintChannels.Count());

            // 3. Choose one channel from those channels and enable the channel for running DI Interrupt.
            interruptChans[0].Enabled = true;
            interruptChans[1].Enabled = true;
            interruptChans[2].Enabled = true;
            interruptChans[3].Enabled = true;
            interruptChans[4].Enabled = true;
            interruptChans[5].Enabled = true;
            interruptChans[6].Enabled = true;
            interruptChans[7].Enabled = true;

            err = instantDiCtrl1.SnapStart();
            if (err != ErrorCode.Success)
            {
                HandleError(err);
            }
            Console.WriteLine("------------DI snapStarted---------------");
            //----------------------------------
            err = instantDoCtrl1.Write(0, (byte)outputCardState);
            if (err != ErrorCode.Success)
            {
                HandleError(err);
            }
            UpdateFinishList();

            comboBoxFinish.SelectedItem = CommonParameters.selectedModel;
            //MessageBox.Show(comboBoxFinish.SelectedItem.ToString() + " " + CommonParameters.selectedModel);
            textBoxBatchNum.Text = CommonParameters.batchNum;
            textBoxRollNum.Text = CommonParameters.rollNum;
            comboBoxOperation.SelectedItem = CommonParameters.operation;
            textBoxBatchNum.Leave += TextBoxModelData_Leave;
            textBoxRollNum.Leave += TextBoxModelData_Leave;
            textBoxBatchNum.TextChanged += TextBox_TextChanged;
            textBoxRollNum.TextChanged += TextBox_TextChanged;

            comboBoxFinish.SelectedIndexChanged += comboBoxFinish_SelectedIndexChanged;
            comboBoxOperation.SelectedIndexChanged += comboBoxOperation_SelectedIndexChanged;

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            modelDataChanged = true;
        }

        void UpdateFinishList()
        {
            comboBoxFinish.Items.Clear();
            DirectoryInfo obj = new DirectoryInfo(string.Format(@"{0}\Models", CommonParameters.projectDirectory));
            DirectoryInfo[] folders = obj.GetDirectories();

            for (int i = 0; i < folders.Length; i++)
            {
                comboBoxFinish.Items.Add(folders[i].Name);
            }
            
        }

        bool modelDataChanged = false;
        bool stopInspection = false;
        int selfStart = 0;
        private void TextBoxModelData_Leave(object sender, EventArgs e)
        {
            if (!stopInspection)
            {
                if (modelDataChanged)
                {
                    DialogResult dialogResult = MessageBox.Show("Model Data Changed. Do you want to stop Inspection ?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //MessageBox.Show("Reopen Inspection form to reflect changes.");
                        CommonParameters.selectedModel = comboBoxFinish.SelectedItem.ToString();
                        CommonParameters.batchNum = textBoxBatchNum.Text;
                        CommonParameters.rollNum = textBoxRollNum.Text;
                        CommonParameters.operation = comboBoxOperation.SelectedItem.ToString();
                        stopInspection = true;
                        sheetLength = 2600;
                        doProcess = false;
                        btnStop_Click(sender, e);
                        TextBox tb = (TextBox)sender;
                        if (tb.Name == "textBoxRollNum")
                        {
                            selfStart = 1;
                        }
                    }
                    else
                    {
                        UpdateDefaultModelData();
                    }
                }
            }
            else
            {
                CommonParameters.selectedModel = comboBoxFinish.SelectedItem.ToString();
                CommonParameters.batchNum = textBoxBatchNum.Text;
                CommonParameters.rollNum = textBoxRollNum.Text;
                CommonParameters.operation = comboBoxOperation.SelectedItem.ToString();
            }

        }

        void UpdateDefaultModelData()
        {
            comboBoxFinish.SelectedItem = CommonParameters.selectedModel;
            textBoxBatchNum.Text = CommonParameters.batchNum;
            textBoxRollNum.Text = CommonParameters.rollNum;
            comboBoxOperation.SelectedItem = CommonParameters.operation;
            modelDataChanged = false;
        }

        private void HandleError(ErrorCode err)
        {
            if (err != ErrorCode.Success)
            {
                MessageBox.Show("Some errors happened in DAQ I/Ocard, the error code is: " + err.ToString());
            }
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        bool saveRecord = true;

        private void btnStart_Click(object sender, EventArgs e)
        {

            btnStop.Enabled = true;
            stopInspection = false;
            //timer1.Enabled = true;
            if (allCameras.Count == 2)
            {
                timerSpeed.Start();
                captureImages = true;
                //camera1.Parameters[PLCamera.TriggerMode].SetValue("Off");
                //camera2.Parameters[PLCamera.TriggerMode].SetValue("Off");
                UpdateCameraPara();

                //ContinuousShot(camera1);
                //ContinuousShot(camera2);
                setCardDO(0, true);
                btnStart.Enabled = false;
                Parallel.Invoke(() =>
                {
                    ContinuousShot(camera1);

                },
                () =>
                {
                    ContinuousShot(camera2);

                });
                bitmapsMerge1.Clear();
                ////bitmapsMerge2.Clear();
                doProcess = true;
                processThread = new Thread(delegate ()
                {
                    while (doProcess)
                    {
                        try
                        {
                            Stopwatch sw = new Stopwatch();
                            sw.Start();

                            if (bitmapsMerge1.Count > 0 && bitmapsMerge2.Count > 0 && image1Grabbed == true && image2Grabbed == true)
                            {
                                Thread.Sleep(10);
                                image1Grabbed = false;
                                image2Grabbed = false;
                                Console.WriteLine("A1  {0}  A2 {1}", bitmapsMerge1.Count, bitmapsMerge2.Count);
                                CommonParameters.algo.overlapC1C2Prop = GetListValueById("Overlap");
                                int height = bitmapsMerge1[0].Height;
                                int width = (bitmapsMerge1[0].Width * 2) - CommonParameters.algo.overlapC1C2Prop;
                                Bitmap fullImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                                Bitmap algoImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                                //Bitmap fullImage = MergeImagesSidewaysOverlap((Bitmap)bitmapsMerge1[0].Clone(), (Bitmap)bitmapsMerge2[0].Clone(), GetListValueById("Overlap"));
                                try
                                {
                                    fullImage = CommonParameters.algo.mergeImagesCpp(
                                                    bitmapsMerge1[0].Clone(new Rectangle(0, 0, bitmapsMerge1[0].Width - CommonParameters.algo.overlapC1C2Prop, bitmapsMerge1[0].Height), PixelFormat.Format24bppRgb),
                                                    bitmapsMerge2[0].Clone(new Rectangle(0, 0, bitmapsMerge2[0].Width, bitmapsMerge2[0].Height), PixelFormat.Format24bppRgb),
                                                    fullImage.Clone(new Rectangle(0, 0, fullImage.Width, fullImage.Height), PixelFormat.Format24bppRgb));
                                    algoImage = CommonParameters.algo.processAllFrontThick(fullImage.Clone(new Rectangle(0, 0, fullImage.Width, fullImage.Height), PixelFormat.Format24bppRgb));
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine("Error in cloning bitmap btnstartClick error: {0}", ex.Message);
                                }

                                string path = string.Format(@"{0}\Models\{1}\DefectImages", CommonParameters.projectDirectory, CommonParameters.selectedModel);

                                
                                path = path + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("hh_mm_ss_");

                                //string fullResPath = string.Format(@"{0}\Models\{1}\DefectImagesFullRes", CommonParameters.projectDirectory, CommonParameters.selectedModel);
                                 
                                //fullResPath = fullResPath + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("HH_mm_ss_") + DateTime.Now.Millisecond.ToString() + ".bmp";
                                bool defectFound = false;



                                labelDefCount.Invoke((Action)delegate
                                {
                                    labelDefCount.Text = CommonParameters.algo.defectCountProp.ToString();
                                });
                                DefectImageData defImgData = new DefectImageData();
                                ImageData imageData = new ImageData();

                                for (int i = 0; i < CommonParameters.algo.defectCountProp; i++)
                                {
                                    
                                    int width1 = CommonParameters.algo.getBottomRightPoint(i).X - CommonParameters.algo.getTopLeftPoint(i).X;
                                    int height1 = CommonParameters.algo.getBottomRightPoint(i).Y - CommonParameters.algo.getTopLeftPoint(i).Y;
                                    double area = CommonParameters.algo.getDefectArea(i);
                                    int cat = CommonParameters.algo.getDefectCat(i);
                                    Rectangle cropRect = new Rectangle(CommonParameters.algo.getTopLeftPoint(i), new Size(width1, height1));
                                    
                                    Point defLocation = new Point(0,0);
                                    Point defSize = new Point(0, 0);

                                    try
                                    {
                                        defLocation = new Point(Convert.ToInt32(CommonParameters.algo.getTopLeftPoint(i).X * 0.114259598),
                                       Convert.ToInt32((CommonParameters.algo.getTopLeftPoint(i).Y + sheetLength) * 0.114259598));

                                        defSize = new Point(Convert.ToInt32((cropRect.Width * 0.114259598)),
                                            Convert.ToInt32(cropRect.Height * 0.114259598));
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error in px to mm {0}", ex.Message);
                                        Console.WriteLine("cropRect {0} loca {1}", cropRect.Size, CommonParameters.algo.getTopLeftPoint(i));
                                        ExceptionLogging.SendErrorToFile(ex);
                                        defLocation = new Point(0, 0);
                                        defSize = new Point(0, 0);
                                        cropRect = new Rectangle(0,0,0,0);

                                    }

                                    if (cropRect.X + cropRect.Width < algoImage.Width && cropRect.Y + cropRect.Height < algoImage.Height && cropRect.X > 0 && cropRect.Y > 0)
                                    {

                                        try
                                        {
                                            defImgData.image = fullImage.Clone(cropRect, PixelFormat.Format24bppRgb);

                                        }
                                        catch (Exception ex)
                                        {

                                            Console.WriteLine("Error in cloning bitmap btnstartClick cropdef error: {0}", ex.Message);
                                        }

                                        //Point defectLoc = CommonParameters.algo.getTopLeftPoint(i);

                                        //defectLoc.Y += (int)((heightAddon - fullImage.Height) * CommonParameters.algo.mmperPixProp);
                                        //defectLoc.X = (int)((defectLoc.X) * CommonParameters.algo.mmperPixProp);

                                        //Point defSize = new Point((int)(cropRect.Width * CommonParameters.algo.mmperPixProp),
                                          //              (int)(cropRect.Height * CommonParameters.algo.mmperPixProp));
                
                                        defImgData.dateTime = DateTime.Now;

                                        defImgData.path = path + defImgData.dateTime.Millisecond.ToString() + string.Format("_{0}_",
                                                            defImgData.defectType[CommonParameters.algo.getDefectCat(i)]) + ".bmp";



                                        defImgData.modelName = CommonParameters.selectedModel;
                                        defImgData.defectCode = CommonParameters.algo.getDefectCat(i);
                                        defImgData.defSize = defSize;
                                        defImgData.finish = CommonParameters.finish;
                                        defImgData.operation = CommonParameters.operation;
                                        defImgData.rollNum = CommonParameters.rollNum;
                                        defImgData.batchNum = CommonParameters.batchNum;
                                        defImgData.defLocation = defLocation;

                                        imageData.defectImageDatas.Add(new DefectImageData(defImgData));

                                    }
                                    defectFound = true;
                                }

                                if (defectFound)
                                {
                                    try
                                    {
                                        imageData.image = fullImage.Clone(new Rectangle(0, 0, fullImage.Width, fullImage.Height), PixelFormat.Format24bppRgb);

                                    }
                                    catch (Exception ex)
                                    {

                                        Console.WriteLine("Error in cloning bitmap btnstartClick fullImage error: {0}", ex.Message);
                                        return;
                                    }
                                    imageDataList.Add(new ImageData(imageData));
                                    setCardDO(1, true);
                                }
                                else
                                {
                                    setCardDO(1, false);
                                }

                                if (CommonParameters.saveImages)
                                {

                                    path = string.Format(@"{0}\Models\{1}\Images", CommonParameters.projectDirectory, CommonParameters.selectedModel);
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    Bitmap bitmap;
                                    try
                                    {
                                        bitmap = (Bitmap)fullImage.Clone();

                                    }
                                    catch (Exception ex)
                                    {

                                        Console.WriteLine("Error in cloning bitmap btnstartClick fullImageSave error: {0}", ex.Message);
                                        return;
                                    }
                                    path = path + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("hh_mm_ss_");

                                    //bitmap.Save(path + @"\"+ DateTime.Now.Date.ToString("dd_MM_yyyy")+ DateTime.Now.TimeOfDay.ToString("hh:mm:ss") + DateTime.Now.Millisecond.ToString() +".bmp");
                                    Console.WriteLine(path + DateTime.Now.ToString("dd_MM_yyyy") + DateTime.Now.ToString("hh:mm:ss") + DateTime.Now.Millisecond.ToString());
                                    bitmap.Save(path + DateTime.Now.Millisecond.ToString() + ".bmp");

                                }

                                File.AppendAllText("BufferLog.txt",string.Format("---------------------{0}----------------{1}",
                                                    DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"), Environment.NewLine));
                                File.AppendAllText("BufferLog.txt", string.Format("Image list1 {0} list2 {1} list3 {2} List4 {3} {4}", bitmapsMerge1.Count, bitmapsMerge2.Count, imageData.defectImageDatas.Count, imageDataList.Count,Environment.NewLine));
                                Console.WriteLine(string.Format("Image list1 {0} list2 {1} list3 {2} List4 {3} {4}", bitmapsMerge1.Count, bitmapsMerge2.Count, imageData.defectImageDatas.Count, imageDataList.Count,Environment.NewLine));
                                if (pictureBox5.InvokeRequired)
                                {
                                    pictureBox5.BeginInvoke((Action)delegate
                                    {
                                        //Pen pen = new Pen(Color.LimeGreen, 20);

                                        //using (var graphics = Graphics.FromImage(algoImage))
                                        //{
                                        //    graphics.DrawLine(pen, new Point(algo.out1Prop, 300), new Point(algo.out2Prop, 300));
                                        //    pictureBox5.Image = algoImage;
                                        //    pbFrame++;
                                        //    pictureBox5.Invalidate();
                                        //}
                                        pbFrame++;
                                        try
                                        {
                                            pictureBox5.Image = algoImage;
                                            pictureBox5.Invalidate();
                                            Console.WriteLine("Images Updated");
                                        }
                                        catch (Exception ex)
                                        {

                                            Console.WriteLine("Error in displaying algo image on pb {0}",ex.Message);
                                        }
                                        

                                    });
                                }
                                else
                                {
                                    try
                                    {
                                        pbFrame++;
                                        pictureBox5.Image = algoImage;
                                        Console.WriteLine("Images Updated");
                                    }
                                    catch (Exception ex)
                                    {

                                        Console.WriteLine("Error in displaying algo image on pb {0}", ex.Message);
                                    }


                                }

                                Console.WriteLine("Sheet length {0}", sheetLength);

                                labelLength.Invoke((Action)delegate
                                {
                                    labelLength.Text = ((sheetLength * 0.114259598) / 1000).ToString("N3");

                                });

                                sheetLength += 2600;


                                //Thread.Sleep(50);
                                bitmapsMerge1.RemoveAt(0);
                                bitmapsMerge2.RemoveAt(0);
                                if (bitmapsMerge1.Count > 20)
                                {
                                    Console.WriteLine("bitmapsMerge1 size exceeded from 20 and cleared.");
                                    bitmapsMerge1.Clear();
                                }
                                if (bitmapsMerge2.Count > 20)
                                {
                                    Console.WriteLine("bitmapsMerge2 size exceeded from 20 and cleared.");
                                    bitmapsMerge2.Clear();
                                }
                                Console.WriteLine("================================Both Lists Cleared==========================");

                                labelJumboWidth.Invoke((Action)delegate
                                {
                                    labelJumboWidth.Text = CommonParameters.algo.sheetWidthProp.ToString();
                                });
                                //Console.WriteLine("Thread Running");
                                sw.Stop();
                                Console.WriteLine("Time taken by thread {0}", sw.ElapsedMilliseconds);
                            }

                        }
                        catch (Exception ex)
                        {
                            ExceptionLogging.SendErrorToFile(ex);
                            Console.WriteLine(ex.Message);
                            //MessageBox.Show("Inspection closed unexpectedly. Start Inspection again");
                            //btnStop_Click(sender, e);
                        }
                    }
                });
                processThread.Start();
                saveData = new Thread(() =>
                {

                    while (saveRecord)
                    {
                        try
                        {
                            if (imageDataList.Count > 0)
                            {
                                Console.WriteLine("If condition entered");
                                string fullResPath = string.Format(@"{0}\Models\{1}\DefectImagesFullRes", CommonParameters.projectDirectory, CommonParameters.selectedModel);

                                fullResPath = fullResPath + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("HH_mm_ss_") + DateTime.Now.Millisecond.ToString() + ".bmp";
                                imageDataList[0].image.Save(fullResPath);
                                foreach (DefectImageData did in imageDataList[0].defectImageDatas)
                                {

                                    did.image.Save(did.path);

                                    db.InsertRecord(
                                    Convert.ToDateTime(did.dateTime.ToString("yyyy-MM-dd")),
                                    Convert.ToDateTime(did.dateTime.ToString("HH:mm:ss")),
                                    did.modelName,
                                    did.defLocation,
                                    did.defectType[did.defectCode],
                                    did.path,
                                    did.defectCode,
                                    did.defSize,
                                    did.finish,
                                    did.operation,
                                    did.rollNum,
                                    did.batchNum,
                                    fullResPath);


                                }


                                //defectImageDataList[0].
                                imageDataList.RemoveAt(0);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in db insert thread {0}", ex.Message);
                            imageDataList.RemoveAt(0);

                        }
                        //Console.WriteLine("Save thread running.");



                    }
                });
                saveData.Start();
                //timer1.Enabled = true;
                //timer1.Start();

            }
            else
            {
                if (stopInspection == true)
                {
                    MessageBox.Show("Model Data Changed. Please reopen form to start inspection.");
                }
                else
                {
                    MessageBox.Show("Please connect all cameras", "Cameras not connected");

                }
            }

        }

        DataTable dt = new DataTable();

        public void ShowReport()
        {
            try
            {
                using (NpgsqlConnection con = db.GetConnection())
                {
                    con.Open();

                    string query = string.Format(@"select _date as ""Date"", _time as ""Time"", serialnum as ""Finish"",
                                   _location as ""Location"", 
                                    deftype as ""Defect Type"",
                                    defectsize as ""Defect Size"" 
                                    from public.logreport where _date = '{0}'
                                    and _time between '{1}' and '{2}' 
                                    and serialnum = '{3}' order by _date asc, _time desc, _location[1] desc",
                                        DateTime.Now.ToString("yyyy-MM-dd"),
                                        DateTime.Now.AddMinutes(-3).ToString("HH:mm:ss"),
                                        DateTime.Now.ToString("HH:mm:ss"),
                                        CommonParameters.selectedModel.ToString()
                                        );

                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));
                    //cmd.Parameters.AddWithValue("@time1", Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")));
                    //cmd.Parameters.AddWithValue("@time2", Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")));
                    //cmd.Parameters.AddWithValue("@srnum", CommonParameters.finish.ToString());
                    NpgsqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        dt.Clear();
                        dt.Load(reader);
                        dataGridViewReport.DataSource = dt;
                        //Console.WriteLine("report populated");

                    }
                    else
                    {
                        Console.WriteLine("No data");
                        //Console.WriteLine(query);

                    }

                    foreach (DataGridViewColumn dgvc in dataGridViewReport.Columns)
                    {
                        dgvc.Width = 200;
                    }

                    reader.Close();
                    con.CloseAsync();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ExceptionLogging.SendErrorToFile(ex);
            }
            
        }

        void ProcessImages()
        {
            
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            doProcess = false;
            setCardDO(1, false);
            setCardDO(0, false);
            Thread.Sleep(10);
            captureImages = false;
            //if (processThread != null && processThread.IsAlive)
            //{
            //    processThread.Abort();

            //}
            
            btnStart.Enabled = true;
            timerSpeed.Stop();
            timer1.Stop();
            labelSpeed.Text = "---";
            if (allCameras.Count == 2)
            {
                //captureImages = false;
                //if (processThread.IsAlive && processThread != null)
                //{
                //    processThread.Abort();

                //}
                ////camera1.Parameters[PLCamera.TriggerMode].SetValue("On");
                ////camera2.Parameters[PLCamera.TriggerMode].SetValue("On");
                //doProcess = false;

                Parallel.Invoke(() =>
                {
                    Stop(camera1);

                },
                () =>
                {
                    Stop(camera2);

                });
                //bitmapsMerge1.Clear();
                //bitmapsMerge2.Clear();
                //MergeImagesCam1();
                //MergeImagesCam2();
            }
            else
            {
                MessageBox.Show("Please connect all cameras", "Cameras not connected");
            }
        }
        public CameraSettings cameraSettings = null;

        private void btnCameraSettings_Click(object sender, EventArgs e)
        {
            if (cameraSettings == null)
            {
                cameraSettings = new CameraSettings();
                cameraSettings.Show();

            }
            else
            {
                cameraSettings.BringToFront();
            }
        }

        public void CloseAllOpenForm(Form currentForm)
        {
            // temp list
            var list = new List<Form>();

            // fill list
            foreach (Form form in Application.OpenForms)
                if (!currentForm.Equals(form) && form.Name != "HomePage")
                    list.Add(form);

            // close selected forms
            foreach (Form form in list)
            {
                form.Close();
            }
        }

        private void InspectionPage_FormClosing(object sender, FormClosingEventArgs e)
        {

            btnStop_Click(sender, e);
            

            CloseAllOpenForm(this);
            
        }
        public ThresholdSettings thresholdSettings = null;

        private void btnSetThresholds_Click(object sender, EventArgs e)
        {
            //mergeImagesThread1.Abort();                                                        
            //mergeImagesThread2.Abort();
            //finalImage.Save(@"C:/finalImage.jpg");                                                                                   
            //Bitmap bitmapNew = finalImage;
            //pictureBox5.Image = algo.showGuides(bitmapNew);
            if (thresholdSettings == null)
            {
                thresholdSettings = new ThresholdSettings();
                thresholdSettings.Show();
            }
            else
            {
                thresholdSettings.BringToFront();
            }
            
        }

        public ReportView reportView = null;

        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (reportView == null)
            {
                reportView = new ReportView();
                reportView.Show();

            }
            else
            {
                reportView.BringToFront();
            }
        }
        private void clock_Tick(object sender, EventArgs e)
        {
            labelDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy")+ " " + DateTime.Now.ToString("HH:mm:ss");
            labelDateTime.Visible = true;
            if (checkBoxLog.Checked)
            {
                ShowReport();

            }
            for (int i = 0; i < dataGridViewReport.Columns.Count; i++)
            {
                DataGridViewCellStyle column = dataGridViewReport.Columns[i].HeaderCell.Style;
                column.Font = new Font("Microsoft Sans Serif", 16);
                column.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (selfStart >0)
            {
                selfStart++;

                if (selfStart == 6)
                {
                    btnStart_Click(sender, e);
                    selfStart = 0;
                }
            }
            //Console.WriteLine("Do process Value {0}", doProcess);
        }

        public IoMonitor ioMonitor = null;
        private void btnIOmonitor_Click(object sender, EventArgs e)
        {
            //Bitmap leftImage = new Bitmap(@"C:\Users\Admin\Downloads\leftImage.jpg");
            //Bitmap rightImage = new Bitmap(@"C:\Users\Admin\Downloads\rightImage.jpg");

            //pictureBox5.Image = MergeImagesSidewaysOverlap(leftImage, rightImage, 50);
            //pictureBox5.Image.Save(@"C:\Users\Admin\Downloads\NewImage.jpg");
            if (ioMonitor == null)
            {
                ioMonitor = new IoMonitor();
                ioMonitor.Show();

            }
            else
            {
                ioMonitor.BringToFront();
            }
        }

        int sheetLength = 2600;

        int pbFrame = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            ////if (bitmapsMerge1.Last() != null && bitmapsMerge2.Last() != null)
            ////{
            ////    Bitmap fullImage = MergeImagesSidewaysOverlap(bitmapsMerge1.Last(), bitmapsMerge2.Last(), 50);
            ////    Console.WriteLine("A1  {0}  A2 {1}", bitmapsMerge1.Count, bitmapsMerge2.Count);
            ////    pictureBox5.Image = fullImage;

            ////    bitmapsMerge1.Clear();
            ////    bitmapsMerge2.Clear();
            ////}
            //try
            //{

            //    //labelDefType.Text = counter1.ToString();
            //    //labelDefArea.Text = counter2.ToString();
            //    if (bitmapsMerge1.Count > 0 && bitmapsMerge2.Count > 0 && image1Grabbed == true && image2Grabbed == true)
            //    {
            //        //Bitmap fullImage = MergeImagesSidewaysOverlap((Bitmap)bitmapsMerge1[0].Clone(), (Bitmap)bitmapsMerge2[0].Clone(), GetListValueById("Overlap"));
            //        int height = bitmapsMerge1[0].Height;
            //        int width = bitmapsMerge1[0].Width - GetListValueById("Overlap");
            //        CommonParameters.algo.overlapC1C2Prop = GetListValueById("Overlap");

            //        Bitmap fullImage = new Bitmap(width, height);
            //        Bitmap algoImage = CommonParameters.algo.processAllFrontThick((Bitmap)fullImage.Clone());
            //        Console.WriteLine("Image List 1  {0}  Image List 2 {1}", bitmapsMerge1.Count, bitmapsMerge2.Count);
            //        //Bitmap fullImage = new Bitmap(bitmapsMerge1.Last().Width*2, bitmapsMerge1.Last().Height);
            //        UpdateCameraPara();
            //        //Console.WriteLine("A1  {0}  A2 {1}", bitmapsMerge1.Count, bitmapsMerge2.Count);
                    
                       
            //        string path = string.Format(@"{0}\Models\sam.bmp", CommonParameters.projectDirectory);

            //        Bitmap bitmap = (Bitmap)fullImage.Clone();
            //        try
            //        {
            //            Console.WriteLine("Image in size {0} -> {1} -> {2}", bitmapsMerge1.Last().Size, bitmapsMerge2.Last().Size, fullImage.Size);
            //            Pen pen = new Pen(Color.LimeGreen, 20);

            //            path = string.Format(@"{0}\Models\{1}\DefectImages", CommonParameters.projectDirectory, CommonParameters.selectedModel);

            //            if (!Directory.Exists(path))
            //            {
            //                Directory.CreateDirectory(path);
            //            }

            //            bool defFound = false;
            //            path = path + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("hh_mm_ss") + DateTime.Now.Millisecond.ToString() + ".bmp";

            //            //for (int i = 0; i < algo.defectCountProp; i++)
            //            //{
            //            //    int width = algo.getBottomRightPoint(i).X - algo.getTopLeftPoint(i).X;
            //            //    int height = algo.getBottomRightPoint(i).Y - algo.getTopLeftPoint(i).Y;

            //            //    Rectangle cropRect = new Rectangle(algo.getTopLeftPoint(i), new Size(width, height));

            //            //    if ((cropRect.Height + cropRect.Y) < bitmap.Height && (cropRect.Width + cropRect.X) < bitmap.Width && cropRect.X > 0 && cropRect.Y > 0)
            //            //    {
            //            //        defFound = true;
            //            //        db.InsertRecord(Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")), Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")), CommonParameters.selectedModel, algo.getTopLeftPoint(i), "Type1", path, 2, (Point)cropRect.Size);
            //            //    }
            //            //}

            //            //if (defFound)
            //            //{
            //            //    bitmap.Save(path);

            //            //}

            //            using (var graphics = Graphics.FromImage(algoImage))
            //            {
            //                //graphics.DrawLine(pen, new Point(algo.out1Prop, 300), new Point(algo.out2Prop, 300));
            //                pictureBox5.Image = algoImage;
            //                pbFrame++;
            //                pictureBox5.Invalidate();
            //            }

            //            labelJumboWidth.Text = CommonParameters.algo.sheetWidthProp.ToString("0.00");
            //            labelDefCount.Text = CommonParameters.algo.defectCountProp.ToString();
            //            //labelDefArea.Text = CommonParameters.algo.defectAreaProp.ToString();

            //            //labelLength.Text = sheetLength.ToString();
            //            bitmapsMerge1.RemoveAt(0);
            //            bitmapsMerge2.RemoveAt(0);
            //            image1Grabbed = false;
            //            image2Grabbed = false;
            //            if (CommonParameters.saveImages)
            //            {
            //                path = string.Format(@"{0}\Models\{1}\Images", CommonParameters.projectDirectory, CommonParameters.selectedModel);
            //                if (!Directory.Exists(path))
            //                {
            //                    Directory.CreateDirectory(path);
            //                }
            //                bitmap = (Bitmap)fullImage.Clone();

            //                //bitmap.Save(path + @"\"+ DateTime.Now.Date.ToString("dd_MM_yyyy")+ DateTime.Now.TimeOfDay.ToString("hh:mm:ss") + DateTime.Now.Millisecond.ToString() +".bmp");
            //                Console.WriteLine(path + DateTime.Now.ToString("dd_MM_yyyy") + DateTime.Now.ToString("hh:mm:ss") + DateTime.Now.Millisecond.ToString());
            //                bitmap.Save(path + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("hh_mm_ss") + DateTime.Now.Millisecond.ToString() + ".bmp");
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            timer1.Stop();
            //            bitmapsMerge1.Clear();
            //            bitmapsMerge2.Clear();
            //            Console.WriteLine(ex.Message);
            //            if (allCameras.Count == 2)
            //            {
            //                captureImages = false;
            //                if (processThread != null)
            //                {
            //                    processThread.Abort();

            //                }
            //                //camera1.Parameters[PLCamera.TriggerMode].SetValue("On");
            //                //camera2.Parameters[PLCamera.TriggerMode].SetValue("On");
            //                doProcess = false;
            //                setCardDO(0, false);

            //                Parallel.Invoke(() =>
            //                {
            //                    Stop(camera1);

            //                },
            //                () =>
            //                {
            //                    Stop(camera2);

            //                });

            //                //MergeImagesCam1();
            //                //MergeImagesCam2();
            //                timer1.Stop();
            //                MessageBox.Show("Inspection stopped unexpectedly. Click start button to resume.");
            //            }

            //        }
            //        finally
            //        {

            //        }


            //        //sheetLength += (sheetLength + (2600 / 9));
                    
            //    }
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //    timer1.Stop();
            //    MessageBox.Show("An Error occured. Closing Inspection");
            //    this.Close();
            //}
            //stopwatch.Stop();
            //Console.WriteLine("Time taken by timer {0}", stopWatch.ElapsedMilliseconds);

        }

        //--------------card DO getST------------------------------------
        public int setCardDO(int outputNo, bool value)
        {
            int port_num = outputNo / 8;
            int bit_num = outputNo % 8;

            if (value == true)
            {

                err = instantDoCtrl1.WriteBit(port_num, bit_num, (byte)1);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return 0;
                }
            }
            else
            {
                err = instantDoCtrl1.WriteBit(port_num, bit_num, (byte)0);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return 0;
                }

            }
            return 1;

        }
        public int getCardDO(int outputNo)
        {
            int port_num = outputNo / 8;
            int bit_num = outputNo % 8;
            Console.WriteLine(" port num: {0}  bit num : {1}", port_num, bit_num);
            byte res;

            err = instantDoCtrl1.ReadBit(port_num, bit_num, out res);
            //   err = instantDoCtrl1.WriteBit(port_num, bit_num, (byte)1);
            if (err != ErrorCode.Success)
            {
                Console.WriteLine(" Exception   port num: {0}  bit num : {1}", port_num, bit_num);
                HandleError(err);
                return 0;
            }

            return res;

        }
        //-----------------card DI get---------------------------------
        public int getCardDI(int inputNo)
        {
            int port_num = inputNo / 8;
            int bit_num = inputNo % 8;

            byte res;

            err = instantDiCtrl1.ReadBit(port_num, bit_num, out res);
            //   err = instantDoCtrl1.WriteBit(port_num, bit_num, (byte)1);
            if (err != ErrorCode.Success)
            {
                HandleError(err);
                return 0;
            }
            return res;

        }


        //-----------------------------------------------------------------

        private void pictureBox_Click(object sender, EventArgs e)
        {
            double fl = 4567.8575952;
            int p = Convert.ToInt32(fl);
            Console.WriteLine(p);

        }

        private void labelDateTime_Click(object sender, EventArgs e)
        {
            
        }
        struct DefectProp
        {
            public Rectangle crop  { get; set; }
            public Bitmap image { get; set; }
            public string imagePath { get; set; }


            public DefectProp(DefectProp dp)
            {
                this.crop = dp.crop;
                this.image = dp.image;
                this.imagePath = dp.imagePath;
            }

            public void SaveDefect()
            {
                if ((crop.Height + crop.Y) < 2600 && crop.X > 0 && crop.Y > 0)
                {
                    Bitmap bmp = image.Clone(crop, PixelFormat.Format24bppRgb);

                    
                    bmp.Save(imagePath);
                    Console.WriteLine("Image Saved");
                    //db.InsertRecord(Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")), Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")), CommonParameters.selectedModel, cr.Location, "Type1", pt, 2);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = new Bitmap(@"D:\New folder (2)\MasteCam.bmp");
            //Bitmap bmp1 = new Bitmap(@"D:\New folder (2)\MasteCam.bmp");
            //Bitmap fl = MergeImagesSidewaysOverlap(bmp, bmp1, 0);
            //pictureBox5.Image = algo.processAllFrontThick((Bitmap)fl.Clone());
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {

                Bitmap fullImage = new Bitmap(@"C:\software\Norton Pc\SandPaperInspection\Models\Peach\Images\28_02_2022_04_26_41_611.bmp");
                string path = string.Format(@"{0}\Models\sam.bmp", CommonParameters.projectDirectory);

                Bitmap bitmap = (Bitmap)fullImage.Clone();
                Bitmap algoImage = null;

                try
                {
                    algoImage = CommonParameters.algo.processAllFrontThick((Bitmap)fullImage.Clone());
                    Pen pen = new Pen(Color.LimeGreen, 20);
                    algoImage.Save(@"C:\software\Norton Pc\SandPaperInspection\Models\defImageAlgo.bmp");
                    //path = string.Format(@"{0}\Models\{1}\DefectImages", CommonParameters.projectDirectory, CommonParameters.selectedModel);

                    path = string.Format(@"{0}\Models\{1}\DefectImages", CommonParameters.projectDirectory, CommonParameters.selectedModel);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    bool defFound = false;
                    path = path + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("hh_mm_ss") + DateTime.Now.Millisecond.ToString() + ".bmp";

                    //for (int i = 0; i < CommonParameters.algo.defectCountProp; i++)
                    //{
                    //    int width = CommonParameters.algo.getBottomRightPoint(i).X - CommonParameters.algo.getTopLeftPoint(i).X;
                    //    int height = CommonParameters.algo.getBottomRightPoint(i).Y - CommonParameters.algo.getTopLeftPoint(i).Y;

                    //    Rectangle cropRect = new Rectangle(CommonParameters.algo.getTopLeftPoint(i), new Size(width, height));

                    //    if ((cropRect.Height + cropRect.Y) < bitmap.Height && (cropRect.Width + cropRect.X) < bitmap.Width && cropRect.X > 0 && cropRect.Y > 0)
                    //    {
                    //        defFound = true;
                    //        db.InsertRecord(Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")), Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")), CommonParameters.selectedModel, CommonParameters.algo.getTopLeftPoint(i), "Type1", path, 2, (Point)cropRect.Size);
                    //    }
                    //}

                    //if (defFound)
                    //{
                    //    bitmap.Save(path);

                    //}
                    //if (!Directory.Exists(path))
                    //{
                    //    Directory.CreateDirectory(path);
                    //}
                    //List<DefectProp> defecs = new List<DefectProp>();


                    //for (int i = 0; i < algo.defectCountProp; i++)
                    //{
                    //    DefectProp defect = new DefectProp();
                    //    int width = algo.getBottomRightPoint(i).X - algo.getTopLeftPoint(i).X;
                    //    int height = algo.getBottomRightPoint(i).Y - algo.getTopLeftPoint(i).Y;

                    //    defect.crop = new Rectangle(algo.getTopLeftPoint(i), new Size(width, height));
                    //    defect.image = (Bitmap)fullImage.Clone();
                    //    defect.imagePath = path + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("hh_mm_ss") + DateTime.Now.Millisecond.ToString() + ".bmp";
                    //    defecs.Add(defect);

                    //}

                    //Parallel.ForEach(defecs, def =>
                    //{

                    //    Task.Factory.StartNew(()=>  def.SaveDefect());

                    //});
                    //--------------------------------------------
                    //path = string.Format(@"{0}\Models\{1}\DefectImages", CommonParameters.projectDirectory, CommonParameters.selectedModel);

                    //if (!Directory.Exists(path))
                    //{
                    //    Directory.CreateDirectory(path);
                    //}

                    //for (int i = 0; i < algo.defectCountProp; i++)
                    //{


                    //    int width = algo.getBottomRightPoint(i).X - algo.getTopLeftPoint(i).X;
                    //    int height = algo.getBottomRightPoint(i).Y - algo.getTopLeftPoint(i).Y;

                    //    Rectangle cropRect = new Rectangle(algo.getTopLeftPoint(i), new Size(width, height));
                    //    Console.WriteLine("This is CropRect {0}", cropRect);
                    //    if ((cropRect.Height + cropRect.Y) < 2600 && cropRect.X > 0 && cropRect.Y > 0)
                    //    {

                    //        bitmap = fullImage.Clone(cropRect, PixelFormat.Format24bppRgb);



                    //        //using (var graphics = Graphics.FromImage(algoImage))
                    //        //{
                    //        //    //graphics.DrawLine(pen, new Point(algo.out1Prop, 0), new Point(algo.out1Prop, 2500));


                    //        //    graphics.DrawRectangle(pen, cropRect);

                    //        //}

                    //        //bitmap.Save(path + @"\"+ DateTime.Now.Date.ToString("dd_MM_yyyy")+ DateTime.Now.TimeOfDay.ToString("hh:mm:ss") + DateTime.Now.Millisecond.ToString() +".bmp");
                    //        Console.WriteLine(path + DateTime.Now.ToString("dd_MM_yyyy") + DateTime.Now.ToString("hh:mm:ss") + DateTime.Now.Millisecond.ToString());
                    //        path = path + @"\" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.ToString("hh_mm_ss") + DateTime.Now.Millisecond.ToString() + ".bmp";


                    //        bitmap.Save(path);



                    //        //Console.WriteLine("Time Taken to save image-->{0}", sw.ElapsedMilliseconds);

                    //        db.InsertRecord(Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")), Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")), CommonParameters.selectedModel, algo.getTopLeftPoint(i), "Type1", path, 2);

                    //    }

                    //}
                    try
                    {
                        pictureBox5.Image = algoImage;
                        pictureBox5.Invalidate();
                        sw.Stop();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Error in displaying algo image on pb event btn1click {0}", ex.Message);
                    }
                    

                    Console.WriteLine("Time Taken -->{0}", sw.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    timer1.Stop();
                    Console.WriteLine(ex.Message);
                    if (allCameras.Count == 2)
                    {
                        captureImages = false;
                        btnStop_Click(sender, e);

                        //MessageBox.Show("Inspection stopped unexpectedly. Click start button to resume.");
                        Console.WriteLine("Inspection stopped unexpectedly. Trying to restart inspection");
                        btnStart_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Please connect all cameras", "Cameras not connected");
                    }
                }
                finally
                {

                }


                    labelJumboWidth.Text = CommonParameters.algo.sheetWidthProp.ToString("0.00");
                    labelDefCount.Text = CommonParameters.algo.defectCountProp.ToString();
                    //labelDefArea.Text = CommonParameters.algo.defectAreaProp.ToString();
                    //labelLength.Text = sheetLength.ToString();
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                timer1.Stop();
                MessageBox.Show("Inspection closed unexpectedly.");
                this.Close();
            }

            sw.Stop();
            Console.WriteLine("Time Taken-->{0}", sw.ElapsedMilliseconds);
        }


        double frameHeight = 0;
        double distance = 0;
        private void timerSpeed_Tick(object sender, EventArgs e)
        {
            //double mmppx = algo.mmperPixProp;
            if (pictureBox5.Image != null)
            {
                frameHeight = 2600 * 0.114259598;

                distance = (frameHeight * FrameCount) * 4;

                labelSpeed.Text = ((distance / 1000)+3).ToString("N2") + " mtr/min";
                Console.WriteLine("Num of frames {0}", FrameCount);
                Console.WriteLine("Num of Pb frames {0}", pbFrame);
                pbFrame = 0;
                FrameCount = 0;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmpLeft = new Bitmap(@"C:\software\Norton Pc\1 (3).jpg");
            Bitmap bmpRight = new Bitmap(@"C:\software\Norton Pc\1 (2).jpg");

            captureImages = true;

            doProcess = true;
            processThread = new Thread(delegate ()
            {
                while (doProcess)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    //labelDefType.Invoke((Action) delegate 
                    //{
                    //    //labelDefType.Text = bitmapsMerge1.Count.ToString();

                    //});
                    //labelDefArea.Invoke((Action)delegate
                    //{
                    //    //labelDefArea.Text = bitmapsMerge2.Count.ToString();
                    //});
                    //Console.WriteLine("Thread Running");

                    if (bmpLeft != null)
                    {
                        //Console.WriteLine("A1  {0}  A2 {1}", bitmapsMerge1.Count, bitmapsMerge2.Count);
                        CommonParameters.algo.overlapC1C2Prop = GetListValueById("Overlap");

                        int height = bmpLeft.Height;
                        int width = (bmpLeft.Width * 2) - CommonParameters.algo.overlapC1C2Prop;
                        Console.WriteLine("Overlap c# {0}", GetListValueById("Overlap"));

                        Bitmap fullImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                        Stopwatch sw1 = new Stopwatch();
                        sw1.Start();
                        //Bitmap algoImage = (Bitmap)bmpLeft.Clone();
                        //Bitmap MergeImage = MergeImagesSidewaysOverlap((Bitmap)bmpLeft.Clone(), (Bitmap)bmpRight.Clone(), GetListValueById("Overlap"));
                        fullImage = CommonParameters.algo.mergeImagesCpp((Bitmap)bmpLeft.Clone(new Rectangle(0, 0, bmpLeft.Width - CommonParameters.algo.overlapC1C2Prop, bmpLeft.Height), PixelFormat.Format24bppRgb), (Bitmap)bmpRight.Clone(new Rectangle(0, 0, bmpRight.Width, bmpRight.Height), PixelFormat.Format24bppRgb), (Bitmap)fullImage.Clone());

                        //Bitmap algoImage = algo.processAllFrontThick(fullImage.Clone(new Rectangle(0,0, fullImage.Width, bmpLeft.Height), PixelFormat.Format24bppRgb));
                        Bitmap algoImage = CommonParameters.algo.processAllFrontThick( (Bitmap)fullImage.Clone());
                        //Bitmap algoImage = algo.mergeImagesCpp((Bitmap)bmpLeft.Clone(new Rectangle(0,0, bmpLeft.Width, bmpLeft.Height), PixelFormat.Format24bppRgb), (Bitmap)bmpRight.Clone(new Rectangle(0, 0, bmpRight.Width, bmpRight.Height), PixelFormat.Format24bppRgb), (Bitmap)fullImage.Clone());
                        //Bitmap algoImage = MergeImagesSidewaysOverlap((Bitmap)bmpLeft.Clone(), (Bitmap)bmpLeft.Clone(), GetListValueById("Overlap"));

                        sw1.Stop();
                        Console.WriteLine("Time taken by algo {0}", sw1.ElapsedMilliseconds);
                        for (int i = 0; i < CommonParameters.algo.defectCountProp; i++)
                        {
                            int width1 = CommonParameters.algo.getBottomRightPoint(i).X - CommonParameters.algo.getTopLeftPoint(i).X;
                            int height1 = CommonParameters.algo.getBottomRightPoint(i).Y - CommonParameters.algo.getTopLeftPoint(i).Y;
                            double area = CommonParameters.algo.getDefectArea(i);
                            int cat = CommonParameters.algo.getDefectCat(i);
                            Rectangle cropRect = new Rectangle(CommonParameters.algo.getTopLeftPoint(i), new Size(width1, height1));
                            Console.WriteLine(i.ToString());
                            Console.WriteLine("This is crop rect {0} {1}", cropRect, i);
                            if (cropRect.X + cropRect.Width < algoImage.Width && cropRect.Y + cropRect.Height < algoImage.Height && cropRect.X > 0 && cropRect.Y > 0)
                            {
                                Bitmap imgSave = algoImage.Clone(cropRect, PixelFormat.Format8bppIndexed);

                                imgSave.Save(string.Format(@"C:\software\Norton Pc\New folder\{0}.bmp", i));

                            }
                        }

                        
                        //Console.WriteLine("Image List 1  {0}  Image List 2 {1}", bitmapsMerge1.Count, bitmapsMerge2.Count);

                        if (pictureBox5.InvokeRequired)
                        {
                            pictureBox5.BeginInvoke((Action)delegate
                            {
                                pictureBox5.Image = algoImage;
                                Console.WriteLine("Images Updated");

                                //image1Grabbed = false;
                                //image2Grabbed = false;

                            });
                        }
                        else
                        {
                            pictureBox5.Image = algoImage;

                        }
                        //Thread.Sleep(50);
                        //bitmapsMerge1.RemoveAt(0);
                        //bitmapsMerge2.RemoveAt(0);
                    }

                    sw.Stop();
                    Console.WriteLine("Time taken by thread {0}", sw.ElapsedMilliseconds);
                    doProcess = false;
                }
            });
            processThread.Start();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
            
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxFinish_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Model Data Changed. Do you want to stop Inspection ?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                CommonParameters.finish = comboBoxFinish.SelectedItem.ToString();
                CommonParameters.selectedModel = comboBoxFinish.SelectedItem.ToString();
                sheetLength = 2600;
                btnStop_Click(sender, e);
            }
            else
            {

                UpdateDefaultModelData();

            }
        }


        private void comboBoxOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Model Data Changed. Do you want to stop Inspection ?",
                        "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                CommonParameters.operation = comboBoxOperation.SelectedItem.ToString();
                sheetLength = 2600;

                btnStop_Click(sender, e);
            }
            else
            {
                UpdateDefaultModelData();

            }

        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            //TextBoxModelData_Leave(sender, e);
        }

        private void buttonRollMinus_Click(object sender, EventArgs e)
        {
            textBoxRollNum.Text = (Convert.ToInt32(textBoxRollNum.Text) - 1).ToString();
            TextBox_TextChanged(sender, e);
            TextBoxModelData_Leave(textBoxRollNum, e);
        }

        private void buttonRollPlus_Click(object sender, EventArgs e)
        {
            textBoxRollNum.Text = (Convert.ToInt32(textBoxRollNum.Text) + 1).ToString();
            TextBox_TextChanged(sender, e);
            TextBoxModelData_Leave(textBoxRollNum, e);
        }

        private void InspectionPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            doProcess = false;
            setCardDO(0, false);
            setCardDO(1, false);
            saveRecord = false;

            //if (processThread != null)
            //{
            //    processThread.Abort();
            //}
            

            DestroyCamera(camera1, converter1);
            DestroyCamera(camera2, converter2);
            err = instantDiCtrl1.SnapStop();
            CommonParameters.InspectionPage = null;

        }

        
    }
}
