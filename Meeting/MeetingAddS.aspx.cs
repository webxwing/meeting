using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.Meeting
{
    public partial class MeetingAddS : System.Web.UI.Page
    {
        public meetingDataContext dataContext = new meetingDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Request.Form["m_title"] == "" || Request.Form["m_date"] == "")
                {
                    runClientScript("addOk", "parent.layer.msg('会议信息填写不完整！', { time: 3000 });");
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
                    m_current_item = 0,
                    m_pass = false
                };
                dataContext.T_meetings.InsertOnSubmit(a_meeting);
                try
                {
                    dataContext.SubmitChanges();
                    runClientScript("addOk", "parent.layer.msg('添加成功！', { time: 2500 });closeLayer();");
                }
                catch (Exception catchMessage)
                {
                    runClientScript("catchMessage", "parent.layer.msg('后台服务器错误："+catchMessage.Message+"', { time: 2500 });");
                    
                }
            }            

        }
        //执行客户端脚本
        public void runClientScript(string key,string clientScript)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), key, clientScript, true);
        }        
    }
}