using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.Meeting
{
    public partial class MeetingEditS : System.Web.UI.Page
    {
        public meetingDataContext dataContext = new meetingDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化
                if (Request["m_id"] == null || Request["m_id"] == "")
                {
                    //App_RegisterClientScriptBlock("infoMessage", "提示", "未找到要修改的会议！");
                    //btnSubmit.Enabled = false;
                    return;
                }
                txt_m_id.Value = Request["m_id"];
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
                    runClientScript("editError", "parent.layer.msg('会议" + a_meeting_get.m_state + "，不允许修改！', { time: 3000 });");
                    return;
                }
                runClientScript("btnOK", "$('button[type=submit]').removeClass('disabled');");
            }
            else
            { 
                //postback
                if (Request.Form["m_title"] == "" || Request.Form["m_date"] == "")
                {
                    runClientScript("addOk", "parent.layer.msg('会议信息填写不完整！', { time: 3000 });");
                    return;
                }
                T_meeting meeting = dataContext.T_meetings.SingleOrDefault(m => m.m_id == Int32.Parse(txt_m_id.Value));
                meeting.m_title = Request.Form["m_title"].ToString().Trim();
                meeting.m_theme = Request.Form["m_theme"].ToString().Trim();
                meeting.m_abstract = Request.Form["m_abstruct"].ToString().Trim();
                meeting.m_place = Request.Form["m_place"].ToString().Trim();
                meeting.m_date = DateTime.Parse(Request.Form["m_date"].ToString().Trim());
                meeting.m_compere = Request.Form["m_compere"].ToString().Trim();
                meeting.m_code = Request.Form["m_code"].ToString().Trim();
                meeting.m_pass = false;
                try
                {
                    dataContext.SubmitChanges();
                    runClientScript("addOk", "parent.layer.msg('修改成功！', { time: 2000 });closeLayer();");

                }
                catch (Exception catchMessage)
                {
                    runClientScript("catchMessage", "parent.layer.msg('后台服务器错误：" + catchMessage.Message + "', { time: 2500 });");                    
                }
            }
        }
        //执行客户端脚本
        public void runClientScript(string key, string clientScript)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), key, clientScript, true);
        }
    }
}