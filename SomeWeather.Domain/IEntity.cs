using System;

namespace SomeWeather.Domain
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }

    public abstract class Entity : IEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}