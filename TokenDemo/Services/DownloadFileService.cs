using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TokenDemo.Util.Request.Entity;
using TokenDemo.Util.Response.Entity;

namespace TokenDemo.Untity
{
    public class DownloadFileService: IDownloadFileService
    {
        public MessageEntity RequestDownloadFile(Dictionary<string, string> fileInfo)
        {
            MessageEntity message = new MessageEntity();
            string fileName = string.Empty;
            string fileExt = string.Empty;
            if (!fileInfo.ContainsKey("fileName"))
            {
                message.Code = -1;
                message.Msg = "文件名不能为空";
                return message;
            }
            fileName = fileInfo["fileName"];
            fileExt = fileInfo["fileExt"];
            string filePath = "e:\\" + $"{fileName}{fileExt}";
            //FileStream fs = null;
            try
            {
                if (!File.Exists(filePath))
                {
                    message.Code = -1;
                    message.Msg = "文件不存在";
                    return message;
                }
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    if (fs.Length <= 0)
                    {
                        message.Code = -1;
                        message.Msg = "文件未处理完";
                        return message;
                    }
                    int size = 1 * 1024 * 1024;// 1m
                    RequestFileEntity requestFile = new RequestFileEntity();
                    requestFile.fileExt = fileExt;
                    requestFile.size = fs.Length;
                    requestFile.count = (int)(fs.Length / size);
                    if (fs.Length % size != 0)
                    {
                        requestFile.count++;
                    }
                    requestFile.fileData = GetCryptoString(fs);
                    message.Data = requestFile;
                }
               
                
            }
            catch
            {
                Console.WriteLine("请求下载错误");
            }
            return message;
        }
        public async Task<byte[]> FileDownload(int index ,string fileName,string fileExt)
        {
            //MessageEntity message = new MessageEntity();
            string filePath = "e:\\" + $"{fileName}{fileExt}";
          
            if (!System.IO.File.Exists(filePath))
            {
               
                throw new Exception("文件不存在，请稍后再试");
            }
            using(var fs = new FileStream(filePath, FileMode.Open))
            {
                if(fs.Length <= 0)
                {
                    throw new Exception("文件尚未处理");
                }
                int shardSize = 1 * 1024 * 1024;//一次1M
                int count = (int)(fs.Length / shardSize);
                if ((fs.Length % shardSize) > 0)
                {                               
                    count += 1;
                }
                if (index > count - 1)
                {
                    throw new Exception("无效的下标");
                }
                fs.Seek(index * shardSize, SeekOrigin.Begin);
                //Console.WriteLine( "fs.length="+ $"{fs.Length}");
                if (index == count - 1)
                {
                    //最后一片 = 总长 - (每次片段大小 * 已下载片段个数)
                    shardSize = (int)(fs.Length - (shardSize * index));
                    Console.WriteLine("shardSize："+$"{shardSize}");
                }
                byte[] datas = new byte[shardSize];
                await fs.ReadAsync(datas, 0, datas.Length);
                
                return datas;
            }
        }
        public async Task<byte[]> FileDownloadOne(int index, string fileName, string fileExt)
        {
            string filePath = "e:\\" + $"{fileName}{fileExt}";

            if (!System.IO.File.Exists(filePath))
            {

                throw new Exception("文件不存在，请稍后再试");
            }
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                if (fs.Length <= 0)
                {
                    throw new Exception("文件尚未处理");
                }
                /*int shardSize = 1 * 1024 * 1024;//一次1M
                int count = (int)(fs.Length / shardSize);
                if ((fs.Length % shardSize) > 0)
                {
                    count += 1;
                }
                if (index > count - 1)
                {
                    throw new Exception("无效的下标");
                }
                fs.Seek(index * shardSize, SeekOrigin.Begin);
                if (index == count - 1)
                {
                    //最后一片 = 总长 - (每次片段大小 * 已下载片段个数)
                    shardSize = (int)(fs.Length - (shardSize * index));
                }*/
                byte[] datas = new byte[fs.Length];
                await fs.ReadAsync(datas, 0, datas.Length);

                return datas;
            }
        }
        private string GetCryptoString(Stream fileStream)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] cryptBytes = md5.ComputeHash(fileStream);
            return GetCryptoString(cryptBytes);
        }

        private string GetCryptoString(byte[] cryptBytes)
        {
            //加密的二进制转为string类型返回
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cryptBytes.Length; i++)
            {
                sb.Append(cryptBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        
    }
}
