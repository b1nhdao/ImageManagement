using Microsoft.AspNetCore.Mvc;

namespace ImageManagement.Api.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
