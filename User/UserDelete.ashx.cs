using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using meeting.Model;

namespace meeting.User
{
    /// <summary>
    /// Summary description for UserDelete
    /// </summary>
    public class UserDelete : IHttpHandler
    {
        public static UserDataContext uDC = new UserDataContext();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string u_id = context.Request["u_id"] == null || context.Request["u_id"].ToString() == "" ? "-1" : context.Request["u_id"].ToString();
            string result = "error";
            var user = uDC.T_users.Where(u=>u.u_id == Int32.Parse(u_id)).SingleOrDefault();
            if(user!=null){
                uDC.T_users.DeleteOnSubmit(user);
            }
            try 
	        {	        
		        uDC.SubmitChanges();
                result = "ok";
	        }
	        catch (Exception Exc)
	        {
		
		        result = Exc.Message.ToString();
	        }
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}