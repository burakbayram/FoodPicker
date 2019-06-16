using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IFoodService
    {
        List<Food> GetAll();
        Food GetById(int id);
        void Create(Food entity);
        void Update(Food entity);
        void Delete(int id);
        Food RandomFood();
        Food HealtyRandomFood();
    }
}
