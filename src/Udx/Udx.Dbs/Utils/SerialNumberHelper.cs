using Microsoft.EntityFrameworkCore.Storage;
using Udx.Admin;

namespace Udx.Utils;
/// <summary>
/// 流水号生成持久层
/// </summary>

 public  static class SerialNumberHelper
{

    /// <summary>
    /// 获取流水号
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="name">流水配置表里的唯一name</param>
    /// <param name="args">配置表里面的动态参数，自增的流水号在最后面会自动加上</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<string> GenerateNumberAsync(string name, List<object> args = null)
    {

        int i = 0;
        next:
        try
        {
            i++;
            using var context = new AdminContext();
            using IDbContextTransaction transaction = context.Database.BeginTransaction();
            var model = await context.NumberEntity.SingleOrDefaultAsync(x => x.Name == name);
            if (model == null) throw new Exception($"“{name}”流水号规则没有配置！");
            var maxNumber = model.MaxNumber;
            var lastNumber = maxNumber + 1;
            model.MaxNumber = lastNumber;
            string template = model.Template;
            var date = System.DateTime.Now;
            template = date.ToString(template);
            if (args == null) args = new List<object>() { lastNumber };
            else args.Add(lastNumber);
            template = string.Format(template, args.ToArray());
            model.LastNumber = template;
            //model.ModifiedUserId = _identityContext.Id;
            model.ModifiedTime = DateTime.UtcNow;
            var result = await context.SaveChangesAsync();
            transaction.Commit();
            return model.LastNumber;
        }
        catch (Exception ex)
        {
            if (i < 3)  //重试3次
                goto next;
            else
                throw new Exception($"“{name}”流水号生成失败，{ex.Message}");
        }
       }
    /// <summary>
    /// 获取流水号
    /// </summary>
    /// <param name="context"></param>
    /// <param name="name">流水配置表里的唯一name</param>
    /// <param name="args">配置表里面的动态参数，自增的流水号在最后面会自动加上</param>
    /// <returns></returns>
    public static async Task<string> GenerateNumberAsync(this DbContext context, string name, List<object> args = null)
        {
            return await GenerateNumberAsync(name, args);
        }






    }
   