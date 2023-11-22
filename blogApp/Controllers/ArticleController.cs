using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blogApp.Models;
using blogApp.Data;
using Microsoft.EntityFrameworkCore;


namespace blogApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly DataContext _context;
        public ArticleController(DataContext context) { _context = context; }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Article model, IFormFile fileInput)
        {
            if (fileInput != null && fileInput.Length > 0)
            {
                // Rasgele dosya adı oluştur
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileInput.FileName);

                // wwwroot/img klasörüne dosyayı kaydet
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileInput.CopyToAsync(fileStream);
                }

                // model.ArticleImage'e dosya adını ata
                model.ArticleImage = fileName;
            }
            //Hazır veriler ************ Değişecek
            model.ArticleWriterID = 1;
            model.ArticlePublicationTime = DateTime.Now;
            model.ArticleCategoryID = 1;

            _context.Articles.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Archive");
        }
        //Arşiv
        public async Task<IActionResult> Archive()
        {
            // Veritabanındaki tüm makaleleri al
            var articles = await _context.Articles.Include(a => a.Category).Include(a => a.ArticleWriter).ToListAsync();

            // Makaleleri view'e gönder
            return View(articles);
        }
        
        [HttpGet]
        public IActionResult Article(int? id)
        {
            var article = _context.Articles
            .Include(a => a.ArticleWriter)
            .Include(a => a.Category)
            .FirstOrDefault(i => i.ArticleID == id);
            if (article == null)
            {
                Console.WriteLine("Veri bulunamadı");
                return NotFound();
            }

            return View("Article", article);
        }
        public async Task<IActionResult> Category(int id)
        {
            // Veritabanındaki belirli bir kategoriye ait makaleleri al
            var articlesInCategory = await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.ArticleWriter)
                .Where(a => a.ArticleCategoryID == id)
                .ToListAsync();

            // Kategori bulunamazsa NotFound döndür
            if (!articlesInCategory.Any())
            {
                return NotFound();
            }

            // Makaleleri view'e gönder
            return View(articlesInCategory);
        }
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}