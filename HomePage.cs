using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SandPaperInspection.classes;

namespace SandPaperInspection
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            var ins_ico = new Bitmap(String.Format(@"{0}\Resources\conveyor.png", CommonParameters.projectDirectory));
            var ins_ico_new = new Bitmap(ins_ico, new Size(100, 100));
            var create_ico = new Bitmap(String.Format(@"{0}\Resources\procedure.png", CommonParameters.projectDirectory));
            var create_ico_new = new Bitmap(create_ico, new Size(100, 100));
            buttonInspect.Image = ins_ico_new;
            buttonCreate.Image = create_ico_new;
        }

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            selectInspectionModel selectInspectionModel = new selectInspectionModel();
            selectInspectionModel.Show();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            CreateModel createModel = new CreateModel();
            createModel.Show();
        }

        public void CloseAllOpenForm(Form currentForm)
        {
            // temp list
            var list = new List<Form>();

            // fill list
            foreach (Form form in Application.OpenForms)
                if (!currentForm.Equals(form) && form.Name == "InspectionPage")
                    list.Add(form);

            // close selected forms
            foreach (Form form in list)
                form.Close();
        }

        private void HomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseAllOpenForm(this);
        }
    }
}
