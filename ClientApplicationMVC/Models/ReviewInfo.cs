﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientApplicationMVC.Models
{

    public class ReviewInfo
    {
        public string companyName { get; set; }
        public string username { get; set; }
        public string review { get; set; }
        public string stars { get; set; }
        public string timestamp { get; set; }
    }
}