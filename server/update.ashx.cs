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
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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