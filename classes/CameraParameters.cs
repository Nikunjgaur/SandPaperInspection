using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandPaperInspection.classes
{
    class Settings
    {
        public string nodeName;
        private float nodeValue;

        public float[] nodeRange = { 0, 255 };

        public Settings(string name, float val, float lb, float ub)
        {
            nodeName = name;
            nodeVal = val;
            nodeRange[0] = lb;
            nodeRange[1] = ub;
        }

        public float nodeVal
        {
            get => nodeValue;
            set
            {
                if (value < nodeRange[0] || value > nodeRange[1])
                {

                    nodeValue = nodeRange[0];
                }
                else
                {

                    nodeValue = value;
                }
            }
        }
    }

    class CameraParameters
    {
        public static List<Settings> List = new List<Settings>();

        public static Bitmap cam1 { get; set; }
        public static Bitmap cam2 { get; set; }
        public static int overlap { get; set; }

        public CameraParameters()
        {
            List = new List<Settings>();
        }

        public void updatePara()
        {
            List = new List<Settings>();
            List.Add(new Settings("PreDiv", 50, 1, 128));
            List.Add(new Settings("Mul", 50, 1, 32));
            List.Add(new Settings("PostDiv", 50, 1, 128));
            List.Add(new Settings("FrameDelay", 50, 1, 128));
            List.Add(new Settings("Cam1Expo", 500, 100, 7000));
            List.Add(new Settings("Cam2Expo", 300, 100, 7000));
            List.Add(new Settings("Overlap", 50, 0, 3000));
            List.Add(new Settings("LineRateCam1", 500, 0, 5000));
            List.Add(new Settings("LineRateCam2", 500, 0, 5000));
        }
    }

}


