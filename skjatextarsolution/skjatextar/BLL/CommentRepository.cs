using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Function for comments that is never used because website does not offer users to comment.
/// </summary>

namespace skjatextar.BLL
{
    public class CommentRepository
    {
        public List<CommentCollection> GetAllComments()
        {
            SkjatextiEntities context = new SkjatextiEntities();
            var list = new List<CommentCollection>();

            var query = from it in context.CommentCollection
                        orderby it.commentDate
                        select it;
            foreach (var item in query)
            {
                var commentItem = new CommentCollection();
                commentItem.comment = item.comment;
                commentItem.commentId = item.commentId;
                commentItem.commentDate = item.commentDate;
                commentItem.dataId = item.dataId;
                commentItem.dataName = item.dataName;
                commentItem.dataText = item.dataText;
                list.Add(commentItem);
            }
            return list;
        }


        public void AddComment(Comment c)
        {
            int newID = 1;
            string comment = "";
            var comItem = new Comment();
            if (newID > 0)
            {
                newID = newID + 1;
            }
            using (var db = new SkjatextiEntities())
            {
                c.commentId = newID;
                c.commentDate = DateTime.Now;
                c.comment1 = comment;

                db.Comment.Add(comItem);
            }
        }
    }
}