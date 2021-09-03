using CardDetails.Data.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CardDetails.Core.Core.Services
{
    public class BinListIntegration : IBinListIntegration
    {
        private HttpClient _client;

        public BinListIntegration()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://lookup.binlist.net");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<CardDetail> GetCardDetail(CancellationToken cancellationToken, int Bin)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"{Bin}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.SendAsync(request, cancellationToken);


            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
            }
            
            var resp = await response.Content.ReadAsStringAsync();
            var ans = JsonConvert.DeserializeObject<CardDetail>(resp);
            return ans;

            //var stream = await response.Content.ReadAsStreamAsync();
            /*using (var streamReader = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize<CardDetail>(jsonTextReader);
                }
            }*/

        }
    }
}
