using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.Meeting
{
    public partial class MeetingItems : System.Web.UI.Page
    {
        private static meetingDataContext meetingContext = new meetingDataContext();
        private meeting_itemsDataContext a_meeting_item = new meeting_itemsDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["m_id"] == null || Request["m_id"] == "") return;
            T_meeting a_meeting = meetingContext.T_meetings.Where(m => m.m_id == Int32.Parse(Request["m_id"])).Single();
            m_title_label.Text = a_meeting.m_title;
        }
    }
}