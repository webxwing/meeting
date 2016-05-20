using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace meeting.server
{
    /// <summary>
    /// Summary description for updateForTV
    /// </summary>
    public class updateForTV : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //status 0  不提示更新
            //status 1  有更新
            //status 2  重要更新，强制更新
            string data = "{\"version\":\"0.9.0\",\"url\":\"http://118.114.252.172:2081/download/xfxc0.9.4.apk\",\"status\":\"1\"}";
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