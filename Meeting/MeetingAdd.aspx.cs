using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.Metting
{
    public partial class AddMeeting : System.Web.UI.Page
    {
        private meetingDataContext dataContext = new meetingDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        //添加会议
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.Form["m_title"] == "" || Request.Form["m_date"] == "")
            {
                App_RegisterClientScriptBlock("infoMessage", "提示", "信息填写不完整！");
                return;
            }
            T_meeting a_meeting = new T_meeting()
            {
                m_title = Request.Form["m_title"].ToString().Trim(),
                m_theme = Request.Form["m_theme"].ToString().Trim(),
                m_abstract = Request.Form["m_abstruct"].ToString().Trim(),
                m_place = Request.Form["m_place"].ToString().Trim(),
                m_date = DateTime.Parse(Request.Form["m_date"].ToString().Trim()),
                m_compere = Request.Form["m_compere"].ToString().Trim(),
                m_code = Request.Form["m_code"].ToString().Trim(),
                m_state = "未开始",
                m_current_item = 0
            };
            dataContext.T_meetings.InsertOnSubmit(a_meeting);
            try
            {
                dataContext.SubmitChanges();
                //App_RegisterClientScriptBlock("infoMessage", "提示", "会议添加成功,请设置会议议程！");
                string meetingAddItemUrl = "MeetingAddItems.aspx?m_title=" + a_meeting.m_title + "&m_id=" + a_meeting.m_id;
                if (!Page.ClientScript.IsClientScriptBlockRegistered("confirm"))
                {
                    //跳转选择，客户端页面跳转
                    Page.ClientScript.RegisterClientScriptBlock(typeof(string), "confirm", "confirm_addItems('" + meetingAddItemUrl + "','创建成功，是否继续设置会议议程？');", true);
                }
            }
            catch (Exception catchMessage)
            {
                App_RegisterClientScriptBlock("infoMessage", "错误", "会议添加失败，请重试！错误代码：" + catchMessage.Message);
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