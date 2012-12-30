var showDetails = function () {
    var id = $(this).data("id");

    if ($("#details-" + id).is(':visible')) {
        $("#answer-" + id).data("details", $("#details-" + id));
        $("#details-" + id).detach();
    } else {
        $("#answer-" + id).after($("#answer-" + id).data("details"));
        $("#details-" + id).data("id", id);
    }

    return false;
};


// Loads and inserts details of periodic fee into table.
var loadDetails = function () {
    var self = this;

    $.ajax({
        url: $(this).attr("data-expand-row-source"),
        cache: false
    })
        .success(function (details) {
            var id = $(self).data("id");
            $("#answer-" + id).after(details);
            $("#details-" + id).data("id", id);
            $(self).click(showDetails);
        })
        .error(function () {
            $("a.details").one("click", loadDetails);
            alert("There was error while getting details.");
        });

    return false;
};

$("a.details").one("click", loadDetails);