using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udx.Dbs.Cache
{
    public class AuthCache
    {
        /// <summary>
        /// 获取验证码的Key
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static string GetVerifyCodeKey(string key)
        {

            return $"VerifyCodeKey-{key}"; 
        }
        public static string GetPassWordEncryptKey(string key)
        {

            return $"PassWordEncryptKey-{key}"; 
        }
        public static async Task<bool> RemoveCache(string key)
        {
            return await Udx.Cache.DistributedCacheHelper.RemoveAsync(GetVerifyCodeKey(key));
        }
        public static T GetCache<T>(string key, Action initCache)
        {
            return Udx.Cache.DistributedCacheHelper.GetValue<T>(key, initCache);
        }
        public static async Task<bool> SetCacheAsync(string key, dynamic value,int minuts=5)
        {
            return await Udx.Cache.DistributedCacheHelper.SetValueAsync(key, value, minuts);
        }
    }
}
