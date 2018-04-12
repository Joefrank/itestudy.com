function OpenEditor() {
    OpenPopup('/editor');
}

function OpenPopup(location, width, height) {
    if (!width)
        width = 1200;
    if (!height)
        height = 700;

    window.open(location, 'editorwin', 'directories=no,titlebar=no,toolbar=no,location=0,status=0,menubar=0,scrollbars=no,resizable=no,width=' + width + ',height=' + height);
}