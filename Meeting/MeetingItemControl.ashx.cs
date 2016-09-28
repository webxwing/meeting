using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;

namespace meeting.Meeting
{
    /// <summary>
    /// Summary description for MeetingItemControl
    /// </summary>
    public class MeetingItemControl : IHttpHandler
    {
        public meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string m_id = context.Request["m_id"] == null || context.Request["m_id"] == null ? "-1" : context.Request["m_id"].ToString();
            string i_id = context.Request["i_id"] == null || context.Request["i_id"] == null ? "-1" :
                context.Request["i_id"].ToString();
            string i_state = context.Request["i_state"] == null || context.Request["i_state"] == null ? "" :context.Request["i_state"].ToString();
            string result = "error";
            var m_items = iDb.T_meeting_items.Where(i => i.m_id == Int32.Parse(m_id));
            switch (i_state)
            {
                case "进行中":
                    foreach (var item in m_items)
                    {
                        if (item.item_state == "进行中") item.item_state = "已结束";
                        if (item.item_id == Int32.Parse(i_id)) item.item_state = "进行中";
                    }
                    result = "ok";
                    break;
                case "已结束":
                    foreach (var item in m_items)
                    {
                        if (item.item_state == "进行中") item.item_state = "已结束";
                        if (item.item_id == Int32.Parse(i_id)) item.item_state = "已结束";
                    }
                    result = "ok";
                    break;
                case "未开始":
                    foreach (var item in m_items)
                    {
                        if (item.item_state == "进行中") item.item_state = "已结束";
                        if (item.item_id == Int32.Parse(i_id)) item.item_state = "未开始";
                    }
                    result = "ok";
                    break;
                default: break;
            }
            try
            {
                iDb.SubmitChanges();
                result = "ok";
            }
            catch (Exception)
            {

                throw;
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