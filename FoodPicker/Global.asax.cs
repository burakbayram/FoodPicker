using Autofac;
using Autofac.Integration.Mvc;
using BLL;
using DAL;
using Service;
using ServiceFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FoodPicker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();

            builder.RegisterType<FoodService>().As<IFoodService>();
            builder.RegisterType<RestaurantService>().As<IRestaurantService>();


            builder.RegisterGeneric(typeof(BaseRepository<>))
           .As(typeof(IRepository<>));
            //  builder.RegisterType<FoodRepository>().As<IFoodRepository>();
            //  builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
