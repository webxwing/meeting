using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class Login : IHttpHandler
    {
        private UserDataContext uDb = new UserDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            var r = new result()
            {
                isLogin = false,
                msg = "程序参数错误!"
            };
            //loginName
            var user = uDb.T_users.SingleOrDefault(u => u.u_username == context.Request["loginName"].ToString());
            if (user == null)
            {
                r.msg = "用户名不存在！";
            }
            else
            {
                if (user.u_password == md5.MD5Encrypt(context.Request["loginPwd"].ToString(), md5.GetKey()))
                {
                    r.isLogin = true;
                    r.msg = user.u_actor;
                }
                else
                {
                    r.msg = "密码错误！";
                }
            }
            var json = JsonConvert.SerializeObject(r);
            context.Response.Write(json);
        }
        protected class result
        {
            public bool isLogin { get; set; }
            public string msg { get; set; }
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