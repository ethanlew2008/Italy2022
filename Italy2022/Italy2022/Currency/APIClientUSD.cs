using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Italy2022
{
    internal class APIClientUSD
    {
        HttpClient _httpClientUSD;
        ReqVarsUSD varsUSD = new ReqVarsUSD();
        public string varsyrUSD { get; set; }

        public APIClientUSD()
        {
            _httpClientUSD = new HttpClient();
        }

        public async Task<ReqVarsUSD> GetUSD()
        {

            Uri uriUSD = new Uri("https://v6.exchangerate-api.com/v6/13190f03d445d8a2357ad591/pair/EUR/USD");
            try
            {
                HttpResponseMessage rsUSD = await _httpClientUSD.GetAsync(uriUSD);
                string rsStrUSD = await rsUSD.Content.ReadAsStringAsync();
                varsUSD = JsonConvert.DeserializeObject<ReqVarsUSD>(rsStrUSD);
                varsyrUSD = Convert.ToString(varsUSD.conversion_rate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return varsUSD;
        }
    }
}
