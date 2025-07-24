using DersYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DersYonetimSistemi.Controllers;

public class OgrenciController : Controller
{
    private readonly DataContext _context;
    private UserManager<AppUser> _userManager;

    public OgrenciController(DataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<ActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);
        var ogrenci = await _context.Ogrenciler
            .Include(d => d.Dersler)
                .ThenInclude(o => o.Ogretmen)
            .FirstOrDefaultAsync(d => d.AppUserId == parsedUserId);

        if (ogrenci == null) return NotFound();

        return View(ogrenci);

    }


    public async Task<ActionResult> Ders(string dersAdi)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        int parsedUserId = int.Parse(userId);

        var ogrenci = await _context.Ogrenciler
            .Include(o => o.Dersler)
            .FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);

        if (ogrenci == null)
        {
            return NotFound();
        }

        var ders = await _context.Dersler
            .Include(d => d.Ogretmen)
            .Include(d => d.Haftalar)
                .ThenInclude(f => f.Dosya)
            .FirstOrDefaultAsync(d => d.Ad == dersAdi);

        if (ders == null)
        {
            return NotFound();
        }

        if (!ogrenci.Dersler.Any(d => d.Id == ders.Id))
        {
            return Forbid();
        }

        return View(ders);
    }

    [HttpGet]
    public async Task<ActionResult> DersSec()
    {
        var userId = _userManager.GetUserId(User);
        var ogrenci = await _context.Ogrenciler
            .Include(o => o.Dersler)
            .FirstOrDefaultAsync(o => o.AppUserId.ToString() == userId);

        if (ogrenci == null) return NotFound();

        var tumDersler = await _context.Dersler.ToListAsync();

        var model = new OgrenciDersSecimModel
        {
            OgrenciId = ogrenci.Id,
            Dersler = tumDersler.Select(d => new DersCheckboxItem
            {
                DersId = d.Id,
                DersAdi = d.Ad,
                Secili = ogrenci.Dersler.Any(od => od.Id == d.Id)
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> DersSec(OgrenciDersSecimModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var ogrenci = await _context.Ogrenciler
            .Include(o => o.Dersler)
            .FirstOrDefaultAsync(o => o.Id == model.OgrenciId);

        if (ogrenci == null) return NotFound();

        var secilenDersler = model.Dersler.Where(d => d.Secili).Select(d => d.DersId).ToList();

        ogrenci.Dersler.Clear();

        var dersler = await _context.Dersler.Where(d => secilenDersler.Contains(d.Id)).ToListAsync();
        foreach (var ders in dersler)
        {
            ogrenci.Dersler.Add(ders);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}