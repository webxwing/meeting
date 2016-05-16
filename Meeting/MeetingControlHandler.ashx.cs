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
        private meetingDataContext db = new meetingDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/Json";
            string contronState = context.Request["cState"] != null && context.Request["cState"] != "" ? context.Request["cState"].ToString() : "";
            if (context.Request["m_id"] != null && context.Request["m_id"] != "")
            {
                var meeting = db.T_meetings.SingleOrDefault(m => m.m_id == Int32.Parse(context.Request["m_id"]));
                if (meeting != null)
                {
                    meeting.m_state = context.Request["cState"];
                    try
                    {
                        db.SubmitChanges();
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