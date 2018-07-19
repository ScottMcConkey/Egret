function SetContentHeight() {
    $("div#main").css("min-height", $("div.leftnav").height() + "px");
}

function SetTestsForDelete() {
    $(".delete").on("click", function() {
        $(this).parent().parent().remove();
        var manager = new TestManager();
        manager.reOrder();
        if ($("tr.fabrictest").length == 0) {
            $("tr#notests").show();
        };
    });
}

function AddTableRow() {
    var manager = new TestManager();
    manager.addOne();
}

class TestManager {

    getCount() {
        return $("tr.fabrictest").length;
    }

    reOrder() {
        if (this.getCount() > 0) {
            for (var j = 0; j < this.getCount(); j++) {
                var testId = $("input.testId")[j];
                $(testId).attr("name", "FabricTests[" + j + "].Id");
                console.log(testId);
            };

            for (var k = 0; k < this.getCount(); k++) {
                var testname = $("input.testName")[k];
                $(testname).attr("name", "FabricTests[" + k + "].Name");
                console.log(testname);
            };

            for (var m = 0; m < this.getCount(); m++) {
                var testresult = $("input.testResult")[m];
                $(testresult).attr("name", "FabricTests[" + m + "].Result");
                console.log(testresult);
            };
        };
    }

    addOne() {
        $("tr#notests").hide();

        $("#tests tr:last").after(
            "<tr class='fabrictest'>" +
                "<td><div class='form-group'><input class='form-control testName' type='text' name='FabricTests[" + this.getCount() + "].Name' /></div></td>" +
                "<td><div class='form-group'><input class='form-control testResult' type='text' name='FabricTests[" + this.getCount() + "].Result' /></div></td>" +
                "<td style='text-align: center;'><a class='delete' title='Delete' href='#'>&times;</a></td>" +
            "</tr>");

        SetTestsForDelete();
    }

}



$(document).ready(function () {

    $("div#main-top").css("height", $("div.leftnav ul li").height() + 1 + "px");
    SetContentHeight();
    $("table.results tr:even").addClass("shaded");
    $("#main-bottom").css("min-height", $(document).height() - 175 + "px");

    // Manage Validation Errors
    $("input.input-validation-error").closest(".form-group").addClass("has-error");
    $(".input-validation-error").on("focus", function () {
        $(this).closest(".form-group").find("input.input-validation-error").val("");
        $(this).closest(".form-group").removeClass("has-error");
        $(this).closest(".form-group").find("span.field-validation-error").remove();
    });

    // Special Form Fields
    //$("input[type='date']").parent().css("width", "50%");


    // Manage Tabs
    $(".tab-header").removeClass("active");
    $(".tab-header:first").addClass("active");
    $(".tab-content").css("display", "none");
    $(".tab-content:first").css("display", "block");
    $("li.tab-header").on("click", function () {
        $("li.tab-header").removeClass("active");
        $(this).addClass("active");
        var idx = $(".tab-header").index(this);
        $(".tab-content").css("display", "none");
        $(".tab-content").eq(idx).css("display", "block");
        SetContentHeight();
    });

    // Autofocus first form input
    $(":input:enabled:visible:not([readonly]):first").focus();

    // Manage Multi-Row Radio Selects
    $(".radio[value='False']").removeAttr("checked");
    $(".radio").on("click", function () {

        if ($(this).attr("checked") == "checked") {
            $(this).prop("checked", false).attr("checked", false).attr("value", false);
        }
        else {
            $(".radio").prop("checked", false).attr("checked", false).attr("value", false);
            $(this).prop("checked", true).attr("checked", true).attr("value", true);
        }
    });

    // Fade Out Success Messages
    $(function () {
        $("#success").delay(2000).fadeOut(5000, function () {
            $(this).css({ "visibility": "hidden", display: 'block' }).slideUp();
        });
    });

    SetTestsForDelete();


});