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
            $("#episodeInfo").hide();
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

    
    $("#srch-term").keypress(function (event) {
        var query = $("#srch-term").val();
       if (event.which == 13 && (!query || query.length == 0)) {
           event.preventDefault();
           console.log(query);
           return false;
            
        }
        
    });
    
    // Loading new srt file;
    initUploadForm();

    

    // sending in new raquest
    //newRequest();

});

// Loading new srt file;
function initUploadForm() {

    var uploadForm = $("#uploadForm");
    
    if(uploadForm.length > 0) 
    {
        radioType = uploadForm.find("input[name='type']");
       
        console.log(radioType);
        $(".upload .tv-field, .upload .movie-field").hide();

        radioType.on("change", function () {
            var typeValue = ($(this).val());

            if(typeValue == 1)
            {
                $(".tv-field").fadeOut();
                $(".movie-field").fadeIn();
            }
            else
            {
                $(".tv-field").fadeIn();
                $(".movie-field").fadeOut();
            }
        })
    }
};
/*function newRequest() {

    alert("ho");
    var beidniForm = $("#beidniForm");
    console.log(beidniForm);
    

};*/
    

    /*if (beidniForm.length > 0) {
        radioType = beidniForm.find("input[name='type']");

        console.log(radioType);
        $(".tv-field, .movie-field").hide();

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
}*/

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
            console.log(data);
            $("#season").text(data.season);
            $("#episode").text(data.episode);
            $("#episodeTitle").text(data.episodeTitle);
            $("#episodeAbout").text(data.episodeAbout);
            $("#episodeLink").attr("href", "/Home/Details/" + data.srtId);
            console.log(data);
        },
    });
};