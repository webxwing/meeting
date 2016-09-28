using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.Meeting.ashx
{
    /// <summary>
    /// Summary description for ShengHe
    /// </summary>
    public class ShengHe : IHttpHandler
    {
        public meetingDataContext db = new meetingDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var r = new result()
            {
                isOk = false,
                msg = "程序参数错误!"
            };
            if (context.Request["m_id"] != null && context.Request["m_id"] != "")
            {
                var meeting = db.T_meetings.SingleOrDefault(m => m.m_id == Int32.Parse(context.Request["m_id"]));
                if (meeting != null)
                {
                    meeting.m_pass = true;
                    try
                    {
                        db.SubmitChanges();
                        r.isOk = true;
                        r.msg = "审核成功！";
                    }
                    catch (Exception e)
                    {

                        r.msg = e.Message;
                    }
                }
                else
                {
                    r.msg = "未发现需要审核的会议！";
                }

            }
            var json = JsonConvert.SerializeObject(r);
            context.Response.Write(json);
        }

        public class result
        {
            public bool isOk {get;set;}
            public string msg { get; set; }
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