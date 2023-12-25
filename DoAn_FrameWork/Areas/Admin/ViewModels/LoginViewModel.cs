using System.ComponentModel.DataAnnotations;

namespace DoAn_FrameWork.Areas.Admin.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAdress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}
