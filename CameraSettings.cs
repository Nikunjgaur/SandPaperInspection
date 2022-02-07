using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using SandPaperInspection.classes;

namespace SandPaperInspection
{
    public partial class CameraSettings : Form
    {
        CameraParameters CameraParameters = new CameraParameters();
        bool changesMade = false;
        
        public CameraSettings()
        {
            InitializeComponent();
            trackBarOverlap.Minimum = 0;
            trackBarOverlap.Maximum = 2000;
        }

        private void CameraSettings_Load(object sender, EventArgs e)
        {
            CameraParameters.updatePara();
            //string result = JsonConvert.SerializeObject(CameraParameters.List);
            //File.WriteAllText(String.Format(@"{0}\bin\x64\Release\CamSettings.json", CommonParameters.projectDirectory), result);

            CameraParameters.List = JsonConvert.DeserializeObject<List<Settings>>(File.ReadAllText(String.Format(@"{0}\bin\x64\Release\CamSettings.json", CommonParameters.projectDirectory)));
            var tb = GetAll(this, typeof(TextBox));

            foreach (TextBox textBox in tb)
            {
                for (int i = 0; i < CameraParameters.List.Count; i++)
                {
                    if (textBox.Name.Remove(0, 7) == CameraParameters.List[i].nodeName)
                    {
                        textBox.Text = CameraParameters.List[i].nodeVal.ToString();
                    }
                }
                textBox.KeyDown += TextBox_KeyDown;
                textBox.Leave += TextBox_Leave;
                textBox.TextChanged += TextBox_TextChanged;

            }

            var trackBars = GetAll(this, typeof(TrackBar));

            foreach (TrackBar trackBar in trackBars)
            {
                for (int i = 0; i < CameraParameters.List.Count; i++)
                {
                    if (trackBar.Name.Remove(0, 8) == CameraParameters.List[i].nodeName)
                    {
                        trackBar.Value = (int)CameraParameters.List[i].nodeVal;
                        trackBar.ValueChanged += TrackBar_ValueChanged;
                    }
                }
            }

            textBoxLineRateCam1.KeyDown += TextBoxLineRateCam1_KeyDown;
        }

        private void TextBoxLineRateCam1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (e.KeyCode == Keys.Enter && textBox.Text != "" && float.TryParse(textBox.Text, out float n) == true)
            {
                CommonParameters.InspectionPage.UpdateCameraPara();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            changesMade = true;
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            var trackBars = GetAll(this, typeof(TrackBar));
            TextBox textBox = (TextBox)sender;

            if (textBox.Text != "" && float.TryParse(textBox.Text, out float n) == true)
            {
                for (int i = 0; i < CameraParameters.List.Count; i++)
                {
                    if (textBox.Name.Remove(0, 7) == CameraParameters.List[i].nodeName)
                    {
                        CameraParameters.List[i].nodeVal = float.Parse(textBox.Text);
                        textBox.Text = CameraParameters.List[i].nodeVal.ToString();

                        foreach (TrackBar trackBar in trackBars)
                        {
                            if (trackBar.Name.Remove(0, 8) == textBox.Name.Remove(0, 7))
                            {
                                trackBar.Value = Convert.ToInt32(textBox.Text);
                            }
                        }
                    }

                }
            }
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            var tb = GetAll(this, typeof(TextBox));

            TrackBar trackBar = (TrackBar)sender;
            for (int i = 0; i < CameraParameters.List.Count; i++)
            {
                if (trackBar.Name.Remove(0, 8) == CameraParameters.List[i].nodeName)
                {
                    CameraParameters.List[i].nodeVal = trackBar.Value;
                    trackBar.Value = (int)CameraParameters.List[i].nodeVal;
                    foreach (TextBox textBox in tb)
                    {
                        if (trackBar.Name.Remove(0, 8) == textBox.Name.Remove(0, 7))
                        {
                            textBox.Text = trackBar.Value.ToString();
                        }
                    }
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var trackBars = GetAll(this, typeof(TrackBar));
            TextBox textBox = (TextBox)sender;

            if (e.KeyCode == Keys.Enter && textBox.Text != "" && float.TryParse(textBox.Text, out float n) == true)
            {
                for (int i = 0; i < CameraParameters.List.Count; i++)
                {
                    if (textBox.Name.Remove(0, 7) == CameraParameters.List[i].nodeName)
                    {

                        CameraParameters.List[i].nodeVal = float.Parse(textBox.Text);
                        textBox.Text = CameraParameters.List[i].nodeVal.ToString();

                        foreach (TrackBar trackBar in trackBars)
                        {
                            if (trackBar.Name.Remove(0, 8) == textBox.Name.Remove(0, 7))
                            {
                                trackBar.Value = Convert.ToInt32(textBox.Text);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Save Changes ?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string camSettings = JsonConvert.SerializeObject(CameraParameters.List);
                File.WriteAllText(String.Format(@"{0}\bin\x64\Release\CamSettings.json", CommonParameters.projectDirectory), camSettings);
                changesMade = false;
                MessageBox.Show("Camera settings Updated", "Settings Saved");
            }
        }

        private void CameraSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changesMade == true)
            {
                DialogResult result = MessageBox.Show("Close wihtout saving ?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    CommonParameters.InspectionPage.cameraSettings = null;
                }
            }
            else
            {
                CommonParameters.InspectionPage.cameraSettings = null;

            }

        }

        private void trackBarOverlap_Scroll(object sender, EventArgs e)
        {
            

            int trackValue = trackBarOverlap.Value;

            if (trackValue % trackBarOverlap.SmallChange != 0)
            {
                trackValue = (trackValue / trackBarOverlap.SmallChange) * trackBarOverlap.SmallChange;

                trackBarOverlap.Value = trackValue;

            }
            if (checkBoxStatic_Live.Checked == true)
            {
                CommonParameters.InspectionPage.ShowStaticImage();
            }
            else
            {
                CommonParameters.InspectionPage.ShowLiveImage();
            }
            //CommonParameters.InspectionPage.UpdateCameraPara();
        }

        private void trackBarLineRateCam1_Scroll(object sender, EventArgs e)
        {
            CommonParameters.InspectionPage.UpdateCameraPara();

        }

        private void trackBarLineRateCam2_Scroll(object sender, EventArgs e)
        {
            CommonParameters.InspectionPage.UpdateCameraPara();

        }

    }
}
