using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coshop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, Display(Name = "Display Name")]
        public string DisplayName { get; set; }
    }
}
