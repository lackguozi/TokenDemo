using System.Collections.Generic;
using System.Threading.Tasks;
using TokenDemo.Util.Response.Entity;

namespace TokenDemo.Untity
{
    public interface IDownloadFileService
    {
        MessageEntity RequestDownloadFile(Dictionary<string, string> fileInfo);
        Task<byte[]> FileDownload(int index, string fileName, string fileExt);
        Task<byte[]> FileDownloadOne(int index, string fileName, string fileExt);
    }
}
