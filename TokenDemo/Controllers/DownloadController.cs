using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TokenDemo.Untity;
using TokenDemo.Util.Response.Entity;

namespace TokenDemo.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IDownloadFileService fileService;
        public DownloadController(IDownloadFileService fileService)
        {
            this.fileService = fileService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageEntity RequestDownloadFile(Dictionary<string, string> fileInfo)
        {
            Console.WriteLine("RequestDownloadFile");
            return  fileService.RequestDownloadFile(fileInfo);
            
        }
        [HttpPost]
        public int RequestDownloadFileTest(int fileInfo)
        {
            Console.WriteLine("调用成功");
            return 1;
            //return fileService.RequestDownloadFile(fileInfo);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FileDownload(Dictionary<string, string> fileInfo)
        {
            Console.WriteLine("FileDownload");
            int index = 0;
            if (fileInfo.ContainsKey("index"))
            {
                int.TryParse(fileInfo["index"].ToString(), out index);
            }
            else
            {
                return Ok(new { Code = -1, Msg = "缺少参数" });
            }
            string fileName = string.Empty;
            string fileExt = string.Empty;
            if (fileInfo.ContainsKey("fileName"))
            {
                fileName = fileInfo["fileName"].ToString();
            }
            if (fileInfo.ContainsKey("fileExt"))
            {
                fileExt = fileInfo["fileExt"].ToString();
            }
            if (string.IsNullOrEmpty(fileName))
            {
                return Ok(new { Code = -1, Msg = "文件名不能为空" });
            }
            byte[] datas = null;
            try
            {
                datas = await fileService.FileDownload(index, fileName, fileExt);
                
            }
            catch(Exception e)
            {
                return Ok(new { Code = -1,Msg=e.Message });
            }
            Console.WriteLine("FileDownloadEnd");
            return File(datas, "application/pdf","opbook");

        }
        public async Task<IActionResult> FileDownloadOne(Dictionary<string, string> fileInfo)
        {
            Console.WriteLine("FileDownloadbegin");
            int index = 0;
            if (fileInfo.ContainsKey("index"))
            {
                int.TryParse(fileInfo["index"].ToString(), out index);
            }
            else
            {
                return Ok(new { Code = -1, Msg = "缺少参数" });
            }
            string fileName = string.Empty;
            string fileExt = string.Empty;
            if (fileInfo.ContainsKey("fileName"))
            {
                fileName = fileInfo["fileName"].ToString();
            }
            if (fileInfo.ContainsKey("fileExt"))
            {
                fileExt = fileInfo["fileExt"].ToString();
            }
            if (string.IsNullOrEmpty(fileName))
            {
                return Ok(new { Code = -1, Msg = "文件名不能为空" });
            }
            
            byte[] datas = null;
            try
            {
                datas = await fileService.FileDownloadOne(index, fileName, fileExt);
            }
            catch(Exception e)
            {
                return Ok(new { Code = -1, Msg = e.Message });
            }
           
            Console.WriteLine("FileDownloadend");
            return File(datas, "application/pdf", "opbook");
        }
    }
}
