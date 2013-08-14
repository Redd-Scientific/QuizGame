using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quiz.Controllers
{
    public class GameUserQuestionsController : ApiController
    {
        QuizContext db = new QuizContext();
        public HttpResponseMessage PostProduct(UserQuestions item)
        {
            item.question = db.Questions.Find(item.QuestionId);
            item.user = db.UserProfiles.Find(item.UserId);
            if (item.question.Correct == item.answered)
            {
                item.correct = true;
            }
            else
            {
                item.correct = false;
            }
            item = db.UserQuestions.Add(item);
            db.SaveChanges();
            var response = Request.CreateResponse<UserQuestions>(HttpStatusCode.Created, item);
            return response;
        }
    }
}
