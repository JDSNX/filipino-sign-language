namespace FSL
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Etc.SaveUserConfig();
            
            if (string.IsNullOrEmpty(Properties.Settings.Default.KW_PATH) ||
                string.IsNullOrEmpty(Properties.Settings.Default.MAIN_PY_PATH) ||
                string.IsNullOrEmpty(Properties.Settings.Default.ICON_PATH) ||
                string.IsNullOrEmpty(Properties.Settings.Default.MAIN_FOLDER) ||
                string.IsNullOrEmpty(Properties.Settings.Default.ACTIVATE_VENV))
            {
                MessageBox.Show("Please set the file path in the settings.", "Settings",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Run(new frmMain(false));
            }
            else
            {
                Application.Run(new frmMain(true));
            }
        }
    }
}