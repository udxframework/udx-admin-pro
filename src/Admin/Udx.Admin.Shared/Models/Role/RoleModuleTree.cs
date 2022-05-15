using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Udx.Auth;
using Udx.Dbs.Entities;
using Udx.Utils;

namespace Udx.Admin.Models;

public partial class RoleModuleTree:ModuleEntity
    {
       public bool Checked { get; set; }
       public string RoleModuleActions { get; set; }
       public List<RuleAction> ActionList{ get; set; }
       public  List<RoleModuleTree> Items { get; set; }


    /// <summary>
    /// 以Str1为标准左链接Str2
    /// </summary>
    /// <param name="leftString"></param>
    /// <param name="joinString"></param>
    /// <param name="splitChar">分隔符</param>
    /// <returns></returns>
    public List<RuleAction> GetRoleModuleActions(string leftString, string joinString, string splitChar = "|")
    {

        var ht = new Hashtable();
        var leftList = leftString?.Split(splitChar, System.StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
        leftList.ForEach(k => ht[k] = false);       
        var hs2 = new HashSet<string>(joinString?.Split(splitChar, System.StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>());
        hs2.ForEach(h => ht[h] = true);
        var list = new List<RuleAction>();
        foreach (string k in ht.Keys)
        {
            list.Add(new RuleAction { Label = k, Value = k, Checked = (bool)ht[k] });
        }
        return list;
    }
    /// <summary>
    /// 以Str1为标准左链接Str2
    /// </summary>
    /// <param name="leftList"></param>
    /// <param name="joinString"></param>
    /// <param name="splitChar">分隔符</param>
    /// <returns></returns>
    public List<RuleAction> GetRoleModuleActionList(List<RuleAction> leftList, string joinString, string splitChar = "|")
    {
        var ht = new Hashtable();
        leftList.ForEach(k => ht[k.Label] = k.Checked);        
        var hs2 = new HashSet<string>(joinString?.Split(splitChar, System.StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>());
        hs2.ForEach(h => ht[h] = true); 
        var list = new List<RuleAction>();
        foreach (string k in ht.Keys)
        {
            list.Add(new RuleAction { Label = k, Value = k, Checked = (bool)ht[k] });
        }
        return list;
    }
    /// <summary>
    /// 合并Actions
    /// </summary>
    /// <param name="joinString">合并串</param>
    /// <param name="splitChar">分隔符</param>
    /// <returns></returns>
    public string GetUserModuleActions(string joinString, string splitChar = "|")
    {
        string[] arry = joinString?.Split(splitChar, System.StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
        var ht = new HashSet<string>();
        foreach (var s in arry)
        {
            ht.Add(s);
        }       
        return string.Join(splitChar, ht.ToArray());
    }
    public string[] GetUserModuleActionsArry(string splitChar = "|")
    {
        string[] arry = RoleModuleActions.Split(splitChar, System.StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();        
        return arry;
    }
    /// <summary>
    /// 获取选择的string
    /// </summary>
    /// <param name="list"></param>
    /// <param name="splitChar"></param>
    /// <returns></returns>
    public  string GetRoleModuleActions(List<RuleAction> list, string splitChar = "|")
    {
        if (list == null)
            return "";
        var ht = new HashSet<string>();
        foreach (var m in list)
        {
            if (m.Checked)
            {
                ht.Add(m.Value);
            }
        }
        return string.Join(splitChar, ht.ToArray());
    }
    public T GetModuleActionsCheckboxOptions<T>() {
     
        return Actions.Mapper<T>();

    }

}
