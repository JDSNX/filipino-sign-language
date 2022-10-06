using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSL
{
    public partial class frmTrainData : Form
    {
        public frmTrainData()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Visible = true;

            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
            else
                Etc.Notify("Background Worker", "Busy...", ToolTipIcon.Warning);
        }

        void SortOutputHandler(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine(e.Data);
            this.BeginInvoke(new MethodInvoker(() => {
                rtbLogs.AppendText(e.Data + "\n" ?? string.Empty);
            }));
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.OutputDataReceived += SortOutputHandler;
            process.Start();
            process.StandardInput.WriteLine(Properties.Settings.Default.ACTIVATE_VENV);
            process.StandardInput.WriteLine("cd /d " + Properties.Settings.Default.MAIN_FOLDER);
            process.StandardInput.WriteLine(string.Format("python main.py {0}", "train"));
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.BeginOutputReadLine();
            process.WaitForExit();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Etc.Notify("Train Data", "Process complete!", ToolTipIcon.Info);

            button1.Enabled = true;
            button2.Visible = false;
        }

        private void rtbLogs_TextChanged(object sender, EventArgs e)
        {  
            rtbLogs.SelectionStart = rtbLogs.Text.Length;
            rtbLogs.ScrollToCaret();
        }     

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Process myProc in Process.GetProcesses())
            {
                if (myProc.ProcessName.Contains("python"))
                    myProc.Kill();
            }
        }
    }
}
