using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnUygun.Entities
{
    [NotMapped]
    public class JobsModel
    {

        public int zorluk { get; set; }
        public int sure { get; set; }
        public string id { get; set; }
    }
}
