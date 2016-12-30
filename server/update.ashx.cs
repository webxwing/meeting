using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace meeting.server
{
    /// <summary>
    /// Summary description for update
    /// </summary>
    public class update : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            //status 0  不提示更新
            //status 1  有更新
            //status 2  重要更新，强制更新
            string data = "{\"version\":\"0.9.4\",\"url\":\"http://10.35.10.203/download/AndroidApp.apk\",\"status\":\"1\"}";
            context.Response.Write(data);
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