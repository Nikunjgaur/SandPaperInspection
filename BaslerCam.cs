using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basler.Pylon;
namespace SandPaperInspection
{
    class BaslerCam : CameraSettings
    {
        public Camera camera = null;
        public PixelDataConverter converter = new PixelDataConverter();
        public string camSerialNum = "23221923";
        public Stopwatch stopWatch = new Stopwatch();
        //public Bitmap grabbedImage;

        private void OnConnectionLost(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                MyForm.CameraSettings.BeginInvoke(new EventHandler<EventArgs>(OnConnectionLost), sender, e);
                return;
            }

            // Close the camera object.
            DestroyCamera(camera, converter);
            // Because one device is gone, the list needs to be updated.
            // UpdateDeviceList();
        }
        public void DestroyCamera(Camera camera, PixelDataConverter converter)
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

        // Occurs when the connection to a camera device is opened.
        private void OnCameraOpened(Object sender, EventArgs e)
        {
            if (MyForm.CameraSettings.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                MyForm.CameraSettings.BeginInvoke(new EventHandler<EventArgs>(OnCameraOpened), sender, e);
                return;
            }

            // The image provider is ready to grab. Enable the grab buttons.
        }

        // Occurs when the connection to a camera device is closed.
        private void OnCameraClosed(Object sender, EventArgs e)
        {
            if (MyForm.CameraSettings.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                MyForm.CameraSettings.BeginInvoke(new EventHandler<EventArgs>(OnCameraClosed), sender, e);
                return;
            }

            // The camera connection is closed. Disable all buttons.
        }

        // Occurs when a camera starts grabbing.
        private void OnGrabStarted(Object sender, EventArgs e)
        {
            if (MyForm.CameraSettings.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                MyForm.CameraSettings.BeginInvoke(new EventHandler<EventArgs>(OnGrabStarted), sender, e);
                return;
            }

            // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed.

            stopWatch.Reset();

            // Do not update the device list while grabbing to reduce jitter. Jitter may occur because the GUI thread is blocked for a short time when enumerating.
            // updateDeviceListTimer.Stop();

            // The camera is grabbing. Disable the grab buttons. Enable the stop button.
            
        }

        public void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        // Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            if (MyForm.CameraSettings.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                MyForm.CameraSettings.BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed), sender, e.Clone());
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
                    converter.OutputPixelFormat = PixelType.BGRA8packed;
                    //  Console.WriteLine("3.25");
                    IntPtr ptrBmp = bmpData.Scan0;
                    converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                    bitmap.UnlockBits(bmpData);

                    
                    //  Console.WriteLine("4");
                    // Assign a temporary variable to dispose the bitmap after assigning the new bitmap to the display control.
                    //  Bitmap bitmapOld = pictureBox.Image as Bitmap;
                    // Provide the display control with the new bitmap. This action automatically updates the display.
                    //   pictureBox.Image = bitmap;
                    // pictureBox.Image = globalVars.algoLib.processOne(newImg);
                    // Console.WriteLine("img1");
                    //if (bitmapOld != null)
                    //{
                    //    // Dispose the bitmap.
                    //    bitmapOld.Dispose();
                    //}
                    // }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("On Grab Exxxxx----------------");
                ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }

        public void Stop(Camera camera)
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
        public void OneShot(Camera camera)
        {
            try
            {
                // Starts the grabbing of one image.
                camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                camera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception exception)
            {
                Console.WriteLine("IOne Shot Exxxxx----------------");
                ShowException(exception);
            }
        }
        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            if (MyForm.CameraSettings.InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                Form.ActiveForm.BeginInvoke(new EventHandler<GrabStopEventArgs>(OnGrabStopped), sender, e);
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
        public void startCamera()
        {
            if (camera != null)
            {
                DestroyCamera(camera, converter);
            }


            // Open the connection to the selected camera device.
            
                
                try
                {
                    // Create a new camera object.
                    camera = new Camera(camSerialNum);
                //CameraSettings cameraSettings = new CameraSettings();
                //cameraSettings.Focus();
                //cameraSettings.Activate();
                //Console.WriteLine(Form.ActiveForm.Name);
                Form currentForm = Form.ActiveForm;
                Console.WriteLine(currentForm.Name);
                    camera.CameraOpened += Configuration.AcquireContinuous;

                    // Register for the events of the image provider needed for proper operation.
                    camera.ConnectionLost += OnConnectionLost;
                    camera.CameraOpened += OnCameraOpened;
                    camera.CameraClosed += OnCameraClosed;
                    camera.StreamGrabber.GrabStarted += OnGrabStarted;
                    camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                    camera.StreamGrabber.GrabStopped += OnGrabStopped;

                    // Open the connection to the camera device.
                    camera.Open();

                    // Set the parameter for the controls.
                    if (camera.Parameters[PLCamera.TestImageSelector].IsWritable)
                    {
                        //testImageControl.Parameter = camera.Parameters[PLCamera.TestImageSelector];
                    }
                    else
                    {
                        //testImageControl.Parameter = camera.Parameters[PLCamera.TestPattern];
                    }
                //pixelFormatControl.Parameter = camera.Parameters[PLCamera.PixelFormat];
                //widthSliderControl.Parameter = camera.Parameters[PLCamera.Width];
                //heightSliderControl.Parameter = camera.Parameters[PLCamera.Height];
                //if (camera.Parameters.Contains(PLCamera.GainAbs))
                //{
                //    //gainSliderControl.Parameter = camera.Parameters[PLCamera.GainAbs];
                //}
                //else
                //{
                //    //gainSliderControl.Parameter = camera.Parameters[PLCamera.Gain];
                //}
                //if (camera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                //{
                //    //exposureTimeSliderControl.Parameter = camera.Parameters[PLCamera.ExposureTimeAbs];
                //}
                //else
                //{
                //    //exposureTimeSliderControl.Parameter = camera.Parameters[PLCamera.ExposureTime];
                //}
                //camera.Parameters[PLCamera.OffsetX].TrySetValue(0);
                camera.Parameters[PLCamera.OffsetX].TrySetValue(0);
                camera.Parameters[PLCamera.OffsetY].TrySetValue(0);
                // Some parameters have restrictions. You can use GetIncrement/GetMinimum/GetMaximum to make sure you set a valid value.
                // Here, we let pylon correct the value if needed.
                //camera.Parameters[PLCamera.Width].SetValue(4096, IntegerValueCorrection.Nearest);
                //Console.WriteLine("aoiHeight:{0}", globalVars.AOIheight);
                //camera.Parameters[PLCamera.Height].SetValue(globalVars.AOIheight, IntegerValueCorrection.Nearest);//1876
                //camera.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(globalVars.ExposureTimeC2);//300
                                                                                                    // camera2.Parameters[PLCamera.AcquisitionLineRateAbs].SetValue(2845);
            }
            catch (Exception exception)
                {
                    ShowException(exception);
                }
            }
            

        // Starts the continuous grabbing of images and handles exceptions.
        public void ContinuousShot(Camera camera)
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
    }
}
