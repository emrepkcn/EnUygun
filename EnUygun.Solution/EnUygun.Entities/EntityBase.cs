using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnUygun.Entities
{
    public class EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Oluşturma Tarihi"), ScaffoldColumn(false), Required]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Güncelleme Tarihi"), ScaffoldColumn(false), Required]
        public DateTime ModifiedOn { get; set; }
    }
}
