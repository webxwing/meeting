using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.server
{
    /// <summary>
    /// Summary description for meetingItemControl
    /// </summary>
    public class meetingItemControl : IHttpHandler
    {
        public meetingDataContext mDb = new meetingDataContext();
        public meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            Response_obj res = new Response_obj();
            res.isNextItem = false;
            res.msg = "未发现相关会议信息！";
            int m_id = context.Request["m_id"] != null && context.Request["m_id"] != "" ? Int32.Parse(context.Request["m_id"]) : -1;
            var meeting = mDb.T_meetings.SingleOrDefault(m => m.m_id == m_id);
            if (meeting != null && meeting.m_state == "进行中")
            {
                int? currentItem = meeting.m_current_item;
                if (currentItem != null)
                {
                    var last_item = iDb.T_meeting_items.Where(i => i.m_id == m_id).Where(s => s.item_number > currentItem).FirstOrDefault();
                    var current_item = iDb.T_meeting_items.Where(i => i.m_id == m_id).Where(s => s.item_number == currentItem).FirstOrDefault();
                    if (last_item != null)
                    {
                        //回写数据库
                        meeting.m_current_item = last_item.item_number; 
                        string begin_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        last_item.item_time_begin = begin_time;
                        current_item.item_time_end = begin_time;
                        try
                        {
                            mDb.SubmitChanges();
                            iDb.SubmitChanges();
                            res.isNextItem = true;
                            res.currentItemsNumber = last_item.item_number;
                            res.itemTime = last_item.item_time;
                            res.msg = "进入下一议程！";
                        }
                        catch (Exception)
                        {
                            res.msg = "回写失败，请重试！";
                        }
                    }
                    else
                    {
                        res.msg = "进入失败，当前已是最后！";
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
    }
    public class Response_obj
    {
        //是否进入下一议程
        public bool isNextItem { get; set; }
        //反馈信息
        public string msg { get; set; }
        //当前议程
        public int currentItemsNumber { get; set; }
        //议程所需时间
        public int? itemTime { get; set; }
    }
}