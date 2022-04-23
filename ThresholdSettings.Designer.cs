namespace SandPaperInspection
{
    partial class ThresholdSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThresholdSettings));
            this.btmThreshold = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.trkBrTh2 = new System.Windows.Forms.TrackBar();
            this.trkBrTh1 = new System.Windows.Forms.TrackBar();
            this.txtth2 = new System.Windows.Forms.TextBox();
            this.txtTH1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCamExposureTC2 = new System.Windows.Forms.TrackBar();
            this.txtExpC2 = new System.Windows.Forms.TextBox();
            this.txtCamExposureT = new System.Windows.Forms.TrackBar();
            this.txtExposer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxSaveImages = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrTh2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrTh1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCamExposureTC2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCamExposureT)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btmThreshold
            // 
            this.btmThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btmThreshold.Location = new System.Drawing.Point(391, 400);
            this.btmThreshold.Name = "btmThreshold";
            this.btmThreshold.Size = new System.Drawing.Size(129, 44);
            this.btmThreshold.TabIndex = 43;
            this.btmThreshold.Text = "SET";
            this.btmThreshold.UseVisualStyleBackColor = true;
            this.btmThreshold.Click += new System.EventHandler(this.btmThreshold_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.trkBrTh2);
            this.groupBox1.Controls.Add(this.txtCamExposureTC2);
            this.groupBox1.Controls.Add(this.trkBrTh1);
            this.groupBox1.Controls.Add(this.txtExpC2);
            this.groupBox1.Controls.Add(this.txtth2);
            this.groupBox1.Controls.Add(this.txtCamExposureT);
            this.groupBox1.Controls.Add(this.txtTH1);
            this.groupBox1.Controls.Add(this.txtExposer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(22, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 348);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Threshold Value";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 31);
            this.label9.TabIndex = 26;
            this.label9.Text = "Block Size:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 31);
            this.label8.TabIndex = 25;
            this.label8.Text = "Defect Size:";
            // 
            // trkBrTh2
            // 
            this.trkBrTh2.Location = new System.Drawing.Point(184, 106);
            this.trkBrTh2.Maximum = 551;
            this.trkBrTh2.Minimum = 51;
            this.trkBrTh2.Name = "trkBrTh2";
            this.trkBrTh2.Size = new System.Drawing.Size(183, 45);
            this.trkBrTh2.TabIndex = 21;
            this.trkBrTh2.Value = 51;
            this.trkBrTh2.Scroll += new System.EventHandler(this.trkBrTh2_Scroll);
            // 
            // trkBrTh1
            // 
            this.trkBrTh1.Location = new System.Drawing.Point(184, 43);
            this.trkBrTh1.Maximum = 255;
            this.trkBrTh1.Minimum = 1;
            this.trkBrTh1.Name = "trkBrTh1";
            this.trkBrTh1.Size = new System.Drawing.Size(183, 45);
            this.trkBrTh1.TabIndex = 20;
            this.trkBrTh1.Value = 1;
            this.trkBrTh1.Scroll += new System.EventHandler(this.trkBrTh1_Scroll);
            // 
            // txtth2
            // 
            this.txtth2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtth2.Location = new System.Drawing.Point(394, 109);
            this.txtth2.MaxLength = 15;
            this.txtth2.Name = "txtth2";
            this.txtth2.ReadOnly = true;
            this.txtth2.Size = new System.Drawing.Size(76, 38);
            this.txtth2.TabIndex = 17;
            this.txtth2.Text = "129";
            this.txtth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTH1
            // 
            this.txtTH1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTH1.Location = new System.Drawing.Point(394, 46);
            this.txtTH1.MaxLength = 15;
            this.txtTH1.Name = "txtTH1";
            this.txtTH1.ReadOnly = true;
            this.txtTH1.Size = new System.Drawing.Size(76, 38);
            this.txtTH1.TabIndex = 14;
            this.txtTH1.Text = "129";
            this.txtTH1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::SandPaperInspection.Properties.Resources.settings__1_;
            this.pictureBox1.Location = new System.Drawing.Point(281, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 52);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 61;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 293);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 31);
            this.label6.TabIndex = 64;
            this.label6.Text = "Camera 2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 235);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 31);
            this.label5.TabIndex = 63;
            this.label5.Text = "Camera 1:";
            // 
            // txtCamExposureTC2
            // 
            this.txtCamExposureTC2.Location = new System.Drawing.Point(154, 286);
            this.txtCamExposureTC2.Maximum = 600;
            this.txtCamExposureTC2.Minimum = 30;
            this.txtCamExposureTC2.Name = "txtCamExposureTC2";
            this.txtCamExposureTC2.Size = new System.Drawing.Size(184, 45);
            this.txtCamExposureTC2.TabIndex = 61;
            this.txtCamExposureTC2.Value = 30;
            this.txtCamExposureTC2.Scroll += new System.EventHandler(this.txtCamExposureTC2_Scroll_1);
            // 
            // txtExpC2
            // 
            this.txtExpC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpC2.Location = new System.Drawing.Point(394, 293);
            this.txtExpC2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtExpC2.MaxLength = 3;
            this.txtExpC2.Name = "txtExpC2";
            this.txtExpC2.ReadOnly = true;
            this.txtExpC2.Size = new System.Drawing.Size(76, 38);
            this.txtExpC2.TabIndex = 62;
            this.txtExpC2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCamExposureT
            // 
            this.txtCamExposureT.Location = new System.Drawing.Point(154, 228);
            this.txtCamExposureT.Maximum = 600;
            this.txtCamExposureT.Minimum = 30;
            this.txtCamExposureT.Name = "txtCamExposureT";
            this.txtCamExposureT.Size = new System.Drawing.Size(184, 45);
            this.txtCamExposureT.TabIndex = 58;
            this.txtCamExposureT.Value = 30;
            this.txtCamExposureT.Scroll += new System.EventHandler(this.txtCamExposureT_Scroll_1);
            // 
            // txtExposer
            // 
            this.txtExposer.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExposer.Location = new System.Drawing.Point(394, 235);
            this.txtExposer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtExposer.MaxLength = 3;
            this.txtExposer.Name = "txtExposer";
            this.txtExposer.ReadOnly = true;
            this.txtExposer.Size = new System.Drawing.Size(76, 38);
            this.txtExposer.TabIndex = 60;
            this.txtExposer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(326, 31);
            this.label4.TabIndex = 59;
            this.label4.Text = "Camera Exposure Time ";
            // 
            // checkBoxSaveImages
            // 
            this.checkBoxSaveImages.AutoSize = true;
            this.checkBoxSaveImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSaveImages.Location = new System.Drawing.Point(40, 409);
            this.checkBoxSaveImages.Name = "checkBoxSaveImages";
            this.checkBoxSaveImages.Size = new System.Drawing.Size(191, 35);
            this.checkBoxSaveImages.TabIndex = 59;
            this.checkBoxSaveImages.Text = "Save Images";
            this.checkBoxSaveImages.UseVisualStyleBackColor = true;
            this.checkBoxSaveImages.CheckedChanged += new System.EventHandler(this.checkBoxSaveImages_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.checkBoxSaveImages);
            this.panel1.Controls.Add(this.btmThreshold);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(18, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 500);
            this.panel1.TabIndex = 60;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ThresholdSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SandPaperInspection.Properties.Resources.blue_1273089_1280;
            this.ClientSize = new System.Drawing.Size(602, 618);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ThresholdSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = resources.GetString("$this.Text");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThresholdSettings_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ThresholdSettings_FormClosed);
            this.Load += new System.EventHandler(this.ThresholdSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrTh2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrTh1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCamExposureTC2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCamExposureT)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btmThreshold;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trkBrTh2;
        private System.Windows.Forms.TrackBar trkBrTh1;
        private System.Windows.Forms.TextBox txtth2;
        private System.Windows.Forms.TextBox txtTH1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar txtCamExposureTC2;
        private System.Windows.Forms.TextBox txtExpC2;
        private System.Windows.Forms.TrackBar txtCamExposureT;
        private System.Windows.Forms.TextBox txtExposer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxSaveImages;
        private System.Windows.Forms.Panel panel1;
    }
}