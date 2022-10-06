namespace FSL
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtVenv = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMainFolder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIcon = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPythonFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFrameVideo = new System.Windows.Forms.TextBox();
            this.txtNoVideo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtKwPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaGreen;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 43);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(880, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "  Settings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel2.Controls.Add(this.txtVenv);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtMainFolder);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.txtIcon);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtPythonFile);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtFrameVideo);
            this.panel2.Controls.Add(this.txtNoVideo);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.txtKwPath);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 510);
            this.panel2.TabIndex = 4;
            // 
            // txtVenv
            // 
            this.txtVenv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.txtVenv.Location = new System.Drawing.Point(178, 161);
            this.txtVenv.Name = "txtVenv";
            this.txtVenv.ReadOnly = true;
            this.txtVenv.Size = new System.Drawing.Size(336, 29);
            this.txtVenv.TabIndex = 4;
            this.txtVenv.Click += new System.EventHandler(this.txtVenv_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(172, 21);
            this.label10.TabIndex = 18;
            this.label10.Text = "ACTIVATE VENV PATH:";
            // 
            // txtMainFolder
            // 
            this.txtMainFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.txtMainFolder.Location = new System.Drawing.Point(178, 126);
            this.txtMainFolder.Name = "txtMainFolder";
            this.txtMainFolder.ReadOnly = true;
            this.txtMainFolder.Size = new System.Drawing.Size(336, 29);
            this.txtMainFolder.TabIndex = 3;
            this.txtMainFolder.Click += new System.EventHandler(this.txtMainFolder_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 21);
            this.label9.TabIndex = 16;
            this.label9.Text = "MAIN FOLDER PATH:";
            // 
            // txtIcon
            // 
            this.txtIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.txtIcon.Location = new System.Drawing.Point(178, 91);
            this.txtIcon.Name = "txtIcon";
            this.txtIcon.ReadOnly = true;
            this.txtIcon.Size = new System.Drawing.Size(336, 29);
            this.txtIcon.TabIndex = 2;
            this.txtIcon.Click += new System.EventHandler(this.txtIcon_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 21);
            this.label8.TabIndex = 14;
            this.label8.Text = "SYSTEM ICON PATH:";
            // 
            // txtPythonFile
            // 
            this.txtPythonFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.txtPythonFile.Location = new System.Drawing.Point(178, 56);
            this.txtPythonFile.Name = "txtPythonFile";
            this.txtPythonFile.ReadOnly = true;
            this.txtPythonFile.Size = new System.Drawing.Size(336, 29);
            this.txtPythonFile.TabIndex = 1;
            this.txtPythonFile.Click += new System.EventHandler(this.txtPythonFile_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 21);
            this.label7.TabIndex = 12;
            this.label7.Text = "PYTHON FILE PATH:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(253, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "*default will always be 30.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(253, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "*default will always be 30.";
            // 
            // txtFrameVideo
            // 
            this.txtFrameVideo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.txtFrameVideo.Location = new System.Drawing.Point(178, 228);
            this.txtFrameVideo.MaxLength = 2;
            this.txtFrameVideo.Name = "txtFrameVideo";
            this.txtFrameVideo.Size = new System.Drawing.Size(69, 29);
            this.txtFrameVideo.TabIndex = 6;
            this.txtFrameVideo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoVideo_KeyPress);
            // 
            // txtNoVideo
            // 
            this.txtNoVideo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.txtNoVideo.Location = new System.Drawing.Point(178, 196);
            this.txtNoVideo.MaxLength = 2;
            this.txtNoVideo.Name = "txtNoVideo";
            this.txtNoVideo.Size = new System.Drawing.Size(69, 29);
            this.txtNoVideo.TabIndex = 5;
            this.txtNoVideo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoVideo_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "FRAMES OF VIDEOS:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "NO. OF VIDEOS:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(777, 455);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 43);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "  Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtKwPath
            // 
            this.txtKwPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.txtKwPath.Location = new System.Drawing.Point(178, 21);
            this.txtKwPath.Name = "txtKwPath";
            this.txtKwPath.ReadOnly = true;
            this.txtKwPath.Size = new System.Drawing.Size(336, 29);
            this.txtKwPath.TabIndex = 0;
            this.txtKwPath.Click += new System.EventHandler(this.txtKwPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "KEYWORD PATH:";
            // 
            // frmSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(880, 553);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(41)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddWord";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private TextBox txtKwPath;
        private Label label2;
        private Button btnSave;
        private TextBox txtFrameVideo;
        private TextBox txtNoVideo;
        private Label label4;
        private Label label3;
        private Label label6;
        private Label label5;
        private TextBox txtPythonFile;
        private Label label7;
        private TextBox txtIcon;
        private Label label8;
        private TextBox txtVenv;
        private Label label10;
        private TextBox txtMainFolder;
        private Label label9;
    }
}