using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiz.Models;

namespace Quiz.Controllers
{
   // [Authorize(Roles = "Administrator")]
    public class QuestionController : Controller
    {
        private QuizContext db = new QuizContext();

        //
        // GET: /Question/

        public ActionResult Index()
        {
            return View(db.Questions.Include(q => q.Category).ToList());
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // GET: /Question/Create

        public ActionResult Create()
        {
            PopulateCategoryDropDownList();
            //ViewBag.categoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        //
        // POST: /Question/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question)
        {
            /*Question q = new Question();
            q.QuestionText = question.GetValue("QuestionText").AttemptedValue;
            q.AnswerA = question.GetValue("AnswerA").AttemptedValue;
            q.AnswerB = question.GetValue("AnswerB").AttemptedValue;
            q.AnswerC = question.GetValue("AnswerC").AttemptedValue;
            q.AnswerD = question.GetValue("AnswerD").AttemptedValue;
            q.Correct = Int16.Parse(question.GetValue("Correct").AttemptedValue);
            q.CategoryId = Int16.Parse(question.GetValue("CategoryId").AttemptedValue);
            q.Category = db.Categories.Find(q.CategoryId);

            db.Questions.Add(q);
            db.SaveChanges();
            return RedirectToAction("Index");*/

            question.CategoryId = question.Category.CategoryId;
            question.Category = db.Categories.Find(question.Category.CategoryId);
            db.Questions.Add(question);
            db.SaveChanges();
            return RedirectToAction("Index");

           /* try
            {
                //PopulateCategoryDropDownList();
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (ModelState.IsValid)
                {
                    db.Questions.Add(question);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                ModelState.AddModelError("",dex.Message);
            }
           // ViewBag.categoryId = new SelectList(db.Categories, "CategoryId", "Name", question.Category.CategoryId);
            PopulateCategoryDropDownList(question.Category.CategoryId);
            return View(question);*/

        }

        //
        // GET: /Question/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            if (question.Category != null)
            {
                ViewBag.categoryId = new SelectList(db.Categories, "CategoryId", "Name", question.Category.CategoryId);
            }
            else
            {
                ViewBag.categoryId = new SelectList(db.Categories, "CategoryId", "Name");
            }
            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question)
        {

            /*question.CategoryId = question.Category.CategoryId;
            question.Category = db.Categories.Find(question.Category.CategoryId);
            db.Entry(question).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");*/


            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Question/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var categoryQuery = from c in db.Categories
                                   orderby c.Name
                                   select c;
            ViewBag.CategoryId = new SelectList(categoryQuery, "CategoryId", "Name", selectedCategory);
        }

    }

}