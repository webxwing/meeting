using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for meetingbyid
    /// </summary>
    public class meetingbyid : IHttpHandler
    {
        private static meetingDataContext mDb = new meetingDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/Json";
            int m_id = context.Request["m_id"] != null && context.Request["m_id"] != "" ? Int32.Parse(context.Request["m_id"]) : -1;
            var m = mDb.T_meetings.SingleOrDefault(n => n.m_id == m_id);
            var json = JsonConvert.SerializeObject(m);
            context.Response.Write(json);
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