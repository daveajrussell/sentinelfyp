﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sentinel.Models;
using DomainModel.SecurityModels;
using DomainModel.Models.AssetModels;
using System.Data;

namespace Sentinel.Helpers.ExtensionMethods
{
    public static class ViewModel
    {
        public static UserViewModel ToViewModel(this User user)
        {
            return new UserViewModel() { UserName = user.UserName, DateLastLoggedIn = user.UserLastLogon.ToShortDateString() };
        }

        public static IEnumerable<AssignDeliveryItemViewModel> ToViewModel(this IEnumerable<DeliveryItem> oDeliveryItemSet, Guid oConsignmentKey)
        {
            return from item in oDeliveryItemSet
                   select new AssignDeliveryItemViewModel()
                   {
                       Item = item,
                       ConsignmentKey = oConsignmentKey
                   };
        }
    }
}