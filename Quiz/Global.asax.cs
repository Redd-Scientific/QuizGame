using Quiz.Migrations;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;

namespace Quiz
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            System.Data.Entity.Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<QuizContext, QuizConfiguration>());

            //TODO: verify how to specify context for migration
            WebSecurity.InitializeDatabaseConnection("CategoryContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            QuizContext context = new QuizContext();
            context.SaveChanges();

        }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QuizContext, QuizConfiguration>());
        }*/
    }
}