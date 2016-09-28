using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;

namespace meeting.Meeting
{
    /// <summary>
    /// Summary description for MeetingDelete
    /// </summary>
    public class MeetingDelete : IHttpHandler
    {
        public meetingDataContext mDb = new meetingDataContext();
        public meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string m_id = context.Request["m_id"] == null || context.Request["m_id"] == null ? "-1" : context.Request["m_id"].ToString();
            string i_state = context.Request["i_state"] == null || context.Request["i_state"] == null ? "" : context.Request["i_state"].ToString();
            string result = "error";
            var m_items = iDb.T_meeting_items.Where(i => i.m_id == Int32.Parse(m_id));
            var m_meeting = mDb.T_meetings.SingleOrDefault(m => m.m_id == Int32.Parse(m_id));
            if (i_state == "进行中")
            {
                result = "会议进行中，不允许删除！";
            }
            else if (m_meeting != null)
            {
                mDb.T_meetings.DeleteOnSubmit(m_meeting);
                if (m_items != null)
                {
                    iDb.T_meeting_items.DeleteAllOnSubmit(m_items);
                }
                try
                {
                    iDb.SubmitChanges();
                    mDb.SubmitChanges();
                    result = "ok";
                }
                catch (Exception)
                {
                    
                    throw;
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