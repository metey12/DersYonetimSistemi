namespace DersYonetimSistemi.Models;

public class Hafta
{
    public int Id { get; set; }
    public int Sayi { get; set; }
    public string Aciklama { get; set; }
    public int DersId { get; set; }
    public Ders Ders { get; set; }
    public DosyaKaynagi? Dosya { get; set; }

}