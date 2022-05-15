using Udx.Admin;
using Udx.Models;

namespace Udx.Dbs.Cache
{
    public class ConfigCache
    {
        /// <summary>
        /// 获取配置主表
        /// </summary>
        /// <param name="key">配置中心的配置主表键key</param>
        /// <returns></returns>
        public static async Task<ConfigOption> GetConfigAsync(string key)
        {
            
                using var ctx = new AdminContext();
                var config = await ctx.ConfigEntity.FirstOrDefaultAsync(c => c.ConfigKey == key);
                if(config == null) return  new ConfigOption() { Options = new List<ConfigOption>() }; 
                var options = from opt in ctx.ConfigDetailEntity
                              where opt.ConfigId == config.Id
                              select new ConfigOption()
                              {
                                  Title = opt.ConfigDetailTitle,
                                  Key = opt.ConfigDetailKey,
                                  Value = opt.ConfigDetailValue,
                                  Disabled = opt.Status == "是" ? true : false,
                                  Description = opt.Description
                              };
                var result = new ConfigOption()
                {
                    Title = config.ConfigTitle,
                    Key = config.ConfigKey,
                    Value = config.ConfigValue,
                    Disabled = config.Status == "是" ? true : false,
                    Description = config.Description,
                    Options = options?.ToList()
                };
                return await Task.FromResult(result);            
        }
        public static async Task<List<ConfigOption>> GetConfigDetailListAsync(string key)
        {
            var config = await GetConfigAsync(key);
            var result = config?.Options;
            return result;
        }
        public static async Task<ConfigOption> GetConfigDetailAsync(string key,string detailKey)
        {
            var list = await GetConfigDetailListAsync(key);
            var result = list?.FirstOrDefault(c => c.Key == detailKey);
            return result;
        }
        public static async Task<string> GetConfigDetailValueAsync(string key, string detailKey)
        {
            var config = await GetConfigDetailAsync(key, detailKey);
            var result = config?.Value.ToString();
            return result;
        }

    }
}
