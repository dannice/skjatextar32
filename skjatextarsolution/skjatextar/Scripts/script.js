$(document).ready(function () {



    // Syncronized scrolling textarea in EditFile

    $('#dataTextReadOnly').scroll(function () {

        $('#dataText').scrollTop($('#dataTextReadOnly').scrollTop());

    });



    $('#dataText').scroll(function () {

        $('#dataTextReadOnly').scrollTop($('#dataText').scrollTop());

    });

    // Syncronized scrolling ends





    var tvDetails;



    $("#selectEpisode").hide();

    $("#episodeInfo").hide();





    $("#selectShow").change(function () {

        tvDetails = $("#selectShow option:selected").val();

        if (tvDetails && tvDetails.length > 0) {

            $("#selectEpisode").fadeIn();

            getEpisodes(tvDetails);

        } else {

            $("#selectEpisode").hide().find('option:selected').removeAttr("selected");

            $("#episodeInfo").hide();

        }

    });



    // When clicked on episode, show episode info

    $("#selectEpisode").change(function () {

        episodeId = $("#selectEpisode option:selected").val();

        if (episodeId && episodeId.length > 0) {

            getEpisode(episodeId);

            $("#episodeInfo").fadeIn();

        } else {

            $("#episodeInfo").hide();

        };

    });





    // Capturing enter press in searchbox

    $("#srch-term").keypress(function (event) {

        var query = $("#srch-term").val();

        if (event.which == 13 && (!query || query.length == 0)) {

            event.preventDefault();

            return false;



        }



    });



    // Loading new srt file;

    initUploadForm();



});



// Loading new srt file;

function initUploadForm() {



    var uploadForm = $("#uploadForm");



    if (uploadForm.length > 0) {

        radioType = uploadForm.find("input[name='type']");



        // Hiding unnescessary boxes

        $(".upload .tv-field, .upload .movie-field").hide();



        // Capture the change to determe what to show and what to hide

        radioType.on("change", function () {

            var typeValue = ($(this).val());



            if (typeValue == 1) {

                $(".tv-field").fadeOut();

                $(".movie-field").fadeIn();

            }

            else {

                $(".tv-field").fadeIn();

                $(".movie-field").fadeOut();

            }

        })

    }

};



// Returning a list of all episodes in a season for dorpdownlist.

function getEpisodes(srtId) {

    $.ajax({

        type: "GET",

        url: "/Home/GetEpisodeDataByShow?srtId=" + srtId,

        success: function (data) {

            $("#selectEpisode option:gt(0)").remove();

            $.each(data, function (idx, episode) {

                $("<option/>").val(episode.tvId).text(episode.episode + " " + episode.episodeTitle).appendTo($("#selectEpisode"));

            });

        },

    });

};



// Returning the episode selected in the dropdownlist.

function getEpisode(epId) {

    $.ajax({

        type: "GET",

        url: "/Home/GetEpisodeData?epId=" + epId,

        success: function (data) {

            console.log(data);

            $("#season").text(data.season);

            $("#episode").text(data.episode);

            $("#episodeTitle").text(data.episodeTitle);

            $("#episodeLink").attr("href", "/Home/Details/" + data.srtId);

            console.log(data);

        },

    });

};