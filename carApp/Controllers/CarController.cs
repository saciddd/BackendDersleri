using carApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace carApp.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.student = "Sacit Polat";
            return View(Repository.Cars);
        }
        public IActionResult addCar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addCar(Cars araba)
        {
            Repository.addCar(araba);
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return Redirect("/Car/Index");
            }
            Cars? car = Repository.GetById(id);
            if (car == null)
            {
                return RedirectToAction("Index");
            }


            return View(car);
        }

    }
}