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
    public partial class MeetingEditItemsS : System.Web.UI.Page
    {
        public meetingDataContext meetingContext = new meetingDataContext();
        public meeting_itemsDataContext meeting_itemsContext = new meeting_itemsDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string m_id = hfM_id.Value.ToString();
                string item_id = hfItem_id.Value.ToString();
                string item_number = hfItem_num.Value.ToString();
                string oldFilesStr = hfOldFile.Value.ToString();
                string fileStr = hfFilePath.Value.ToString();
                if (m_id == "" || item_id == "" || item_number == "")
                {
                    runClientScript("addOk", "parent.layer.msg('未发现相关会议及议程！', { time: 3000 });");
                    return;
                }
                var i_meeting_get = meeting_itemsContext.T_meeting_items.SingleOrDefault(i => i.item_id == Int32.Parse(item_id));
                if (i_meeting_get != null)
                {
                    i_meeting_get.item_title = Request.Form["item_title"].ToString().Trim();
                    //HTML转码
                    i_meeting_get.item_content = HttpUtility.HtmlEncode(Request.Form["item_content"].ToString().Trim());
                    i_meeting_get.item_number = Int32.Parse(item_number);
                    i_meeting_get.item_time = Int32.Parse(Request.Form["item_time"]);
                    i_meeting_get.item_files_url = fileStr;
                }
                try
                {
                    meeting_itemsContext.SubmitChanges(); runClientScript("addOk", "parent.layer.msg('修改成功！', { time: 3000 });closeLayer();"); 

                }
                catch (Exception catchMsg)
                {
                    meeting_itemsContext.SubmitChanges(); runClientScript("addOk", "parent.layer.msg('修改失败！'" + catchMsg.Message + ", { time: 3000 });");                   
                }
            }
            else
            {
                string m_id = Request.QueryString["m_id"] != null ? Request.QueryString["m_id"] : "";
                string item_id = Request.QueryString["item_id"] != null ? Request.QueryString["item_id"] : "";
                hfM_id.Value = m_id;
                hfItem_id.Value = item_id;
                if (m_id == ""||item_id=="")
                {
                    runClientScript("error", "parent.layer.msg('未发现相关会议及议程！', { time: 3000 });");
                    return;
                }

                //判断是否可以修改
                T_meeting a_meeting = meetingContext.T_meetings.SingleOrDefault(m => m.m_id == Int32.Parse(m_id));
                T_meeting_item i_meeting = meeting_itemsContext.T_meeting_items.SingleOrDefault(i => i.item_id == Int32.Parse(item_id));
                h5.InnerText = a_meeting.m_title;
                if (a_meeting.m_state != "未开始" || i_meeting.item_state != "未开始")
                {
                    runClientScript("error", "parent.layer.msg('当前会议正在进行中或已结束，不允许修改！', { time: 3000 });");
                    return;
                }
                hfItem_num.Value = i_meeting.item_number.ToString();
                item_title.Value = i_meeting.item_title;
                item_content.Value = HttpUtility.HtmlDecode(i_meeting.item_content);
                item_time.Value = i_meeting.item_time.ToString();
                //处理附件
                
                if (i_meeting.item_files_url != null && i_meeting.item_files_url != "")
                {
                    hfOldFile.Value = i_meeting.item_files_url;
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(i_meeting.item_files_url);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        runClientScript("filesOk" + i, "var $list = $('#thelist');$list.append('<div id=\"" + dt.Rows[i]["queueId"].ToString() + "\" class=\"item\"><div class=\"info\">" + dt.Rows[i]["name"].ToString() + "</div><p class=\"state\"><span class=\"text-danger\">已上传</span>&nbsp;&nbsp;<a href=\"javascript:void(0);\" class=\"btn btn-link deleteOldLink\">删除</a></p><hr/></div>');");
                    }
                }
                runClientScript("btnOk", "$('button[type=submit]').removeClass('disabled')");
               
            }
        }
        //执行客户端脚本
        public void runClientScript(string key, string clientScript)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), key, clientScript, true);
        }
    }
}