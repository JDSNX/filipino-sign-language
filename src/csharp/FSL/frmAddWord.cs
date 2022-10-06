namespace FSL
{
    public partial class frmAddWord : Form
    {
        public frmAddWord()
        {
            InitializeComponent();
        }

        private void txtAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            Etc.OnlyLetters(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var myWord = txtEnglish.Text.ToLower();
            var filipino = txtFilipino.Text.ToLower();

            if (string.IsNullOrWhiteSpace(myWord))
                return;

            if (string.IsNullOrWhiteSpace(filipino))
                filipino = "None";

            List<string> tmp = new List<string>();
            using (var reader = new StreamReader(Properties.Settings.Default.KW_PATH))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    tmp.Add(values[0]);
                }
            }

            try
            {
                if (tmp.Contains(myWord))
                    Etc.Notify("Add New Word", myWord + " is already in your list.\n" +
                        "Please input another word", ToolTipIcon.Error);
                else
                {
                    if (File.Exists(Properties.Settings.Default.KW_PATH))
                    {
                        File.AppendAllText(Properties.Settings.Default.KW_PATH, "\n" + myWord + "," + filipino);
                    }

                    txtEnglish.Clear();
                    txtFilipino.Clear();
                }
            }
            catch (Exception ex)
            {
                Etc.Notify("Exception", ex.Message, ToolTipIcon.Warning);
            }
            finally
            {
                Etc.Notify("Adding New Word", "FSL is now running. Be patient.", ToolTipIcon.Info);

                var s = Etc.RunPythonScript("new_word");
                if (!string.IsNullOrEmpty(s))
                    Etc.Notify("New Word", "Completed...", ToolTipIcon.Info);
                else
                    Etc.Notify("New Word", s, ToolTipIcon.Info);

                Etc.Notify("Add New Word", myWord + " successfully saved!", ToolTipIcon.Info);

                this.Close();
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
