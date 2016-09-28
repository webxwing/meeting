using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using meeting.Model;

namespace meeting.User
{
    public partial class UserEditS : System.Web.UI.Page
    {
        public static UserDataContext udb = new UserDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                //初始化
                string u_id = Request["u_id"] != null ? Request["u_id"].ToString() : "-1";
                T_user user = udb.T_users.Where(u => u.u_id == Int32.Parse(u_id)).SingleOrDefault();
                if (user == null) return;
                u_username.Value = user.u_username;
                hfU_id.Value = u_id;
                runClientScript("radioCheck", "$('input[value="+user.u_actor+"]').attr(\"checked\",\"true\");");
                    //$('input[value=user]').attr("checked","true");
            }
            else 
            {
                //保存
                string u_id = hfU_id.Value;
                if (Request.Form["u_password"].ToString().Trim() == "")
                {
                    runClientScript("addError", "parent.layer.msg('用户名或密码不能为空！', { time: 2500 });");
                    return;
                }
                T_user user = udb.T_users.Where(u => u.u_id == Int32.Parse(u_id)).SingleOrDefault();
                if (Request.Form["u_old_password"].ToString() != md5.MD5Decrypt(user.u_password, md5.GetKey()))
                {
                    runClientScript("addError", "parent.layer.msg('原始密码错误！', { time: 2500 });");
                    return;
                }
                user.u_password = md5.MD5Encrypt(Request.Form["u_password"].ToString().Trim(), md5.GetKey());
                try
                {
                    udb.SubmitChanges();
                    runClientScript("addOK", "parent.layer.msg('修改成功！', { time: 2500 });closeLayer();");
                }
                catch (Exception exc)
                {

                    runClientScript("addError", "parent.layer.msg('修改失败：'"+exc.Message+", { time: 2500 });");
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