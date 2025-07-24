using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DersYonetimSistemi.Models;

public class DersOgrenciEkleModel
{
    public int DersId { get; set; }
    public string DersAdi { get; set; }

    public List<OgrenciCheckboxItem> Ogrenciler { get; set; } = new List<OgrenciCheckboxItem>();
}

public class OgrenciCheckboxItem
{
    public int OgrenciId { get; set; }
    public string AdSoyad { get; set; }
    public bool Secili { get; set; }
}