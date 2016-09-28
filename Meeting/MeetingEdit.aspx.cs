using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.Meeting
{
    public partial class MeetingEdit : System.Web.UI.Page
    {
        private meetingDataContext dataContext = new meetingDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化
                if (Request["m_id"] == null || Request["m_id"] == "")
                {
                    App_RegisterClientScriptBlock("infoMessage", "提示", "未找到要修改的会议！");
                    btnSubmit.Enabled = false;
                    return;
                }
                var a_meeting_get = dataContext.T_meetings.Where(m => m.m_id == Int32.Parse(Request["m_id"])).Single();
            
                m_title.Value = a_meeting_get.m_title;
                m_theme.Value = a_meeting_get.m_theme;
                m_abstruct.Value = a_meeting_get.m_abstract;
                m_place.Value = a_meeting_get.m_place;
                m_date.Value = a_meeting_get.m_date.ToString();
                m_compere.Value = a_meeting_get.m_compere;
                m_code.Value = a_meeting_get.m_code;
                if (a_meeting_get.m_state != "未开始")
                {
                    App_RegisterClientScriptBlock("infoMessage", "提示", "会议已结束或正在进行中，不允许修改！");
                    btnSubmit.Enabled = false;
                }
            }
        }

        //修改
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.Form["m_title"] == "" || Request.Form["m_date"] == "")
            {
                App_RegisterClientScriptBlock("infoMessage", "提示", "信息填写不完整！");
                return;
            }
            T_meeting meeting = dataContext.T_meetings.SingleOrDefault(m => m.m_id == Int32.Parse(Request["m_id"]));
            meeting.m_title = Request.Form["m_title"].ToString().Trim();
            meeting.m_theme = Request.Form["m_theme"].ToString().Trim();
            meeting.m_abstract = Request.Form["m_abstruct"].ToString().Trim();
            meeting.m_place = Request.Form["m_place"].ToString().Trim();
            meeting.m_date = DateTime.Parse(Request.Form["m_date"].ToString().Trim());
            meeting.m_compere = Request.Form["m_compere"].ToString().Trim();
            meeting.m_code = Request.Form["m_code"].ToString().Trim();
            try
            {
                dataContext.SubmitChanges();
                Page.ClientScript.RegisterClientScriptBlock(typeof(string), "confirm", "confirm_addItems('MeetingList.aspx','修改成功，是否返回列表？');", true);
            }
            catch (Exception catchMessage)
            {

                App_RegisterClientScriptBlock("infoMessage", "错误", "更新失败，请重试！错误代码：" + catchMessage.Message);
            }
            

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