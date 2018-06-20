function SetContentHeight() {
    $("div#main").css("min-height", $("div.leftnav").height() + "px");
}

function AddTableRow() {
    $("#tests tr:last").after(
        "<tr class='fabrictests'><td><input class='form-control' type='text' name='FabricTests[" + $('.fabrictests').length + "].Name' /></td><td>" +
        "<input class='form-control' type='text' name='FabricTests[" + $('.fabrictests').length + "].Result' /></td></tr>");
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
    $("input[type='date']").parent().css("width", "50%");


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

});