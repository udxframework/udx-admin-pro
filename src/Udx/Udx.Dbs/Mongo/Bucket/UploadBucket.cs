using Udx.DBUtility;

namespace Udx.Mongo.Bucket
{
    public class UploadBucket : MongoContext
    {
        public UploadBucket() : base("udx_bucket_upload")
        {
        }
    }
    
}
