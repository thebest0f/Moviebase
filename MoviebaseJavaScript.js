$(document).ready(function () {

    //---------------------------------------------------public page
    $(".h1Titles").on("click", function () {

        var targetID = event.target.id;

        var getHTML = document.getElementById(targetID).innerHTML;

        document.cookie = "genre=" + getHTML + ";";

        window.location.href = ("FullMovieList.aspx");
    });

    $(".liMovies").on("click", function () {

        var targetID = event.target.id;

        var getHTML = document.getElementById(targetID).innerHTML;

        document.cookie = "movie=" + getHTML + ";";

        window.location.href = ("SingleMovie.aspx");
    });


    
        $(function () {
            $("#txtSearch").autocomplete({
                source: function (request, response) {
                    var param = { movie_name: $('#txtSearch').val() };
                    $.ajax({
                        url: "FetchMovieTitles.aspx/GetMovieTitles",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(errorThrown);
                        }
                    });
                },
                minLength: 2
            });
        });
         
    //-------------------------------------------------------admin page
});