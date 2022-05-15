using System;
using System.Collections.Generic;
using System.Text;

namespace UdxDb.Cache
{
    public class CmsCategoryCache
    {
        /// <summary>
        /// CMS的分类缓存key
        /// </summary>
        /// <param name="shopGid"></param>
        /// <returns></returns>
        public static string GetCacheKey(Guid shopGid) {

            return $"GetCmsCategory-{shopGid}";
        }
        public static bool RemoveCache(Guid shopGid) {

            return Udx.Cache.DistributedCacheHelper.Remove(GetCacheKey(shopGid));
        }
        public static T GetCache<T>(Guid shopGid,Action initCache){

           return Udx.Cache.DistributedCacheHelper.GetValue<T>(GetCacheKey(shopGid), initCache);
        }
        public static bool SetCache(Guid shopGid, dynamic value)
        {
            return Udx.Cache.DistributedCacheHelper.SetValue(GetCacheKey(shopGid), value,1);
        }
    }
}
