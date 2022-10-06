using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSL
{
    public partial class frmMain : Form
    {
        public frmMain()
        { 
            InitializeComponent();
        }

        public frmMain(bool settings)
        {
            InitializeComponent();

            if (!settings)  
                LoadMainPanel(new frmSettings());
        }

        private void HideSubMenu()
        {
            panelTraining.Visible = false;
        }

        private void ShowSubMenu()
        {
            panelTraining.Visible = true;
        }

        private void LoadMainPanel(Form frm)
        {
            frm.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnTraining_Click(object sender, EventArgs e)
        {
            ShowSubMenu();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            HideSubMenu();          
            LoadMainPanel(new frmDashboard());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            LoadMainPanel(new frmSettings());
        }

        private void btnDictionary_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            LoadMainPanel(new frmDictionary());
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            LoadMainPanel(new frmTrainData());
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Run FSL?", "FSL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                HideSubMenu();
                this.Hide();
                // Etc.BackgroundWorker("run");
                Etc.Notify("RUN FSL", "FSL is now running. Be patient.", ToolTipIcon.Info);
                var s = Etc.RunPythonScript("run");
                if (!string.IsNullOrEmpty(s))
                    Etc.Notify("Run FSL", "Completed...", ToolTipIcon.Info);
                else
                    Etc.Notify("Run FSL", s, ToolTipIcon.Info);
                this.Show();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Etc.SaveUserConfig();
        }
    }
}