using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // SettingForm settingForm = new SettingForm();
            // WeatherData.FormClosing += (sender, args) => { Application.Exit(); };
            // settingForm.Show();
            Application.Run(new SettingForm());


        }
    }
}
