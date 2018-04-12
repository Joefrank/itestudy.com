
var editor = ace.edit("editor");
editor.setTheme("ace/theme/monokai");
editor.session.setMode("ace/mode/javascript");

$(document).ready(function () {

    $('#topToolBar').on("mouseover",
        function() {
            $(this).animate({ height: "10%" }, { queue: false, duration: 500 });
        });

    $('#topToolBar').on("mouseout",
        function () {

        });

    $('#editor').on("mouseover",
        function() {

        });

    $('#bottomBoard').on("mouseover",
        function() {

        });

    $('#bottomBoard').on("mouseout",
        function () {

        });
});