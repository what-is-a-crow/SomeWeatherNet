using System;
using System.Web.Mvc;
using SomeWeather.Core;
using SomeWeather.Services;
using SomeWeather.Ui.Models;
using SomeWeather.Ui.Utility;

namespace SomeWeather.Ui.Controllers
{
    public class MainController : BaseController
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMailService _mailService;

        public MainController(IPortfolioService portfolioService, IMailService mailService)
        {
            _portfolioService = portfolioService;
            _mailService = mailService;
        }

        public ViewResult Index()
        {
            var model = _portfolioService.GetPortfolio();

            return View(Constants.Views.Portfolio, model);
        }

        public ViewResult About()
        {
            return View(Constants.Views.About);
        }

        public ViewResult Contact()
        {
            return View(Constants.Views.Contact);
        }

        [HttpPost]
        public ViewResult Contact(ContactModel model)
        {
            if (ModelState.IsValid)
                _mailService.Send(MessageTemplate.Expand(model));

            return View(Constants.Views.Contact, model);
        }

        public ViewResult Login()
        {
            return View(Constants.Views.Login);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            throw new NotImplementedException();
        }
    }
}