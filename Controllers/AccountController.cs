using System.Threading.Tasks;
using DersYonetimSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_store.Controllers;

public class AccountController : Controller
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private RoleManager<AppRole> _roleManager;
    private DataContext _context;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, DataContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
    }
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(AccountCreateModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                AdSoyad = model.AdSoyad,
                OgretmenMi = model.OgretmenMi
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                string roleName = model.OgretmenMi ? "Ogretmen" : "Ogrenci";

                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new AppRole { Name = roleName });
                }

                await _userManager.AddToRoleAsync(user, roleName);

                if (model.OgretmenMi)
                {
                    var ogretmen = new Ogretmen
                    {
                        AdSoyad = model.AdSoyad,
                        AppUserId = user.Id
                    };
                    _context.Ogretmenler.Add(ogretmen);
                }
                else
                {
                    var ogrenci = new Ogrenci
                    {
                        AdSoyad = model.AdSoyad,
                        AppUserId = user.Id
                    };
                    _context.Ogrenciler.Add(ogrenci);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(AccountLoginModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.BeniHatirla, true);

                if (result.Succeeded)
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    var ogretmen = await _context.Ogretmenler.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
                    var ogrenci = await _context.Ogrenciler.FirstOrDefaultAsync(x => x.AppUserId == user.Id);

                    if (ogretmen != null)
                    {
                        return RedirectToAction("Index", "Ogretmen");
                    }
                    else if (ogrenci != null)
                    {
                        return RedirectToAction("Index", "Ogrenci");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (result.IsLockedOut)
                {
                    var lockOutDate = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockOutDate.Value - DateTime.UtcNow;
                    ModelState.AddModelError("", $"Hesabınız kitlendi. Lütfen {timeLeft.Minutes + 1} dakika sonra tekrar deneyiniz");
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı parola");
                }
            }
            else
            {
                ModelState.AddModelError("", "Hatalı email");
            }
        }

        return View(model);
    }


    [Authorize]
    public async Task<ActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    [Authorize]
    public ActionResult Settings()
    {
        return View();
    }

    public ActionResult AccessDenied()
    {
        return View();
    }
}