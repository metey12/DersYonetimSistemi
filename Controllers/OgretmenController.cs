using System.Threading.Tasks;
using DersYonetimSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DersYonetimSistemi.Controllers;

[Authorize(Roles = "Ogretmen")]
public class OgretmenController : Controller
{
    private UserManager<AppUser> _userManager;

    private readonly DataContext _context;
    private readonly IWebHostEnvironment _env;



    public OgretmenController(DataContext context, IWebHostEnvironment env, UserManager<AppUser> userManager)
    {
        _context = context;
        _env = env;
        _userManager = userManager;
    }
    public async Task<ActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);

        var ogretmen = await _context.Ogretmenler
            .Include(o => o.Dersler)
            .FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);

        if (ogretmen == null) return NotFound();

        return View(ogretmen);
    }

    public async Task<ActionResult> Ders(int id)
    {
        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);

        var ogretmen = await _context.Ogretmenler
            .FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);

        if (ogretmen == null)
        {
            return Unauthorized();
        }

        var ders = await _context.Dersler
            .Include(d => d.Haftalar)
                .ThenInclude(w => w.Dosya)
            .Include(d => d.Ogretmen)
            .FirstOrDefaultAsync(d => d.Id == id && d.OgretmenId == ogretmen.Id);

        if (ders == null)
        {
            return NotFound();
        }

        return View(ders);
    }

    [HttpPost]
    public async Task<ActionResult> PdfYukle(int haftaId, IFormFile pdf)
    {
        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);

        var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);
        if (ogretmen == null) return Unauthorized();

        var hafta = await _context.Haftalar
            .Include(h => h.Ders)
            .FirstOrDefaultAsync(h => h.Id == haftaId && h.Ders.OgretmenId == ogretmen.Id);

        if (hafta == null) return Forbid();

        if (pdf != null && pdf.Length > 0)
        {
            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDir)) Directory.CreateDirectory(uploadsDir);

            var dosyaAdi = Guid.NewGuid() + Path.GetExtension(pdf.FileName);
            var dosyaYolu = Path.Combine(uploadsDir, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await pdf.CopyToAsync(stream);
            }

            var existing = await _context.DosyaKaynaklari.FirstOrDefaultAsync(f => f.HaftaId == haftaId);
            if (existing != null)
            {
                _context.DosyaKaynaklari.Remove(existing);
            }

            var file = new DosyaKaynagi
            {
                DosyaAdi = pdf.FileName,
                DosyaYolu = "/uploads/" + dosyaAdi,
                HaftaId = haftaId
            };

            _context.DosyaKaynaklari.Add(file);
            await _context.SaveChangesAsync();
        }


        var dersId = hafta.DersId;

        return RedirectToAction("Ders", new { id = dersId });
    }
    public ActionResult HaftaEkle(int dersId)
    {
        ViewBag.DersId = dersId;
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> HaftaEkle(HaftaEkleModel model, IFormFile pdf)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);

        var ders = await _context.Dersler
            .FirstOrDefaultAsync(d => d.Id == model.DersId && d.Ogretmen.AppUserId == parsedUserId);

        if (ders == null)
        {
            return Forbid();
        }

        var yeniHafta = new Hafta
        {
            DersId = model.DersId,
            Sayi = model.Sayi,
            Aciklama = model.Aciklama
        };

        _context.Haftalar.Add(yeniHafta);
        await _context.SaveChangesAsync();

        if (pdf != null && pdf.Length > 0)
        {
            var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDir)) Directory.CreateDirectory(uploadsDir);

            var dosyaAdi = Guid.NewGuid() + Path.GetExtension(pdf.FileName);
            var dosyaYolu = Path.Combine(uploadsDir, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await pdf.CopyToAsync(stream);
            }


            var file = new DosyaKaynagi
            {
                DosyaAdi = pdf.FileName,
                DosyaYolu = "/uploads/" + dosyaAdi,
                HaftaId = yeniHafta.Id
            };

            _context.DosyaKaynaklari.Add(file);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Ders", new { id = model.DersId });
    }

    public async Task<ActionResult> HaftaSil(int id)
    {
        if (id == null) return NotFound();

        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);

        var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);
        if (ogretmen == null) return Unauthorized();

        var hafta = await _context.Haftalar
            .Include(h => h.Ders)
            .FirstOrDefaultAsync(m => m.Id == id && m.Ders.OgretmenId == ogretmen.Id);

        if (hafta == null) return NotFound();

        return View(hafta);
    }

    [HttpPost]
    public async Task<ActionResult> HaftaSilOnay(int id)
    {
        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);

        var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);
        if (ogretmen == null) return Unauthorized();

        var hafta = await _context.Haftalar
            .Include(h => h.Ders)
            .FirstOrDefaultAsync(h => h.Id == id && h.Ders.OgretmenId == ogretmen.Id);

        if (hafta != null)
        {
            _context.Haftalar.Remove(hafta);
            await _context.SaveChangesAsync();
        }
        else
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    public ActionResult DersEkle()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> DersEkle(DersEkleModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);
        var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);

        if (ogretmen == null)
            return BadRequest("Öğretmen bulunamadı.");

        var ders = new Ders
        {
            Ad = model.Ad,
            OgretmenId = ogretmen.Id
        };

        _context.Dersler.Add(ders);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<ActionResult> DersOgrenciEkle(int dersId)
    {
        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);

        var ogretmen = await _context.Ogretmenler
            .Include(o => o.Dersler)
            .FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);

        if (ogretmen == null)
            return Unauthorized();

        if (!ogretmen.Dersler.Any(d => d.Id == dersId))
            return Forbid();

        var ders = await _context.Dersler
            .Include(d => d.Ogrenciler)
            .FirstOrDefaultAsync(d => d.Id == dersId);

        if (ders == null)
            return NotFound();

        var tumOgrenciler = await _context.Ogrenciler.ToListAsync();

        var model = new DersOgrenciEkleModel
        {
            DersId = ders.Id,
            DersAdi = ders.Ad,
            Ogrenciler = tumOgrenciler.Select(o => new OgrenciCheckboxItem
            {
                OgrenciId = o.Id,
                AdSoyad = o.AdSoyad,
                Secili = ders.Ogrenciler.Any(doğru => doğru.Id == o.Id)
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> DersOgrenciEkle(DersOgrenciEkleModel model)
    {
        var userId = _userManager.GetUserId(User);
        int parsedUserId = int.Parse(userId);

        var ogretmen = await _context.Ogretmenler
            .Include(o => o.Dersler)
            .FirstOrDefaultAsync(o => o.AppUserId == parsedUserId);

        if (ogretmen == null)
            return Unauthorized();

        if (!ogretmen.Dersler.Any(d => d.Id == model.DersId))
            return Forbid();

        var ders = await _context.Dersler
            .Include(d => d.Ogrenciler)
            .FirstOrDefaultAsync(d => d.Id == model.DersId);

        if (ders == null)
            return NotFound();

        var secilenOgrenciIdler = model.Ogrenciler.Where(o => o.Secili).Select(o => o.OgrenciId).ToList();

        ders.Ogrenciler.Clear();

        var secilenOgrenciler = await _context.Ogrenciler.Where(o => secilenOgrenciIdler.Contains(o.Id)).ToListAsync();
        foreach (var ogrenci in secilenOgrenciler)
        {
            ders.Ogrenciler.Add(ogrenci);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Ders", new { id = model.DersId });
    }
}
