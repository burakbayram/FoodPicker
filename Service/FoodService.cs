using BLL;
using DAL;
using Entity;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFood
{

  
    public class FoodService : IFoodService
    {

        private readonly IRepository<Food> repository;
     
        private readonly IUnitOfWork unitofWork;
        public FoodService(IRepository<Food> repository, IUnitOfWork unitofWork)
        {
      

            this.repository = repository;
            this.unitofWork = unitofWork;

        }
        public void Create(Food entity)
        {
            repository.Create(entity);
            unitofWork.Save();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitofWork.Save();
        }

        public List<Food> GetAll()
        {
            return repository.GetAll();
        }

        public Food GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Update(Food entity)
        {
            repository.Update(entity);
            unitofWork.Save();
        }
        public Food RandomFood()
        {
            List<Food> foodList = repository.GetAll();///////dsdneme
            Random rnd = new Random();
            Food food = foodList.ElementAt(rnd.Next(foodList.Count()));
            return food;
        }
        //
        public Food HealtyRandomFood()
        {
            List<Food> healthyFood = repository.GetAll().Where(x => x.IsHealty == true).ToList();
            Random rnd = new Random();
            Food food = healthyFood.ElementAt(rnd.Next(healthyFood.Count()));
            return food;
        }
    }
}
