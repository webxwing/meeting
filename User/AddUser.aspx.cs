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
    public partial class AddUser : System.Web.UI.Page
    {
        private static UserDataContext dataContext = new UserDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.Form["username"] == "" || Request.Form["password"] == "")
            {
                App_RegisterClientScriptBlock("infoMessage", "错误", "用户名或密码为空！");
                return;
            }
            T_user user = new T_user()
            {
                u_username = Request.Form["username"],
                u_password = md5.MD5Encrypt(Request.Form["password"], md5.GetKey()),
                u_actor = Request.Form["select_actor"]
            };
            dataContext.T_users.InsertOnSubmit(user);
            try
            {
                dataContext.SubmitChanges();
                App_RegisterClientScriptBlock("infoMessage", "提示", "用户" + Request.Form["username"] + "添加成功！");
            }
            catch (Exception catchMessage)
            {

                App_RegisterClientScriptBlock("infoMessage", "错误", "用户添加失败，请重试！错误代码：" + catchMessage.Message);
            }
            
            
            //ado.net模式
            //string sqlAdd = "insert into T_user values(@username,@password,@actor)";

            //if (Sqlhelper.ExcuteNonQuery(sqlAdd, new SqlParameter("username", Request.Form["username"]), new SqlParameter("password", md5.MD5Encrypt(Request.Form["password"], md5.GetKey())), new SqlParameter("actor", Request.Form["select_actor"])) > 0)
            //{
            //    App_RegisterClientScriptBlock("infoMessage", "提示", "用户" + Request.Form["username"] + "添加成功！");
            //}
            //else
            //{
            //    App_RegisterClientScriptBlock("infoMessage", "错误", "用户" + Request.Form["username"] + "添加失败，请重试！");
            //}
            
            
        }

        //执行客户端脚本
        protected void App_RegisterClientScriptBlock(string key, string message_title,string message_info)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered(key))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(string), key, "$.messager.alert('" + message_title + "','" + message_info + "','info');", true);
            }   
        }
    }
}