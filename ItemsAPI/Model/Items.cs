using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ItemsAPI.Model
{
  //  [Table("Item")]
    public class Items
    {
        [Key]
       public int ItemId { get; set; }

        [StringLength(50)]
      
        public string ItemName { get; set; }

        public double Price { get; set; }
    }
}
