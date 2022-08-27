﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using System.Threading;
using static Xamarin.Essentials.Permissions;

namespace Italy2022
{
    public partial class MainPage : ContentPage
    {
        string input = "";
        bool before = false;
        bool bypass = false;
        bool bypass2 = false;
        bool dev = false;
        bool uk = false;
        bool pregunta = false;
        static bool flash = true;
        Stopwatch flighttime = new Stopwatch();
        Stopwatch sleep = new Stopwatch(); double sleephours = 0;
        public MainPage()
        {
            InitializeComponent();
           
            if(DateTime.Now.Month < 10 && DateTime.Now.Year == 2022 || DateTime.Now.Month == 10 && DateTime.Now.Day < 29) { before = true; ButtonDateFly.Text = "Days"; }
            Box.Text = "1. IRE\n2. GBR";
            NightorDay();
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            input += "1"; Box.Text = input;
            if (pregunta == false) { uk = true; Datetime(); ConvertButton.Text = "GBP"; pregunta = true; input = ""; }             
        }

        private void Button2_Clicked(object sender, EventArgs e)
        {          
            input += "2"; Box.Text = input;
            if (pregunta == false) { uk = false; Datetime(); ConvertButton.Text = "EUR"; pregunta = true; input = ""; }
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

            double conversion = Convert.ToDouble(input);
            try
            {
                if (uk) 
                {
                    conversion /= 1.18;
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
                    int temp = 0;

                    if (uk) { temp = Convert.ToInt32(8100000 - flighttime.ElapsedMilliseconds); temp /= 1000; temp /= 60; }
                    else { temp = Convert.ToInt32(8700000 - flighttime.ElapsedMilliseconds); temp /= 1000; temp /= 60; }
                    
                    if(temp <= 0) { Box.Text = "Welcome To Italy"; flighttime.Reset(); return; }
                    TimeSpan spWorkMin = TimeSpan.FromMinutes(temp);
                    string workHours = spWorkMin.ToString(@"hh\:mm");
                    double percentage = Convert.ToDouble(flighttime.ElapsedMilliseconds) / 8100000; percentage *= 100; percentage = 100 - percentage;
                    Box.Text += workHours + "\n" + Math.Floor(percentage) + "% Left";

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
            if (DateTime.Now.Hour >= 21 || DateTime.Now.Hour < 9 || bypass2 == true)
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
                if (uk) { Box.Text += "\nLondon: " + uktime + ":" + minstring; }
                else { Box.Text += "\nDublin: " + uktime + ":" + minstring; }
                

            }
        }

        private async void ButtonFlash_Clicked(object sender, EventArgs e)
        {
            FLash2Async();
        }


        static async Task FLash2Async()
        {
            flash = !flash;
            if (flash) { await Xamarin.Essentials.Flashlight.TurnOffAsync(); }
            else { await Xamarin.Essentials.Flashlight.TurnOnAsync(); }
        }
    }
}
