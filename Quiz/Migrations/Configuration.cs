namespace Quiz.Migrations
{
    using Quiz.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Management;

    internal sealed class QuizConfiguration : DbMigrationsConfiguration<Quiz.Models.QuizContext>
    {
        public QuizConfiguration()
        {
            new LogEvent("start quiz configuration").Raise();
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Quiz.Models.QuizContext context)
        {
            //System.Data.Entity.Database.SetInitializer(
            //    new MigrateDatabaseToLatestVersion<QuizContext, QuizConfiguration>());
            context.SaveChanges();
        }
    }

    public class LogEvent : WebRequestErrorEvent
    {
        public LogEvent(string message)
            : base(null, null, 100001, new Exception(message))
        {
        }
    }
}
