using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    public class BookController : Controller
    {
        [Route("book/home")]
        public IActionResult Home()
        {
            return View("Home");
        }

        [Route("book/books")]
        public IActionResult Books()
        {
            return View("Books");
        }

        [Route("book/authors")]
        public IActionResult Authors()
        {
            return View("Authors");
        }

        [Route("book/categories")]
        public IActionResult Categories()
        {
            return View("Categories");
        }
    }
}
