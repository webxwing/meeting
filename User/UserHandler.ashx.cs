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
        public static UserDataContext dataContext = new UserDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int pageIndex = context.Request["page"] == null || context.Request["page"] == "" ? 1 : Int32.Parse(context.Request["page"]);
            int pageSize = context.Request["rows"] == null || context.Request["rows"] == "" ? 10 : Int32.Parse(context.Request["rows"]);
            if (context.Request["id"] != null)
            {
                var s = from user in dataContext.T_users
                        where user.u_id == Int32.Parse(context.Request["id"])
                        select new
                        {
                            id = user.u_id,
                            username = user.u_username,
                            password = md5.MD5Encrypt(user.u_password,md5.GetKey()),
                            actor = getActorName(user.u_actor)
                        };
                string json = JsonConvert.SerializeObject(s);
                context.Response.Write("{\"total\":1,\"rows\":" + json + "}");

            }
            else
            //返回所有用户
            {
                var s = from var_user in dataContext.T_users
                        select new
                        {
                            id = var_user.u_id,
                            username = var_user.u_username,
                            password = getPassword(md5.MD5Decrypt(var_user.u_password, md5.GetKey())),
                            actor = getActorName(var_user.u_actor)
                        };
                var r = s.Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToList();
                string jsons = JsonConvert.SerializeObject(r);
                jsons = "{\"total\":" + s.Count() + ",\"rows\":" + jsons +"}";
                context.Response.Write(jsons);
            }
        }

        //密码处理
        private string getPassword(string p)
        {
            return p.Substring(0, 1) + "*******";
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