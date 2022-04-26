using System;
using System.Collections.Generic;
using EnUygun.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnUygun.Services.Provider
{
    public class TwoProvider : BaseProvider<JobsModel>
    {
        public TwoProvider() : base(new Uri("http://www.mocky.io/v2/5d47f235330000623fa3ebf7")) { }
        public async override Task<List<JobsModel>> GetDataAsync()
        {
            var json = await GetJsonString();
            var data = JsonConvert.DeserializeObject<List<JObject>>(json);

            return data.Select(x =>
            {
                var pros = x.Properties().FirstOrDefault();
                var id = pros.Name;
                var sure = x.First.First.SelectToken("estimated_duration");
                var zorluk = x.First.First.SelectToken("level");
                return new JobsModel
                {
                    id = pros.Name,
                    sure = Convert.ToInt32(sure),
                    zorluk = Convert.ToInt32(zorluk),
                };
            }).ToList();
        }
      
    }
}
