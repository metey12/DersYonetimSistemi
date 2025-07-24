using System.ComponentModel.DataAnnotations;

namespace DersYonetimSistemi.Models;

public class HaftaEkleModel
{
    public int DersId { get; set; }

    [Required(ErrorMessage = "Hafta numarası zorunludur.")]
    [Range(1, int.MaxValue, ErrorMessage = "Hafta numarası 1 veya daha büyük olmalıdır.")]
    public int Sayi { get; set; }

    [Required(ErrorMessage = "Açıklama zorunludur.")]
    public string Aciklama { get; set; }
}