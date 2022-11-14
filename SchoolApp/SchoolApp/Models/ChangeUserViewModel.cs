using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name  ="First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePictureFile { get; set; }

        public string profilepicturepath { get; set; }
    }
}
