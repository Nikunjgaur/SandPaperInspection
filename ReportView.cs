using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Npgsql;
using SandPaperInspection.classes;

namespace SandPaperInspection
{
    public partial class ReportView : Form
    {
        Random rnd = new Random();
        Database db = new Database();
        Bitmap reportImg;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        Bitmap[] inputImages = new Bitmap[4];
        string[] inputImagesPath = new string[4];
        NpgsqlTypes.NpgsqlPoint point = new NpgsqlTypes.NpgsqlPoint(0, 0);
        public ReportView()
        {
            InitializeComponent();
            //reportImg = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            //chart1.ChartAreas[0].AxisX2.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisX.Maximum = 16000;
            chart1.ChartAreas[0].AxisY.Maximum = 20000;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            //inputImages[0] = new Bitmap(@"C:\Users\ADVANTECh\Desktop\1.png");
            //inputImages[1] = new Bitmap(@"C:\Users\ADVANTECh\Desktop\2.png");
            //inputImages[2] = new Bitmap(@"C:\Users\ADVANTECh\Desktop\3.png");
            //inputImages[3] = new Bitmap(@"C:\Users\ADVANTECh\Desktop\4.png");
            inputImagesPath[0] = @"C:\Users\Admin\Desktop\1.png";
            inputImagesPath[1] = @"C:\Users\Admin\Desktop\2.png";
            inputImagesPath[2] = @"C:\Users\Admin\Desktop\3.png";
            inputImagesPath[3] = @"C:\Users\Admin\Desktop\4.png";
        }

        
        public void UpdateLog()
        {
            int imgIndex = rnd.Next(1,4);

            //InsertRecord(Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")), DateTime.Now.ToString("HH:mm:ss"), "SrNum", new NpgsqlTypes.NpgsqlPoint(rnd.Next(10, 8000), rnd.Next(20, 4000)), imgIndex.ToString(), inputImagesPath[imgIndex - 1], rnd.Next(0,11));
            InsertRecord(Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")), DateTime.Now.ToString("HH:mm:ss"), "SrNum2", new NpgsqlTypes.NpgsqlPoint(8000,100), imgIndex.ToString(), inputImagesPath[0], rnd.Next(0,11));

        }


        public void InsertRecord(DateTime date, string time, string srNum, NpgsqlTypes.NpgsqlPoint locationPoint,
                               string deftype, string inputImgPath, int productCode)
        {

            NpgsqlTypes.NpgsqlPoint location = new NpgsqlTypes.NpgsqlPoint(locationPoint.X, locationPoint.Y);

            using (NpgsqlConnection con = db.GetConnection())
            {

                string query = @"insert into public.logreport (_date, _time, serialNum, _location, deftype, imagepath, productcode)
                                values(@date, @time, @srNum, @location, @deftype, @imagepath, @productCode)";

                NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@srNum", srNum);
                cmd.Parameters.AddWithValue("@location", location);
                cmd.Parameters.AddWithValue("@deftype", deftype);
                //cmd.Parameters.AddWithValue("@defsize", defsize);
                cmd.Parameters.AddWithValue("@imagepath", inputImgPath);
                cmd.Parameters.AddWithValue("@productCode", productCode);

                con.Open();
                int n = cmd.ExecuteNonQuery();
            }
        }

        void del()
        {
            
            using (NpgsqlConnection con = db.GetConnection())
            {
                con.Open();
                string query = @"delete from logreport where _location  = @loc and productcode = 2;";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@loc", point);
                NpgsqlDataReader reader = cmd.ExecuteReader();

            }
        }

        public void ShowDateWise()
        {
            using (NpgsqlConnection con = db.GetConnection())
            {
                con.Open();
                string query = @"select Max(_location[0]), Max(_location[1]) from public.logreport where _date = @date and serialnum = @srnum";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                cmd.Parameters.AddWithValue("@srnum", comboBoxSrNum.SelectedItem.ToString());
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (chart1.ChartAreas[0].AxisX.Maximum < (double)reader[0])
                    {
                        chart1.ChartAreas[0].AxisX.Maximum = (double)reader[0] + 50;
                        chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

                    }
                    if (chart1.ChartAreas[0].AxisY.Maximum < (double)reader[1])
                    {
                        chart1.ChartAreas[0].AxisY.Maximum = (double)reader[1];
                        chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

                    }
                }

                reader.Close();

                query = @"select * from public.logreport where _date = @date and serialnum = @srnum";
                cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                cmd.Parameters.AddWithValue("@srnum", comboBoxSrNum.SelectedItem.ToString());

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Clear();
                    dt.Load(reader);
                    dataGridViewReport.DataSource = dt;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        NpgsqlTypes.NpgsqlPoint point = (NpgsqlTypes.NpgsqlPoint)reader[3];
                        Console.WriteLine("This is Point X {0} and this is Point Y {1}", point.X, point.Y);
                        chart1.Series[Convert.ToInt32(reader[6])-1].Points.AddXY(point.X, point.Y);
                    }

                    labeltotalDef.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    MessageBox.Show("No Data Found for this date and shift");
                }
                reader.Close();

            }
        }


        private DataTable Transpose(DataTable dt)
        {
            DataTable dtNew = new DataTable();

            //adding columns    
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(i.ToString());
            }

            //Changing Column Captions: 
             dtNew.Columns[0].ColumnName = "Property";
            dtNew.Columns[1].ColumnName = "Value";

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    //For dateTime columns use like below
            //    dtNew.Columns[i].ColumnName = dt.Rows[i].ItemArray[0].ToString();
            //    //Else just assign the ItermArry[0] to the columnName prooperty
            //}

            //Adding Row Data
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                DataRow r = dtNew.NewRow();
                r[0] = dt.Columns[k].ToString();
                for (int j = 1; j <= dt.Rows.Count; j++)
                    r[j] = dt.Rows[j - 1][k];
                dtNew.Rows.Add(r);
            }

            return dtNew;
        }

        private void ReportView_Load(object sender, EventArgs e)
        {
            labeltotalDef.Text = "";
            //Console.WriteLine("This is rows count" + dt.Rows.Count);
            //timer1.Enabled = true;
            //timer1.Start();

        }

        void UpdateList()
        {
            using (NpgsqlConnection con = db.GetConnection())
            {
                con.Open();
                string query = @"select serialnum from public.logreport where _date = @date group by (serialnum)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                //cmd.Parameters.AddWithValue("@date2", dateTimePickerTo.Value.ToString("dd-MM-yyyy"));

                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBoxSrNum.Items.Add(reader[0]);
                        comboBoxSrNum.SelectedIndex = 0;
                    }
                }
                else
                {
                    MessageBox.Show("No Data Found for this date and shift");
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
            {
      
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void SetReportWithPoint(Point point)
        {
            byte[] imgData = null;
            Bitmap bmp = null;
            NpgsqlTypes.NpgsqlPoint npPoint = new NpgsqlTypes.NpgsqlPoint (point.X, point.Y);
            Console.WriteLine(npPoint);
            dt2.Clear();
            using (NpgsqlConnection con = db.GetConnection())
            {
                con.Open();
                string query = @"select _location as ""Location"", deftype as ""Defect Type"",
                                productcode ""Product Code"", imagepath from public.logreport where _date = @date and _location ~= @point";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                cmd.Parameters.AddWithValue("@point", npPoint);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    //while (reader.Read)
                    //{
                    //    Bitmap defImage = new Bitmap(string.Format(@"{0}", reader[3]));
                    //    pictureBoxDefImage.Image = defImage;
                    //}
                    
                    dt2.Load(reader);
                    dataGridView1.DataSource = Transpose(dt2);
                    dataGridView1.Columns[0].Width = 120;
                    

                }
                else
                {
                    MessageBox.Show("No Data Found for this date and shift");
                }
                reader.Close();
                query = @"select _location as ""Location"", deftype as ""Defect Type"",
                                productcode ""Product Code"", imagepath from public.logreport where _date = @date and _location ~= @point";
                cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                cmd.Parameters.AddWithValue("@point", npPoint);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("This is imgPath{0}", reader[3]);

                    Bitmap defImage = new Bitmap(string.Format(@"{0}", reader[3]));
                    pictureBoxDefImage.Image = defImage;
                }

            }

            //using (NpgsqlConnection con = db.GetConnection())
            //{
            //    con.Open();
            //    string query = @"select imagebytes from public.logreport where _date = @date and _location ~= @point";
            //    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            //    cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd")));
            //    cmd.Parameters.AddWithValue("@point", npPoint);

            //    NpgsqlDataReader reader = cmd.ExecuteReader();

            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            imgData = (byte[])reader[0];
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data Found for this date and shift");
            //    }
            //}
            //if (imgData != null)
            //{
            //    using (var ms = new MemoryStream(imgData))
            //    {
            //        bmp = new Bitmap(ms);
            //    }

            //    pictureBoxDefImage.Image = bmp;
            //}
        }

        public DataPoint GetPointAtMouse(Chart c, MouseEventArgs e)
        {
            var result = c.HitTest(e.X, e.Y);
            // If the mouse if over a data point
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                // Find selected data point
                var point = c.Series[result.Series.Name].Points[result.PointIndex];
                Console.WriteLine(result.Series.Name);
                Console.WriteLine(point.YValues[0]);
                SetReportWithPoint(new Point((int)point.XValue, (int)point.YValues[0]));
                return point;
            }
            return null;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            GetPointAtMouse(chart1, me);
        }

        private void buttonShowData_Click(object sender, EventArgs e)
        {
            if (comboBoxSrNum.SelectedItem != null)
            {
                for (int i = 0; i < chart1.Series.Count; i++)
                {
                    chart1.Series[i].Points.Clear();
                }
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                ShowDateWise();
            }
            else
            {
                MessageBox.Show("Please select a serial Number", "Invalid Serial Number", MessageBoxButtons.OK ,MessageBoxIcon.Information);
            }
        }

        private void radioButtonChartView_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewReport.Visible = false;
            chart1.Visible = true;
        }

        private void radioButtonGridView_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewReport.Visible = true;
            chart1.Visible = false;
        }
       

        private void comboBoxSrNum_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBoxSrNum.Items.Count > 0)
            {
                comboBoxSrNum.Items.Clear();
            }

            UpdateList();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateLog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(CommonParameters.projectDirectory);
        }

        private void ReportView_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonParameters.InspectionPage.reportView = null;
        }
    }
}
