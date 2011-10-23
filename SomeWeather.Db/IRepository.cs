using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using SomeWeather.Domain;

namespace SomeWeather.Db
{
    public interface IRepository<T> where T : IEntity
    {
        T Get(int id);
        T Get(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Save(T entity);
        void Delete(T entity);
    }

    public class RavenRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IDocumentSession _session;

        public RavenRepository(IDocumentSession session)
        {
            _session = session;
        }

        public T Get(int id)
        {
            return Get(t => t.Id == id);
        }

        public T Get(Func<T, bool> predicate)
        {
            return _session.Query<T>().SingleOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _session.Query<T>();
        }

        public void Create(T entity)
        {
            _session.Store(entity);
        }

        public void Save(T entity)
        {
            _session.Store(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }
    }
}