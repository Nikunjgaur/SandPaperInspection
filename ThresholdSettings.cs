﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using SandPaperInspection.classes;

namespace SandPaperInspection
{
    public partial class ThresholdSettings : Form
    {
        ModelData modelData = new ModelData();
        public ThresholdSettings()
        {
            InitializeComponent();
            txtTH1.ReadOnly = true;
            bool CheckedBox = CommonParameters.saveImages;
            checkBoxSaveImages.Checked = CheckedBox;
        }
        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        void UpdateDefaultSettings()
        {
            modelData = new ModelData();
            modelData = JsonConvert.DeserializeObject<ModelData>(File.ReadAllText(string.Format(@"{0}\Models\{1}\thresholds.json", CommonParameters.projectDirectory, CommonParameters.selectedModel)));
            Console.WriteLine("{0} , {1}", CommonParameters.selectedModel, ModelData.webDetect);
            trkBrTh1.Value = ModelData.webDetect;
            trkBrTh2.Value = ModelData.blockSize;
            
            txtCamExposureT.Value = Convert.ToInt32(ModelData.cam1Expo);
            txtCamExposureTC2.Value = Convert.ToInt32(ModelData.cam2Expo);
            txtExposer.Text = ModelData.cam1Expo.ToString();
            txtExpC2.Text = ModelData.cam2Expo.ToString();

            txtTH1.Text = trkBrTh1.Value.ToString();
            CommonParameters.algo.defMinSizeProp = trkBrTh1.Value;
            txtth2.Text = trkBrTh2.Value.ToString();
            CommonParameters.algo.defBlockSizeProp = trkBrTh2.Value;
            
            //CommonParameters.algo.th1Prop = trkBrTh4.Value;
            //CommonParameters.algo.th5Prop = trkBrTh1.Value;

            var textBoxes = GetAll(this, typeof(TextBox));
            foreach (TextBox textBox in textBoxes)
            {
                if (textBox.Name != "txtFg_item_code")
                {
                    textBox.ReadOnly = true;
                    textBox.Enter += TextBox_Enter;
                }
            }
        }

        private void ThresholdSettings_Load(object sender, EventArgs e)
        {
            
            UpdateDefaultSettings();
           //CommonParameters.InspectionPage.UpdateLabel();
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            ToolTip toolTip = new ToolTip();
            toolTip.Show("Use trackbar to change value", textBox, textBox.Width + 5, 0, 1000);
        }

        private void trkBrTh1_Scroll(object sender, EventArgs e)
        {
            txtTH1.Text = trkBrTh1.Value.ToString();
            CommonParameters.algo.defMinSizeProp = trkBrTh1.Value;

        }

        private void trkBrTh2_Scroll(object sender, EventArgs e)
        {
            if (trkBrTh2.Value % 2 != 0)
            {
                txtth2.Text = trkBrTh2.Value.ToString();
                CommonParameters.algo.defBlockSizeProp = trkBrTh2.Value;

            }

        }

        

        private void btmThreshold_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Save settings for current model ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                ModelData.webDetect = Convert.ToInt32(txtTH1.Text);
                ModelData.blockSize = Convert.ToInt32(txtth2.Text);
                
                string threshResult = JsonConvert.SerializeObject(modelData);
                string path = string.Format(@"{0}\Models\{1}", CommonParameters.projectDirectory, CommonParameters.selectedModel);
                File.WriteAllText(path + @"\thresholds.json", threshResult);
                MessageBox.Show("Settings saved.");
            }
            else
            {
                UpdateDefaultSettings();
                MessageBox.Show("Default Settings Loaded.");
            }
        }

        private void txtCamExposureT_Scroll_1(object sender, EventArgs e)
        {
            ModelData.cam1Expo = txtCamExposureT.Value;
            txtExposer.Text = txtCamExposureT.Value.ToString();
            CommonParameters.InspectionPage.UpdateCameraPara();
        }

        private void txtCamExposureTC2_Scroll_1(object sender, EventArgs e)
        {
            ModelData.cam2Expo = txtCamExposureTC2.Value;
            txtExpC2.Text = txtCamExposureTC2.Value.ToString();
            CommonParameters.InspectionPage.UpdateCameraPara();

        }

        private void checkBoxSaveImages_CheckedChanged(object sender, EventArgs e)
        {
            CommonParameters.saveImages = checkBoxSaveImages.Checked;
            CommonParameters.InspectionPage.UpdateLabel();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ThresholdSettings_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ThresholdSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonParameters.InspectionPage.thresholdSettings = null;
        }
    }
}
