using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;

namespace meeting.Meeting.ashx
{
    /// <summary>
    /// Summary description for MeetingItemsPush
    /// </summary>
    public class MeetingItemsPush : IHttpHandler
    {
        public meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string m_id = context.Request["m_id"] == null || context.Request["m_id"] == "" ? "-1" : context.Request["m_id"].ToString();
            string i_id = context.Request["i_id"] == null || context.Request["i_id"] == "" ? "-1" : context.Request["i_id"].ToString();
            string i_num = context.Request["i_num"] == null || context.Request["i_num"] == "" ? "-1" :
                context.Request["i_num"].ToString();
            string i_state = context.Request["i_state"] == null ? "" : context.Request["i_state"].ToString();
            string oper = context.Request["operation"] == null ? "" : context.Request["operation"].ToString();
            string result = "error";
            //1 已是第一个 2 已是最后一个 3 成功 other 失败
            if (i_state == "进行中")
            {
                result = "进行中，不允许操作！";
            }
            else
            {
                //查询最大序号值
                T_meeting_item item_max = iDb.T_meeting_items.Where(m => m.m_id == Int32.Parse(m_id)).OrderByDescending(o => o.item_number).First();
                T_meeting_item item_select = iDb.T_meeting_items.Where(i => i.item_id == Int32.Parse(i_id)).SingleOrDefault();
                string tempNum = "-1";
                if (oper == "up" && Int32.Parse(i_num) > 1)
                {
                    T_meeting_item item_up = iDb.T_meeting_items.Where(m => m.m_id == Int32.Parse(m_id)).OrderByDescending(i => i.item_number).Where(i => i.item_number < Int32.Parse(i_num)).First();
                    //交换序号值
                    tempNum = item_up.item_number.ToString();
                    item_up.item_number = item_select.item_number;
                    item_select.item_number = Int32.Parse(tempNum);
                    try
                    {
                        iDb.SubmitChanges();
                    }
                    catch (Exception)
                    {

                        result = "修改序号失败！";
                    }

                    result = "3";
                }
                else if (oper == "down" && Int32.Parse(i_num) < item_max.item_number)
                {
                    T_meeting_item item_down = iDb.T_meeting_items.Where(m => m.m_id == Int32.Parse(m_id)).OrderBy(i => i.item_number).Where(i => i.item_number > Int32.Parse(i_num)).First();
                    //交换序号值
                    tempNum = item_down.item_number.ToString();
                    item_down.item_number = item_select.item_number;
                    item_select.item_number = Int32.Parse(tempNum);
                    try
                    {
                        iDb.SubmitChanges();
                    }
                    catch (Exception)
                    {

                        result = "修改序号失败！";
                    }
                    result = "3";
                }
                else if (oper == "up" && Int32.Parse(i_num) <= 1)
                {
                    result = "1";
                }
                else if (oper == "down" && Int32.Parse(i_num) >= item_max.item_number)
                {
                    result = "2";
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