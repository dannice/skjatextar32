using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{
    public class SkjatextiRepository
    {
        // Funtion to make a dropdown list.
        public List<Models.SrtFileModel> GetTvShow()
        {
            SkjatextiEntities context = new SkjatextiEntities();

            var listTitle = new List<Models.SrtFileModel>();
            
            var query = from it in context.SrtFile
                        orderby it.title
                        select it;      

            foreach (var item in query)
            {
                var tvName = new Models.SrtFileModel();
                tvName.title = item.title;
                listTitle.Add(tvName);
            }
            return listTitle;
        }

        public List<Models.TvShowModel> GetTvShowModel()
        {
            SkjatextiEntities context = new SkjatextiEntities();

            var listSeason = new List<Models.TvShowModel>();

            var query = from it in context.TvShow
                             orderby it.season
                             select it;

            foreach(var item in query)
            {
                    var tvSeason = new Models.TvShowModel();
                    tvSeason.season = item.season;
                    listSeason.Add(tvSeason);
            }
            return listSeason;
        }
    }
}