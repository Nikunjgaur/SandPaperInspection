using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SandPaperInspection.classes
{
    class CommonParameters
    {

        public static InspectionPage InspectionPage;
        // This will get the current WORKING directory (i.e. \bin\Debug)
        private static string workingDirectory = Environment.CurrentDirectory;

        // This will get the current PROJECT directory
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        public static processWeb.Class1 algo = new processWeb.Class1();
        public static string selectedModel { get; set; }
        public static bool saveImages {get; set;}
    }
    public class ModelData
    {
        public int webDetect = 56;
        public int blockSize = 70;
        public int okLimit = 70;
        [JsonProperty]
        public static double cam1Expo = 100;
        [JsonProperty]
        public static double cam2Expo = 100;
        public int thresh1 = 90;
        public int thresh2 = 50;
    }
}
