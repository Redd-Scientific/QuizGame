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

/* timer section code */
function Timer(data) {
    this.TimeLeft = ko.observable(data.TimeLeft);
}

function TimerViewModel() {
    var self = this;
    var start, elapsed;
    self.timer;
    // self.BetAmt = new Bet({BetAmt:100});
    self.TimeLeft = ko.observable(10, { persist: 'timeLeft' });
    self.reduceCount = function()
    {
        var time = new Date().getTime() - start;
        elapsed = Math.floor(time / 100) / 10;
        
            var timeLeft = self.TimeLeft();
            if (elapsed >= 60 && timeLeft > 0) {
                timeLeft -= Math.round(elapsed);
                start = new Date().getTime();
                elapsed = 0.0;
                self.TimeLeft(timeLeft, { persist: 'timeLeft' });
                //$("#timerSection").countdown({ until: '+' + localStorage.timer + 'm', onTick: everySecond, tickInterval: 1 });
                //setInterval(self.reduceCount, 1000);      
        }
    }

    self.start = function()
    {
        clearInterval(self.timer);
        start = new Date().getTime();
        elapsed = 0.0;
        self.timer = setInterval(self.reduceCount, 60000);
    }
}

var timerVM = new TimerViewModel();

ko.applyBindings(timerVM, document.getElementById('timerSection'));

$(function () {
    if (localStorage.toStart == "true") {
        timerVM.TimeLeft(parseInt(localStorage.timeLeft), { persist: 'timeLeft' });
        timerVM.start();
    }
});


function startGame() {
    //console.log("Starting timer: " + timerVM.TimeLeft());
    timerVM.TimeLeft(10, { persist: 'timeLeft' });
    localStorage.toStart = true;
    window.location.href = "/Game/DisplayCategory";
}




