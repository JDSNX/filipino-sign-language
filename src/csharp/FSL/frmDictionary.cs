using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSL
{
    public partial class frmDictionary : Form
    {
        public frmDictionary()
        {
            InitializeComponent();
        }

        private void LoadDictionary()
        {
            pictureBox1.Image = null;
            dictList.Items.Clear();
            using (var reader = new StreamReader(Properties.Settings.Default.KW_PATH))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    if (!string.IsNullOrWhiteSpace(values[0]))
                        dictList.Items.Add(values[0].ToLower().Split(',')[0]);
                }
            }
        }

        private void frmDictionary_Load(object sender, EventArgs e)
        {
            LoadDictionary();
        }

        private void DisplayGIF(string fileName)
        {
            string RunningPath = Path.Combine(System.IO.Path.GetFullPath(@"..\..\..\"), "Resources\\", "gifs\\", fileName);

            try
            {
                if (!File.Exists(RunningPath))
                {
                    Etc.Notify("Dictionary", "Please upload .gif file for " + 
                        fileName.Substring(0, fileName.Length - 4), ToolTipIcon.Error);
                    pictureBox1.Image = null;
                }
                else
                    pictureBox1.Image = new Bitmap(RunningPath);
            }
            catch (Exception ex)
            {
                Etc.Notify("Exception", ex.Message, ToolTipIcon.Warning);
                return;
            }

        }

        private void dictList_MouseClick(object sender, MouseEventArgs e)
        {
            if (dictList.SelectedItem != null)
            {
                DisplayGIF(dictList.SelectedItem.ToString() + ".gif");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var temp = dictList.SelectedItem;

            if (temp == null)
                Etc.Notify("Add New Word", "Select a word to delete!", ToolTipIcon.Error);
            else
            {
                try
                {
                    dictList.Items.Remove(temp);

                    List<string> tmp = new List<string>();

                    using (var reader = new StreamReader(Properties.Settings.Default.KW_PATH))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            if (!string.Equals(values[0], temp))
                                tmp.Add(line.TrimEnd(' '));
                        }
                    }

                    Directory.Delete(Properties.Settings.Default.MAIN_FOLDER + "\\mediapipe_data\\" + temp, true);

                    using (StreamWriter myOutputStream = new StreamWriter(Properties.Settings.Default.KW_PATH))
                    {
                        foreach (var item in tmp)
                        {
                            myOutputStream.WriteLine(item.ToString().TrimEnd(' '));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Etc.Notify("Exception", ex.Message, ToolTipIcon.Warning);
                }
                finally
                {
                    Etc.Notify("Dictionary", temp + " sucessfully deleted in the list.", ToolTipIcon.Info);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddWord frm = new frmAddWord();
            frm.ShowDialog(); 
            LoadDictionary();
        }
    }
}
