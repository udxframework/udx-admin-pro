using Udx.Mongo;
using Udx.Mongo.Bucket;

namespace Udx.Admin.Services
{
    /// <summary>
    /// FileService
    /// </summary>
    public class FileService : AdminService
    {
        private readonly MongoContext mongoContext;
        private readonly ExportBucket exportBucket;
        /// <summary>
        /// 文件服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="export">导出Bucket</param>
        public FileService(MongoContext context, ExportBucket export)
        {
            mongoContext = context;
            exportBucket = export;
        }
        /// <summary>
        /// FileService 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public override string Index()
        {
            return "File Service";
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        [HttpGet("{id}/")]
        [AllowAnonymous]
        public async Task<IActionResult> Download(string id)
        {
            var fileInfo = await mongoContext.GetFileById(id);
            if (fileInfo == null)
            {
                return NotFound();
            }
            return File(await mongoContext.DownloadFileStream(id), "application/octet-stream", fileInfo.Filename);
        }
       
        /// <summary>
        /// 导出文件
        /// </summary>
        [HttpGet("{id}/")]
        [AllowAnonymous]
        public async Task<IActionResult> Export(string id)
        {
            var fileInfo = await exportBucket.GetFileById(id);
            if (fileInfo == null)
            {
                return NotFound();
            }
            return File(await exportBucket.DownloadFileStream(id), "application/octet-stream", fileInfo.Filename);
        }
    }
}
