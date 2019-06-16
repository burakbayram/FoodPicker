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
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Restaurant> repository;

        private readonly IUnitOfWork unitofWork;
        public RestaurantService(IRepository<Restaurant> repository, IUnitOfWork unitofWork)
        {


            this.repository = repository;
            this.unitofWork = unitofWork;

        }
        public void Create(Restaurant entity)
        {
            repository.Create(entity);
            unitofWork.Save();

        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitofWork.Save();

        }

        public List<Restaurant> GetAll()
        {
            return repository.GetAll();
        }

        public Restaurant GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Update(Restaurant entity)
        {
            repository.Update(entity);
            unitofWork.Save();
        }
    }
   
}
