namespace SandPaperInspection
{
    partial class InspectionPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InspectionPage));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSetThresholds = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelDefCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxLog = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelLength = new System.Windows.Forms.Label();
            this.labelJumboLength = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelJumboWidth = new System.Windows.Forms.Label();
            this.lblRun = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnCameraSettings = new System.Windows.Forms.Button();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOneShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonContinuousShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.btnIOmonitor = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRollMinus = new System.Windows.Forms.Button();
            this.buttonRollPlus = new System.Windows.Forms.Button();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.comboBoxFinish = new System.Windows.Forms.ComboBox();
            this.comboBoxOperation = new System.Windows.Forms.ComboBox();
            this.textBoxBatchNum = new System.Windows.Forms.TextBox();
            this.textBoxRollNum = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonReport = new System.Windows.Forms.Button();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.clock = new System.Windows.Forms.Timer(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.instantDiCtrl1 = new Automation.BDaq.InstantDiCtrl(this.components);
            this.instantDoCtrl1 = new Automation.BDaq.InstantDoCtrl(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.labelSaveImage = new System.Windows.Forms.Label();
            this.timerSpeed = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Red;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(146, 86);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(125, 44);
            this.btnStop.TabIndex = 83;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSetThresholds
            // 
            this.btnSetThresholds.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSetThresholds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetThresholds.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetThresholds.ForeColor = System.Drawing.Color.White;
            this.btnSetThresholds.Location = new System.Drawing.Point(274, 86);
            this.btnSetThresholds.Name = "btnSetThresholds";
            this.btnSetThresholds.Size = new System.Drawing.Size(245, 44);
            this.btnSetThresholds.TabIndex = 67;
            this.btnSetThresholds.Text = "Set Thresholds";
            this.btnSetThresholds.UseVisualStyleBackColor = false;
            this.btnSetThresholds.Click += new System.EventHandler(this.btnSetThresholds_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1637, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(215, 24);
            this.label16.TabIndex = 74;
            this.label16.Text = "Add Innovations Pvt. Ltd.";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox2.Controls.Add(this.labelDefCount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.checkBoxLog);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.labelLength);
            this.groupBox2.Controls.Add(this.labelJumboLength);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.labelJumboWidth);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(14, 358);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(497, 353);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameters ";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // labelDefCount
            // 
            this.labelDefCount.AutoSize = true;
            this.labelDefCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDefCount.Location = new System.Drawing.Point(357, 47);
            this.labelDefCount.Name = "labelDefCount";
            this.labelDefCount.Size = new System.Drawing.Size(54, 42);
            this.labelDefCount.TabIndex = 90;
            this.labelDefCount.Text = "---";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 42);
            this.label1.TabIndex = 33;
            this.label1.Text = "Defect Count:";
            // 
            // checkBoxLog
            // 
            this.checkBoxLog.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxLog.Checked = true;
            this.checkBoxLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxLog.Location = new System.Drawing.Point(11, 224);
            this.checkBoxLog.Name = "checkBoxLog";
            this.checkBoxLog.Size = new System.Drawing.Size(301, 49);
            this.checkBoxLog.TabIndex = 89;
            this.checkBoxLog.Text = "Update Log";
            this.checkBoxLog.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(349, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2, 290);
            this.panel3.TabIndex = 88;
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLength.ForeColor = System.Drawing.Color.White;
            this.labelLength.Location = new System.Drawing.Point(357, 102);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(60, 42);
            this.labelLength.TabIndex = 50;
            this.labelLength.Text = "00";
            // 
            // labelJumboLength
            // 
            this.labelJumboLength.AutoSize = true;
            this.labelJumboLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJumboLength.ForeColor = System.Drawing.Color.White;
            this.labelJumboLength.Location = new System.Drawing.Point(6, 102);
            this.labelJumboLength.Name = "labelJumboLength";
            this.labelJumboLength.Size = new System.Drawing.Size(331, 42);
            this.labelJumboLength.TabIndex = 51;
            this.labelJumboLength.Text = "Jumbo Length(mtr)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(9, 165);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(339, 42);
            this.label11.TabIndex = 43;
            this.label11.Text = "Jumbo Width (mm):";
            // 
            // labelJumboWidth
            // 
            this.labelJumboWidth.AutoSize = true;
            this.labelJumboWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJumboWidth.ForeColor = System.Drawing.Color.Chartreuse;
            this.labelJumboWidth.Location = new System.Drawing.Point(357, 165);
            this.labelJumboWidth.Name = "labelJumboWidth";
            this.labelJumboWidth.Size = new System.Drawing.Size(60, 42);
            this.labelJumboWidth.TabIndex = 44;
            this.labelJumboWidth.Text = "00";
            // 
            // lblRun
            // 
            this.lblRun.AutoSize = true;
            this.lblRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRun.ForeColor = System.Drawing.Color.White;
            this.lblRun.Location = new System.Drawing.Point(389, 991);
            this.lblRun.Name = "lblRun";
            this.lblRun.Size = new System.Drawing.Size(60, 42);
            this.lblRun.TabIndex = 17;
            this.lblRun.Text = "00";
            this.lblRun.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(43, 991);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(288, 42);
            this.label14.TabIndex = 49;
            this.label14.Text = "Defect Location:";
            this.label14.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1741, 7);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 20);
            this.label15.TabIndex = 72;
            this.label15.Text = "Developed By:";
            // 
            // btnCameraSettings
            // 
            this.btnCameraSettings.BackColor = System.Drawing.Color.Orange;
            this.btnCameraSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCameraSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCameraSettings.Location = new System.Drawing.Point(172, 144);
            this.btnCameraSettings.Name = "btnCameraSettings";
            this.btnCameraSettings.Size = new System.Drawing.Size(237, 44);
            this.btnCameraSettings.TabIndex = 69;
            this.btnCameraSettings.Text = "Camera Settings";
            this.btnCameraSettings.UseVisualStyleBackColor = false;
            this.btnCameraSettings.Click += new System.EventHandler(this.btnCameraSettings_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOneShot,
            this.toolStripButtonContinuousShot,
            this.toolStripButtonStop});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1904, 39);
            this.toolStrip.TabIndex = 58;
            this.toolStrip.Text = "toolStrip";
            this.toolStrip.Visible = false;
            // 
            // toolStripButtonOneShot
            // 
            this.toolStripButtonOneShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOneShot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOneShot.Image")));
            this.toolStripButtonOneShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOneShot.Name = "toolStripButtonOneShot";
            this.toolStripButtonOneShot.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonOneShot.Text = "One Shot";
            this.toolStripButtonOneShot.ToolTipText = "One Shot";
            this.toolStripButtonOneShot.Visible = false;
            // 
            // toolStripButtonContinuousShot
            // 
            this.toolStripButtonContinuousShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonContinuousShot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonContinuousShot.Image")));
            this.toolStripButtonContinuousShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonContinuousShot.Name = "toolStripButtonContinuousShot";
            this.toolStripButtonContinuousShot.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonContinuousShot.Text = "Continuous Shot";
            this.toolStripButtonContinuousShot.ToolTipText = "Continuous Shot";
            this.toolStripButtonContinuousShot.Visible = false;
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Enabled = false;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonStop.Text = "Stop Grab";
            this.toolStripButtonStop.ToolTipText = "Stop Grab";
            this.toolStripButtonStop.Visible = false;
            // 
            // btnIOmonitor
            // 
            this.btnIOmonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnIOmonitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIOmonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIOmonitor.ForeColor = System.Drawing.Color.White;
            this.btnIOmonitor.Location = new System.Drawing.Point(7, 144);
            this.btnIOmonitor.Name = "btnIOmonitor";
            this.btnIOmonitor.Size = new System.Drawing.Size(162, 44);
            this.btnIOmonitor.TabIndex = 76;
            this.btnIOmonitor.Text = "I/O Monitor";
            this.btnIOmonitor.UseVisualStyleBackColor = false;
            this.btnIOmonitor.Click += new System.EventHandler(this.btnIOmonitor_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Green;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(7, 86);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(133, 44);
            this.btnStart.TabIndex = 61;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1858, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(60, 60);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 59;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.White;
            this.pictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox5.Location = new System.Drawing.Point(543, 64);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(1367, 579);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 60;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(6, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 737);
            this.panel1.TabIndex = 85;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.buttonRollMinus);
            this.groupBox1.Controls.Add(this.buttonRollPlus);
            this.groupBox1.Controls.Add(this.buttonEnter);
            this.groupBox1.Controls.Add(this.comboBoxFinish);
            this.groupBox1.Controls.Add(this.comboBoxOperation);
            this.groupBox1.Controls.Add(this.textBoxBatchNum);
            this.groupBox1.Controls.Add(this.textBoxRollNum);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(14, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 327);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Model Data ";
            // 
            // buttonRollMinus
            // 
            this.buttonRollMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRollMinus.ForeColor = System.Drawing.Color.Black;
            this.buttonRollMinus.Location = new System.Drawing.Point(235, 274);
            this.buttonRollMinus.Name = "buttonRollMinus";
            this.buttonRollMinus.Size = new System.Drawing.Size(61, 40);
            this.buttonRollMinus.TabIndex = 95;
            this.buttonRollMinus.Text = "-";
            this.buttonRollMinus.UseVisualStyleBackColor = true;
            this.buttonRollMinus.Click += new System.EventHandler(this.buttonRollMinus_Click);
            // 
            // buttonRollPlus
            // 
            this.buttonRollPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRollPlus.ForeColor = System.Drawing.Color.Black;
            this.buttonRollPlus.Location = new System.Drawing.Point(418, 274);
            this.buttonRollPlus.Name = "buttonRollPlus";
            this.buttonRollPlus.Size = new System.Drawing.Size(60, 40);
            this.buttonRollPlus.TabIndex = 94;
            this.buttonRollPlus.Text = "+";
            this.buttonRollPlus.UseVisualStyleBackColor = true;
            this.buttonRollPlus.Click += new System.EventHandler(this.buttonRollPlus_Click);
            // 
            // buttonEnter
            // 
            this.buttonEnter.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnter.ForeColor = System.Drawing.Color.Black;
            this.buttonEnter.Location = new System.Drawing.Point(305, 273);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(105, 40);
            this.buttonEnter.TabIndex = 93;
            this.buttonEnter.Text = "Enter";
            this.buttonEnter.UseVisualStyleBackColor = false;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // comboBoxFinish
            // 
            this.comboBoxFinish.Enabled = false;
            this.comboBoxFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFinish.FormattingEnabled = true;
            this.comboBoxFinish.Items.AddRange(new object[] {
            "DIP FILL",
            "BACK FILL",
            "FRONT FILL",
            "WET ON WET"});
            this.comboBoxFinish.Location = new System.Drawing.Point(227, 24);
            this.comboBoxFinish.Name = "comboBoxFinish";
            this.comboBoxFinish.Size = new System.Drawing.Size(252, 50);
            this.comboBoxFinish.TabIndex = 92;
            // 
            // comboBoxOperation
            // 
            this.comboBoxOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxOperation.FormattingEnabled = true;
            this.comboBoxOperation.Items.AddRange(new object[] {
            "DIP FILL",
            "BACK FILL",
            "FRONT FILL",
            "WET ON WET"});
            this.comboBoxOperation.Location = new System.Drawing.Point(227, 81);
            this.comboBoxOperation.Name = "comboBoxOperation";
            this.comboBoxOperation.Size = new System.Drawing.Size(252, 50);
            this.comboBoxOperation.TabIndex = 92;
            // 
            // textBoxBatchNum
            // 
            this.textBoxBatchNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBatchNum.Location = new System.Drawing.Point(227, 143);
            this.textBoxBatchNum.Name = "textBoxBatchNum";
            this.textBoxBatchNum.Size = new System.Drawing.Size(252, 49);
            this.textBoxBatchNum.TabIndex = 91;
            this.textBoxBatchNum.Text = "ABC123";
            // 
            // textBoxRollNum
            // 
            this.textBoxRollNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRollNum.Location = new System.Drawing.Point(227, 213);
            this.textBoxRollNum.Name = "textBoxRollNum";
            this.textBoxRollNum.Size = new System.Drawing.Size(252, 49);
            this.textBoxRollNum.TabIndex = 91;
            this.textBoxRollNum.Text = "1234";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(213, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2, 285);
            this.panel2.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 42);
            this.label3.TabIndex = 58;
            this.label3.Text = "Batch Num:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(6, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 42);
            this.label6.TabIndex = 58;
            this.label6.Text = "Operation:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 42);
            this.label4.TabIndex = 57;
            this.label4.Text = "Roll no:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 42);
            this.label2.TabIndex = 22;
            this.label2.Text = "Finish:";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpeed.Location = new System.Drawing.Point(242, 1002);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(54, 42);
            this.labelSpeed.TabIndex = 90;
            this.labelSpeed.Text = "---";
            this.labelSpeed.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(47, 1002);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 42);
            this.label7.TabIndex = 89;
            this.label7.Text = "Speed:";
            this.label7.Visible = false;
            // 
            // buttonReport
            // 
            this.buttonReport.BackColor = System.Drawing.Color.MediumPurple;
            this.buttonReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReport.ForeColor = System.Drawing.Color.White;
            this.buttonReport.Location = new System.Drawing.Point(415, 144);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(122, 44);
            this.buttonReport.TabIndex = 86;
            this.buttonReport.Text = "Report";
            this.buttonReport.UseVisualStyleBackColor = false;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // labelDateTime
            // 
            this.labelDateTime.AutoSize = true;
            this.labelDateTime.BackColor = System.Drawing.Color.Transparent;
            this.labelDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateTime.Location = new System.Drawing.Point(19, 969);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(150, 33);
            this.labelDateTime.TabIndex = 86;
            this.labelDateTime.Text = "Date/Time";
            this.labelDateTime.Visible = false;
            this.labelDateTime.Click += new System.EventHandler(this.labelDateTime_Click);
            // 
            // clock
            // 
            this.clock.Interval = 1000;
            this.clock.Tick += new System.EventHandler(this.clock_Tick);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(1660, 656);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.Legend = "Legend1";
            series3.MarkerSize = 10;
            series3.Name = "Ok Percent";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.Legend = "Legend1";
            series4.MarkerSize = 15;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series4.Name = "Defect Percent";
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(251, 392);
            this.chart1.TabIndex = 87;
            this.chart1.Text = "chart1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // instantDiCtrl1
            // 
            this.instantDiCtrl1._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("instantDiCtrl1._StateStream")));
            this.instantDiCtrl1.ChangeOfState += new System.EventHandler<Automation.BDaq.DiSnapEventArgs>(this.instantDiCtrl1_ChangeOfState);
            // 
            // instantDoCtrl1
            // 
            this.instantDoCtrl1._StateStream = ((Automation.BDaq.DeviceStateStreamer)(resources.GetObject("instantDoCtrl1._StateStream")));
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(629, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 49);
            this.button1.TabIndex = 88;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelSaveImage
            // 
            this.labelSaveImage.AutoSize = true;
            this.labelSaveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSaveImage.ForeColor = System.Drawing.Color.Red;
            this.labelSaveImage.Location = new System.Drawing.Point(19, 937);
            this.labelSaveImage.Name = "labelSaveImage";
            this.labelSaveImage.Size = new System.Drawing.Size(190, 25);
            this.labelSaveImage.TabIndex = 89;
            this.labelSaveImage.Text = "Save Images: OFF";
            // 
            // timerSpeed
            // 
            this.timerSpeed.Interval = 30000;
            this.timerSpeed.Tick += new System.EventHandler(this.timerSpeed_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(315, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 32);
            this.button2.TabIndex = 90;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Location = new System.Drawing.Point(553, 656);
            this.dataGridViewReport.Name = "dataGridViewReport";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewReport.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewReport.RowTemplate.Height = 44;
            this.dataGridViewReport.Size = new System.Drawing.Size(1357, 392);
            this.dataGridViewReport.TabIndex = 91;
            // 
            // InspectionPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1924, 1061);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.dataGridViewReport);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelSaveImage);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblRun);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.labelDateTime);
            this.Controls.Add(this.btnSetThresholds);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnCameraSettings);
            this.Controls.Add(this.btnIOmonitor);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip);
            this.Name = "InspectionPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inspection Page";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InspectionPage_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InspectionPage_FormClosed);
            this.Load += new System.EventHandler(this.InspectionPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSetThresholds;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblRun;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelJumboWidth;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnCameraSettings;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonOneShot;
        private System.Windows.Forms.ToolStripButton toolStripButtonContinuousShot;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.Button btnIOmonitor;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Label labelDateTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer clock;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.Label labelJumboLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Automation.BDaq.InstantDiCtrl instantDiCtrl1;
        private Automation.BDaq.InstantDoCtrl instantDoCtrl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelSaveImage;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timerSpeed;
        private System.Windows.Forms.Label labelDefCount;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.CheckBox checkBoxLog;
        private System.Windows.Forms.ComboBox comboBoxOperation;
        private System.Windows.Forms.TextBox textBoxRollNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBatchNum;
        private System.Windows.Forms.ComboBox comboBoxFinish;
        private System.Windows.Forms.Button buttonRollMinus;
        private System.Windows.Forms.Button buttonRollPlus;
        private System.Windows.Forms.Button buttonEnter;
    }
}

