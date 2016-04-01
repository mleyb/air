/*
* Form Validation
* This script will set Bootstrap error classes when form.submit is called.
* The errors are produced by the MVC unobtrusive validation.
*/
$(function () {
    $('form').submit(function () {
        $(this).find('div.control-group').each(function () {
            if ($(this).find('span.field-validation-error').length == 0) {
                $(this).removeClass('error');
            }
        });

        if (!$(this).valid()) {
            $(this).find('div.control-group').each(function () {
                if ($(this).find('span.field-validation-error').length > 0) {
                    $(this).addClass('error');
                }
            });
        }
    });
    $('form').each(function () {
        $(this).find('div.control-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('error');
            }
        });
    });
});

var page = function () {
    //Update that validator
    $.validator.setDefaults({
        highlight: function (element) {
            $(element).closest(".control-group").addClass("error");
        },
        unhighlight: function (element) {
            $(element).closest(".control-group").removeClass("error");
        }
    });
}();