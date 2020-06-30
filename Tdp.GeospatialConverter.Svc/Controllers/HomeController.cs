using System.Configuration;
using System.Web.Mvc;

namespace Tdp.GeospatialConverter.Svc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.BuildNumber = ConfigurationManager.AppSettings["BuildNumber"];
           

            return View();
        }
    }
}