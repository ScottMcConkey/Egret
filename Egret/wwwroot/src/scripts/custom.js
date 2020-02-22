
function AutoFocusFirstFormInput() {
    if ($(".validation-summary-errors").length === 0) {
        $(":input:enabled:visible:not(':button'):not([readonly]):first").focus();
    }
}

function CleanUpQueryString() {
    // remove &example
    var newString = window.location.href.replace(/\&(\w)*=(?=\&)/g, '');
    // remove ?example&
    newString = newString.replace(/(?=\?)*\w*=(\&)/g, '');
    window.history.replaceState("object or string", "Title", newString);
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

        if ($(this).attr("checked") === "checked") {
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
    $(".input-validation-error").addClass("is-invalid");
    $(".input-validation-error").on("focus", function () {
        $(this).find(".input-validation-error").val("");
        $(this).removeClass("is-invalid");
    });
}

function PrepApiController() {
    $("#callApi").on("click", function () {
        $(".egret-refresh").addClass("fa fa-spin");

        $.get('/api/InventoryApi/' + $("#InventoryItemCode").val(), function (data) {
            $('#apiDescription').val(data.description);
            $('#apiCustomer').text(data.customerReservedFor);
            $('#apiUnit').text(data.unit);
        }, 'json');

        $(".egret-refresh").removeClass("fa-spin").removeClass("fa");
    });
}

function SetContentHeights() {
    $("div#main-top").css("min-height", $("nav.leftnav ul li").height() + 1 + "px");
    $("div#main").css("min-height", $("div.leftnav").height() + "px");
    $("#main-bottom").css("min-height", $(document).height() - 250 + "px");
}

function ConfirmDelete(objectType) {
    var textOutput = objectType ? ' ' + objectType : '';
    var userAgrees = confirm('Are you sure you want to delete this' + textOutput + '?');
    return userAgrees;
}


function SetObjectsForDelete() {
    $(".delete").on("click", function () {

        if (ConfirmDelete(this.getAttribute("delete-name")) === true) {
            null;
        }
        else {
            return false;
        }
    });
}


function SetTestsForDelete() {
    $(".delete-test").on("click", function () {

        if (ConfirmDelete('Fabric Test') === true) {
            $(this).parent().parent().remove();

            var manager = new TestManager();
            manager.reOrder();

            if ($("tr.fabrictest").length === 0) {
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
                "<td class='delete-box'><a style='font-size: 1em;' class='delete-test' title='Delete' href='#'><span class='egret egret-trash'></span></a></td>" +
            "</tr>");

        SetTestsForDelete();
    }

}

function AddTableRow() {
    var manager = new TestManager();
    manager.addOne();
}

function AllowEditCode() {
    $("#editCode").change(function () {
        var ischecked = $(this).is(':checked');

        if (ischecked) {
            $("#ConsumptionEvent_InventoryItemCode").removeAttr("readonly");
        }
        else if (!ischecked) {
            $("#ConsumptionEvent_InventoryItemCode").attr("readonly", "readonly");
        }

    });
}
