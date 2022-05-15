using AntDesign.Core.Helpers.MemberPath;

namespace Udx.Admin.Components
{
    partial class UdxDropdown<TItem, TItemValue>: UdxComponentBase
    {
        [Parameter]
        public string Value { get; set; }
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public bool ReadOnly { get; set; } = false;

        [Parameter]
        public bool AllowClear { get; set; } = false;

        [Parameter]
        public string Placeholder { get; set; }
        [Parameter]
        public string Icon { get; set; }

        bool Visible = false;

        [Parameter]
        public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
        [Parameter]
        public string LabelName
        {
            get => _labelName;
            set
            {
                _getLabel = string.IsNullOrWhiteSpace(value) ? null : PathHelper.GetDelegate<TItem, string>(value);
                _setLabel = string.IsNullOrWhiteSpace(value) ? null : PathHelper.SetDelegate<TItem, string>(value);
                _labelName = value;
            }
        }
        [Parameter]
        public string ValueName
        {
            get => _valueName;
            set
            {
                _getValue = string.IsNullOrWhiteSpace(value) ? null : PathHelper.GetDelegate<TItem, string>(value);
                _setValue = string.IsNullOrWhiteSpace(value) ? null : PathHelper.SetDelegate<TItem, string>(value);
                _valueName = value;
            }
        }

        [Parameter]
        public RenderFragment ContentTemplate { get; set; }

        [Parameter]
        public RenderFragment ChildTemplate { get; set; }
        [Parameter]
        public RenderFragment valueTemplate { get; set; }

        [Parameter]
        public EventCallback<TItem> OnSelect { get; set; }
        [Parameter]
        public EventCallback IconClick { get; set; }

        private Dropdown _dropdown;
        private string _value
        {
            get
            {
                var txt = Value;
                if (_getLabel != null && Items != null && !string.IsNullOrEmpty(Value))
                {
                    var item = Items.FirstOrDefault(p => _getValue(p) == Value);
                    if (item != null)
                        txt = (_getLabel == null ? Value : _getLabel(item));
                }
                return txt;
            }
            set
            {
                Value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        #region childContent
        private string _styleValue
        {
            get
            {
                var str = "udx-dropdown-value-top";
                if (string.IsNullOrEmpty(Value))
                {
                    str = "udx-dropdown-value-none";
                }
                return str;
            }
            set { }
        }

        private string _stylePlaceholder
        {
            get
            {
                var str = "udx-dropdown-placeholder";
                if (string.IsNullOrEmpty(Value))
                {
                    str = "udx-dropdown-placeholder-none";
                }
                return str;
            }
            set { }
        }

        private string _styleClose
        {
            get
            {
                var str = "udx-dropdown-close-none udx-dropdown-close";
                if (string.IsNullOrEmpty(Value))
                {
                    str = "udx-dropdown-close-none";
                }
                return str;
            }
            set { }
        }

        private string _iconVisible
        {
            get
            {
                var str = "display: inline-block";
                if (string.IsNullOrEmpty(Icon))
                {
                    str = "display: none";
                }
                return str;
            }
            set { }
        }



        private async Task OnDelClickAsync(MouseEventArgs e)
        {

            _value = "";
            await OnSelect.InvokeAsync(default(TItem));
        }

        private void OnSelectClick()
        {
            _value = "";
        }
        #endregion

        #region menu
        private string _labelName;
        private Func<TItem, string> _getLabel;
        private Action<TItem, string> _setLabel;

        private string _valueName;
        private Func<TItem, string> _getValue;
        private Action<TItem, string> _setValue;
        #endregion

        #region action
        private async Task OnSelectClick(TItem item)
        {
            _value = (_getValue == null ? item.ToString() : _getValue(item));
            Visible = false;
            StateHasChanged();
            await OnSelect.InvokeAsync(item);
        }
        #endregion

        public async void Close()
        {
            await _dropdown.Close();

        }

        void OnVisibleChange(bool visible) 
        {
            Visible = visible;
            StateHasChanged();
        }

        void OnMouseLeave()
        {
            Visible = false;
            StateHasChanged();
        }

    }
}
