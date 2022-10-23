using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace Italy2022
{


    public partial class MainPage : ContentPage
    {
        string input = "";
        static string location = "";
        public string inpput;
        bool before = false;
        bool bypass = false;
        bool bypass2 = false;
        bool dev = false;
        bool uk = false;
        bool pregunta = false;
        static bool flash = true;
        static bool SOSUPDATE = false;
        public double percentage = 0;
        



        Stopwatch flighttime = new Stopwatch();
        Stopwatch sleep = new Stopwatch(); double sleephours = 0;
        APIClient Client = new APIClient();
        APIClientUSD ClientUSD = new APIClientUSD();


        public MainPage()
        {
            InitializeComponent();
            if (DateTime.Now.Month < 10 && DateTime.Now.Year == 2022 || DateTime.Now.Month == 10 && DateTime.Now.Day < 29) { before = true; ButtonDateFly.Text = "Days"; }
            Box.Text = "1. IRE\n2. GBR";
            NightorDay();
            Client.GetGBP();
            ClientUSD.GetUSD();
            var getperm = Geolocation.GetLastKnownLocationAsync();
        }
        private void Button1_Clicked(object sender, EventArgs e)
        {
            if (pregunta == false) { uk = false; Datetime(); ConvertButton.Text = "EUR"; pregunta = true; input = ""; return; }
            input += "1"; Box.Text = input;           
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            if (pregunta == false) { uk = true; Datetime(); ConvertButton.Text = "GBP"; pregunta = true; input = ""; return; }
            input += "2"; Box.Text = input;          
        }

        private void Button3_Clicked(object sender, EventArgs e)
        {
            input += "3"; Box.Text = input;
        }

        private void Button4_Clicked(object sender, EventArgs e)
        {
            input += "4"; Box.Text = input;
        }

        private void Button5_Clicked(object sender, EventArgs e)
        {
            input += "5"; Box.Text = input;
        }

        private void Button6_Clicked(object sender, EventArgs e)
        {
            input += "6"; Box.Text = input;
        }

        private void Button7_Clicked(object sender, EventArgs e)
        {
            input += "7"; Box.Text = input;
        }

        private void Button8_Clicked(object sender, EventArgs e)
        {
            input += "8"; Box.Text = input;
        }

        private void Button9_Clicked(object sender, EventArgs e)
        {
            input += "9"; Box.Text = input;
        }

        private void ButtonDot_Clicked(object sender, EventArgs e)
        {
            input += "."; Box.Text = input;
        }

        private void Button0_Clicked(object sender, EventArgs e)
        {
            input += "0"; Box.Text = input;
        }

        private void ButtonDel_Clicked(object sender, EventArgs e)
        {
            try { input = input.Remove(input.Length - 1); Box.Text = input; }
            catch (Exception) { input = ""; return; }
        }

        private void ConvertButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                double conversion = Convert.ToDouble(input);
                if (uk)
                {                  
                    if (Client.varsyr == null) { conversion *= 0.87; }
                    else { conversion *= Convert.ToDouble(Client.varsyr); }
                    int temp = Convert.ToInt32(conversion);
                    conversion = Math.Round(conversion, 2);
                    Box.Text = "That's about £" + conversion;
                }
                else
                {
                    Box.Text = "That's about €" + conversion;
                }

                input = "";
            }
            catch (Exception) { Box.Text = "Error"; input = ""; return; }
        }



        private void ButtonSOS_Clicked(object sender, EventArgs e)
        {
            try { Box.Text = ""; PhoneDialer.Open("112"); }
            catch (Exception) { Box.Text = "Any Emergency: 112"; }
            input = "";
            SOSUPDATE = true;
            if(DateTime.Now.Hour >= 6) { FLash3Async(); }
        }

        private void ButtonDateFly_Clicked(object sender, EventArgs e)
        {
            input = "";
            if (Box.Text == "1622") { dev = true; Box.Text = ""; ButtonDateFly.Text = "Flight"; }
            if (before && dev == false) { Datetime(); }
            else
            {
                if (!flighttime.IsRunning) { flighttime.Start(); Box.Text = "Flight Started"; PBar.Opacity = 1; PBar.Progress = 100; }
                else
                {
                    Box.Text = "";
                    int temp = 0;
                    

                    if (uk) { temp = Convert.ToInt32(8100000 - flighttime.ElapsedMilliseconds); temp /= 1000; temp /= 60; percentage = flighttime.ElapsedMilliseconds; percentage /= 8100000;  }
                    else { temp = Convert.ToInt32(8700000 - flighttime.ElapsedMilliseconds); temp /= 1000; temp /= 60; percentage = flighttime.ElapsedMilliseconds; percentage /= 8700000; }

                    percentage *= 100;
                    percentage = percentage - 100;
                    percentage *= -1;
                    percentage = Math.Round(percentage);
                    PBar.Progress = percentage / 100;

                    if (temp <= 0) { Box.Text = "Welcome To Italy"; flighttime.Reset(); PBar.Opacity = 0; return; }
                    TimeSpan spWorkMin = TimeSpan.FromMinutes(temp);
                    string workHours = spWorkMin.ToString(@"hh\:mm");
                    Box.Text += workHours + "\n" + percentage + "%"; 

                }
            }
        }

        private void ButtonTime_Clicked(object sender, EventArgs e)
        {
            bypass = true;
            Datetime();
        }

        private void ButtonSleep_Clicked(object sender, EventArgs e)
        {

            if (!sleep.IsRunning) { sleep.Start(); Box.Text = "Goodnight"; ButtonSleep.Text = "End"; bypass2 = true; NightorDay(); }
            else
            {
                sleep.Stop();
                Box.Text = "Good Morning\n";
                sleephours = sleep.ElapsedMilliseconds / 1000; sleephours /= 60;

                TimeSpan spWorkMin = TimeSpan.FromMinutes(sleephours);
                string workHours = spWorkMin.ToString(@"hh\:mm");
                Box.Text += "You Slept " + workHours;
                Box.Text += "\nYou took " + Convert.ToInt32(sleephours * 16) + " Breaths";
                ButtonSleep.Text = "Sleep";

                bypass2 = false;
                NightorDay();
            }


        }



        public void NightorDay()
        {
            Xamarin.Essentials.AppTheme theme = AppInfo.RequestedTheme;
            if (DateTime.Now.Hour >= 21 || DateTime.Now.Hour < 9 || bypass2 == true || theme == AppTheme.Dark)
            {
                BackgroundImageSource = "appnightbackround.png";
                Button1.BackgroundColor = Color.Orange; Button2.BackgroundColor = Color.Orange;
                Button3.BackgroundColor = Color.Orange; Button4.BackgroundColor = Color.Orange;
                Button5.BackgroundColor = Color.Orange; Button6.BackgroundColor = Color.Orange;
                Button7.BackgroundColor = Color.Orange; Button8.BackgroundColor = Color.Orange;
                Button9.BackgroundColor = Color.Orange; Button0.BackgroundColor = Color.Orange;
                ButtonDot.BackgroundColor = Color.Orange; ButtonDel.BackgroundColor = Color.Orange;
                ConvertButton.BackgroundColor = Color.Orange; ButtonDateFly.BackgroundColor = Color.Orange;
                ButtonTime.BackgroundColor = Color.Orange;
                ButtonSleep.BackgroundColor = Color.Orange;
                ButtonFlash.BackgroundColor = Color.Orange;

                Button1.TextColor = Color.Black; Button3.TextColor = Color.Black;
                Button4.TextColor = Color.Black; Button6.TextColor = Color.Black;
                Button7.TextColor = Color.Black; Button9.TextColor = Color.Black;
                ButtonDot.TextColor = Color.Black; ButtonDel.TextColor = Color.Black;
                ConvertButton.TextColor = Color.Black; ButtonDateFly.TextColor = Color.Black;
                ButtonFlash.TextColor = Color.Black;
                ButtonTime.TextColor = Color.Black;
            }
            else
            {
                BackgroundImageSource = "appdaybackround.png";
                Button1.BackgroundColor = Color.OrangeRed; Button1.TextColor = Color.White;
                Button2.BackgroundColor = Color.White;
                Button3.BackgroundColor = Color.LightGreen; Button3.TextColor = Color.White;

                Button4.BackgroundColor = Color.OrangeRed; Button4.TextColor = Color.White;
                Button5.BackgroundColor = Color.White;
                Button6.BackgroundColor = Color.LightGreen; Button6.TextColor = Color.White;

                Button7.BackgroundColor = Color.OrangeRed; Button7.TextColor = Color.White;
                Button8.BackgroundColor = Color.White;
                Button9.BackgroundColor = Color.LightGreen; Button9.TextColor = Color.White;

                ButtonDot.BackgroundColor = Color.OrangeRed; ButtonDot.TextColor = Color.White;
                Button0.BackgroundColor = Color.White;
                ButtonDel.BackgroundColor = Color.LightGreen; ButtonDel.TextColor = Color.White;

                ButtonDateFly.BackgroundColor = Color.OrangeRed; ButtonDateFly.TextColor = Color.White;
                ConvertButton.BackgroundColor = Color.LightGreen; ConvertButton.TextColor = Color.White;

                ButtonTime.BackgroundColor = Color.OrangeRed; ButtonTime.TextColor = Color.White;
                ButtonSleep.BackgroundColor = Color.White;
                ButtonFlash.BackgroundColor = Color.LightGreen; ButtonFlash.TextColor = Color.White;

            }


        }

        public async void Datetime()
        {
            input = ""; Box.Text = "";
            

            if (before == true && bypass == false)
            {
                await ClientUSD.GetUSD();
                double i = Convert.ToDouble(ClientUSD.varsyrUSD); i = Math.Round(i, 2);
                DateTime futurDate = Convert.ToDateTime("29/10/2022");
                DateTime TodayDate = DateTime.Now;                
                Box.Text += Convert.ToInt32((futurDate - TodayDate).TotalDays); Box.Text += " Days\n";               
                if (i != 0) { Box.Text += "EUR/USD: " + i; }
                
            }
            else
            {
                int min = DateTime.Now.Minute;
                string minstring = Convert.ToString(DateTime.Now.Minute);
                bypass = false;

                int uktime = DateTime.Now.Hour - 1; int italytime = DateTime.Now.Hour;
                if (uktime >= 24) { uktime -= 24; }
                if (italytime >= 24) { italytime -= 24; }

                if (uktime < 0) { uktime *= -1; }
                if (italytime < 0) { italytime *= -1; }

                if (before) { uktime++; italytime++; }

                if (min.ToString().Length == 1 && min >= 10) { minstring = Convert.ToString(min) + "0"; }
                if (min.ToString().Length == 1 && min < 10) { minstring = "0" + Convert.ToString(min); }

                Box.Text += "Rome: " + italytime + ":" + minstring;
                if (uk) { Box.Text += "\nLondon: " + uktime + ":" + minstring; }
                else { Box.Text += "\nDublin: " + uktime + ":" + minstring; }


            }
        }

        private void ButtonFlash_Clicked(object sender, EventArgs e)
        {
            //FLash2Async();
            SpeakIT();
        }

        public async void SpeakIT()
        {
            var locales = await TextToSpeech.GetLocalesAsync();
            var locale = locales.FirstOrDefault();
            var settings = new SpeechOptions() { Locale = locale };
            
            if (DateTime.Now.Hour >= 12) 
            {
                await TextToSpeech.SpeakAsync("Good Afternoon,",settings);
                await TextToSpeech.SpeakAsync("It's" + DateTime.Now.ToString("hh: mm") + "pm, on " + DateTime.Now.DayOfWeek, settings);
            }
            else 
            {
                await TextToSpeech.SpeakAsync("Good Morning,", settings); 
                await TextToSpeech.SpeakAsync("It's" + DateTime.Now.ToString("hh: mm") + "am, on " + DateTime.Now.DayOfWeek,settings);
            }

            if(ClientUSD.varsyrUSD != null) await TextToSpeech.SpeakAsync("The Euro is currently worth" + Math.Round(Convert.ToDouble(ClientUSD.varsyrUSD),2) + "Dollars",settings);

            await TextToSpeech.SpeakAsync("Right Now, your Phone is on " + Convert.ToInt32(Battery.ChargeLevel * 100) + "percent",settings);

            ;




            int altit = 0;
            double dist = 0;
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                double temp = Convert.ToDouble(location.Altitude);
                temp *= 3.2808399;
                altit = Convert.ToInt32(temp);

                Location londloc = new Location();
                londloc.Latitude = 41.9028;
                londloc.Longitude = 12.4964;

                dist = location.CalculateDistance(londloc, DistanceUnits.Miles);
                
            }
            catch (Exception) { };


           

            await TextToSpeech.SpeakAsync("You are at a height of " + altit + "ft and you are " + Convert.ToInt32(dist) + "miles away from Rome",settings) ;
            await TextToSpeech.SpeakAsync("Have a great Day", settings);

        }

       public async Task FLash2Async()
        {                   
            flash = !flash;
            if (flash) { await Xamarin.Essentials.Flashlight.TurnOffAsync(); }
            else { await Xamarin.Essentials.Flashlight.TurnOnAsync(); }                            
        }

        public async Task FLash3Async()
        {
            while (true)
            {
                flash = !flash;
                if (flash) { await Xamarin.Essentials.Flashlight.TurnOffAsync(); }
                else { await Xamarin.Essentials.Flashlight.TurnOnAsync(); }
                Thread.Sleep(1000);
            }           
        }

    }
}

