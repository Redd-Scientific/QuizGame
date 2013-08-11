using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quiz.Controllers
{
    public class GameQuestionsController : ApiController
    {
        QuizContext db = new QuizContext();
        private IQueryable<QuestionDTO> MapProducts()
        {
            return from q in db.Questions
                   select new QuestionDTO() { QuestionId = q.QuestionId, QuestionText = q.QuestionText,
                                              AnswerA = q.AnswerA,
                                              AnswerB= q.AnswerB,
                                              AnswerC = q.AnswerC,
                                              AnswerD = q.AnswerD,
                                              CategoryId = q.Category.CategoryId,
                                              Category = q.Category
                   };
        }

        public IEnumerable<QuestionDTO> GetQuestion()
        {
            return MapProducts().AsEnumerable();
        }

        public QuestionDTO GetQuestion(int id)
        {
            var question = (from q in MapProducts()
                            where q.QuestionId == id
                            select q).FirstOrDefault();
            if (question != null)
                return question;
            else
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
        }

        public IEnumerable<QuestionDTO> GetQuestionsByCategory(int category)
        {
            var questions = from q in MapProducts()
                            where q.Category.CategoryId == category
                            select q;
            return questions;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
