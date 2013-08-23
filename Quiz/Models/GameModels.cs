using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class QuestionDTO
    {
        [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }

        public int CategoryId;

        public Category Category;
    }

    public class QuestionBetDTO
    {
        public int QuestionID;
        public int Bet;
    }

    public class QuestionAnswerDTO
    {
        public int QuestionID;
        public int Answer;
    }


    public class UserQuestions
    {
       // [Key]
        //public int UserQuestionId { get; set; }
        [Key, Column(Order = 0)]
        [ForeignKey("user")]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("question")]
        public int QuestionId { get; set; }
        [Range (0,4)]
        public int answered { get; set; }
        public bool correct { get; set; }
        public int betAmount { get; set; }
        //navigation properties
        public UserProfile user { get; set; }
        public Question question { get; set; }
    }

    public class UserCategories
    {
        public int totalQuestionsAnswered { get; set; }
        //[Key]
        //public int UserCategoryId { get; set; }
        [Key, Column(Order = 0)]
        //[ForeignKey("UserProfile")]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        //[ForeignKey("Category")]
        public int CategoryId { get; set; }

        //navigation properties
        public UserProfile user {get; set;}
        public Category category { get; set; }
    }

    public class UserBet
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        public int BetAmt { get; set; }
        //navigation properties
        public UserProfile user { get; set; }
    }

}