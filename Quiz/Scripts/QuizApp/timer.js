/* timer section code */
function Timer(data) {
    this.TimeLeft = ko.observable(data.TimeLeft);
}

var timer;
var maxTime = 30;
function TimerViewModel() {
    var self = this;
    var start, elapsed;
    // self.BetAmt = new Bet({BetAmt:100});
    self.TimeLeft = ko.observable(30-1, { persist: 'timeLeft' });
    self.secondsLeft = ko.observable(60, { persist: 'secondsLeft' });
    self.reduceCount = function () {
        var time = new Date().getTime() - start;
        elapsed = Math.floor(time / 100) / 10;

        var timeLeft = parseInt(self.TimeLeft());
        var secLeft = parseInt(self.secondsLeft());
        if (timeLeft >= 0 && secLeft > 0 && elapsed > 0) {
            secLeft -= Math.round(elapsed);
            start = new Date().getTime();
            elapsed = 0.0;
            self.secondsLeft(secLeft, { persist: 'secondsLeft' });
            if (secLeft == 0 && timeLeft > 0) {
                timeLeft -= 1;
                secLeft = 60;
                self.TimeLeft(timeLeft, { persist: 'timeLeft' });
                self.secondsLeft(secLeft, { persist: 'secondsLeft' });
            }
            //$("#timerSection").countdown({ until: '+' + localStorage.timer + 'm', onTick: everySecond, tickInterval: 1 });
            //setInterval(self.reduceCount, 1000);      
        }
        else if (timeLeft === 0 && secLeft == 0) {
            clearInterval(timer);
            localStorage.toStart = "false";
            window.location.href = "/Game/GameOver"
        }
    }

    self.start = function () {
        clearInterval(timer);
        start = new Date().getTime();
        elapsed = 0.0;
        timer = setInterval(self.reduceCount, 1000);
        localStorage.timer = timer;
    }
}

var timerVM = new TimerViewModel();

ko.applyBindings(timerVM, document.getElementById('timerSection'));

$(function () {
    if (localStorage.toStart == "true") {
        timerVM.TimeLeft(parseInt(localStorage.timeLeft), { persist: 'timeLeft' });
        timerVM.secondsLeft(parseInt(localStorage.secondsLeft), { persist: 'secondsLeft' });
        timerVM.start();
    }
});


function startGame() {
    //console.log("Starting timer: " + timerVM.TimeLeft());
    timerVM.TimeLeft(30 - 1, { persist: 'timeLeft' });
    timerVM.secondsLeft(60, { persist: 'secondsLeft' });
    localStorage.toStart = true;
    window.location.href = "/Game/DisplayCategory";
}

function resetTimer() {
    timerVM.TimeLeft(30 - 1, { persist: 'timeLeft' });
    timerVM.secondsLeft(60, { persist: 'secondsLeft' });
    localStorage.toStart = true;
}