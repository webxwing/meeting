using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;

namespace meeting.Meeting
{
    /// <summary>
    /// Summary description for MeetingControlHandler
    /// </summary>
    public class MeetingControlHandler : IHttpHandler
    {
        public meetingDataContext db = new meetingDataContext();
        public meeting_itemsDataContext mdb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/Json";
            string contronState = context.Request["cState"] != null && context.Request["cState"] != "" ? context.Request["cState"].ToString() : "";
            if (context.Request["m_id"] != null && context.Request["m_id"] != "")
            {
                var meeting = db.T_meetings.SingleOrDefault(m => m.m_id == Int32.Parse(context.Request["m_id"]));
                var m_id = context.Request["m_id"].ToString();
                var items = mdb.T_meeting_items.Where(item => item.m_id == Int32.Parse(m_id));
                if (meeting != null)
                {                    
                    meeting.m_state = context.Request["cState"];
                    if (context.Request["cState"].ToString() != "已结束" && items != null)
                    {
                        meeting.m_current_item = 0;
                        foreach (var item in items)
                        {
                            item.item_time_begin = null;
                            item.item_time_end = null;
                        }
                    }
                    try
                    {                        
                        db.SubmitChanges();
                        mdb.SubmitChanges();
                        context.Response.Write("ok");
                    }
                    catch (Exception)
                    {                        
                        context.Response.Write("error");
                    }
                }
                else
                {
                    context.Response.Write("error");
                }
                
            }
            else
            {
                context.Response.Write("{'error");
            }
            
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