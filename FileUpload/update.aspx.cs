using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace meeting.FileUpload
{
    public partial class update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Files["Filedata"] != null)//判断是否有文件上传上来
            {
                SaveImages("Files/");
                Response.End();
            }


        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="value">cookie名称</param>
        /// <returns></returns>
        private string getCookie(string value)
        {
            return Request.Form["access2008_cookie_" + value];
        }



        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="url">保存路径,填写相对路径</param>
        /// <returns></returns>
        private void SaveImages(string url)
        {
            //'遍历File表单元素
            HttpFileCollection files = HttpContext.Current.Request.Files;

            ///'检查文件扩展名字
            //HttpPostedFile postedFile = files[iFile];
            HttpPostedFile postedFile = Request.Files["Filedata"]; //得到要上传文件
            string fileName, fileExtension, filesize,fileNameInServer;
            fileName = System.IO.Path.GetFileName(postedFile.FileName.ToString()); //得到文件名
            filesize = System.IO.Path.GetFileName(postedFile.ContentLength.ToString()); //得到文件大小
            if (fileName != "")
            {
                fileExtension = System.IO.Path.GetExtension(fileName);//'获取扩展名
                fileNameInServer = dateTranslate() + fileExtension; //修改文件名，存于服务器
                //注意：可能要修改你的文件夹的匿名写入权限。
                postedFile.SaveAs(System.Web.HttpContext.Current.Request.MapPath(url) + fileName);
            }
            
            if (Session["files"] == null || Session["files"].ToString() == "")
            {
                Session["files"] = fileName;
            }
            else
            {
                Session["files"] += "," + fileName ;
            }

            Response.Write( fileName + "  <font color=\"#ff0000\">上传成功!</font>");
            

        }
        private string dateTranslate()
        {
            DateTime dt = DateTime.Now;
            return dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
        }


    }

}