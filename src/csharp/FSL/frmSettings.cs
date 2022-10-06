using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FSL.Properties;

namespace FSL
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        private void txtPythonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Python Files (*.py)|*.py*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                txtPythonFile.Text = sFileName;
            }
        }

        private void txtKwPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "CSV Files (*.csv)|*.csv*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                txtKwPath.Text = sFileName;
            }
        }

        private void txtIcon_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Icon (*.ico)|*.ico*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                txtIcon.Text = sFileName;
            }
        }

        private void txtMainFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string folderName = fbd.SelectedPath;
                txtMainFolder.Text = folderName;
            }
        }

        private void txtVenv_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "Activate (*.bat)|*.bat";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = choofdlog.FileName;
                txtVenv.Text = sFileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFrameVideo.Text == "0")
                txtFrameVideo.Text = "30";

            if(txtNoVideo.Text == "0")
                txtNoVideo.Text = "30";


            Settings.Default.KW_PATH = txtKwPath.Text;
            Settings.Default.MAIN_PY_PATH = txtPythonFile.Text;
            Settings.Default.ICON_PATH = txtIcon.Text;
            Settings.Default.MAIN_FOLDER = txtMainFolder.Text;
            Settings.Default.ACTIVATE_VENV = txtVenv.Text;
            Settings.Default.NO_OF_VIDEOS = Convert.ToInt32(txtNoVideo.Text);
            Settings.Default.FRAME_OF_VIDEOS = Convert.ToInt32(txtFrameVideo.Text);
            Settings.Default.Save(); 

            Etc.Notify("Settings", "Settings successfully saved!", ToolTipIcon.Info);
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            txtKwPath.Text = Settings.Default.KW_PATH;
            txtPythonFile.Text = Settings.Default.MAIN_PY_PATH;
            txtIcon.Text = Settings.Default.ICON_PATH;
            txtMainFolder.Text = Settings.Default.MAIN_FOLDER;
            txtVenv.Text = Settings.Default.ACTIVATE_VENV;
            txtNoVideo.Text = Settings.Default.NO_OF_VIDEOS.ToString();
            txtFrameVideo.Text = Settings.Default.FRAME_OF_VIDEOS.ToString();

            if (string.IsNullOrEmpty(Settings.Default.KW_PATH))
                txtKwPath.Text = "Please select path for keyword path...";

            if (string.IsNullOrEmpty(Settings.Default.MAIN_PY_PATH))
                txtPythonFile.Text = "Please select path for `main` python file...";

            if (string.IsNullOrEmpty(Settings.Default.ICON_PATH))
                txtIcon.Text = "Please select path for icon file...";

            if (string.IsNullOrEmpty(Settings.Default.MAIN_FOLDER))
                txtMainFolder.Text = "Please select path for python file...";

            if (string.IsNullOrEmpty(Settings.Default.ACTIVATE_VENV))
                txtVenv.Text = "Please select path for activating virtual environment file...";

            if (Settings.Default.NO_OF_VIDEOS == 0)
                txtNoVideo.Text = "30";

            if (Settings.Default.FRAME_OF_VIDEOS == 0)
                txtFrameVideo.Text = "30";
        }

        private void txtNoVideo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Etc.OnlyNumbers(e);
        }
    }
}
