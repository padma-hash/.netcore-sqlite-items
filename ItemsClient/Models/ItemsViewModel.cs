using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ItemsClient.Models
{
    public class ItemsViewModel
    {
        
        public int ItemId { get; set; }

       
        public string ItemName { get; set; }

        public double Price { get; set; }

        public string DeleteUri { get; set; }
    }
}
