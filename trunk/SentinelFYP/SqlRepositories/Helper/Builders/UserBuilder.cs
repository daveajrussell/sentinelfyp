using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;
using System.Data;
using SqlRepositories.Helper.Extensions;

namespace SqlRepositories.Helper.Builders
{
    public static class UserBuilder
    {
        public static User ToUser(this DataSet oSet)
        {
            var user = new User();
            var data = oSet.FirstDataTableAsEnumerable();

            user.UserKey = data.Select(p => p.Field<Guid>("USER_KEY")).First();
            user.UserName = data.Select(p => p.Field<string>("USERNAME")).First();
            user.FirstName = data.Select(p => p.Field<string>("USER_FIRST_NAME")).First();
            user.LastName = data.Select(p => p.Field<string>("USER_LAST_NAME")).First();
            user.Email = data.Select(p => p.Field<string>("USER_EMAIL")).First();
            user.UserAccountCreatedOn = data.Select(p => p.Field<DateTime>("USER_ACCOUNT_CREATED_ON_DATE_TIME")).First();
            user.UserAccountExpires = data.Select(p => p.Field<DateTime>("USER_ACCOUNT_EXPIRES_DATE_TIME")).First();
            user.UserLastLogon = data.Select(p => p.Field<DateTime>("USER_LAST_LOGON_DATE_TIME")).First();

            return user;
        }
    }
}
