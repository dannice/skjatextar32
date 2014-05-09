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
            var query = from it in context.RegisteredUser
                        orderby it.userName
                        select it;
            foreach (var item in query)
            {
                var user = new Models.User();
                user.email = item.email;
                user.name = item.name;
                user.password = item.password;
                user.regId = item.regId;
                user.username = item.userName;
                list.Add(user);
            }
            return list;
        }

        public List<Models.RequestModel> GetRequests()
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
                request.requestText = item.requestText;
                request.reqDate = item.reqDate;
                request.reqLike = item.reqLike;
                list.Add(request);
            }
            return list;
        }
    }
}