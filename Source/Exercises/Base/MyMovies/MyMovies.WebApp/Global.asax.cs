using System;
using System.Collections.Generic;
using System.Data.Entity.Database;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyMovies.DependencyResolution;
using MyMovies.DomainModel;
using MyMovies.WebApp.Models;

namespace MyMovies.WebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Comments", // Route name
                "Movies/{movieId}/{action}/{commentId}", // URL with parameters
                new { controller = "Movies", commentId = UrlParameter.Optional }, // Parameter defaults,
                new { movieId = @"\d+" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Movies", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            DbDatabase.SetInitializer(new MoviesInitializer());
            AppStart_Structuremap.Start();
            DbDatabase.SetInitializer<MovieDbContext>(new MoviesInitializer());


            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}