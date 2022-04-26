using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace EnUygun.Entities
{
    [Table("Jobs")]
    public class Job : EntityBase
    {
        public string TaskName { get; set; }
        [JsonProperty("name1")]
        public int TaskTime { get; set; }
        [JsonProperty("level")]
        public int TaskLevel { get; set; }
    }
}
