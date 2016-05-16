using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.Meeting
{
    /// <summary>
    /// Summary description for MeetingItemsHandler
    /// </summary>
    public class MeetingItemsHandler : IHttpHandler
    {
        private meeting_itemsDataContext dbContext = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/Json";
            int pageIndex = context.Request["page"] == null || context.Request["page"] == "" ? 1 : Int32.Parse(context.Request["page"]);
            int pageSize = context.Request["rows"] == null || context.Request["rows"] == "" ? 1 : Int32.Parse(context.Request["rows"]);
            if (context.Request["m_id"] != null && context.Request["m_id"] != "")
            {
                var items = from item in dbContext.T_meeting_items
                            orderby item.item_number
                            where item.m_id == Int32.Parse(context.Request["m_id"])
                            select new
                            {
                                id = item.item_id,
                                item_title = item.item_title,
                                item_time = item.item_time,
                                item_state = item.item_state,
                                item_number = item.item_number
                            };
                var r = items.Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToList();
                string jsons = JsonConvert.SerializeObject(r);
                jsons = "{\"total\":" + items.Count() + ",\"rows\":" + jsons + "}";
                context.Response.Write(jsons);
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