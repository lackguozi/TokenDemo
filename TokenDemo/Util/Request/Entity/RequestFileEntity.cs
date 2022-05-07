namespace TokenDemo.Util.Request.Entity
{
    public class RequestFileEntity
    {
        /// <summary>
        /// 文件大小
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 文件片段数量
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 文件数据
        /// </summary>
        public string fileData { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string fileExt { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string fileName { get; set; }
    }
}
