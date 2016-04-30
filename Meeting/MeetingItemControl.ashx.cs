using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;

namespace meeting.Meeting
{
    /// <summary>
    /// Summary description for MeetingItemControl
    /// </summary>
    public class MeetingItemControl : IHttpHandler
    {
        private meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string test = "sss"; 
            context.Response.Write(test);
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