using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    public class BlogController : Controller
    {
        [Route("blog/home")]
        public IActionResult Home()
        {
            return View("Home");
        }

        [Route("blog/posts")]
        public IActionResult Posts()
        {
            return View("Posts");
        }

        [Route("blog/authors")]
        public IActionResult Authors()
        {
            return View("Authors");
        }

        [Route("blog/categories")]
        public IActionResult Categories()
        {
            return View("Categories");
        }
    }
}
