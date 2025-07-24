using System.ComponentModel.DataAnnotations;

namespace DersYonetimSistemi.Models;

public class AccountCreateModel
{
    [Required]
    [Display(Name = "Ad Soyad")]
    //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Sadece sayı ve harf giriniz.")]
    public string AdSoyad { get; set; } = null!;

    [Required]
    [Display(Name = "Eposta")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Parola")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Display(Name = "Parola Doğrula")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Parola eşleşmiyor.")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "Öğretmen Misiniz?")]
    public bool OgretmenMi { get; set; }
}