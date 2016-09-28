using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for meetingitembymidForTV
    /// </summary>
    public class meetingitembymidForTV : IHttpHandler
    {
        private meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            int m_id = context.Request["m_id"] != null && context.Request["m_id"] != "" ? Int32.Parse(context.Request["m_id"]) : -1;
            int m_current_item = context.Request["m_current_item"] != null && context.Request["m_current_item"] != "" ? Int32.Parse(context.Request["m_current_item"]) : -1;
            var item = iDb.T_meeting_items.Where(i => i.m_id == m_id).Where(n => n.item_number == m_current_item).SingleOrDefault();
            var r = JsonConvert.SerializeObject(item);
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