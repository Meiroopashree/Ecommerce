using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    public class MenuController : Controller
    {
        [Route("menu/home")]
        public IActionResult Home()
        {
            return View("Home");
        }
        [Route("menu/customers")]

        public IActionResult Customers()
        {
            return View("Customers");
        }

        [Route("menu/products")]
        public IActionResult Products()
        {
            return View("Products");
        }

        [Route("menu/orders")]
        public IActionResult Orders()
        {
            return View("Orders");
        }
    }
}
