using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{
    public class SkjatextiRepository
    {
        // Gets all tvshows and movies
        public List<Models.CollectionOfSrt> GetBothTvshowsAndMovies()
        {
            // Connect to db through Skjatexti.context.cs
            SkjatextiEntities contex = new SkjatextiEntities();
            // Creates new empty list using collectionofstr
            var list = new List<Models.CollectionOfSrt>();
            // Sql query thats selects all in SrtCollection and orders it by title
            var query = from item in contex.SrtCollection
                         orderby item.title
                         select item;
            // Loops through every item in query.
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                show.title = item.title;
                show.episodeAbout = item.episodeAbout;
                show.season = item.season;
                show.episode = item.episode;
                show.year = item.year;
                show.episodeTitle = item.episodeTitle;
                list.Add(show);

            }
            return list;
        }

        public List<Models.CollectionOfSrt> GetTopTenSrt()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var list = new List<Models.CollectionOfSrt>();
            var query = (from item in contex.SrtCollection
                         orderby item.title
                         select item).Take(10);
            foreach (var item in query)
            {
                var show = new Models.CollectionOfSrt();
                show.title = item.title;
                show.episodeAbout = item.episodeAbout;
                show.season = item.season;
                show.episode = item.episode;
                show.year = item.year;

                list.Add(show);

            }
            return list;
        }

        /*public List<Models.CollectionOfSrt> GetSrtData()
        {
            SkjatextiEntities contex = new SkjatextiEntities();
            var model = new List<Models.SrtDataModel>();
            var query = from b in contex.SrtData
                        select b.dataText;
                foreach (var item in query)
                {
                    var dataItem = new SrtData();
                    dataItem.dataText = item.
                    newsitem.blogId = item.BlogId;
                    newsitem.title = item.Title;
                    newsitem.date = item.Date;
                    newsitem.texti = item.Name;
                    newsitem.category = item.Category;
                    model.Add(newsitem);
                }
        }*/

    }
}