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
    self.reduceCount = function () {
        var time = new Date().getTime() - start;
        elapsed = Math.floor(time / 100) / 10;

        var timeLeft = self.TimeLeft();
        if (timeLeft > 0 && elapsed > 0) {
            timeLeft -= Math.round(elapsed);
            start = new Date().getTime();
            elapsed = 0.0;
            self.TimeLeft(timeLeft, { persist: 'timeLeft' });
            //$("#timerSection").countdown({ until: '+' + localStorage.timer + 'm', onTick: everySecond, tickInterval: 1 });
            //setInterval(self.reduceCount, 1000);      
        }
    }

    self.start = function () {
        clearInterval(self.timer);
        start = new Date().getTime();
        elapsed = 0.0;
        self.timer = setInterval(self.reduceCount, 1000);
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