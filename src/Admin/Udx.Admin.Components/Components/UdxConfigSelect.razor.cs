namespace Udx.Admin.Components;
public partial class UdxConfigSelect
{
   
    [Parameter] public string Value { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
   
    /// <summary>
    /// 
    /// </summary>
    [Parameter] public string Style { get; set; }

    [Parameter] public bool Disabled { get; set; }=false;

    [Parameter] public string Mode { get; set; } = "default";

    [Parameter] public string ConfigKey { get; set; }
    public  ConfigOption configOption;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Style += Style ?? "";
        ConfigOptionCache = ConfigOptionCache ?? new Hashtable();
        if (!ConfigOptionCache.ContainsKey(ConfigKey))
            await GetConfigOptionAsync(ConfigKey);

         configOption = ConfigOptionCache[ConfigKey] as ConfigOption;
       
    }
  

    void OnSelected(ConfigOption option)
    {
        if (Mode == "default") {

            ValueChanged.InvokeAsync(option?.Value?.ToString());
        }
    }
    private void OnSelectedItemsChangedHandler(IEnumerable<ConfigOption> values)
    {
        if (Mode == "multiple" && values != null) {

            var vs = values.Select(a => a.Value).ToArray();
            string str = string.Join(",", vs);
            ValueChanged.InvokeAsync(str);
        }
    }

   
}