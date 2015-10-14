using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile uploadedfile = context.Request.Files[0];
            string FileName = uploadedfile.FileName;
            string FileType = uploadedfile.ContentType;
            int FileSize = uploadedfile.ContentLength;
            FileInfo info = new FileInfo(FileName);
            string folderFullPath = context.Server.MapPath(context.Request["folder"]);
            if (!Directory.Exists(folderFullPath))
            {
                Directory.CreateDirectory(folderFullPath);
            }
            uploadedfile.SaveAs(folderFullPath + "\\" + info.Name);
            context.Response.ContentType = "text/plain";

            //重点这里一定要返回 一个 对象。否则 会请求错误的
            context.Response.Write("{\"name\":\"" + info.Name + "\",\"url\":\"" + FileType + "\",\"size\":\"" + FileSize + "\"}");
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