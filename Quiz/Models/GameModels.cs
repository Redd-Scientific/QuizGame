using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quiz.Models
{
    public class QuestionDTO
    {
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
        public List<BetChip> chips { get; set; }
        public int getTotalBet()
        {
            chips.Add(new BetChip("25", 25, 2));
            int totalValue = 0; 
            foreach (BetChip b in chips)
            {
                totalValue += b.Value * b.Quantity;
            }
            return totalValue;
        }
    }

    public class UserQuestions
    {
        public Bet betAmount { get; set; }
        public int answered { get; set; }
        public bool correct { get; set; }

        public int UserId;
        public int QuestionId;

        //navigation properties
        public UserProfile user;
        public Question question;
    }

    public class UserCategories
    {
        public int totalQuestionsAnswered { get; set; }

        public int UserId;
        public int CategoryId;

        //navigation properties
        public UserProfile user;
        public Category question;
    }

    public class UserGame
    {
        public Bet AmountLeft { get; set; }

        public int UserId;

        //navigation properties
        public UserProfile user;
    }




}