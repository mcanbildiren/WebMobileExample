using System.ComponentModel.DataAnnotations;

namespace Identity101.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Yeni şifre alanı gereklidir!")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifreniz minimum 6 karakterli olmalıdır!")]
        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre Tekrar")]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler uyuşmuyor!")]
        public string ConfirmNewPassword { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
    }
}
