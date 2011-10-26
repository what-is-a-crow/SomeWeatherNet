using System.Collections.Generic;
using SomeWeather.Db;
using SomeWeather.Domain;

namespace SomeWeather.Services
{
    public interface IPortfolioService
    {
        IEnumerable<PortfolioItem> GetPortfolio();
        Response CreateItem(PortfolioItem item);
    }

    public class PortfolioService : IPortfolioService
    {
        private readonly IRepository<PortfolioItem> _repository;

        public PortfolioService(IRepository<PortfolioItem> repository)
        {
            _repository = repository;
        }

        public IEnumerable<PortfolioItem> GetPortfolio()
        {
            return _repository.GetAll();
        }

        public Response CreateItem(PortfolioItem item)
        {
            _repository.Save(item);
            _repository.SaveChanges();
            return new Response { Success = true };
        }
    }
}