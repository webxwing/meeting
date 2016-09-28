using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting
{
    public partial class Index : System.Web.UI.Page
    {
        public UserDataContext dataContext = new UserDataContext(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            string username = txt_count.Text.Trim();
            string password = txt_pass.Text.Trim();            
            if (username == "" || password == "")
            {
                App_RegisterClientScriptBlock("infoMessage", "错误", "帐号或密码不能为空！");
                return;
            }

            password = md5.MD5Encrypt(password, md5.GetKey());
            T_user user = dataContext.T_users.SingleOrDefault(u => u.u_username == username && u.u_password == password);
            if (user == null)
            {
                App_RegisterClientScriptBlock("infoMessage", "错误", "帐号或密码错误，请重试！");
            }
            else
            {
                Response.Redirect("main.aspx",false);
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