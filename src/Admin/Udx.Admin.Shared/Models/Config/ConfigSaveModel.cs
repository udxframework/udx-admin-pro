using Udx.Models;

namespace Udx.Admin.Models;
public class ConfigSaveModel
{
    public SaveOperater Operater { get; set; }
    public string ConfigId { get; set; }
    public ConfigEdit Config { get; set; }
    public List<ConfigDetailEdit> ConfigDetails { get; set; }
}