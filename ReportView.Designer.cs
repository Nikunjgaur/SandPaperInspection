namespace SandPaperInspection
{
    partial class ReportView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBoxSrNum = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.labeltotalDef = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonShowData = new System.Windows.Forms.Button();
            this.radioButtonChartView = new System.Windows.Forms.RadioButton();
            this.radioButtonGridView = new System.Windows.Forms.RadioButton();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.comboBoxStartLen = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxEndLen = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBoxDefImage = new System.Windows.Forms.PictureBox();
            this.comboBoxDefect = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxBatch = new System.Windows.Forms.ComboBox();
            this.comboBoxRollNum = new System.Windows.Forms.ComboBox();
            this.comboBoxOperation = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelL2M = new System.Windows.Forms.Label();
            this.buttonViewImg = new System.Windows.Forms.Button();
            this.timerLiveChart = new System.Windows.Forms.Timer(this.components);
            this.checkBoxLiveChart = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDefImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(829, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Report View";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1924, 55);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // chart1
            // 
            chartArea2.AxisX.LabelAutoFitMaxFontSize = 16;
            chartArea2.AxisX.LabelAutoFitMinFontSize = 16;
            chartArea2.AxisX.Title = "Width in Mili Meters";
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 25.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisX2.LabelAutoFitMaxFontSize = 16;
            chartArea2.AxisX2.LabelAutoFitMinFontSize = 16;
            chartArea2.AxisY.LabelAutoFitMaxFontSize = 16;
            chartArea2.AxisY.LabelAutoFitMinFontSize = 16;
            chartArea2.AxisY.Title = "Length in Mili Meters";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 25.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY2.LabelAutoFitMaxFontSize = 16;
            chartArea2.AxisY2.LabelAutoFitMinFontSize = 16;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(40, 114);
            this.chart1.Name = "chart1";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series7.Legend = "Legend1";
            series7.MarkerSize = 20;
            series7.Name = "Line Marks";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series8.Legend = "Legend1";
            series8.MarkerSize = 20;
            series8.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series8.Name = "Wrinkle";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series9.Legend = "Legend1";
            series9.MarkerSize = 20;
            series9.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series9.Name = "Hole/Cut";
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series10.Legend = "Legend1";
            series10.MarkerSize = 20;
            series10.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star4;
            series10.Name = "Tape";
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series11.Legend = "Legend1";
            series11.MarkerSize = 20;
            series11.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Triangle;
            series11.Name = "Other";
            series11.YValuesPerPoint = 2;
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series12.Legend = "Legend1";
            series12.MarkerSize = 20;
            series12.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star6;
            series12.Name = "Nozzle Marks";
            this.chart1.Series.Add(series7);
            this.chart1.Series.Add(series8);
            this.chart1.Series.Add(series9);
            this.chart1.Series.Add(series10);
            this.chart1.Series.Add(series11);
            this.chart1.Series.Add(series12);
            this.chart1.Size = new System.Drawing.Size(1489, 850);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // comboBoxSrNum
            // 
            this.comboBoxSrNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSrNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSrNum.FormattingEnabled = true;
            this.comboBoxSrNum.Location = new System.Drawing.Point(1735, 118);
            this.comboBoxSrNum.Name = "comboBoxSrNum";
            this.comboBoxSrNum.Size = new System.Drawing.Size(159, 33);
            this.comboBoxSrNum.TabIndex = 6;
            this.comboBoxSrNum.SelectedIndexChanged += new System.EventHandler(this.comboBoxSrNum_SelectedIndexChanged);
            this.comboBoxSrNum.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxSrNum_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1558, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Select Finish";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1558, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(1735, 72);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(160, 31);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1565, 547);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 34;
            this.dataGridView1.Size = new System.Drawing.Size(332, 158);
            this.dataGridView1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1565, 470);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 31);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total Defects:";
            // 
            // labeltotalDef
            // 
            this.labeltotalDef.AutoSize = true;
            this.labeltotalDef.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeltotalDef.Location = new System.Drawing.Point(1748, 470);
            this.labeltotalDef.Name = "labeltotalDef";
            this.labeltotalDef.Size = new System.Drawing.Size(44, 31);
            this.labeltotalDef.TabIndex = 10;
            this.labeltotalDef.Text = "14";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1565, 506);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 31);
            this.label5.TabIndex = 10;
            this.label5.Text = "Defect Details";
            // 
            // buttonShowData
            // 
            this.buttonShowData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowData.Location = new System.Drawing.Point(1735, 362);
            this.buttonShowData.Name = "buttonShowData";
            this.buttonShowData.Size = new System.Drawing.Size(117, 44);
            this.buttonShowData.TabIndex = 11;
            this.buttonShowData.Text = "Show Data";
            this.buttonShowData.UseVisualStyleBackColor = true;
            this.buttonShowData.Click += new System.EventHandler(this.buttonShowData_Click);
            // 
            // radioButtonChartView
            // 
            this.radioButtonChartView.AutoSize = true;
            this.radioButtonChartView.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonChartView.Location = new System.Drawing.Point(1571, 363);
            this.radioButtonChartView.Name = "radioButtonChartView";
            this.radioButtonChartView.Size = new System.Drawing.Size(134, 29);
            this.radioButtonChartView.TabIndex = 12;
            this.radioButtonChartView.TabStop = true;
            this.radioButtonChartView.Text = "Chart View";
            this.radioButtonChartView.UseVisualStyleBackColor = true;
            this.radioButtonChartView.CheckedChanged += new System.EventHandler(this.radioButtonChartView_CheckedChanged);
            // 
            // radioButtonGridView
            // 
            this.radioButtonGridView.AutoSize = true;
            this.radioButtonGridView.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonGridView.Location = new System.Drawing.Point(1571, 399);
            this.radioButtonGridView.Name = "radioButtonGridView";
            this.radioButtonGridView.Size = new System.Drawing.Size(122, 29);
            this.radioButtonGridView.TabIndex = 13;
            this.radioButtonGridView.TabStop = true;
            this.radioButtonGridView.Text = "Grid View";
            this.radioButtonGridView.UseVisualStyleBackColor = true;
            this.radioButtonGridView.CheckedChanged += new System.EventHandler(this.radioButtonGridView_CheckedChanged);
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Location = new System.Drawing.Point(23, 107);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.Size = new System.Drawing.Size(754, 884);
            this.dataGridViewReport.TabIndex = 14;
            this.dataGridViewReport.Visible = false;
            // 
            // comboBoxStartLen
            // 
            this.comboBoxStartLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartLen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStartLen.FormattingEnabled = true;
            this.comboBoxStartLen.Location = new System.Drawing.Point(989, 75);
            this.comboBoxStartLen.Name = "comboBoxStartLen";
            this.comboBoxStartLen.Size = new System.Drawing.Size(151, 33);
            this.comboBoxStartLen.TabIndex = 6;
            this.comboBoxStartLen.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(825, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Start Length";
            this.label6.Visible = false;
            // 
            // comboBoxEndLen
            // 
            this.comboBoxEndLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEndLen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEndLen.FormattingEnabled = true;
            this.comboBoxEndLen.Location = new System.Drawing.Point(1351, 75);
            this.comboBoxEndLen.Name = "comboBoxEndLen";
            this.comboBoxEndLen.Size = new System.Drawing.Size(151, 33);
            this.comboBoxEndLen.TabIndex = 6;
            this.comboBoxEndLen.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1187, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 25);
            this.label7.TabIndex = 7;
            this.label7.Text = "End Length:";
            this.label7.Visible = false;
            // 
            // pictureBoxDefImage
            // 
            this.pictureBoxDefImage.Location = new System.Drawing.Point(1565, 730);
            this.pictureBoxDefImage.Name = "pictureBoxDefImage";
            this.pictureBoxDefImage.Size = new System.Drawing.Size(320, 225);
            this.pictureBoxDefImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDefImage.TabIndex = 15;
            this.pictureBoxDefImage.TabStop = false;
            // 
            // comboBoxDefect
            // 
            this.comboBoxDefect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDefect.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDefect.FormattingEnabled = true;
            this.comboBoxDefect.Location = new System.Drawing.Point(1735, 165);
            this.comboBoxDefect.Name = "comboBoxDefect";
            this.comboBoxDefect.Size = new System.Drawing.Size(159, 33);
            this.comboBoxDefect.TabIndex = 6;
            this.comboBoxDefect.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefect_SelectedIndexChanged_1);
            this.comboBoxDefect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxDefect_MouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1558, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 25);
            this.label8.TabIndex = 7;
            this.label8.Text = "Select Defect";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1560, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 25);
            this.label9.TabIndex = 7;
            this.label9.Text = "Select Batch";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1558, 269);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 25);
            this.label10.TabIndex = 7;
            this.label10.Text = "Select Roll Num";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1552, 315);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(172, 25);
            this.label11.TabIndex = 7;
            this.label11.Text = "Select Operation";
            // 
            // comboBoxBatch
            // 
            this.comboBoxBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBatch.FormattingEnabled = true;
            this.comboBoxBatch.Location = new System.Drawing.Point(1734, 221);
            this.comboBoxBatch.Name = "comboBoxBatch";
            this.comboBoxBatch.Size = new System.Drawing.Size(158, 33);
            this.comboBoxBatch.TabIndex = 17;
            // 
            // comboBoxRollNum
            // 
            this.comboBoxRollNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxRollNum.FormattingEnabled = true;
            this.comboBoxRollNum.Location = new System.Drawing.Point(1734, 265);
            this.comboBoxRollNum.Name = "comboBoxRollNum";
            this.comboBoxRollNum.Size = new System.Drawing.Size(159, 33);
            this.comboBoxRollNum.TabIndex = 18;
            // 
            // comboBoxOperation
            // 
            this.comboBoxOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxOperation.FormattingEnabled = true;
            this.comboBoxOperation.Location = new System.Drawing.Point(1734, 312);
            this.comboBoxOperation.Name = "comboBoxOperation";
            this.comboBoxOperation.Size = new System.Drawing.Size(160, 33);
            this.comboBoxOperation.TabIndex = 19;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(26, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(384, 33);
            this.label12.TabIndex = 20;
            this.label12.Text = "Length converted to Meters :";
            // 
            // labelL2M
            // 
            this.labelL2M.AutoSize = true;
            this.labelL2M.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelL2M.Location = new System.Drawing.Point(428, 65);
            this.labelL2M.Name = "labelL2M";
            this.labelL2M.Size = new System.Drawing.Size(45, 33);
            this.labelL2M.TabIndex = 21;
            this.labelL2M.Text = "---";
            // 
            // buttonViewImg
            // 
            this.buttonViewImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonViewImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonViewImg.Location = new System.Drawing.Point(1626, 961);
            this.buttonViewImg.Name = "buttonViewImg";
            this.buttonViewImg.Size = new System.Drawing.Size(196, 41);
            this.buttonViewImg.TabIndex = 22;
            this.buttonViewImg.Text = "View HD image";
            this.buttonViewImg.UseVisualStyleBackColor = true;
            this.buttonViewImg.Click += new System.EventHandler(this.buttonViewImg_Click);
            // 
            // timerLiveChart
            // 
            this.timerLiveChart.Enabled = true;
            this.timerLiveChart.Interval = 5000;
            this.timerLiveChart.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBoxLiveChart
            // 
            this.checkBoxLiveChart.AutoSize = true;
            this.checkBoxLiveChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxLiveChart.Location = new System.Drawing.Point(1571, 435);
            this.checkBoxLiveChart.Name = "checkBoxLiveChart";
            this.checkBoxLiveChart.Size = new System.Drawing.Size(112, 28);
            this.checkBoxLiveChart.TabIndex = 23;
            this.checkBoxLiveChart.Text = "Live Chart";
            this.checkBoxLiveChart.UseVisualStyleBackColor = true;
            this.checkBoxLiveChart.CheckedChanged += new System.EventHandler(this.checkBoxLiveChart_CheckedChanged);
            // 
            // ReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.checkBoxLiveChart);
            this.Controls.Add(this.buttonViewImg);
            this.Controls.Add(this.labelL2M);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBoxOperation);
            this.Controls.Add(this.comboBoxRollNum);
            this.Controls.Add(this.comboBoxBatch);
            this.Controls.Add(this.pictureBoxDefImage);
            this.Controls.Add(this.radioButtonGridView);
            this.Controls.Add(this.radioButtonChartView);
            this.Controls.Add(this.buttonShowData);
            this.Controls.Add(this.labeltotalDef);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxEndLen);
            this.Controls.Add(this.comboBoxStartLen);
            this.Controls.Add(this.comboBoxDefect);
            this.Controls.Add(this.comboBoxSrNum);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridViewReport);
            this.Name = "ReportView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportView_FormClosing);
            this.Load += new System.EventHandler(this.ReportView_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDefImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ComboBox comboBoxSrNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labeltotalDef;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonShowData;
        private System.Windows.Forms.RadioButton radioButtonChartView;
        private System.Windows.Forms.RadioButton radioButtonGridView;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.PictureBox pictureBoxDefImage;
        private System.Windows.Forms.ComboBox comboBoxStartLen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxEndLen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxDefect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxBatch;
        private System.Windows.Forms.ComboBox comboBoxRollNum;
        private System.Windows.Forms.ComboBox comboBoxOperation;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelL2M;
        private System.Windows.Forms.Button buttonViewImg;
        private System.Windows.Forms.CheckBox checkBoxLiveChart;
        private System.Windows.Forms.Timer timerLiveChart;
    }
}