using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOS.Entities.Models
{
   public class Country :BaseEntity
    {
        public string ZipCode { get; set; }
        public int Address { get; set; }
    }
}
