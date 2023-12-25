using System.ComponentModel.DataAnnotations;

namespace DoAn_FrameWork.Areas.Admin.ViewModels
{
    public class RegisterViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]

        public string Role { get; set; }
    }
}
