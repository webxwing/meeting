using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for meetingItemCheck
    /// </summary>
    public class meetingItemCheck : IHttpHandler
    {
        private static meetingDataContext mDb = new meetingDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/Json";
            var  r = new Res();
            r.isCheck = false;
            r.currentItems = -1;
            int m_id = context.Request["m_id"] != null && context.Request["m_id"] != "" ? Int32.Parse(context.Request["m_id"]) : -1;
            int check_item_number = context.Request["m_current_item"] != null && context.Request["m_current_item"] != "" ? Int32.Parse(context.Request["m_current_item"]) : 99999;
            var meeting = mDb.T_meetings.SingleOrDefault(m => m.m_id == m_id);
            if (meeting != null && meeting.m_current_item > check_item_number)
            {
                r.isCheck = true;
                r.currentItems = meeting.m_current_item;
            }
            var json = JsonConvert.SerializeObject(r);
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
    public class Res
    {
        public bool isCheck { get; set; }
        public int? currentItems { get; set; }
    }
}