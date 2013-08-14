using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Quiz.Controllers
{
    public class GameBetController : ApiController
    {
        QuizContext db = new QuizContext();

        //public HttpResponseMessage PostBet(Bet bet)
        //{
        //    bet = db.Bets.Add(bet);
        //    var response = Request.CreateResponse<Bet>(HttpStatusCode.Created, bet);
        //    string uri = Url.Link("DefaultApi", new { id = bet.BetId });
        //    response.Headers.Location = new Uri(uri);
        //    return response;
        //}

    }
}
