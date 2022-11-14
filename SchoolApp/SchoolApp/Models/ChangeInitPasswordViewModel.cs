using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class ChangeInitPasswordViewModel
    {
        [Required]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
