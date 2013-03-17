using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;
using SqlRepositories.Helper.Extensions;
using DomainModel.Models.SecurityModels;

namespace SqlRepositories.Helper.Builders
{
    public static class RoleBuilder
    {
        public static string[] ToRoleStringSet(this DataSet oSet)
        {
            return (from role in oSet.FirstDataTableAsEnumerable()
                    select role.Field<string>("ROLE")).ToArray();
        }

        public static IEnumerable<Role> ToRoleSet(this DataSet oSet)
        {
            return from role in oSet.FirstDataTableAsEnumerable()
                   select new Role()
                   {
                       RoleKey = role.Field<Guid>("ROLE_KEY"),
                       RoleName = role.Field<string>("ROLE"),
                       RoleDescription = role.Field<string>("ROLE_DESCRIPTION")
                   };
        }
    }
}
