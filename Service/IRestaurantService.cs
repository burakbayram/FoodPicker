using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public interface IRestaurantService
    {
        List<Restaurant> GetAll();
        Restaurant GetById(int id);
        void Create(Restaurant entity);
        void Update(Restaurant entity);
        void Delete(int id);
    }
}
