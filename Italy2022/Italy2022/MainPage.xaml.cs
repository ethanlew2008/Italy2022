﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Italy2022
{
    public partial class MainPage : ContentPage
    {
        string input = "";
        bool before = false;
        public MainPage()
        {
            InitializeComponent();

            if(DateTime.Now.Month < 10 && DateTime.Now.Year == 2022 || DateTime.Now.Month == 10 && DateTime.Now.Day < 29) { before = true; }

            if(DateTime.Now.Hour >= 21 || DateTime.Now.Hour < 9)
            {
                BackgroundImageSource = "appnightbackround.png";
                Button1.BackgroundColor = Color.Orange; Button2.BackgroundColor = Color.Orange;
                Button3.BackgroundColor = Color.Orange; Button4.BackgroundColor = Color.Orange;
                Button5.BackgroundColor = Color.Orange; Button6.BackgroundColor = Color.Orange;
                Button7.BackgroundColor = Color.Orange; Button8.BackgroundColor = Color.Orange;
                Button9.BackgroundColor = Color.Orange; Button0.BackgroundColor = Color.Orange;
                ButtonDot.BackgroundColor = Color.Orange; ButtonDel.BackgroundColor = Color.Orange;
                ConvertButton.BackgroundColor = Color.Orange;

                Button1.TextColor = Color.Black; Button3.TextColor = Color.Black;
                Button4.TextColor = Color.Black; Button6.TextColor = Color.Black;
                Button7.TextColor = Color.Black; Button9.TextColor = Color.Black;
                ButtonDot.TextColor = Color.Black; ButtonDel.TextColor = Color.Black;
                ConvertButton.TextColor = Color.Black;                
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
            try
            {
                input = input.Remove(input.Length - 1);
                Box.Text = input;
            }
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
    }
}
