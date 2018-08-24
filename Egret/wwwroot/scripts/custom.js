function SetContentHeight() {
    $("div#main").css("min-height", $("div.leftnav").height() + "px");
}

function SetTestsForDelete() {
    $(".delete").on("click", function() {
        if (confirm('Are you sure you want to delete this Fabric Test?')) {
            $(this).parent().parent().remove();
            console.log($(this));
            var manager = new TestManager();
            manager.reOrder();

            if ($("tr.fabrictest").length == 0) {
                $("tr#notests").show();
            };
        }
        else {
            return false;
        }
    });
}

//if (confirm('Testing')) DeleteFabricTest($(this)); return false

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
            };

            for (var k = 0; k < this.getCount(); k++) {
                var testname = $("input.testName")[k];
                $(testname).attr("name", "FabricTests[" + k + "].Name");
            };

            for (var m = 0; m < this.getCount(); m++) {
                var testresult = $("input.testResult")[m];
                $(testresult).attr("name", "FabricTests[" + m + "].Result");
            };
        };
    }

    addOne() {
        $("tr#notests").hide();

        $("#tests tr:last").after(
            "<tr class='fabrictest'>" +
                "<td><div class='form-group'><input class='form-control testName' type='text' name='FabricTests[" + this.getCount() + "].Name' /></div></td>" +
                "<td><div class='form-group'><input class='form-control testResult' type='text' name='FabricTests[" + this.getCount() + "].Result' /></div></td>" +
                "<td class='delete-box'><a style='font-size: 1em;' class='delete' title='Delete' href='#'><span class='glyphicon glyphicon-trash'></span></a></td>" +
            "</tr>");

        SetTestsForDelete();
    }

}



$(document).ready(function () {

    $("div#main-top").css("min-height", $("nav.leftnav ul li").height() + 1 + "px");
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

    $(function () {
        $("#callApi").on("click", function () {
            $(".glyphicon-refresh").addClass("spin");
            $.get('/api/Inventory/' + $("#InventoryItemCode").val(), function (data) {

                


                $('#apiDescription').empty();
                $('#apiDescription').html(data.description);

                $('#apiCustomer').empty();
                $('#apiCustomer').html(data.customerReservedFor);

                $('#Unit').empty();
                $('#Unit').val(data.unit);

                $(".glyphicon-refresh").removeClass("spin");

            }, 'json');
        });
    });


});