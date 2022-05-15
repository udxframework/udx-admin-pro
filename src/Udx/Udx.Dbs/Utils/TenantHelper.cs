using Udx.Admin;
using Udx.Tenants;

namespace Udx.Utils;
/// <summary>
/// Tenant
/// </summary>

public static class TenantHelper
{
    public static string GetTenantId()
    {
        var tenantProvider = Udx.DependencyInjection.ServiceProviderManager.GetService<ITenantProvider>();
        var tenantId = tenantProvider.GetTenantId();
        return tenantId;
    }
    public static  TenantEntity? GetTenant(string id=null)
    {
        id = id?? GetTenantId();
        using var context = new AdminContext();
        var tenant = context.TenantEntity.Find(id);
        return tenant;
       }
   

   




}
   