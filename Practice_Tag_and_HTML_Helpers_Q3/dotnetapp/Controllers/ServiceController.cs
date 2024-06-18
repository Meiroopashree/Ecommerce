using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    public class ServiceController : Controller
    {
        [Route("service/overview")]
        public IActionResult Overview()
        {
            return View("Overview");
        }

        [Route("service/details")]
        public IActionResult Details()
        {
            return View("Details");
        }

        [Route("service/pricing")]
        public IActionResult Pricing()
        {
            return View("Pricing");
        }

        [Route("service/testimonials")]
        public IActionResult Testimonials()
        {
            return View("Testimonials");
        }
    }
}
