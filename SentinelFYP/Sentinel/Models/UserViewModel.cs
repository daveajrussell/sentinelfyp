﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.SecurityModels;

namespace Sentinel.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string DateLastLoggedIn { get; set; }
    }
}