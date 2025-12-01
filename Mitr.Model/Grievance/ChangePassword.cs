using System.ComponentModel.DataAnnotations;

namespace Mitr.Model
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Old Password Can't Blank")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password Can't Blank")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password Can't Blank")]
        public string ConfirmPassword { get; set; }
        public string IPAddress { get; set; }
        public string Token { get; set; }
        public long LoginID { get; set; }
    }
}
