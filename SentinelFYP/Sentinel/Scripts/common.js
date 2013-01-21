function getHex() {
    return '#' + Math.floor(Math.random() * 16777215).toString(16);
}

function navigateBack(href) {
    window.location.href = href;
}