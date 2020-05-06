function loadServerPartialView(container, baseUrl) {
    $.get(baseUrl, function (responseData) {
        $(container).html(responseData);
    });
}