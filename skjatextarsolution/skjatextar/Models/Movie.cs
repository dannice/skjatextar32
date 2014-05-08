using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class MovieModel
    {
        public int movieId { get; set; }
        [Required]
        public int year { get; set; }
    }
}