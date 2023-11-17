using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blogApp.Models;

namespace blogApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    //Ana sayfa (Index)
    public IActionResult Index()
    {
        return View();
    }
    //Spor Sayfası
    public IActionResult Spor()
    {
        return View();
    }
    //Edebiyat Sayfası
    public IActionResult Literature()
    {
        return View();
    }
    //Teknoloji Sayfası
    public IActionResult Tech()
    {
        return View();
    }

    //Login Sayfası
    public IActionResult Login()
    {
        return View();
    }
    // Login işlemi
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Kullanıcı adı veya şifre boş mu diye kontrol et, hata ver, sayfayı yenile
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ViewBag.ErrorMessage = "Kullanıcı adı ve şifre boş olamaz!";
            return View();
        }

        // Giriş işlemleri burada gerçekleştirilir (örneğin, veritabanı kontrolü)

        // Başarılı giriş durumunda başka bir sayfaya yönlendirme yapabilirsiniz
        return RedirectToAction("Index", "Home");
    }

    //Register Sayfası
    public IActionResult Register()
    {
        return RedirectToAction("Create", "User");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
