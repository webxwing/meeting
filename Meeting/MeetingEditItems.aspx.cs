using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.Meeting
{
    public partial class MeetingEditItems : System.Web.UI.Page
    {
        private meetingDataContext mDb = new meetingDataContext();
        private meeting_itemsDataContext iDb = new meeting_itemsDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //初始化
            if (Request["m_id"] == null || Request["m_id"] == "" || Request["item_id"] == null || Request["item_id"] == "")
            {
                App_RegisterClientScriptBlock("infoMessage", "提示", "未找到要修改的会议！");
                btnSubmit.Enabled = false;
                return;
            }
            //判断是否可以修改
            T_meeting a_meeting = mDb.T_meetings.SingleOrDefault(m => m.m_id == Int32.Parse(Request["m_id"]));
            T_meeting_item i_meeting = iDb.T_meeting_items.SingleOrDefault(i => i.item_id == Int32.Parse(Request["item_id"]));
            if(a_meeting != null) m_title_label.Text = a_meeting.m_title;
            if (i_meeting == null) return;
            if (a_meeting.m_state != "未开始" || i_meeting.item_state != "未开始")
            {
                App_RegisterClientScriptBlock("infoMessage", "提示", "会议已结束或正在进行中，不允许修改！");
                btnSubmit.Enabled = false;
                return;
            }
            item_number.Value = i_meeting.item_number.ToString();
            item_title.Value = i_meeting.item_title;
            item_content.Value = i_meeting.item_content;
            item_time.Value = i_meeting.item_time.ToString();
            string[] item_files = new string[] { };
            string temp = "";
            if (i_meeting.item_files_url != null && i_meeting.item_files_url != "")
            {
                item_files = i_meeting.item_files_url.Split(',');
                foreach (var item in item_files)
                {
                    if(item == "") continue;
                    hideFiles.Value += item + ',';
                    var tempItem = item.Length > 35 ? item.Substring(0, 35) + "..." : item;
                    temp += "<li item_value ='"+item+"'>" + tempItem + "<a class='easyui-linkbutton' href=\"javascript:void(0);\">移除</a></li>";
                }
                uploadFiles.InnerHtml = temp;
            }

        }
        

        //确定按钮
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.Form["item_number"] == "" || Request.Form["item_title"] == "" || Request.Form["item_time"] == "")
            {
                App_RegisterClientScriptBlock("infoMessage", "错误！", "填写内容不完整！");
                btnSubmit.Enabled = false;
                return;
            }
            string item_files_url = null;
            if (hideFiles.Value != "")
            {
                if (Session["files"] != null)
                {
                    item_files_url = hideFiles.Value + Session["files"].ToString();
                }
                else
                {
                    item_files_url = hideFiles.Value.TrimEnd(',');
                }
            }
            else
            {
                if (Session["files"] != null)
                {
                    item_files_url = Session["files"].ToString();
                }
            }

            var i_meeting_get = iDb.T_meeting_items.SingleOrDefault(i => i.item_id == Int32.Parse(Request["item_id"]));
            if (i_meeting_get != null)
            {
                i_meeting_get.item_title = Request.Form["item_title"].ToString().Trim();
                i_meeting_get.item_content = Request.Form["item_content"].ToString().Trim();
                i_meeting_get.item_number = Int32.Parse(Request.Form["item_number"]);
                i_meeting_get.item_time = Int32.Parse(Request.Form["item_time"]);
                i_meeting_get.item_files_url = item_files_url;
                try
                {
                    iDb.SubmitChanges();
                    Page.ClientScript.RegisterClientScriptBlock(typeof(string), "confirm", "confirm_addItems('MeetingItems.aspx?m_id="+Request["m_id"]+"','修改成功，是否返回列表？');", true);

                }
                catch (Exception catchMsg)
                {

                    App_RegisterClientScriptBlock("infoMessage", "错误", "更新失败，请重试！错误代码：" + catchMsg.Message);
                }
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