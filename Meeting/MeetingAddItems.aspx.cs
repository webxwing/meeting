using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.Meeting
{
    public partial class MeetingAddItems : System.Web.UI.Page
    {
        private static meetingDataContext meetingContext = new meetingDataContext();
        private static meeting_itemsDataContext meeting_itemsContext = new meeting_itemsDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            //m_title.Text ="ss";
            
            string m_id = Request.QueryString["m_id"]!=null ?Request.QueryString["m_id"]:"";
            if (m_id == null || m_id == "") return;
            string m_title = Request.QueryString["m_title"] != null ? Request.QueryString["m_title"] : "";
            if (m_title ==null||m_title == "")
            {
                T_meeting a_meeting = meetingContext.T_meetings.Where(m => m.m_id == Int32.Parse(m_id)).Single();
                m_title = a_meeting.m_title; 
            }
            m_title_label.Text = m_title;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string[] sss = Session["files"]!=null ? Session["files"].ToString().Split(',') : new string[]{};
            //foreach (var item in sss)
            //{
            //    Response.Write(item+"<br>");
            //}
            string m_id = Request.QueryString["m_id"] != null ? Request.QueryString["m_id"] : "";
            if (m_id == null || m_id == "")
            {
                App_RegisterClientScriptBlock("infoMessage", "错误！", "未创建会议，不能添加议程！");
                btnSubmit.Enabled = false;
                return;
            }
            if (Request.Form["item_number"] == "" || Request.Form["item_title"] == "" || Request.Form["item_time"] == "")
            {
                App_RegisterClientScriptBlock("infoMessage", "错误！", "填写内容不完整！");
                btnSubmit.Enabled = false;
                return;
            }

            T_meeting_item a_meeting_item = new T_meeting_item()
            {
                m_id = Int32.Parse(Request.QueryString["m_id"].ToString()),
                item_title = Request.Form["item_title"].ToString().Trim(),
                item_content = Request.Form["item_content"].ToString().Trim(),
                item_number = Int32.Parse(Request.Form["item_number"]),
                item_time = Int32.Parse(Request.Form["item_time"]),
                item_files_url = Session["files"] != null ? Session["files"].ToString() : null,
                item_state = "未开始"
            };
            meeting_itemsContext.T_meeting_items.InsertOnSubmit(a_meeting_item);
            try
            {
                meeting_itemsContext.SubmitChanges();
                App_RegisterClientScriptBlock("infoMessage", "提示", "添加成功！");
                Response.Redirect("MeetingItems?m_id=" + m_id);
            }
            catch (Exception message)
            {
                App_RegisterClientScriptBlock("infoMessage", "错误", "添加失败！错误代码："+message.Message);
            }
            //清空上传的文件名
            Session["files"] = null;
        }
        //执行客户端脚本
        protected void App_RegisterClientScriptBlock(string key, string message_title, string message_info)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered(key))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(string), key, "$.messager.alert('" + message_title + "','" + message_info + "','info');", true);
            }
        }
    }
}