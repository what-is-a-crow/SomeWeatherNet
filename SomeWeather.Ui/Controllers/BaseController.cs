using System.Web.Mvc;
using SomeWeather.Core;
using StructureMap;

namespace SomeWeather.Ui.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Title = ObjectFactory.GetInstance<IConfigurationService>().Get(Constants.Config.SiteTitle);

            base.OnActionExecuting(filterContext);
        }
    }
}