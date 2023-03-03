using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp5.Properties;
using static WindowsFormsApp5.SettingForm;

namespace WindowsFormsApp5
{
    public partial class WeatherData : Form
    {
        
        private WeatherService weatherService;
        private int panelIndex = 0;
        private int totalCities = 0;
        private WeatherSettings settings;

        public WeatherData(WeatherSettings settings)
        {
            InitializeComponent();
            this.weatherService = new WeatherService(settings);
            totalCities = settings.Cities.Count;       
            weatherService.UpdateLabels(label1, lblHumidity, lblAtmosphere, lblTemperature, panelIndex);
            timer1.Interval = Int32.Parse(settings.RefreshTime) * 1000;
            timer1.Start();
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            panelIndex++;

            if (panelIndex >= totalCities)
            {
                panelIndex = 0;
            }
            Console.WriteLine(MyStrings.Tick + panelIndex);
            await weatherService.UpdateLabels(label1, lblHumidity, lblAtmosphere, lblTemperature, panelIndex);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Left = WindowsFormsApp5.Properties.Settings.Default.Left;
            this.Width = WindowsFormsApp5.Properties.Settings.Default.Width;
            this.Height = WindowsFormsApp5.Properties.Settings.Default.Height;
            this.Top = WindowsFormsApp5.Properties.Settings.Default.Top;
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var newSettingsForm = new SettingForm();
                newSettingsForm.ShowDialog();

                if (newSettingsForm.Settings != null)
                {
                    weatherService = new WeatherService(newSettingsForm.Settings);
                    settings = newSettingsForm.Settings;
                    if (settings.Cities != null)
                    {
                        totalCities = settings.Cities.Count;

                    }
                    timer1.Interval = Int32.Parse(newSettingsForm.Settings.RefreshTime) * 1000;
                    weatherService.UpdateLabels(label1, lblHumidity, lblAtmosphere, lblTemperature, panelIndex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }      
       private void WeatherData_FormClosed(object sender, FormClosedEventArgs e)
        {
            WindowsFormsApp5.Properties.Settings.Default.Left = this.Left;
            WindowsFormsApp5.Properties.Settings.Default.Width = this.Width;
            WindowsFormsApp5.Properties.Settings.Default.Height = this.Height;
            WindowsFormsApp5.Properties.Settings.Default.Top = this.Top;
            WindowsFormsApp5.Properties.Settings.Default.Save();
        }

        private void WeatherData_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
}



      

