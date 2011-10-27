using System.Web.Mvc;

namespace SomeWeather.Ui.Controllers
{
    public class BlogController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}