using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOS.Entities.Models
{
   public class ApplicationUser : IdentityUser
    {
        public double Balance { get; set; }
    }
}
