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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(40, 114);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.MarkerSize = 10;
            series1.Name = "Line Marks";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.MarkerSize = 15;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series2.Name = "Wrinkle";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.Legend = "Legend1";
            series3.MarkerSize = 15;
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.Name = "Hole/Cut";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.Legend = "Legend1";
            series4.MarkerSize = 15;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Triangle;
            series4.Name = "Other";
            series4.YValuesPerPoint = 2;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(1489, 850);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // comboBoxSrNum
            // 
            this.comboBoxSrNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSrNum.FormattingEnabled = true;
            this.comboBoxSrNum.Location = new System.Drawing.Point(1742, 180);
            this.comboBoxSrNum.Name = "comboBoxSrNum";
            this.comboBoxSrNum.Size = new System.Drawing.Size(125, 21);
            this.comboBoxSrNum.TabIndex = 6;
            this.comboBoxSrNum.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxSrNum_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1628, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Select SrNum";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1628, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(1744, 142);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 20);
            this.dateTimePicker1.TabIndex = 8;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1629, 389);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Size = new System.Drawing.Size(268, 128);
            this.dataGridView1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1625, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total Defects:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // labeltotalDef
            // 
            this.labeltotalDef.AutoSize = true;
            this.labeltotalDef.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeltotalDef.Location = new System.Drawing.Point(1776, 290);
            this.labeltotalDef.Name = "labeltotalDef";
            this.labeltotalDef.Size = new System.Drawing.Size(36, 25);
            this.labeltotalDef.TabIndex = 10;
            this.labeltotalDef.Text = "14";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1624, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Defect Details";
            // 
            // buttonShowData
            // 
            this.buttonShowData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowData.Location = new System.Drawing.Point(1742, 225);
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
            this.radioButtonChartView.Location = new System.Drawing.Point(1636, 227);
            this.radioButtonChartView.Name = "radioButtonChartView";
            this.radioButtonChartView.Size = new System.Drawing.Size(76, 17);
            this.radioButtonChartView.TabIndex = 12;
            this.radioButtonChartView.TabStop = true;
            this.radioButtonChartView.Text = "Chart View";
            this.radioButtonChartView.UseVisualStyleBackColor = true;
            this.radioButtonChartView.CheckedChanged += new System.EventHandler(this.radioButtonChartView_CheckedChanged);
            // 
            // radioButtonGridView
            // 
            this.radioButtonGridView.AutoSize = true;
            this.radioButtonGridView.Location = new System.Drawing.Point(1636, 263);
            this.radioButtonGridView.Name = "radioButtonGridView";
            this.radioButtonGridView.Size = new System.Drawing.Size(70, 17);
            this.radioButtonGridView.TabIndex = 13;
            this.radioButtonGridView.TabStop = true;
            this.radioButtonGridView.Text = "Grid View";
            this.radioButtonGridView.UseVisualStyleBackColor = true;
            this.radioButtonGridView.CheckedChanged += new System.EventHandler(this.radioButtonGridView_CheckedChanged);
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Location = new System.Drawing.Point(23, 94);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.Size = new System.Drawing.Size(754, 884);
            this.dataGridViewReport.TabIndex = 14;
            this.dataGridViewReport.Visible = false;
            // 
            // comboBoxStartLen
            // 
            this.comboBoxStartLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStartLen.FormattingEnabled = true;
            this.comboBoxStartLen.Location = new System.Drawing.Point(1734, 61);
            this.comboBoxStartLen.Name = "comboBoxStartLen";
            this.comboBoxStartLen.Size = new System.Drawing.Size(125, 21);
            this.comboBoxStartLen.TabIndex = 6;
            this.comboBoxStartLen.Visible = false;
            this.comboBoxStartLen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxSrNum_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1620, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Start Length";
            this.label6.Visible = false;
            // 
            // comboBoxEndLen
            // 
            this.comboBoxEndLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEndLen.FormattingEnabled = true;
            this.comboBoxEndLen.Location = new System.Drawing.Point(1734, 98);
            this.comboBoxEndLen.Name = "comboBoxEndLen";
            this.comboBoxEndLen.Size = new System.Drawing.Size(125, 21);
            this.comboBoxEndLen.TabIndex = 6;
            this.comboBoxEndLen.Visible = false;
            this.comboBoxEndLen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxSrNum_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1620, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "End Length:";
            this.label7.Visible = false;
            // 
            // pictureBoxDefImage
            // 
            this.pictureBoxDefImage.Location = new System.Drawing.Point(1650, 583);
            this.pictureBoxDefImage.Name = "pictureBoxDefImage";
            this.pictureBoxDefImage.Size = new System.Drawing.Size(247, 203);
            this.pictureBoxDefImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDefImage.TabIndex = 15;
            this.pictureBoxDefImage.TabStop = false;
            // 
            // ReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1061);
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
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxEndLen);
            this.Controls.Add(this.comboBoxStartLen);
            this.Controls.Add(this.comboBoxSrNum);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridViewReport);
            this.Name = "ReportView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportView";
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
    }
}