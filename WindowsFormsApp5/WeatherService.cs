using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApp5.WeatherData;
using static WindowsFormsApp5.SettingForm;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public class WeatherService
    {
        private HttpClient httpClient = new HttpClient();
        private string apiKey = MyStrings.APIKEY;
        private WeatherSettings settings;

        public  WeatherService(WeatherSettings settings)
        {
            this.settings = settings;
        }

        public async Task<string> GetWeatherData(string city)
        {
            try
            {
                string url = String.Format(MyStrings.WeatherAPIUrl,city, apiKey);
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(String.Format(MyStrings.HttpExceptionMessage,city, ex.Message));
                return null;
            }
        }

        public async Task UpdateLabels(Label label1, Label lblHumidity, Label lblAtmosphere, Label lblTemperature, int panelIndex)
        {
            try
            {
                var city = settings.Cities.ElementAt(panelIndex);
                Console.WriteLine(panelIndex.ToString());
                var weatherData = await GetWeatherData(city); 
                dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(weatherData);
                double temperature = json.main.temp;
                double humidity = json.main.humidity;
                double pressure = json.main.pressure;
                label1.Text = city;
                lblHumidity.Text = String.Format(MyStrings.HumidityText, humidity);
                lblAtmosphere.Text = String.Format(MyStrings.AtmoshphereText, pressure);
                lblTemperature.Text = String.Format (MyStrings.TemperatureText, temperature);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
