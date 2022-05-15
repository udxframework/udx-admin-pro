using Vditor.Models;
namespace Udx.Admin.Components
{
    public partial class UdxVditor : UdxComponentBase, IDisposable
    {
        /// <summary>
        /// Markdown Content
        /// </summary>
        [Parameter]
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    _wattingUpdate = true;
                }
            }
        }

        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        /// <summary>
        /// Html Content
        /// </summary>
        [Parameter]
        public string Html { get; set; }

        [Parameter] public EventCallback<string> HtmlChanged { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Options { get; set; }

        [Parameter]
        public string Mode { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public string Height { get; set; }

        [Parameter]
        public string Width { get; set; }

        [Parameter]
        public string MinHeight { get; set; }

        [Parameter]
        public bool Outline { get; set; }

        [Parameter]
        public Toolbar Toolbar { get; set; }

        [Parameter]
        public bool ReadOnly { get; set; }

        [Parameter]
        public Vditor.Models.Upload Upload { get; set; } = Udx.Configs.AppSetting.UdxSettings.Vditor.Mapper<Vditor.Models.Upload>();      

    private ElementReference _ref;

        private bool _editorRendered = false;
        private bool _wattingUpdate = false;
        private string _value;

        private bool _afterFirstRender = false;

        protected override async Task<Task> OnAfterRenderAsync(bool firstRender)
        {           
            if (firstRender)
            {
                _afterFirstRender = true;
                await CreateVditor();
                if (ReadOnly)
                {
                    await Disabled();
                }
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Options ??= new Dictionary<string, object>();

            Options["Mode"] = Mode ?? "ir";
            Options["Placeholder"] = Placeholder ?? "";
            Options["Height"] = int.TryParse(Height, out var h) ? h : (object)Height;
            Options["Width"] = int.TryParse(Width, out var w) ? w : (object)Width;
            Options["MinHeight"] = int.TryParse(MinHeight, out var m) ? m : (object)MinHeight;
            Options["Options"] = Outline;

            if (Upload != null)
            {
                Options["Upload"] = Upload;
            }

            if (Toolbar != null)
            {
                List<object> bars = new List<object>();
                foreach (var item in Toolbar.Buttons)
                {
                    if (item is string)
                    {
                        bars.Add(item);
                    }
                    else if (item is CustomToolButton toolbar)
                    {
                        bars.Add(new Dictionary<string, object>()
                        {
                            ["hotkey"] = toolbar.Hotkey,
                            ["name"] = toolbar.Name,
                            ["tipPosition"] = toolbar.TipPosition,
                            ["tip"] = toolbar.Tip,
                            ["className"] = toolbar.ClassName,
                            ["icon"] = toolbar.Icon,
                        });
                    }
                }
                Options["Toolbar"] = bars;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (_wattingUpdate && _editorRendered)
            {
                _wattingUpdate = false;
                await SetValue(_value, true);
            }
        }

        public async ValueTask<string> GetValueAsync()
        {
            string val = string.Empty;
            if (_editorRendered)
            {
                val = await GetValue();
            }
            await this.ValueChanged.InvokeAsync(val);
            return val;
        }

        public void Dispose()
        {
            Destroy();
        }
 
    }

}
