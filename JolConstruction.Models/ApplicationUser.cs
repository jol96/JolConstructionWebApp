using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JolConstruction.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int Name { get; set; }
    }
}
