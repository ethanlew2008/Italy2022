using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;

namespace Italy2022
{
    public partial class MainPage : ContentPage
    {
        string input = "";
        bool before = false;
        bool bypass = false;
        bool dev = false;
        Stopwatch flighttime = new Stopwatch();
        public MainPage()
        {
            InitializeComponent();
           
            if(DateTime.Now.Month < 10 && DateTime.Now.Year == 2022 || DateTime.Now.Month == 10 && DateTime.Now.Day < 29) { before = true; ButtonDateFly.Text = "Days"; }
            Datetime();

            if (DateTime.Now.Hour >= 21 || DateTime.Now.Hour < 9)
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

                Button1.TextColor = Color.Black; Button3.TextColor = Color.Black;
                Button4.TextColor = Color.Black; Button6.TextColor = Color.Black;
                Button7.TextColor = Color.Black; Button9.TextColor = Color.Black;
                ButtonDot.TextColor = Color.Black; ButtonDel.TextColor = Color.Black;
                ConvertButton.TextColor = Color.Black;ButtonDateFly.TextColor = Color.Black;
                ButtonTime.TextColor = Color.Black;
            }
            else
            {
                BackgroundImageSource = "appdaybackround.png";
            }
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            input += "1"; Box.Text = input;
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
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
                double conversion = Convert.ToDouble(input) / 1.19;
                conversion = Math.Round(conversion, 2);
                Box.Text = "That's about £" + conversion;
                input = "";
            }
            catch (Exception) { Box.Text = "Error"; input = ""; return; }
        }

        

        private void ButtonSOS_Clicked(object sender, EventArgs e)
        {
            try { PhoneDialer.Open("112"); }
            catch (Exception) { Box.Text = "Any Emergency: 112"; }
            input = "";
        }      

        private void ButtonDateFly_Clicked(object sender, EventArgs e)
        {
            input = "";
            if(Box.Text == "1622") { dev = true; Box.Text = ""; ButtonDateFly.Text = "Flight"; }
            if (before && dev == false) { Datetime(); }
            else
            {
                if (!flighttime.IsRunning) { flighttime.Start(); Box.Text = "Flight Started"; }
                else
                {
                    Box.Text = ""; 
                    int temp = Convert.ToInt32(flighttime.ElapsedMilliseconds-8100000); temp /= 1000; temp /= 60; temp /= 60; temp *= -1;
                    TimeSpan spWorkMin = TimeSpan.FromMinutes(temp);
                    string workHours = spWorkMin.ToString(@"hh\:mm");
                    double percentage = Convert.ToDouble(flighttime.ElapsedMilliseconds); percentage /= 8100000;
                    Box.Text += workHours + "\n" + Math.Floor(percentage) + "%";

                }
            }
        }

        private void ButtonTime_Clicked(object sender, EventArgs e)
        {
            bypass = true;
            Datetime();
        }

        public void Datetime()
        {
            input = ""; Box.Text = "";
            if (before && bypass == false)
            {
                DateTime futurDate = Convert.ToDateTime("29/10/2022");
                DateTime TodayDate = DateTime.Now;
                Box.Text += Convert.ToInt32((futurDate - TodayDate).TotalDays); Box.Text += " Days";
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
                Box.Text += "\nLondon: " + uktime + ":" + minstring;
                
            }
        }
    }
}
