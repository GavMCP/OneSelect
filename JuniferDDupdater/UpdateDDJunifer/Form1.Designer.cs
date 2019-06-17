namespace UpdateDDJunifer
{
    partial class frmMain
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
            this.btnGetCSVfile = new System.Windows.Forms.Button();
            this.txtMainDisplay = new System.Windows.Forms.TextBox();
            this.btnUpdateJunifer = new System.Windows.Forms.Button();
            this.openCSVfileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtCounter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnGetCSVfile
            // 
            this.btnGetCSVfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetCSVfile.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnGetCSVfile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnGetCSVfile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnGetCSVfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetCSVfile.Location = new System.Drawing.Point(404, 83);
            this.btnGetCSVfile.Name = "btnGetCSVfile";
            this.btnGetCSVfile.Size = new System.Drawing.Size(123, 34);
            this.btnGetCSVfile.TabIndex = 0;
            this.btnGetCSVfile.Text = "Select CSV File";
            this.btnGetCSVfile.UseVisualStyleBackColor = true;
            this.btnGetCSVfile.Click += new System.EventHandler(this.btnGetCSVfile_Click);
            // 
            // txtMainDisplay
            // 
            this.txtMainDisplay.BackColor = System.Drawing.Color.LightGray;
            this.txtMainDisplay.Cursor = System.Windows.Forms.Cursors.No;
            this.txtMainDisplay.Location = new System.Drawing.Point(36, 41);
            this.txtMainDisplay.Multiline = true;
            this.txtMainDisplay.Name = "txtMainDisplay";
            this.txtMainDisplay.ReadOnly = true;
            this.txtMainDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMainDisplay.Size = new System.Drawing.Size(349, 363);
            this.txtMainDisplay.TabIndex = 1;
            this.txtMainDisplay.TabStop = false;
            // 
            // btnUpdateJunifer
            // 
            this.btnUpdateJunifer.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnUpdateJunifer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnUpdateJunifer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnUpdateJunifer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateJunifer.Location = new System.Drawing.Point(404, 135);
            this.btnUpdateJunifer.Name = "btnUpdateJunifer";
            this.btnUpdateJunifer.Size = new System.Drawing.Size(123, 69);
            this.btnUpdateJunifer.TabIndex = 1;
            this.btnUpdateJunifer.Text = "Run D.D Update on Junifer!";
            this.btnUpdateJunifer.UseVisualStyleBackColor = true;
            this.btnUpdateJunifer.Click += new System.EventHandler(this.btnUpdateJunifer_Click);
            // 
            // openCSVfileDialog
            // 
            this.openCSVfileDialog.DefaultExt = "csv";
            this.openCSVfileDialog.FileName = "openFileDialog1";
            this.openCSVfileDialog.Filter = "\"CSV files (*.csv)|*.csv\"";
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(404, 370);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(123, 34);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtCounter
            // 
            this.txtCounter.Location = new System.Drawing.Point(404, 41);
            this.txtCounter.Name = "txtCounter";
            this.txtCounter.Size = new System.Drawing.Size(123, 22);
            this.txtCounter.TabIndex = 3;
            this.txtCounter.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(401, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Account Counter:";
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateReport.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCreateReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnCreateReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCreateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateReport.Location = new System.Drawing.Point(404, 300);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(123, 34);
            this.btnCreateReport.TabIndex = 5;
            this.btnCreateReport.Text = "Create Report";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(550, 426);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCounter);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUpdateJunifer);
            this.Controls.Add(this.txtMainDisplay);
            this.Controls.Add(this.btnGetCSVfile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Junifer DD Details";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetCSVfile;
        private System.Windows.Forms.TextBox txtMainDisplay;
        private System.Windows.Forms.Button btnUpdateJunifer;
        private System.Windows.Forms.OpenFileDialog openCSVfileDialog;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtCounter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

