namespace Quiz.Migrations
{
    using Quiz.Models;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            new LogEvent("in up").Raise();
            QuizContext context = new QuizContext();
            context.SaveChanges();
            //context.Database.Initialize();
        }
        
        public override void Down()
        {
        }
     
    }
}
