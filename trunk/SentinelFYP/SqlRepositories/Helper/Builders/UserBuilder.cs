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
            return (from user in oSet.FirstDataTableAsEnumerable()
                    select new User()
                    {
                        UserKey = user.Field<Guid>("USER_KEY"),
                        UserCompanyKey = user.Field<Guid>("USER_COMPANY_KEY"),
                        UserName = user.Field<string>("USERNAME"),
                        FirstName = user.Field<string>("USER_FIRST_NAME"),
                        LastName = user.Field<string>("USER_LAST_NAME"),
                        UserContactNumber = user.Field<string>("USER_CONTACT_NUMBER"),
                        Email = user.Field<string>("USER_EMAIL"),
                        UserAccountCreatedOn = user.Field<DateTime>("USER_ACCOUNT_CREATED_ON_DATE_TIME"),
                        UserAccountExpires = user.Field<DateTime>("USER_ACCOUNT_EXPIRES_DATE_TIME"),
                        UserLastLogon = user.Field<DateTime>("USER_LAST_LOGON_DATE_TIME")
                    }).First();
        }

        public static IEnumerable<User> ToUserSet(this DataSet oSet)
        {
            return from user in oSet.FirstDataTableAsEnumerable()
                   select new User()
                   {
                       UserKey = user.Field<Guid>("USER_KEY"),
                       UserName = user.Field<string>("USERNAME"),
                       FirstName = user.Field<string>("USER_FIRST_NAME"),
                       LastName = user.Field<string>("USER_LAST_NAME"),
                       UserContactNumber = user.Field<string>("USER_CONTACT_NUMBER"),
                       Email = user.Field<string>("USER_EMAIL"),
                       UserAccountCreatedOn = user.Field<DateTime>("USER_ACCOUNT_CREATED_ON_DATE_TIME"),
                       UserAccountExpires = user.Field<DateTime>("USER_ACCOUNT_EXPIRES_DATE_TIME"),
                       UserLastLogon = user.Field<DateTime>("USER_LAST_LOGON_DATE_TIME")
                   };
        }
    }
}
