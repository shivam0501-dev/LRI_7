using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRI7_DataAccess.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public string name { get; set; }
        public string? imageUrl { get; set; }
    }
}
