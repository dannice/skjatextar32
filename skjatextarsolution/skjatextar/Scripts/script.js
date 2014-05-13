$(document).ready(function () {

    var tvDetails;



    $("#selectEpisode").hide();
    $("#episodeInfo").hide();

    $("#selectShow").change(function () {
        tvDetails = $("#selectShow option:selected").val();
        if (tvDetails && tvDetails.length > 0)
        {
            $("#selectEpisode").fadeIn();
            getEpisodes(tvDetails);
        }else
        {
            $("#selectEpisode").hide().find('option:selected').removeAttr("selected");
        }
    });

    $("#selectEpisode").change(function () {
        episodeId = $("#selectEpisode option:selected").val();
        if (episodeId && episodeId.length > 0) {
            getEpisode(episodeId);
            $("#episodeInfo").fadeIn();
        } else {
            $("#episodeInfo").hide();
        };
    });

});


function getEpisodes(srtId){  
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

function getEpisode(epId) {
    $.ajax({
        type: "GET",
        url: "/Home/GetEpisodeData?epId=" + epId,
        success: function (data) {
            $("#season").text(data.season);
            $("#episode").text(data.episode);
            $("#episodeTitle").text(data.episodeTitle);
            $("#episodeAbout").text(data.episodeAbout);
            console.log(data);
        },
    });
};