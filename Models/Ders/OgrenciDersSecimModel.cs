using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DersYonetimSistemi.Models;

public class OgrenciDersSecimModel
{
    public int OgrenciId { get; set; }

    [Display(Name = "Alabileceğiniz Dersler")]
    public List<DersCheckboxItem> Dersler { get; set; } = new List<DersCheckboxItem>();
}

public class DersCheckboxItem
{
    public int DersId { get; set; }
    public string DersAdi { get; set; }
    public bool Secili { get; set; }
}
