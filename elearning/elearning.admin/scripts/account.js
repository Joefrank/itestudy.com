
$(document).ready(function () {

    $('#lnk-edit-password').on("click", function (e) {
        e.preventDefault();
        $('#pass-hidden').hide();
        $('#pass-show').show();
        return false;
    });   

});

