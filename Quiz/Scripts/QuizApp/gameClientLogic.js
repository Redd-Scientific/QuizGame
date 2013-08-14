/// <reference path="../jquery-1.8.2.min.js" />
/// <reference path="../knockout-2.3.0.debug.js" />
var questionAndBet = function (question) {
    var self = this;
    self.Category = question["Category"];
    self.QuestionId = question["QuestionId"];
    self.QuestionText = question["QuestionText"];
    self.AnswerA = question["AnswerA"];
    self.AnswerB = question["AnswerB"];
    self.AnswerC = question["AnswerC"];
    self.AnswerD = question["AnswerD"];
}