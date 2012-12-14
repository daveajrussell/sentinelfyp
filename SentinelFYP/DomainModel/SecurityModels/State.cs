using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DomainModel.SecurityModels
{
    public static class State
    {
        public static User User
        {
            get
            {
                if (HttpContext.Current.Session["User"] != null)
                    return (User)HttpContext.Current.Session["User"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["User"] = value;
            }
        }

        public static Session Session
        {
            get
            {
                if (HttpContext.Current.Session["Session"] != null)
                    return (Session)HttpContext.Current.Session["Session"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["Session"] = value;
            }
        }
    }
}
