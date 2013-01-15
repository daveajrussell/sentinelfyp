using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;
using SqlRepositories.Helper.Extensions;

namespace SqlRepositories.Helper.Builders
{
    public static class RoleBuilder
    {
        public static string[] ToRoleSet(this DataSet oSet)
        {
            return (from role in oSet.FirstDataTableAsEnumerable()
                    select role.Field<string>("ROLE")).ToArray();
        }
    }
}
