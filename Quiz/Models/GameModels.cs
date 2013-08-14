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

    public class BetChip
    {
        [Key]
        public int BetChipId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }

        public BetChip(string N, int V, int Q)
        {
            Name = N;
            Value = V;
            Quantity = Q;
        }
    }

    public class Bet
    {
        [Key]
        public int BetId { get; set; }
        public ICollection<BetChip> chips { get; set; }
        public int totalBet
        {
            get { return totalBet; }
            set 
            {
                int totalValue = 0;
                foreach (BetChip b in chips)
                {
                    totalValue += b.Value * b.Quantity;
                }
                value = totalValue;
            }

        }
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
        [ForeignKey("betAmount")]
        public int BetId;
        public int answered { get; set; }
        public bool correct { get; set; }

        //navigation properties
        public UserProfile user { get; set; }
        public Question question { get; set; }
        public Bet betAmount { get; set; }
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
        public Category question { get; set; }
    }

}