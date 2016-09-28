using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;
using Newtonsoft.Json;

namespace meeting.Meeting
{
    public partial class MeetingAddItemsS : System.Web.UI.Page
    {
        public meetingDataContext meetingContext = new meetingDataContext();
        public meeting_itemsDataContext meeting_itemsContext = new meeting_itemsDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string m_id = hfM_id.Value != null ? hfM_id.Value : "";
                
                if (m_id == null || m_id == "")
                {
                    runClientScript("addOk", "parent.layer.msg('未发现会议！', { time: 3000 });");
                    return;
                }
                if (Request.Form["item_title"] == "" || Request.Form["item_time"] == "")
                {
                    runClientScript("addNoInfo", "parent.layer.msg('议程信息填写不完整！', { time: 3000 });");
                    return;                    
                }

                //生成当前添加议程item_number
                int var_item_number = 0;
                try
                {
                    T_meeting_item item = meeting_itemsContext.T_meeting_items.Where(m => m.m_id == Int32.Parse(m_id)).OrderByDescending(o => o.item_number).First();
                    var_item_number = item.item_number + 1;
                }
                catch (Exception)
                {
                    var_item_number = 1;               
                }
                //处理上传文件
                //string files = "" ;
                //if (hfFilePath.Value != "")
                //{
                //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(hfFilePath.Value);
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        files = files + dt.Rows[i]["name"].ToString() + ",";
                //    }
                //    files.TrimEnd(',');
                //}

                T_meeting_item a_meeting_item = new T_meeting_item()
                {
                    m_id = Int32.Parse(m_id),
                    item_title = Request.Form["item_title"].ToString().Trim(),
                    item_content = HttpUtility.HtmlEncode(Request.Form["item_content"].ToString().Trim()),
                    item_number = var_item_number,
                    item_time = Int32.Parse(Request.Form["item_time"]),
                    item_files_url = hfFilePath.Value,
                    item_state = "未开始"
                };
                meeting_itemsContext.T_meeting_items.InsertOnSubmit(a_meeting_item);
                try
                {
                    meeting_itemsContext.SubmitChanges();                    
                    runClientScript("addOk", "parent.layer.msg('添加成功！', { time: 3000 });");
                    runClientScript("closeLayer", "closeLayer();");
                }
                catch (Exception exc)
                {
                    runClientScript("addError", "parent.layer.msg('添加失败！'" + exc.Message + ", { time: 3000 });");
                }

            }
            else 
            {//!ispostback
                string m_id = Request.QueryString["m_id"] != null ? Request.QueryString["m_id"] : "";
                if (m_id == null || m_id == "")
                {
                    runClientScript("addOk", "parent.layer.msg('未发现会议！', { time: 3000 });");
                    return;
                }
                hfM_id.Value = m_id;
                string m_title = Request.QueryString["m_title"] != null ? Request.QueryString["m_title"] : "";
                if (m_title == null || m_title == "")
                {
                    T_meeting a_meeting = meetingContext.T_meetings.Where(m => m.m_id == Int32.Parse(m_id)).Single();
                    m_title = a_meeting.m_title;
                }
                h5.InnerText = m_title;
                runClientScript("btnOk", "$('button[type=submit]').removeClass('disabled');");
            }
            
        }
        //执行客户端脚本
        public void runClientScript(string key, string clientScript)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), key, clientScript, true);
        }
    }
}