using System.ComponentModel.DataAnnotations.Schema;

namespace EnUygun.Entities
{
    [Table("Developers")]
   public class Developers : EntityBase
    {
        public string DevName { get; set; }
        public int DevTime { get; set; }
        public int DevLevel { get; set; }
    }
}
