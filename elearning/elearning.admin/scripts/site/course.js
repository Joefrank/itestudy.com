var returnObj;
var uploadCount = 0;


$(document).ready(function () {

    //var YoutubePreviewId must be specified on calling page script
    var previewId = (YoutubePreviewId !== null) ? YoutubePreviewId : "youtubepreview";

    $('#Description').summernote({
        //lang: 'ko-KR' // default: 'en-US'
        placeholder: 'Course Description',
        tabsize: 2,
        height: 400,                 // set editor height
        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor
        focus: true
    });

    $('#' + YoutubeSourceCtrlId).blur(function () {
        var link = $(this).val();
        makePreview(link);
    });


    function getYoutubeID(url) {
        var id = url.match(/(?:https?:\/{2})?(?:w{3}\.)?youtu(?:be)?\.(?:com|be)(?:\/watch\?v=|\/)([^\s&]+)/);
        id = id[1];
        return id;
    };

    function makePreview(url) {
        var id = getYoutubeID(url);
        var thumb_url = "https://i.ytimg.com/vi/" + id + "/mqdefault.jpg";
        var youtube_url = "https://www.youtube.com/embed/" + id + "";
        $('#' + previewId).html('<a href="' + youtube_url + '"><img src="' + thumb_url + '" /></a>');
    }

    function LoadYoutubeVideo() {
        var link = $('#' + YoutubeSourceCtrlId).val();

        if (link !== "")
            makePreview(link);
    }

    //$('#frmCourse').submit(function () {
    //    $('#MainImageLink').prop("disabled", false);
    //    alert('enabled mainlinke');
    //    return false;
    //});

    LoadYoutubeVideo();
});