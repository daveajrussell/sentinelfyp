using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sentinel.Models;
using DomainModel.SecurityModels;
using DomainModel.Models.AssetModels;

namespace Sentinel.Helpers.ExtensionMethods
{
    public static class ViewModel
    {
        public static UserViewModel ToViewModel(this User user)
        {
            return new UserViewModel() { UserName = user.UserName, DateLastLoggedIn = user.UserLastLogon.ToShortDateString() };
        }
    }
}