using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for meetingfrompage
    /// </summary>
    public class meetingfrompage : IHttpHandler
    {
        private static meetingDataContext dataContext = new meetingDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/Json";
            int pageIndex = context.Request["page"] == null || context.Request["page"] == "" ? 1 : Int32.Parse(context.Request["page"]);
            int pageSize = context.Request["rows"] == null || context.Request["rows"] == "" ? 7 : Int32.Parse(context.Request["rows"]);
            var s = from meetings in dataContext.T_meetings
                    orderby meetings.m_state
                    select meetings;
            var r = s.Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToList();

            var jsons = JsonConvert.SerializeObject(r);
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