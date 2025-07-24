namespace DersYonetimSistemi.Models;

public class Ogretmen
{
    public int Id { get; set; }
    public string AdSoyad { get; set; }

    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public ICollection<Ders> Dersler { get; set; }
}