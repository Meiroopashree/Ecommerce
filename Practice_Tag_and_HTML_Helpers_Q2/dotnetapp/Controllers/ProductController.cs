using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    public class ProductController : Controller
    {
        [Route("product/list")]
        public IActionResult List()
        {
            return View("List");
        }

        [Route("product/info")]
        public IActionResult Info()
        {
            return View("Info");
        }

        [Route("product/category")]
        public IActionResult Category()
        {
            return View("Category");
        }

        [Route("product/reviews")]
        public IActionResult Reviews()
        {
            return View("Reviews");
        }
    }
}
