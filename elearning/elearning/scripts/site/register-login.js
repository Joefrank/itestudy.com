
$(document).ready(function () {

    $('#lnk-show-passwordreminder').on("click", function (e) {
        e.preventDefault();
        $('#login-div').hide();
        $('#reminder-div').show();
        return false;
    });

    //$('#lnk-show-registration').on("click", function (e) {
    //    e.preventDefault();
    //    $('#login-div').hide();
    //    $('#register-div').show();
    //    return false;
    //});

});

