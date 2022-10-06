using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSL
{
    public class Etc
    {
        static frmTrainData trainData = new frmTrainData();

        public static void BackgroundWorker(string args)
        {
            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.DoWork += bgWorkerDoWork;
            bgWorker.RunWorkerCompleted += bgWorkerCompleted;

            bgWorker.RunWorkerAsync(argument: args);
        }

        static void LoadingGIF(bool flag)
        {
            frmMain frm = new frmMain();
            frm.loading.Visible = flag;
            frm.loading.Refresh();
        }

        static void bgWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var args = e.Argument.ToString();

                if (args == null)
                    Etc.Notify("System", "Arguement null!", ToolTipIcon.Error);

                e.Result = RunPythonScript(args);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        static void bgWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result.ToString();

            if (result == null)
                Notify("System", "Result null", ToolTipIcon.Error);

            if (e.Cancelled)
                Notify("System", "Process is cancelled!", ToolTipIcon.Warning);
            else if (e.Error != null)
                Notify("System", "Error occured!", ToolTipIcon.Error);
            else
                Notify("System", result, ToolTipIcon.Info);
        }

        public static void Notify(string header, string context, ToolTipIcon tipIcon)
        {
            NotifyIcon notify = new();
            notify.Icon = new Icon(Properties.Settings.Default.ICON_PATH);
            notify.BalloonTipIcon = ToolTipIcon.Info;
            notify.Visible = true;
            notify.ShowBalloonTip(10, header, context, tipIcon);
            notify.BalloonTipClosed += (sender, e) => {
                var thisIcon = sender as NotifyIcon;
                thisIcon.Visible = false;
                thisIcon.Icon = null;
                thisIcon.Dispose();
            };

        }

        public static void SaveUserConfig()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.MAIN_FOLDER))
            {
                MessageBox.Show("Select your MAIN_FOLDER for FSL-PY.", "User Configuration",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                FolderBrowserDialog fbd = new FolderBrowserDialog();

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string folderName = fbd.SelectedPath;
                    Properties.Settings.Default.MAIN_FOLDER = folderName;
                    Properties.Settings.Default.Save();
                }
                Application.Restart();
            }

            try
            {
                string user = Environment.UserName;
                string settingPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming).FilePath.Replace("Roaming", "Local");

                using (FileStream fs = File.Create(Properties.Settings.Default.MAIN_FOLDER + "\\.env"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("GET_USER=" + settingPath);
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Notify("User Config", ex.ToString(), ToolTipIcon.Error);
            }
        }

        public static string GetPythonPath()
        {
            var path = Environment.GetEnvironmentVariable("PATH");
            if (path == null)
                throw new Exception("Environment Variable Path is null!");

            string pythonPath = "";

            foreach (var p in path.Split(new char[] { ';' }))
            {
                if (!p.Contains("Python310"))
                {
                    var fullPath = Path.Combine(p, "python.exe");
                    if (File.Exists(fullPath))
                    {
                        pythonPath = fullPath;
                        break;
                    }
                }
            }

            if (pythonPath == null)
                throw new Exception("Python path is null!");

            return pythonPath;
        }

        public static void OnlyNumbers(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public static void OnlyLetters(KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        /// <summary>
        /// WiP
        /// - Refactor
        /// - Path to constants
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string RunPythonScript(string args)
        {
            var result = "";
            List<string> results = new List<string>();

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine(Properties.Settings.Default.ACTIVATE_VENV);
            process.StandardInput.WriteLine("cd /d " + Properties.Settings.Default.MAIN_FOLDER);
            process.StandardInput.WriteLine(string.Format("python main.py {0}", args));
            process.StandardInput.Flush();
            process.StandardInput.Close();

            while (!process.StandardOutput.EndOfStream)
            {
                result = process.StandardOutput.ReadLine();
                if (args == "train")
                    if (!string.IsNullOrEmpty(result))
                        trainData.rtbLogs.Text += result;

                if (!string.IsNullOrEmpty(result))
                    results.Add(result.ToString());
            }

            process.WaitForExit();
            Console.WriteLine(results);
            results.RemoveAt(results.Count - 1);
            Console.WriteLine(results.Last());

            return results.Last();
        }


    }
}
