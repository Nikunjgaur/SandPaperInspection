using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SandPaperInspection
{
    public static class ExceptionLogging
    {
        // This will get the current WORKING directory (i.e. \bin\Debug)
        private static string workingDirectory = Environment.CurrentDirectory;

        // This will get the current PROJECT directory
        private static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        private static String ErrorlineNo, Errormsg, extype, ErrorLocation, ErrorFile;

        public static void SendErrorToFile(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;
            var st = new StackTrace(ex, true);
            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            ErrorLocation = ex.Message.ToString();
            ErrorFile = st.GetFrames()         // get the frames
                 .Select(frame => new
                 {                   // get the info
                      FileName = frame.GetFileName(),
                     
                 }).ToString();

            try
            {
                string filepath = string.Format(@"{0}\ExceptionDetailsFile", projectDirectory);  //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);

                }
                filepath = filepath + @"\"+ DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {


                    File.Create(filepath).Dispose();

                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() 
                                    + line + "Error Line No :" + " " + ErrorlineNo 
                                    + line + "Error Message:" + " " + Errormsg 
                                    + line + "Exception Type:" + " " + extype 
                                    + line + "Error Location :" + " " + ErrorLocation 
                                    + line + " Error Filename:" + " " + ErrorFile;

                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();

                }

            }
            catch (Exception e)
            {
                e.ToString();

            }
        }

    }
}
