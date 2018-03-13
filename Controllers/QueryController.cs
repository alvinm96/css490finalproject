using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProject
{
    public class QueryController : Controller
    {

        public ActionResult Queries()
        {
            string[] result = new string[] { "" };
            ViewBag.Results = result;
            return View();
        }

        public ActionResult GetGroups()
        {
            string[] result = new string[] { "asd", "asda", "asdas", "asdasf" };
            ViewBag.Results = result;
            return View("Queries");
        }

        public ActionResult GetPosts()
        {
            return View("Queries");
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
