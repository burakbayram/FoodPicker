using BLL;
using Entity;
using Microsoft.AspNet.Identity;
using Service;
using ServiceFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodPicker.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodService foodService;
        private readonly IRestaurantService restaurantService;
        public FoodController(IFoodService foodService,IRestaurantService restaurantService)
        {
            this.foodService = foodService;
            this.restaurantService = restaurantService;
        }


        public ActionResult Index()
        {
            bool es = User.Identity.IsAuthenticated;

            List<Food> foodList = foodService.GetAll();

            string strUserId = User.Identity.GetUserId();
            foodList = foodService.GetAll().Where(x => x.ApplicationUserId == strUserId).ToList();

            return View(foodList);
        }

        public ActionResult DeleteFood(int id)
        {
            foodService.Delete(id);
          

            return RedirectToAction("Index", "Food");
        }

        [Authorize]
        public ActionResult AddFood()
        {
            string strUserId = User.Identity.GetUserId();
            if (/*_uw.restRep.GetAll().Count == 0 && */ restaurantService.GetAll().Where(x => x.ApplicationUserId == strUserId).Count() == 0)
            {
                return RedirectToAction("AddRestaurant", "Restaurant");
            }

            //IEnumerable<Restaurant> restaurant = _uw.restRep.GetAll();            
            IEnumerable<Restaurant> restaurant = restaurantService.GetAll().Where(x => x.ApplicationUserId == strUserId).ToList();
            var restaurantList = restaurant.Select(x => new SelectListItem()
            {
                Text = x.RestaurantName,
                Value = x.Id.ToString()
            });
            ViewBag.restaurantList = restaurantList;

            ViewBag.foodTypes = new SelectList(Enum.GetValues(typeof(FoodType))
                .OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = Enum.GetName(typeof(FoodType), x),
                    Value = (Convert.ToInt32(x)).ToString()
                }), "Value", "Text");

            return View();
        }
        [HttpPost]
        public ActionResult AddFood(Food food, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength != 0)
            {
                var path = Server.MapPath("/Content/Uploads/");
                var filename = ImageFile.FileName;
                ImageFile.SaveAs(path + filename);
                food.ImageURL = filename;

                ViewBag.UploadedImage = ImageFile.FileName;
            }

            if (ModelState.IsValid) //checks if the model is valid
            {
                string userId = User.Identity.GetUserId();
                food.ApplicationUserId = userId;

              foodService.Create(food); //Add
               

                return RedirectToAction("Index", "Food"); //Go to Home
            }

            //We stil need DropDownList so we keep this here as the 
            //List<Restaurant> restaurant = _uw.restRep.GetAll();
            string strUserId = User.Identity.GetUserId();
            IEnumerable<Restaurant> restaurant = restaurantService.GetAll().Where(x => x.ApplicationUserId == strUserId).ToList();
            var restaurantList = restaurant.Select(x => new SelectListItem()
            {
                Text = x.RestaurantName,
                Value = x.Id.ToString()
            });
            ViewBag.RestaurantList = restaurantList;

            ViewBag.foodTypes = new SelectList(Enum.GetValues(typeof(FoodType))
                .OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = Enum.GetName(typeof(FoodType), x),
                    Value = (Convert.ToInt32(x)).ToString()
                }), "Value", "Text");

            return View(food);
        }

        public ActionResult EditFood(int? id)
        {
            if (!id.HasValue) //if int is null. We need to check this as we set id nullable
                return HttpNotFound();

            List<Restaurant> restaurant = restaurantService.GetAll();
            var restaurantList = restaurant.Select(x => new SelectListItem()
            {
                Text = x.RestaurantName,
                Value = x.Id.ToString()
            });
            ViewBag.RestaurantList = restaurantList;

            ViewBag.foodTypes = new SelectList(Enum.GetValues(typeof(FoodType))
                .OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = Enum.GetName(typeof(FoodType), x),
                    Value = (Convert.ToInt32(x)).ToString()
                }), "Value", "Text");

            return View(foodService.GetById(id.Value));
        }
        [HttpPost]
        public ActionResult EditFood(Food food)
        {
            if (ModelState.IsValid)
            {
                foodService.Update(food);
                

                return RedirectToAction("Index", "Food");
            }

            List<Restaurant> restaurant = restaurantService.GetAll();
            var restaurantList = restaurant.Select(x => new SelectListItem()
            {
                Text = x.RestaurantName,
                Value = x.Id.ToString()
            });
            ViewBag.RestaurantList = restaurantList;

            ViewBag.foodTypes = new SelectList(Enum.GetValues(typeof(FoodType))
                .OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = Enum.GetName(typeof(FoodType), x),
                    Value = (Convert.ToInt32(x)).ToString()
                }), "Value", "Text");

            return View(food); //shows the last written values
        }
    }
}