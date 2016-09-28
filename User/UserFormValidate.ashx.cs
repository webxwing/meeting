using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
namespace meeting.User
{
    /// <summary>
    /// Summary description for UserFormValidate
    /// </summary>
    public class UserFormValidate : IHttpHandler
    {
        public static UserDataContext udb = new UserDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string re = "false";
            string username = context.Request["username"] != null ? context.Request["username"].ToString() : "";
            string password = context.Request["password"] != null ? context.Request["password"].ToString() : "";
            var user = udb.T_users.SingleOrDefault(u => u.u_username == username);
            if (user.u_password == md5.MD5Encrypt(context.Request["password"].ToString(), md5.GetKey()))
            {
                re = "true";
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