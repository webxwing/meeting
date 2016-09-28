using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;

namespace meeting.Meeting
{
    /// <summary>
    /// Summary description for MeetingItemsDelete
    /// </summary>
    public class MeetingItemsDelete : IHttpHandler
    {
        public meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string m_id = context.Request["m_id"] == null || context.Request["m_id"] == "" ? "-1" : context.Request["m_id"].ToString();
            string i_id = context.Request["i_id"] == null || context.Request["i_id"] == "" ? "-1" :
                context.Request["i_id"].ToString();
            string i_state = context.Request["i_state"] == null ? "" : context.Request["i_state"].ToString();
            string result = "error";
            var m_item = iDb.T_meeting_items.Where(i => i.m_id == Int32.Parse(m_id)).SingleOrDefault(m => m.item_id == Int32.Parse(i_id));

            if (i_state == "进行中")
            {
                result = "进行中，不允许删除！";
            }
            else if (m_item != null)
            {
                try
                {
                    iDb.T_meeting_items.DeleteOnSubmit(m_item);
                    iDb.SubmitChanges();
                    result = "ok";
                }
                catch (Exception)
                {
                    
                    result ="删除异常，请重试！";
                }
            }
            context.Response.Write(result);
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