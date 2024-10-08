using System.ComponentModel.DataAnnotations;

namespace CPM_Project.Models
{
    public class LoginUser
    {        
        [Display(Name = "Username")]
        public string USER_NAME { get; set; }

        [Display(Name = "Password")]
        public string PASSWORD { get; set; }
    }
}