using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using blogApp.Models;
using blogApp.Data;

namespace blogApp.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        public UserController(DataContext context) { _context = context; }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            model.UserRole = "Writer";
            _context.Users.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Success");
        }
        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı adı ve şifre kontrolü
                var user = await _context.Users
                    .Where(u => u.UserName == model.UserName && u.UserPassword == model.UserPassword)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    // Ana sayfaya yönlendirme
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı");
                }
                return RedirectToAction("Index", "Home");
            }
            return View("Error", "Home");
        }
        public IActionResult ShowError(string errorMessage)
        {
            return View("Error", errorMessage);
        }



        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}