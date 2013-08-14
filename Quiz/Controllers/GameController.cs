using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Quiz.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        QuizContext db = new QuizContext();

        public ActionResult StartGame()
        {
            return View();
        }

        public ActionResult DisplayCategory()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            var guid = user.ProviderUserKey;

            var get_all_questions_answered_query = from q in db.UserQuestions
                                                   where q.UserId == (int)guid
                                                   select q;

            var get_next_question_not_answered = from q in db.Questions
                                                 where !(get_all_questions_answered_query).Any(quest => quest.QuestionId == q.QuestionId)
                                                 select new QuestionDTO()
                                                 {
                                                     QuestionId = q.QuestionId,
                                                     QuestionText = q.QuestionText,
                                                     AnswerA = q.AnswerA,
                                                     AnswerB = q.AnswerB,
                                                     AnswerC = q.AnswerC,
                                                     AnswerD = q.AnswerD,
                                                     CategoryId = q.Category.CategoryId,
                                                     Category = q.Category
                                                 };
            return View(get_next_question_not_answered.FirstOrDefault());
        }

    }
}
