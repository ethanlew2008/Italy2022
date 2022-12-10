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
            Box.Text = "1. IRE\n2. GBR";
            NightorDay();
            Client.GetGBP(); ClientUSD.GetUSD();
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
                if (uk) { Box.Text = "That's about £" + Convert.ToString(Math.Round(Convert.ToDouble(input) / Convert.ToDouble(Client.varsyr),2)); } else { Box.Text = "That's about €" + Math.Round(Convert.ToDouble(input), 2); }
            }
            catch (Exception) { } input = "";      
        }



        private void ButtonSOS_Clicked(object sender, EventArgs e)
        {
            try { Box.Text = ""; PhoneDialer.Open("112"); }
            catch (Exception) { Box.Text = "Any Emergency: 112"; }
            input = "";
            SOSUPDATE = true;
            if(DateTime.Now.Hour >= 18) { FLash3Async(); }
        }

        private void ButtonDateFly_Clicked(object sender, EventArgs e)
        {
            if (!flighttime.IsRunning) { flighttime.Start(); }
            if (flighttime.IsRunning)
            {
                int flightm = Convert.ToInt32(flighttime.Elapsed.TotalMinutes);
                if (uk) { flightm = 140 - flightm; } else { flightm = 195 - flightm; }
                TimeSpan spWorkMin = TimeSpan.FromMinutes(Convert.ToInt32(flightm));
                Box.Text = spWorkMin.ToString(@"hh\:mm");
                Box.Text += "\n" + Convert.ToInt32(100 - (flighttime.Elapsed.TotalMinutes / flightm * 100)) + "% Left";
                input = "";
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
            string x;
            if (uk) { x = "\nLondon: "; } else { x = "\nDublin: "; }
            if(DateTime.Now.Month > 3 && DateTime.Now.Month < 11) { Box.Text = "Rome: " + DateTime.UtcNow.AddHours(2).ToString("HH:mm") + x + DateTime.UtcNow.AddHours(1).ToString("HH:mm"); }
            else { Box.Text = "Rome: " + DateTime.UtcNow.AddHours(1).ToString("HH:mm") + x + DateTime.UtcNow.ToString("HH:mm"); }
            input = "";
        }

        private void ButtonFlash_Clicked(object sender, EventArgs e)
        {
            //FLash2Async();
            SpeakIT();
        }
        
        public async void SpeakIT()
        {
            DateTime futurDate = Convert.ToDateTime("29/10/2022");
            DateTime TodayDate = DateTime.Now;

            if (before) { await TextToSpeech.SpeakAsync("There are currently" + Convert.ToInt32(futurDate-TodayDate) + "Days until Italy"); }


            if (DateTime.Now.Hour >= 12) 
            {
                await TextToSpeech.SpeakAsync("Good Afternoon,");
                await TextToSpeech.SpeakAsync("It's" + DateTime.Now.ToString("hh: mm") + "pm, on " + DateTime.Now.DayOfWeek);
            }
            else 
            {
                await TextToSpeech.SpeakAsync("Good Morning,"); 
                await TextToSpeech.SpeakAsync("It's" + DateTime.Now.ToString("hh: mm") + "am, on " + DateTime.Now.DayOfWeek);
            }

            if(ClientUSD.varsyrUSD != null) await TextToSpeech.SpeakAsync("The Euro is currently worth" + Math.Round(Convert.ToDouble(ClientUSD.varsyrUSD),2) + "Dollars");

            await TextToSpeech.SpeakAsync("Right Now, your Phone is on " + Convert.ToInt32(Battery.ChargeLevel * 100) + "percent");

            
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


           

            await TextToSpeech.SpeakAsync("You are at a height of " + altit + "ft and you are " + Convert.ToInt32(dist) + "miles away from Rome") ;
            await TextToSpeech.SpeakAsync("Have a great Day");

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

