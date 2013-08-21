namespace Quiz.Migrations
{
    using Quiz.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class QuizConfiguration : DbMigrationsConfiguration<Quiz.Models.QuizContext>
    {
        public QuizConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Quiz.Models.QuizContext context)
        {
            context.SaveChanges();
        }
    }
}
