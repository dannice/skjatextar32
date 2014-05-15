using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.BLL
{
    public class UserRepository
    {
        // Gets lists of all users.
        public List<Models.User> GetUsers()
        {
            SkjatextiEntities context = new SkjatextiEntities();
            var list = new List<Models.User>();
            var query = from it in context.AspNetUsers
                        orderby it.UserName
                        select it;
            foreach (var item in query)
            {
                var user = new Models.User();
                //user.email = item;
                //user.name = item.name;
                user.password = item.PasswordHash;
                user.regId = item.Id;
                user.username = item.UserName;
                list.Add(user);
            }
            return list;
        }

        /*public List<Models.RequestModel> GetRequests()
        {
            SkjatextiEntities context = new SkjatextiEntities();
            var list = new List<Models.RequestModel>();
            var query = from it in context.Request
                        orderby it.reqId
                        select it;
            foreach (var item in query)
            {
                var request = new Models.RequestModel();
                request.reqId = item.reqId;
                request.reqTitle = item.
                // request.reqTitle = item.reqTitle;
                // request.reqDate = item.reqDate;
                //request.reqLike = item.reqLike;
                list.Add(request);
            }
            return list;
        } */
    }
}
