using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.SecurityModels;
using System.Data;
using SqlRepositories.Helper.Extensions;

namespace SqlRepositories.Helper.Builders
{
    public static class SessionBuilder
    {
            public static Session ToSession(this DataSet set)
            {
                var session = new Session();
                var data = set.SecondDataTableAsEnumerable();

                session.SessionID = data.Select(p => p.Field<int>("SESSION_ID")).First();
                session.SessionBeginDateTime = data.Select(p => p.Field<DateTime>("SESSION_BEGIN_DATE_TIME")).First();

                return session;
            }
    }
}
