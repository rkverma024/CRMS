window.setTimeout(function () {
    $(".alert").fadeTo(900, 0).slideUp(500, function () {
        $(this).remove();
    })
}, 10000);