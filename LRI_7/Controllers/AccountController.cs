using LRI7_Models;
using LRI7_Utility.Common;
using Microsoft.AspNetCore.Mvc;

namespace LRI_7.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Registration r)
        {
           
        }
    }
}
