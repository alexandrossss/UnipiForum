$(document).ready(function () {


    //var $this = $(this);
    //var question =this.data("div.question")
    $("#new-question-button").click(function () {
        var html = '<ul class="nav nav-pills"><li><div class="panel-heading">Questions:</div></li></ul><input id="new-question-name" type="text" class="new-question-name form-control" />            <br /><div class="question" style="position:relative;left:20%;width:80%;"><ul class="nav nav-pills"><li> <div class=" panel-heading">Answers:</div></li>             <li class="pull-right"><button disabled class="btn btn-primary add-answer-button">Add Answer</button></li>                </ul>                <ul class="nav nav-pills">                    <li style="position:relative; width:85%;"><input id="new-answer-name" type="text" class="new-answer-name form-control" /></li>                    <li class="pull-right"> <input type="checkbox" value="IsCorrect" />Is Correct </li>                </ul>                <br />                <ul class="nav nav-pills">                    <li style="position:relative; width:85%;"><input id="new-answer-name" type="text" class="new-answer-name form-control" /></li><li class="pull-right"> <input type="checkbox" value="IsCorrect" />Is Correct </li></ul></div>'
        debugger;
        $(".panel-body").append(html);
    });
    //$("#btn2").click(function () {
    //    $("ol").append("<li>Appended item</li>");
    //});





});