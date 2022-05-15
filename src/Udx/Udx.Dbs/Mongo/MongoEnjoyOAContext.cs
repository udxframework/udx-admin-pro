using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Udx.Configs;
using Udx.DBUtility;

namespace Udx.Mongo
{
    /// <summary>
    /// 类名和配置节点的key一致，通过类名nameof(MongoEnjoyOA)来找到配置节点
    /// </summary>
    public class MongoEnjoyOA
    {
        public static DbConfig DbConfig { get {
                return Udx.Dbs.DbsStartup.DbConfigs[nameof(MongoEnjoyOA)]; 
            } }

    }

    public  class MongoEnjoyOAContext : MongoDbContext
    {
        /// <summary>
        /// 默认是fs
        /// </summary>
        public readonly string BucketName = "fs";
        private const string DOCID = "_id";
        const string DBNAME = "DocDB";

        public MongoEnjoyOAContext():base(MongoEnjoyOA.DbConfig)
        { }
        public MongoEnjoyOAContext(string bucketName) : base(MongoEnjoyOA.DbConfig, bucketName)
        {
            BucketName = bucketName;
        }


        public async Task<GridFSFileInfo> GetOAFileById(string id)
        {
            var filterName = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, id);
            var file = GridFsBucket.Find(filterName).FirstOrDefault();

            var filterId = Builders<GridFSFileInfo>.Filter.Eq("_id", file.Id);
            return await GridFsBucket.Find(filterId).FirstOrDefaultAsync();
        }

        public async Task<GridFSDownloadStream<ObjectId>> DownloadToStreamByNameAsync(string id)
        {
            return await GridFsBucket.OpenDownloadStreamByNameAsync(id);
        }
    }
}
