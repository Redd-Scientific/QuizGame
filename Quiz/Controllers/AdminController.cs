﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Quiz.Models;

namespace Quiz.Controllers
{
    public class AdminController : ApiController
    {
        private QuizContext db = new QuizContext();

        // GET api/Admin
        public IEnumerable<Question> GetQuestions()
        {
            return db.Questions.AsEnumerable();
        }

        // GET api/Admin/5
        public Question GetQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return question;
        }

        // PUT api/Admin/5
        public HttpResponseMessage PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != question.QuestionId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Admin
        public HttpResponseMessage PostQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, question);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = question.QuestionId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Admin/5
        public HttpResponseMessage DeleteQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Questions.Remove(question);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, question);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}