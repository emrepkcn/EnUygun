using System;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using EnUygun.Services.Provider.Interfaces;
using System.Collections.Generic;
using EnUygun.Entities.Messages;

namespace EnUygun.Services.Provider
{
    public class BaseProvider<T> : IProviderGetData<T> where T : class
    {
        public Uri Url { get; }
        public BaseProvider(Uri url)
        {
            this.Url = url;
        }

        protected async Task<string> GetJsonString()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Url.ToString());
                var statusCode = response.EnsureSuccessStatusCode();
                if (response.EnsureSuccessStatusCode().StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return ErrorMessageCode.CouldNotBeReceived.ToString();
            }
        }

        public async virtual Task<List<T>> GetDataAsync()
        {
            var json = await GetJsonString();
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
