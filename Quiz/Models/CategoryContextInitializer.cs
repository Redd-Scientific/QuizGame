using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Quiz.Models
{
    public class QuizContextInitializer : DropCreateDatabaseIfModelChanges<QuizContext>
    {
        protected override void Seed(QuizContext context)
        {
            var category = new List<Category>()            
            {
                new Category() { Name = "Harry Potter" },
                new Category() { Name = "Lord of the Rings" },
                new Category() { Name = "Game of Thrones" }
            };

            category.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var question = new List<Question>() { 
            
                new Question() {QuestionText="What is Harry's house?", AnswerA="Gryffindor", AnswerB="Ravenclaw", AnswerC="Slytherin", AnswerD="Hufflepuff", 
                    Correct=1, categoryId=category[0].CategoryId, Category=category[0] },
                new Question() {QuestionText="What is Hermione's last name?", AnswerA="Perkins", AnswerB="Dickens", AnswerC="Granger", AnswerD="Dursley", 
                    Correct=3, categoryId=category[0].CategoryId, Category=category[0] },
                new Question() {QuestionText="Where should Frodo take the ring?", AnswerA="Mordor", AnswerB="Rivendell", AnswerC="Mount Doom", AnswerD="Shire", 
                    Correct=3, categoryId=category[1].CategoryId, Category=category[1] },
            };
            question.ForEach(q => context.Questions.Add(q));
            context.SaveChanges();
        }
        
    }
}