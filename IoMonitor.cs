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
    public partial class IoMonitor : Form
    {
        public IoMonitor()
        {
            InitializeComponent();
        }
        TextBox[] textBox_arr_in;
        TextBox[] textBox_arr_out;
        Button[] buttonArr;
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IoMonitor_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            comboBox1.SelectedIndex = 0;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CenterToScreen();

            checkBox1.Checked = true;
            comboBox1.SelectedIndex = 0;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CenterToScreen();





            textBox_arr_in = new TextBox[] { textBox0, textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7 };
            textBox_arr_out = new TextBox[] { textBox32, textBox33, textBox34, textBox35, textBox36, textBox37, textBox38, textBox39 };
            buttonArr = new Button[] { button0, button1, button2, button3, button4, button5, button6, button7 };


            for (int i = 0; i < 64; i++)
            {
                if (i < 8)
                {
                    int res = CommonParameters.InspectionPage.getCardDI(i); //globalVars.mainform.getCardDI(i);
                    if (res == 1)
                    {
                        textBox_arr_in[i].BackColor = Color.Green;
                    }
                    else
                    {
                        textBox_arr_in[i].BackColor = Color.White;
                    }
                    textBox_arr_in[i].Text = "In" + i.ToString();
                    buttonArr[i].Text = "Force Out" + i.ToString();
                    buttonArr[i].Click += new EventHandler(ButtonClick);
                }
                else if (i > 31 && i < 40)
                {
                    int res = CommonParameters.InspectionPage.getCardDO(i - 32);
                    if (res == 1)
                    {
                        textBox_arr_out[i - 32].BackColor = Color.Green;
                    }
                    else
                    {
                        textBox_arr_out[i - 32].BackColor = Color.White;
                    }
                    textBox_arr_out[i - 32].Text = "Out" + (i - 32).ToString();
                }

            }

            tmrRefreshVals.Enabled = true;
            tmrRefreshVals.Start();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button temp = (Button)sender;
            Console.WriteLine("object name =" + temp.Name);
            // String index = (temp.Name.ElementAt(temp.Name.Length-1)).ToString();
            String index = Array.IndexOf(buttonArr, (Button)sender).ToString();

            Console.WriteLine("button clicked is " + index);
            int idx = int.Parse(index);

            Console.WriteLine("index {0} ", idx);
            int res = CommonParameters.InspectionPage.getCardDO(idx);
            if (res == 1)
            {
                Console.WriteLine("on " + textBox_arr_out[idx].Name);
                CommonParameters.InspectionPage.setCardDO(idx, false);
                textBox_arr_out[idx].BackColor = Color.White;
            }
            else
            {
                Console.WriteLine("off " + textBox_arr_out[idx].Name);
                CommonParameters.InspectionPage.setCardDO(idx, true);
                textBox_arr_out[idx].BackColor = Color.Green;

            }
        }

        private void tmrRefreshVals_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 64; i++)
            {
                if (i < 8)
                {
                    int res = CommonParameters.InspectionPage.getCardDI(i);
                    if (res == 1)
                    {
                        textBox_arr_in[i].BackColor = Color.Green;
                    }
                    else
                    {
                        textBox_arr_in[i].BackColor = Color.White;
                    }

                    //textBox_arr_in[i].Text = "In" + i.ToString();
                    //buttonArr[i].Text = "Force Out" + i.ToString();
                    //buttonArr[i].Click += new EventHandler(ButtonClick);

                }
            }
            this.Refresh();
        }

        private void IoMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrRefreshVals.Stop();
            tmrRefreshVals.Enabled = false;
            CommonParameters.InspectionPage.ioMonitor = null;
        }
    }
}
