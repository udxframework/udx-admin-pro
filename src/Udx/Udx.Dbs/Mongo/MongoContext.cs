using Udx.Configs;
using Udx.DBUtility;

namespace Udx.Mongo
{
    /// <summary>
    /// 类名和配置节点的key一致，通过类名nameof(MogoDb)来找到配置节点
    /// </summary>
    public class MongoDb
    {
        public static DbConfig DbConfig { get {
                return Udx.Dbs.DbsStartup.DbConfigs[nameof(MongoDb)]; 
            } }

    }

    public  class MongoContext:MongoDbContext
    {
        /// <summary>
        /// 默认是fs
        /// </summary>
        public readonly string BucketName = "fs";
        public MongoContext():base(MongoDb.DbConfig)
        { }
        public MongoContext(string bucketName) : base(MongoDb.DbConfig, bucketName)
        {
            BucketName = bucketName;
        }
    }
}
