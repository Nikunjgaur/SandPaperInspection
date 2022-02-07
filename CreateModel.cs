using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using SandPaperInspection.classes;

namespace SandPaperInspection
{
    public partial class CreateModel : Form
    {
        public CreateModel()
        {
            InitializeComponent();
        }


        bool btnTeext = false;
        private void CreateModel_Load(object sender, EventArgs e)
        {
           
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void comboBoxFGcodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxModelName.Text = comboBoxFGcodes.SelectedItem.ToString();
            comboBoxFGcodes.Text = comboBoxFGcodes.SelectedItem.ToString();
        }

        public static void DeleteDirectoryRecursively(string target_dir)
        {
            foreach (string file in Directory.GetFiles(target_dir))
            {
                File.Delete(file);
            }

            foreach (string subDir in Directory.GetDirectories(target_dir))
            {
                DeleteDirectoryRecursively(subDir);
            }

            Thread.Sleep(1); // This makes the difference between whether it works or not. Sleep(0) is not enough.
            Directory.Delete(target_dir);
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (textBoxModelName.Text != "")
            {
                string modelName = textBoxModelName.Text;

                string path = string.Format(@"{0}\Models\{1}", CommonParameters.projectDirectory, modelName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    Directory.CreateDirectory(path+ @"\Images");

                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Model already exists. Overwrite model data and delete model Images?","Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DeleteDirectoryRecursively(path);
                        Directory.CreateDirectory(path);
                        Directory.CreateDirectory(path + @"\Images");

                    }
                    else
                    {
                        return;
                    }
                }

                ModelData modelData = new ModelData();
                string threshResult = JsonConvert.SerializeObject(modelData);
                File.WriteAllText(path + @"\thresholds.json", threshResult);
                CommonParameters.selectedModel = modelName;
                MessageBox.Show("Set thresholds for this model on Inspection screen.","Model created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InspectionPage inspectionPage = new InspectionPage();
                inspectionPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter Model Name.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
