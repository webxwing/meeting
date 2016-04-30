using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.User
{
    /// <summary>
    /// Summary description for UserHandler1
    /// </summary>
    public class UserHandler1 : IHttpHandler
    {
        private static UserDataContext dataContext = new UserDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["id"] != null)
            {
                var u = from user in dataContext.T_users
                        where user.u_id == Int32.Parse(context.Request["id"])
                        select new
                        {
                            id = user.u_id,
                            username = user.u_username,
                            password = "******",
                            actor = getActorName(user.u_actor)
                        };
                string json = JsonConvert.SerializeObject(u);
                context.Response.Write("{\"total\":1,\"rows\":" + json + "}");

            }
            else
            //返回所有用户
            {
                var s = from user in dataContext.T_users
                        select new
                        {
                            id = user.u_id,
                            username = user.u_username,
                            password = "******",
                            actor = getActorName(user.u_actor)
                        };
                string jsons = JsonConvert.SerializeObject(s);
                jsons = "{\"total\":" + s.Count() + ",\"rows\":" + jsons +"}";
                context.Response.Write(jsons);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 角色value和text的转换
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        protected string getActorName(string actor)
        {
            switch (actor)
            {
                case "user": return "普通用户";
                case "admin": return "管理员";
                case "superAdmin": return "超级管理员";
                default:
                    break;
            }
            return "";
        }
    }
}