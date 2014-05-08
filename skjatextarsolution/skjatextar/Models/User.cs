using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class User
    {
        public int regId { get; set; }
        [Required]
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}