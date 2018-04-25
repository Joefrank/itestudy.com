var returnObj;
var uploadCount = 0;


$(document).ready(function () {

    $('#Content').summernote({
        //lang: 'ko-KR' // default: 'en-US'
        placeholder: 'Article Content',
        tabsize: 2,
        height: 400,                 // set editor height
        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor
        focus: true
    });

    

});


$(document).ready(function () {

    $('#YoutubeLinks').blur(function () {

        var id = getYoutubeID($(this).val());

        var thumb_url = "https://i.ytimg.com/vi/" + id + "/mqdefault.jpg";

        var youtube_url = "https://www.youtube.com/embed/" + id + "";

        $('<a href="' + youtube_url + '"><img src="' + thumb_url + '" /></a>').appendTo($('#youtubepreview'));

    });


    function getYoutubeID(url) {

        var id = url.match(/(?:https?:\/{2})?(?:w{3}\.)?youtu(?:be)?\.(?:com|be)(?:\/watch\?v=|\/)([^\s&]+)/);

        id = id[1];

        return id;

    };

    //$('p').each(function () {

    //    var id = getYoutubeID($(this).html());

    //    var thumb_url = "https://i.ytimg.com/vi/" + id + "/mqdefault.jpg";

    //    var youtube_url = "https://www.youtube.com/embed/" + id + "";

    //    $('<a href="' + youtube_url + '"><img src="' + thumb_url + '" /></a>').appendTo($(this));

    //});

});