using System;
using Raven.Client;
using SomeWeather.Core;
using StructureMap;

namespace SomeWeather.Db
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession _session;

        public UnitOfWork(IDocumentStore documentStore, IConfigurationService configService)
        {
            var dbUrl = configService.Get(Constants.Config.DbUrl);
            _session = documentStore.OpenSession(dbUrl);
        }

        public void Commit()
        {
            _session.SaveChanges();
        }

        public static IUnitOfWork Begin()
        {
            return ObjectFactory.GetInstance<IUnitOfWork>();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}