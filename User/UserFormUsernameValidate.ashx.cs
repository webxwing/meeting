using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
namespace meeting.User
{
    /// <summary>
    /// Summary description for UserFormUsernameValidate
    /// </summary>
    public class UserFormUsernameValidate : IHttpHandler
    {
        public static UserDataContext dataContext = new UserDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string re = "true";
            if(context.Request["username"] != null)
            {
                T_user s = dataContext.T_users.Where(u => u.u_username == context.Request["username"].ToString()).SingleOrDefault();
                if (s != null)
                {
                    re = "false";
                }
            } 
            context.Response.Write(re);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}