﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;


namespace Italy2022
{

    internal class APIClient
    {
        HttpClient _httpClient;
        ReqVars vars = new ReqVars();
        public string varsyr { get; set; }
        public string boxtextAPI { get; set; }


        
        public APIClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<ReqVars> GetName()
        {

            Uri uri = new Uri("https://v6.exchangerate-api.com/v6/13190f03d445d8a2357ad591/pair/EUR/GBP");
            try
            {
                HttpResponseMessage rs = await _httpClient.GetAsync(uri);
                string rsStr = await rs.Content.ReadAsStringAsync();
                vars = JsonConvert.DeserializeObject<ReqVars>(rsStr);
                varsyr = Convert.ToString(vars.conversion_rate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return vars;
        }      
    }
}
