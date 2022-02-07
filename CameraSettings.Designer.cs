namespace SandPaperInspection
{
    partial class CameraSettings
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
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxStatic_Live = new System.Windows.Forms.CheckBox();
            this.trackBarLineRateCam1 = new System.Windows.Forms.TrackBar();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPostDiv = new System.Windows.Forms.TextBox();
            this.textBoxMul = new System.Windows.Forms.TextBox();
            this.textBoxPreDiv = new System.Windows.Forms.TextBox();
            this.txtPostdivider = new System.Windows.Forms.Label();
            this.lblPredivider = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCalib = new System.Windows.Forms.TextBox();
            this.camIcon = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarOverlap = new System.Windows.Forms.TrackBar();
            this.trackBarLineRateCam2 = new System.Windows.Forms.TrackBar();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.textBoxLineRateCam1 = new System.Windows.Forms.TextBox();
            this.textBoxLineRateCam2 = new System.Windows.Forms.TextBox();
            this.textBoxOverlap = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineRateCam1)).BeginInit();
            this.lblPredivider.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camIcon)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOverlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineRateCam2)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 373);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 20);
            this.label8.TabIndex = 42;
            this.label8.Text = "C1C2 Overlap";
            // 
            // checkBoxStatic_Live
            // 
            this.checkBoxStatic_Live.AutoSize = true;
            this.checkBoxStatic_Live.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxStatic_Live.Location = new System.Drawing.Point(42, 461);
            this.checkBoxStatic_Live.Name = "checkBoxStatic_Live";
            this.checkBoxStatic_Live.Size = new System.Drawing.Size(155, 20);
            this.checkBoxStatic_Live.TabIndex = 41;
            this.checkBoxStatic_Live.Text = "Static Image / Live";
            this.checkBoxStatic_Live.UseVisualStyleBackColor = true;
            // 
            // trackBarLineRateCam1
            // 
            this.trackBarLineRateCam1.Location = new System.Drawing.Point(137, 279);
            this.trackBarLineRateCam1.Maximum = 5000;
            this.trackBarLineRateCam1.Name = "trackBarLineRateCam1";
            this.trackBarLineRateCam1.Size = new System.Drawing.Size(209, 45);
            this.trackBarLineRateCam1.TabIndex = 37;
            this.trackBarLineRateCam1.Value = 100;
            this.trackBarLineRateCam1.Scroll += new System.EventHandler(this.trackBarLineRateCam1_Scroll);
            // 
            // buttonReset
            // 
            this.buttonReset.BackColor = System.Drawing.Color.Black;
            this.buttonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.ForeColor = System.Drawing.Color.White;
            this.buttonReset.Location = new System.Drawing.Point(146, 487);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(120, 45);
            this.buttonReset.TabIndex = 35;
            this.buttonReset.Text = "Reset to Default";
            this.buttonReset.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 78);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 18);
            this.label5.TabIndex = 1;
            this.label5.Text = "Multiplier (1-32)";
            // 
            // textBoxPostDiv
            // 
            this.textBoxPostDiv.Location = new System.Drawing.Point(206, 112);
            this.textBoxPostDiv.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxPostDiv.MaxLength = 3;
            this.textBoxPostDiv.Name = "textBoxPostDiv";
            this.textBoxPostDiv.Size = new System.Drawing.Size(82, 24);
            this.textBoxPostDiv.TabIndex = 10;
            this.textBoxPostDiv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxMul
            // 
            this.textBoxMul.Location = new System.Drawing.Point(206, 76);
            this.textBoxMul.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxMul.MaxLength = 3;
            this.textBoxMul.Name = "textBoxMul";
            this.textBoxMul.Size = new System.Drawing.Size(82, 24);
            this.textBoxMul.TabIndex = 9;
            this.textBoxMul.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPreDiv
            // 
            this.textBoxPreDiv.Location = new System.Drawing.Point(206, 42);
            this.textBoxPreDiv.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxPreDiv.MaxLength = 3;
            this.textBoxPreDiv.Name = "textBoxPreDiv";
            this.textBoxPreDiv.Size = new System.Drawing.Size(82, 24);
            this.textBoxPreDiv.TabIndex = 8;
            this.textBoxPreDiv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPostdivider
            // 
            this.txtPostdivider.AutoSize = true;
            this.txtPostdivider.Location = new System.Drawing.Point(44, 112);
            this.txtPostdivider.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtPostdivider.Name = "txtPostdivider";
            this.txtPostdivider.Size = new System.Drawing.Size(159, 18);
            this.txtPostdivider.TabIndex = 2;
            this.txtPostdivider.Text = "Post Divider (1-128)";
            // 
            // lblPredivider
            // 
            this.lblPredivider.Controls.Add(this.textBoxPostDiv);
            this.lblPredivider.Controls.Add(this.textBoxMul);
            this.lblPredivider.Controls.Add(this.textBoxPreDiv);
            this.lblPredivider.Controls.Add(this.txtPostdivider);
            this.lblPredivider.Controls.Add(this.label5);
            this.lblPredivider.Controls.Add(this.label4);
            this.lblPredivider.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPredivider.Location = new System.Drawing.Point(33, 85);
            this.lblPredivider.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblPredivider.Name = "lblPredivider";
            this.lblPredivider.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.lblPredivider.Size = new System.Drawing.Size(341, 144);
            this.lblPredivider.TabIndex = 32;
            this.lblPredivider.TabStop = false;
            this.lblPredivider.Text = "Encoder Pulse Setting";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pre Divider (1-128)";
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Black;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(16, 487);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 45);
            this.buttonSave.TabIndex = 31;
            this.buttonSave.Text = "Save Changes";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(25, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 24);
            this.label2.TabIndex = 29;
            this.label2.Text = "Calibration (FOV in mm) :";
            // 
            // textBoxCalib
            // 
            this.textBoxCalib.Location = new System.Drawing.Point(274, 37);
            this.textBoxCalib.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxCalib.MaxLength = 3;
            this.textBoxCalib.Name = "textBoxCalib";
            this.textBoxCalib.Size = new System.Drawing.Size(100, 20);
            this.textBoxCalib.TabIndex = 30;
            this.textBoxCalib.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // camIcon
            // 
            this.camIcon.BackColor = System.Drawing.Color.Transparent;
            this.camIcon.Image = global::SandPaperInspection.Properties.Resources.icons8_camera_1002;
            this.camIcon.Location = new System.Drawing.Point(197, 3);
            this.camIcon.Name = "camIcon";
            this.camIcon.Size = new System.Drawing.Size(104, 102);
            this.camIcon.TabIndex = 44;
            this.camIcon.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.trackBarLineRateCam1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.trackBarOverlap);
            this.panel1.Controls.Add(this.trackBarLineRateCam2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.checkBoxStatic_Live);
            this.panel1.Controls.Add(this.buttonDefault);
            this.panel1.Controls.Add(this.buttonReset);
            this.panel1.Controls.Add(this.textBoxLineRateCam1);
            this.panel1.Controls.Add(this.textBoxLineRateCam2);
            this.panel1.Controls.Add(this.textBoxOverlap);
            this.panel1.Controls.Add(this.lblPredivider);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxCalib);
            this.panel1.Location = new System.Drawing.Point(44, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 545);
            this.panel1.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 332);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 18);
            this.label6.TabIndex = 61;
            this.label6.Text = "C2 Line Rate:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 286);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 18);
            this.label1.TabIndex = 60;
            this.label1.Text = "C1 Line Rate:";
            // 
            // trackBarOverlap
            // 
            this.trackBarOverlap.LargeChange = 4;
            this.trackBarOverlap.Location = new System.Drawing.Point(135, 370);
            this.trackBarOverlap.Maximum = 300;
            this.trackBarOverlap.Minimum = 4;
            this.trackBarOverlap.Name = "trackBarOverlap";
            this.trackBarOverlap.Size = new System.Drawing.Size(210, 45);
            this.trackBarOverlap.SmallChange = 4;
            this.trackBarOverlap.TabIndex = 59;
            this.trackBarOverlap.TickFrequency = 4;
            this.trackBarOverlap.Value = 4;
            this.trackBarOverlap.Scroll += new System.EventHandler(this.trackBarOverlap_Scroll);
            // 
            // trackBarLineRateCam2
            // 
            this.trackBarLineRateCam2.Location = new System.Drawing.Point(137, 322);
            this.trackBarLineRateCam2.Maximum = 5000;
            this.trackBarLineRateCam2.Minimum = 50;
            this.trackBarLineRateCam2.Name = "trackBarLineRateCam2";
            this.trackBarLineRateCam2.Size = new System.Drawing.Size(208, 45);
            this.trackBarLineRateCam2.TabIndex = 58;
            this.trackBarLineRateCam2.Value = 100;
            this.trackBarLineRateCam2.Scroll += new System.EventHandler(this.trackBarLineRateCam2_Scroll);
            // 
            // buttonDefault
            // 
            this.buttonDefault.BackColor = System.Drawing.Color.Black;
            this.buttonDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDefault.ForeColor = System.Drawing.Color.White;
            this.buttonDefault.Location = new System.Drawing.Point(274, 487);
            this.buttonDefault.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(120, 45);
            this.buttonDefault.TabIndex = 36;
            this.buttonDefault.Text = "Save as Default";
            this.buttonDefault.UseVisualStyleBackColor = false;
            // 
            // textBoxLineRateCam1
            // 
            this.textBoxLineRateCam1.Location = new System.Drawing.Point(350, 287);
            this.textBoxLineRateCam1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxLineRateCam1.MaxLength = 4;
            this.textBoxLineRateCam1.Name = "textBoxLineRateCam1";
            this.textBoxLineRateCam1.Size = new System.Drawing.Size(43, 20);
            this.textBoxLineRateCam1.TabIndex = 34;
            this.textBoxLineRateCam1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxLineRateCam2
            // 
            this.textBoxLineRateCam2.Location = new System.Drawing.Point(350, 330);
            this.textBoxLineRateCam2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxLineRateCam2.MaxLength = 4;
            this.textBoxLineRateCam2.Name = "textBoxLineRateCam2";
            this.textBoxLineRateCam2.Size = new System.Drawing.Size(43, 20);
            this.textBoxLineRateCam2.TabIndex = 34;
            this.textBoxLineRateCam2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxOverlap
            // 
            this.textBoxOverlap.Location = new System.Drawing.Point(350, 375);
            this.textBoxOverlap.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxOverlap.MaxLength = 4;
            this.textBoxOverlap.Name = "textBoxOverlap";
            this.textBoxOverlap.Size = new System.Drawing.Size(43, 20);
            this.textBoxOverlap.TabIndex = 34;
            this.textBoxOverlap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CameraSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::SandPaperInspection.Properties.Resources.blue_1273089_1280;
            this.ClientSize = new System.Drawing.Size(496, 688);
            this.Controls.Add(this.camIcon);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(512, 727);
            this.Name = "CameraSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Camera Parameter Setup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraSettings_FormClosing);
            this.Load += new System.EventHandler(this.CameraSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineRateCam1)).EndInit();
            this.lblPredivider.ResumeLayout(false);
            this.lblPredivider.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camIcon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOverlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineRateCam2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxStatic_Live;
        private System.Windows.Forms.TrackBar trackBarLineRateCam1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPostDiv;
        private System.Windows.Forms.TextBox textBoxMul;
        private System.Windows.Forms.TextBox textBoxPreDiv;
        private System.Windows.Forms.Label txtPostdivider;
        private System.Windows.Forms.GroupBox lblPredivider;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCalib;
        private System.Windows.Forms.PictureBox camIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarOverlap;
        private System.Windows.Forms.TrackBar trackBarLineRateCam2;
        private System.Windows.Forms.TextBox textBoxOverlap;
        private System.Windows.Forms.TextBox textBoxLineRateCam1;
        private System.Windows.Forms.TextBox textBoxLineRateCam2;
    }
}