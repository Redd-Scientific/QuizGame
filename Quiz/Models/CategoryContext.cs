using Quiz.Migrations;
using System.Data.Entity;
namespace Quiz.Models
{
}


namespace Quiz.Models
{
    public class CategoryContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Quiz.Models.CategoryContext>());

        public CategoryContext() : base("name=CategoryContext")
        {
        }

        //public DbSet<Category> Categories { get; set; }
    }

    public class QuizContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Quiz.Models.CategoryContext>());

        public QuizContext() : base("name=CategoryContext")
        {

        }

        //Server administration properties
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }

        //Game properties
        public DbSet<UserCategories> UserCategories { get; set; }
        public DbSet<UserQuestions> UserQuestions { get; set; }
        //public DbSet<Bet> Bets { get; set; }
        //public DbSet<BetChip> BetChips { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<QuizContext, QuizConfiguration>());

        }

    }
}
