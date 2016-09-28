using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;
namespace meeting.Meeting
{
    /// <summary>
    /// Summary description for MeetingHandler
    /// </summary>
    public class MeetingHandler : IHttpHandler
    {
        public meetingDataContext dataContext = new meetingDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/Json";
            context.Response.Cache.SetNoStore();
            int pageIndex = context.Request["page"] == null || context.Request["page"] == "" ? 1 : Int32.Parse(context.Request["page"]);
            int pageSize = context.Request["rows"] == null || context.Request["rows"] == "" ? 10 : Int32.Parse(context.Request["rows"]);
            var s = from meetings in dataContext.T_meetings
                    orderby meetings.m_date descending
                    select new 
                    {
                        id = meetings.m_id,
                        m_title = meetings.m_title,
                        m_date = meetings.m_date,
                        m_place = meetings.m_place,
                        m_state = meetings.m_state,
                        m_pass = meetings.m_pass
                    };
            var r = s.Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToList();
            
            string jsons = JsonConvert.SerializeObject(r);
            jsons = "{\"total\":" + s.Count() + ",\"rows\":" + jsons + "}";
            context.Response.Write(jsons);
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