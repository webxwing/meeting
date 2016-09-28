using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for meetingItemReback
    /// </summary>
    public class meetingItemReback : IHttpHandler
    {
        public meetingDataContext mDb = new meetingDataContext();
        public meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Response_obj res = new Response_obj();
            res.isProItem = false;
            res.msg = "未发现相关会议信息！";
            int m_id = context.Request["m_id"] != null && context.Request["m_id"] != "" ? Int32.Parse(context.Request["m_id"]) : -1;
            var meeting = mDb.T_meetings.SingleOrDefault(m => m.m_id == m_id);
            if (meeting != null && meeting.m_state == "进行中")
            {
                int? currentItem = meeting.m_current_item;
                if (currentItem != null)
                {
                    var pro_item = iDb.T_meeting_items.Where(i => i.m_id == m_id).Where(s => s.item_number < currentItem).OrderByDescending(s=>s.item_number).FirstOrDefault();
                    var current_item = iDb.T_meeting_items.Where(i => i.m_id == m_id).Where(s => s.item_number == currentItem).FirstOrDefault();
                    if (pro_item != null)
                    {
                        //回写数据库
                        meeting.m_current_item = pro_item.item_number;
                        string begin_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //重新记录开始时间 
                        current_item.item_time_begin = null; 
                        pro_item.item_time_begin = begin_time;
                        pro_item.item_time_end = null;  
                        try
                        {
                            mDb.SubmitChanges();
                            iDb.SubmitChanges();
                            res.isProItem = true;
                            res.currentItemsNumber = pro_item.item_number;
                            res.itemTime = pro_item.item_time;
                            res.msg = "进入上一议程！";
                        }
                        catch (Exception)
                        {
                            res.msg = "回写失败，请重试！";
                        }
                    }
                    else
                    {
                        res.msg = "当前已是第一个议程";
                    }
                }
            }
            else
            {
                res.msg = "失败，会议未开启！";
            }
            var json = JsonConvert.SerializeObject(res);
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public class Response_obj
        {
            //是否进入上一议程
            public bool isProItem { get; set; }
            //反馈信息
            public string msg { get; set; }
            //当前议程
            public int currentItemsNumber { get; set; }
            //议程所需时间
            public int? itemTime { get; set; }
        }
    }
}