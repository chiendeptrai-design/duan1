using Microsoft.AspNetCore.Mvc;

namespace DuAn1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
