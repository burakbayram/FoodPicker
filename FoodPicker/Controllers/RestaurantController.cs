using BLL;
using Entity;
using Microsoft.AspNet.Identity;
using Service;
using ServiceFood;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FoodPicker.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        public ActionResult Index()
        {
            bool es = User.Identity.IsAuthenticated;

            List<Restaurant> restaurantList = restaurantService.GetAll();

            string strUserId = User.Identity.GetUserId();
            restaurantList = restaurantService.GetAll().Where(x => x.ApplicationUserId == strUserId).ToList();

            return View(restaurantList);
        }

        [Authorize]
        public ActionResult AddRestaurant()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRestaurant(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            { //validlik nullable mı. değilse ayarla
                string userId = User.Identity.GetUserId();
                restaurant.ApplicationUserId = userId;

                restaurantService.Create(restaurant);
              

                return RedirectToAction("Index", "Restaurant");
            }
            //validation ayarla

            //_uw.restRep.Create(restaurant);
            //_uw.Save();

            return RedirectToAction("Index", "AddRestaurant");
        }

        public ActionResult DeleteRestaurant(int id)
        {
            restaurantService.Delete(id);
          

            return RedirectToAction("Index", "Restaurant");
        }

        public ActionResult EditRestaurant(int? id)
        {
            if (!id.HasValue) //if int is null. We need to check this as we set id nullable
                return HttpNotFound();

            return View(restaurantService.GetById(id.Value));
        }
        [HttpPost]
        public ActionResult EditRestaurant(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
              restaurantService.Update(restaurant);
             

                return RedirectToAction("Index", "Restaurant");
            }

            return View(restaurant); //shows the last written values
        }
    }
}