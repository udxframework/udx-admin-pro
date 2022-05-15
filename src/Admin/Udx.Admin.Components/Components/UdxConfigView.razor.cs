using System.Collections;

namespace Udx.Admin.Components;
public partial class UdxConfigView
{
   
    [Parameter] public string Value { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
   
   


    [Parameter] public string ConfigKey { get; set; }
    
    public  ConfigOption configOption;
    public ConfigOption configOptionView;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        ConfigOptionCache = ConfigOptionCache ?? new Hashtable();
        if (!ConfigOptionCache.ContainsKey(ConfigKey))
            await GetConfigOptionAsync(ConfigKey);

         configOption = ConfigOptionCache[ConfigKey] as ConfigOption;

         configOptionView = configOption.Options.Find(s => s.Value.ToString() == Value);

    }

    protected override void OnParametersSet()
    {
        configOptionView = configOption.Options.Find(s => s.Value.ToString() == Value);
        StateHasChanged();
        base.OnParametersSet();
    }





}