﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace skjatextar.Models
{
    public class User
    {
        public int regId { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}