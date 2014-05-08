using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class CommentModel
    {
        public int commentId { get; set; }
        [Required]
        public string comment { get; set; }
        public int report { get; set; }
        public DateTime commentDate { get; set; }


    }
}