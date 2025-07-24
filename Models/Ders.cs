namespace DersYonetimSistemi.Models;

public class Ders
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public int OgretmenId { get; set; }
    public Ogretmen Ogretmen { get; set; }

    public ICollection<Ogrenci> Ogrenciler { get; set; } = new List<Ogrenci>();
    public ICollection<Hafta> Haftalar { get; set; } = new List<Hafta>();
}