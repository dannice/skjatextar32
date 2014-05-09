using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{
    public class SkjatextiRepository
    {
        // Funtion to make a dropdown list.
        public List<Models.TvShowModel> GetTvShow()
        {
            SkjatextiEntities context = new SkjatextiEntities();

            var list = new List<Models.TvShowModel>();
            var query = from it in context.SrtFile
                        orderby it.title
                        select it;
            foreach(var item in query)
            {
                var tvName = new Models.TvShowModel();
                list.Add(tvName);
            }
            return list;
        }
    }

    
}