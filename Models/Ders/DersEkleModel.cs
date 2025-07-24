using System.ComponentModel.DataAnnotations;

namespace DersYonetimSistemi.Models;

public class DersEkleModel
{
    [Required(ErrorMessage = "Ders adı giriniz.")]
    public string Ad { get; set; }
}