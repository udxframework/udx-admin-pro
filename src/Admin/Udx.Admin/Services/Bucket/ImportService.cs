using Microsoft.AspNetCore.Http;
using System.IO;
using Udx.Attributes;
using Udx.Mongo.Bucket;

namespace Udx.Admin.Services
{
    /// <summary>
    /// ImportService
    /// </summary>
    public class ImportService : AdminService
    {
        private readonly ImportBucket importBucket;
        /// <summary>
        /// 文件服务
        /// </summary>
        /// <param name="import">导入Bucket</param>
        public ImportService(ImportBucket import)
        {
            importBucket = import;
        }
        /// <summary>
        /// ImportService 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public override string Index()
        {
            return "Import Service";
        }
       
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [DisableRequestSizeLimit]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
        {
            if (files == null || files.Count() == 0)
            {
                return new JsonResult(new
                {
                    Msg = "请选择上传文件！",
                    Code = -1000
                });
            }
            var responses = new List<ImportResponse>();
            var host = $"{Request.Scheme}://{Request.Host}";
            foreach (IFormFile file in files)
            {
                using StreamReader reader = new StreamReader(file.OpenReadStream());
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                //var content = reader.ReadToEnd();
                var response = new ImportResponse()
                {
                    BucketName = importBucket.BucketName,
                    FileName = file.FileName
                };
                //存储文件到导入的MongoDb
                response.ObjectId = await importBucket.UploadFile(response.FileName, reader.BaseStream);
                response.Url = $"{host}/Udx/Admin/IImportervice/Download/{response.ObjectId}/";
                responses.Add(response);
            }
            return new JsonResult(responses);
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        [HttpGet("{id}/")]
        [AllowAnonymous]
        public async Task<IActionResult> Download(string id)
        {
            var fileInfo = await importBucket.GetFileById(id);
            if (fileInfo == null)
            {
                return NotFound();
            }
            return File(await importBucket.DownloadFileStream(id), "application/octet-stream", fileInfo.Filename);
        }
       
    }
}
