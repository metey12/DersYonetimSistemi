using Microsoft.AspNetCore.Identity;

namespace DersYonetimSistemi.Models;

public class AppUser : IdentityUser<int>
{
    public string AdSoyad { get; set; } = null!;
    public bool OgretmenMi { get; set; }
}