namespace DersYonetimSistemi.Models;

public class DosyaKaynagi
{
    public int Id { get; set; }
    public string DosyaAdi { get; set; }
    public string DosyaYolu { get; set; }

    public int HaftaId { get; set; }
    public Hafta Hafta { get; set; }

}