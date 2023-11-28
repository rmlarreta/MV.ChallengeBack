using System.ComponentModel.DataAnnotations;
using WeatherRequest.Infraestructure.Interfaces;

namespace WeatherRequest.Infraestructure.Entities
{
    public class Entity : IEntity
    {
        private Guid _id;

        [Key]
        public Guid Id
        {
            get
            {
                if (_id == default || _id == Guid.Empty) _id = Guid.NewGuid();
                return _id;
            }
            set { _id = value; }
        }
    }
}
