using Casino.Models;
using Microsoft.AspNetCore.Mvc;

namespace Casino.Controllers
{
    public class RouletteController : Controller
    {
        public IActionResult Index()
        {
            Wheel w = new Wheel();

            return View(w.WheelNumbers);
        }
    }
}
