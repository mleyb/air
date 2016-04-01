var site = site || {};
site.baseUrl = site.baseUrl || "";

$(document).ready(function (e) {

    // locate each partial section.
    // if it has a URL set, load the contents into the area.
    $(".partialContents").each(function (index, item) {
        var url = site.baseUrl + $(item).data("url");
        if (url && url.length > 0) {
            $(item).load(url);
        }
    });    

    // attach tooltips
    $('a[rel=tooltip]').tooltip();
});

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#detailsDialog').modal('hide');
                    // Refresh:
                    // location.reload();
                } else {
                    $('#detailsDialogContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}

var siteFunctions = (function ($, undefined) {
    "use strict";

    function hideBrand() {
        $('#brand').hide();
    }

    function showBrand() {
        $('#brand').show();
    }

    function showLoadingImage() {
        $('#loading').show();
    }

    function hideLoadingImage() {
        $('#loading').hide();
    }    

    function applyDatePickers() {
        $('.date').datepicker( {
            dateFormat: "dd-MM-yyyyy"
        })
        .on('changeDate', function (ev) {
            $('.date').datepicker('hide');
        });
    }

    function applyDataTable(id) {
        $(id).dataTable({
            "sDom": "t<'row'<'span5'i><'span5'p>>",
            "sPaginationType": "bootstrap",
            "oLanguage": {
                "sLengthMenu": "_MENU_ records per page"
            },
            "iDisplayLength": 5,
            "rowHeight": 'auto',
            "bDestroy": true,
            "bPaginate": true,
            "bLengthChange": false,
            "bFilter": false,
            "bSort": false,
            "bInfo": true,
            "bAutoWidth": true
        });
    }

    function applyDataTables() {
        $('.dataTable').dataTable({
            "sDom": "t<'row'<'span5'i><'span5'p>>",
            "sPaginationType": "bootstrap",
            "oLanguage": {
                "sLengthMenu": "_MENU_ records per page"
            },
            "iDisplayLength": 5,
            "rowHeight": 'auto',
            "bDestroy": true,
            "bPaginate": true,
            "bLengthChange": false,
            "bFilter": false,
            "bSort": false,
            "bInfo": true,
            "bAutoWidth": true
        });
    }

    function setSubmitButtonEnabled(enabled) {
        if (enabled == true) {
            $('form button[type=submit]').removeAttr('disabled');
        }
        else {
            $('form button[type=submit]').attr('disabled', 'disabled');
        }
    }

    function applyEntryRowDetailsClickHandlers() {
        // add dialog click handler
        $('.showDetails').on('click', function () {
            $('#detailsDialogContent').load(this.href, function () {
                $('#detailsDialog').modal({
                    backdrop: 'static',
                    keyboard: true
                }, 'show');
                bindForm(this);
            });
            return false;
        });
    }

    function makeEqualHeight(group) {
        var tallest = 0;
        group.each(function () {
            var thisHeight = $(this).height();
            if (thisHeight > tallest) {
                tallest = thisHeight;
            }
        });
        group.height(tallest);
    }

    // public api
    return {
        hideBrand: hideBrand,
        showBrand: showBrand,
        showLoadingImage: showLoadingImage,
        hideLoadingImage: hideLoadingImage,
        applyDatePickers: applyDatePickers,
        applyDataTable: applyDataTable,
        applyDataTables: applyDataTables,
        setSubmitButtonEnabled: setSubmitButtonEnabled,
        applyEntryRowDetailsClickHandlers: applyEntryRowDetailsClickHandlers,
        makeEqualHeight: makeEqualHeight
    };
})(jQuery);