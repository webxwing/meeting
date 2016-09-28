using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.User
{
    public partial class AddUserS : System.Web.UI.Page
    {
        public static UserDataContext dataContext = new UserDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Request.Form["u_username"] == "" || Request.Form["u_password"] == "")
                {
                    runClientScript("addError", "parent.layer.msg('用户名或密码为空！', { time: 2500 });closeLayer();");
                    return;
                }
                T_user user = new T_user()
                {
                    u_username = Request.Form["u_username"].Trim(),
                    u_password = md5.MD5Encrypt(Request.Form["u_password"].Trim(), md5.GetKey()),
                    u_actor = Request.Form["actor"].Trim()
                };
                dataContext.T_users.InsertOnSubmit(user);
                try
                {
                    dataContext.SubmitChanges();
                    runClientScript("addOK", "parent.layer.msg('用户添加成功！', { time: 2500 });closeLayer();");
                }
                catch (Exception catchMessage)
                {
                    runClientScript("addError", "parent.layer.msg('服务器失败："+catchMessage.Message+"', { time: 2500 });closeLayer();");
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