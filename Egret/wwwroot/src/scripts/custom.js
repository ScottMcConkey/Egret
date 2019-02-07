
function AutoFocusFirstFormInput() {
    if ($(".validation-summary-errors").length === 0) {
        $(":input:enabled:visible:not(':button'):not([readonly]):first").focus();
    }
}

function FadeOutSuccessMessages() {
    $(function () {
        $("#success").delay(2000).fadeOut(5000, function () {
            $(this).css({ "visibility": "hidden", display: 'block' }).slideUp();
        });
    });
}

function ManageMultiRowRadioSelects() {
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
}

function ManageTabs() {
    $(".nav-link").on("click", function () {
        $(this).blur();
    });
    $(".nav-item").removeClass("active");
    $(".nav-item:first").addClass("active");
    $(".tab-content").css("display", "none");
    $(".tab-content:first").css("display", "block");
    $("li.nav-link").on("click", function () {
        $("li.nav-item").removeClass("active");
        $(this).preventDefault();
        $(this).addClass("active");
        var idx = $(".nav-item").index(this);
        $(".tab-content").css("display", "none");
        $(".tab-content").eq(idx).css("display", "block");
        SetContentHeight();
    });
}

function ManageValidationErrors() {
    $("input.input-validation-error").closest(".form-group").addClass("has-error");
    $(".input-validation-error").on("focus", function () {
        $(this).closest(".form-group").find("input.input-validation-error").val("");
        $(this).closest(".form-group").removeClass("has-error");
        $(this).closest(".form-group").find("span.field-validation-error").remove();
    });
}

function PrepApiController() {
    $(function () {
        $("#callApi").on("click", function () {
            $(".egret .egret-refresh").addClass("spin");
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
}

function SetContentHeights() {
    $("div#main-top").css("min-height", $("nav.leftnav ul li").height() + 1 + "px");
    $("div#main").css("min-height", $("div.leftnav").height() + "px");
    $("#main-bottom").css("min-height", $(document).height() - 250 + "px");
}

function SetTestsForDelete() {
    $(".delete").on("click", function () {
        if (confirm('Are you sure you want to delete this?')) {
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

function TriggerResponsiveNavigation() {
    var x = document.getElementsByTagName("nav")[0];
    if (x.className === "leftnav") {
        x.className += " responsive";
    } else {
        x.className = "leftnav";
    }
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
                "<td class='delete-box'><a style='font-size: 1em;' class='delete' title='Delete' href='#'><span class='egret egret-trash'></span></a></td>" +
            "</tr>");

        SetTestsForDelete();
    }

}

function AddTableRow() {
    var manager = new TestManager();
    manager.addOne();
}



$(document).ready(function () {

    SetContentHeights();
    
    ManageValidationErrors();

    ManageTabs();

    AutoFocusFirstFormInput();
    
    ManageMultiRowRadioSelects();

    FadeOutSuccessMessages();

    SetTestsForDelete();

    PrepApiController();

})



