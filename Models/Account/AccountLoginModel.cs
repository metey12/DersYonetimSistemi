using System.ComponentModel.DataAnnotations;

namespace DersYonetimSistemi.Models;


public class AccountLoginModel
{
    [Required]
    [Display(Name = "Eposta")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Parola")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Beni Hatırla")]
    public bool BeniHatirla { get; set; } = true;

}