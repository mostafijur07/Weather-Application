using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;


namespace Weather_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string APIKey = "7b3d96abb616e47159a5c5c6b968767c";

        private void SearchButton_Click(object sender, EventArgs e)
        {
            getWeather();
        }

        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", textBox1.Text, APIKey);
                var json = web.DownloadString(url);
                Weatherinfo.root Info = JsonConvert.DeserializeObject<Weatherinfo.root>(json);

                pictureBox1.ImageLocation = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                LabCondition.Text = Info.weather[0].main;
                LabDetails.Text = Info.weather[0].description;

                LabSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                LabSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();
                LabWindSpeed.Text = Info.wind.speed.ToString();
                LabPressure.Text = Info.main.pressure.ToString();

            }
        }

        DateTime convertDateTime(long millisec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisec).ToLocalTime();

            return day;
        }
    }
}
