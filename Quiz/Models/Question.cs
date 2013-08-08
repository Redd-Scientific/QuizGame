using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models
{
    public class Question
    {
        [ScaffoldColumn(false)]
        public int QuestionId { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public string AnswerA { get; set; }
        [Required]
        public string AnswerB { get; set; }
        [Required]
        public string AnswerC { get; set; }
        [Required]
        public string AnswerD { get; set; }
        [Required]
        public int Correct { get; set; }
        
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        [Required]
        public int CategoryId;

        //Navigation property
        public virtual Category Category { get; set; }
    }
}