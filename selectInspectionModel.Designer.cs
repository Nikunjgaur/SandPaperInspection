namespace SandPaperInspection
{
    partial class selectInspectionModel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewModel = new System.Windows.Forms.DataGridView();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonDelModel = new System.Windows.Forms.Button();
            this.SrNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModel)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewModel
            // 
            this.dataGridViewModel.AllowUserToAddRows = false;
            this.dataGridViewModel.AllowUserToDeleteRows = false;
            this.dataGridViewModel.AllowUserToResizeColumns = false;
            this.dataGridViewModel.AllowUserToResizeRows = false;
            this.dataGridViewModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNum,
            this.Model});
            this.dataGridViewModel.Location = new System.Drawing.Point(36, 56);
            this.dataGridViewModel.Name = "dataGridViewModel";
            this.dataGridViewModel.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewModel.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewModel.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewModel.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewModel.RowTemplate.Height = 44;
            this.dataGridViewModel.Size = new System.Drawing.Size(429, 418);
            this.dataGridViewModel.TabIndex = 0;
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(75)))), ((int)(((byte)(110)))));
            this.buttonSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelect.ForeColor = System.Drawing.Color.White;
            this.buttonSelect.Location = new System.Drawing.Point(63, 509);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(133, 54);
            this.buttonSelect.TabIndex = 1;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // buttonDelModel
            // 
            this.buttonDelModel.BackColor = System.Drawing.Color.Red;
            this.buttonDelModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelModel.ForeColor = System.Drawing.Color.White;
            this.buttonDelModel.Location = new System.Drawing.Point(279, 509);
            this.buttonDelModel.Name = "buttonDelModel";
            this.buttonDelModel.Size = new System.Drawing.Size(131, 54);
            this.buttonDelModel.TabIndex = 1;
            this.buttonDelModel.Text = "Delete";
            this.buttonDelModel.UseVisualStyleBackColor = false;
            this.buttonDelModel.Click += new System.EventHandler(this.buttonDelModel_Click);
            // 
            // SrNum
            // 
            this.SrNum.HeaderText = "Sr Num";
            this.SrNum.Name = "SrNum";
            this.SrNum.ReadOnly = true;
            this.SrNum.Width = 80;
            // 
            // Model
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Model.DefaultCellStyle = dataGridViewCellStyle1;
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 300;
            // 
            // selectInspectionModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SandPaperInspection.Properties.Resources.white_blue;
            this.ClientSize = new System.Drawing.Size(510, 637);
            this.Controls.Add(this.buttonDelModel);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.dataGridViewModel);
            this.MaximumSize = new System.Drawing.Size(526, 676);
            this.MinimumSize = new System.Drawing.Size(526, 676);
            this.Name = "selectInspectionModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Model for Inspection";
            this.Load += new System.EventHandler(this.selectInspectionModel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewModel;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonDelModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
    }
}