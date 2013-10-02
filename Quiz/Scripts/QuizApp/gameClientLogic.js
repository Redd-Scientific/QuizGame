/// <reference path="../jquery-1.8.2.min.js" />
/// <reference path="../knockout-2.3.0.debug.js" />
/// <reference path="jquery.countdown.js" />
/// <reference path="knockout.localStorage.js" />

$.ready(function () {
    console.log("heloo");
    $("#betamt").text("100");
});

function addBet(amt) {
    var qid = $("#QuestionId").val();
    var bet = parseInt($("#betamt").text());
    bet -= amt;
    $("#betAmt").text(bet);
    $.ajax({
        type: 'POST',
        url: "/Game/SubmitBet",
        //url: "quiz-6.apphb.com/Game/SubmitBet",
        data: {
            "questionId": parseInt(qid),
            "betAmt": amt
        },
        success: function (result) {
            if (result.redirectToUrl != null) {
                window.location.href = result.redirectToUrl;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console && console.log ("request failed");
        }
    });
}

function answer(ans) {
    $.ajax({
        type: 'POST',
        url: "/Game/SubmitAnswer",
        data: {
            "answer":ans
        },
        success: function (result) {
            if (result.redirectToUrl != null) {
                window.location.href = result.redirectToUrl;
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console && console.log("request failed");
        }
    });
}



/**
Knockout JS for Bet
*/

function Bet(data)
{
    this.BetAmt = ko.observable(data.BetAmt);
}

function BetViewModel() {
    var self = this;
    // self.BetAmt = new Bet({BetAmt:100});
    self.BetAmt = ko.observable();
    $.getJSON("/Game/getBet").done( function (data) {
        console.log(data);
        // self.BetAmt = new Bet(data);
        self.BetAmt(data.BetAmt);
    });
}

ko.applyBindings(new BetViewModel(), document.getElementById('betSection'));






