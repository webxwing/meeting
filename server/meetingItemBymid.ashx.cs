using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for meetingItemBymid
    /// </summary>
    public class meetingItemBymid : IHttpHandler
    {
        private static meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/Json";
            int m_id = context.Request["m_id"] != null && context.Request["m_id"] != "" ? Int32.Parse(context.Request["m_id"]) : -1;
            var items = iDb.T_meeting_items.Where(i => i.m_id == m_id).OrderBy(s => s.item_number);
            var r = JsonConvert.SerializeObject(items);
            context.Response.Write(r);
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