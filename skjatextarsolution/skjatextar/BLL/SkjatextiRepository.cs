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

            var list = new List<Models.SrtFileModel>();
            var query = from it in context.SrtFile
                        orderby it.title
                        select it;
           
            foreach (var item in query)
            {
                var tvName = new Models.SrtFileModel();
                tvName.title = item.title;
                list.Add(tvName);
            }
            return list;
        }

    }
}