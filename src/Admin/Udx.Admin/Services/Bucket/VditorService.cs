using Microsoft.AspNetCore.Http;
using System.IO;
using Udx.Mongo.Bucket;

namespace Udx.Admin.Services
{
    /// <summary>
    /// VditorService
    /// </summary>
    public class VditorService : AdminService
    {
        private readonly VditorBucket vditorContext;
        /// <summary>
        /// 编辑框文件服务
        /// </summary>
        /// <param name="context"></param>
        public VditorService(VditorBucket context)
        {
            vditorContext = context;
        }
        /// <summary>
        /// FileService 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public override string Index()
        {
            return "Vditor Service";
        }
        #region Vditor
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public  async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
        {

            if (files == null || files.Count() == 0)
            {
                return new JsonResult(new
                {
                    Msg = "请选择上传文件！",
                    Code = -1000,
                    Data = new { }
                });
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var host = $"{Request.Scheme}://{Request.Host}";
            foreach (IFormFile file in files)
            {              
                using  StreamReader reader = new StreamReader(file.OpenReadStream());
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                //var content = reader.ReadToEnd();
                var name = file.FileName;
                //存储文件到Mongo
                var id = await vditorContext.UploadFile(name, reader.BaseStream);
                var url = $"{host}/Udx/Admin/IVditorService/Download/{id}/";
                dict.Add(name, url);
            }
            return new JsonResult(new UploadResponseModel
            {
                Msg = "",
                Code = 0,
                Data = new UploadResponseModel.SuccMapModel(dict)
            });
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        [HttpGet("{id}/")]
        [AllowAnonymous]
        public async Task<IActionResult> Download(string id)
        {
            var fileInfo = await vditorContext.GetFileById(id);
            if (fileInfo == null)
            {
                return NotFound();
            }
            return File(await vditorContext.DownloadFileStream(id), "application/octet-stream", fileInfo.Filename);
        }
        #endregion

       
    }
}
