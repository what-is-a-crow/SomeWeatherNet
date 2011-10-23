using System.Web.Mvc;
using SomeWeather.Core;
using SomeWeather.Services;

namespace SomeWeather.Ui.Controllers
{
    public class MainController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public MainController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public ActionResult Index()
        {
            var model = _portfolioService.GetPortfolio();

            return View(Constants.Views.Portfolio, model);
        }
    }
}