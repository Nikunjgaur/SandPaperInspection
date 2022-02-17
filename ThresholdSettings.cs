using System;
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
            Console.WriteLine("{0} , {1}", CommonParameters.selectedModel, modelData.webDetect);
            trkBrTh1.Value = modelData.webDetect;
            trkBrTh2.Value = modelData.blockSize;
            trkBrTh3.Value = modelData.okLimit;
            trkBrTh4.Value = modelData.thresh1;
            trkBrTh5.Value = modelData.thresh2;
            txtCamExposureT.Value = Convert.ToInt32(ModelData.cam1Expo);
            txtCamExposureTC2.Value = Convert.ToInt32(ModelData.cam2Expo);
            txtExposer.Text = ModelData.cam1Expo.ToString();
            txtExpC2.Text = ModelData.cam2Expo.ToString();

            txtTH1.Text = trkBrTh1.Value.ToString();
            CommonParameters.algo.defMinSizeProp = trkBrTh1.Value;
            txtth2.Text = trkBrTh2.Value.ToString();
            CommonParameters.algo.defBlockSizeProp = trkBrTh2.Value;
            txtth3.Text = trkBrTh3.Value.ToString();
            CommonParameters.algo.th3Prop = trkBrTh1.Value;
            txtth4.Text = trkBrTh4.Value.ToString();
            CommonParameters.algo.th4Prop = trkBrTh1.Value;
            txtth5.Text = trkBrTh5.Value.ToString();
            CommonParameters.algo.th5Prop = trkBrTh1.Value;

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

        private void trkBrTh3_Scroll(object sender, EventArgs e)
        {
            txtth3.Text = trkBrTh3.Value.ToString();
            CommonParameters.algo.th3Prop = trkBrTh1.Value;

        }

        private void trkBrTh4_Scroll(object sender, EventArgs e)
        {
            txtth4.Text = trkBrTh4.Value.ToString();
            CommonParameters.algo.th4Prop = trkBrTh1.Value;

        }

        private void trkBrTh5_Scroll(object sender, EventArgs e)
        {
            txtth5.Text = trkBrTh5.Value.ToString();
            CommonParameters.algo.th5Prop = trkBrTh1.Value;
        }

        private void btmThreshold_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Save settings for current model ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                modelData.webDetect = trkBrTh1.Value;
                modelData.blockSize = trkBrTh2.Value;
                modelData.okLimit = trkBrTh3.Value;
                modelData.thresh1 = trkBrTh4.Value;
                modelData.thresh2 = trkBrTh5.Value;
                string threshResult = JsonConvert.SerializeObject(modelData);
                string path = string.Format(@"{0}\Models\{1}", CommonParameters.projectDirectory, CommonParameters.selectedModel);
                File.WriteAllText(path + @"\thresholds.json", threshResult);
                Console.WriteLine(threshResult);
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
