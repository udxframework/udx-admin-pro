using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udx.Admin.Components
{
    public partial class UdxVditor : UdxComponentBase
    { 
        [Parameter] public EventCallback<string> OnFocus { get; set; }

        [Parameter] public EventCallback<string> OnBlur { get; set; }

        [Parameter] public EventCallback<string> OnEscPress { get; set; }

        [Parameter] public EventCallback<string> OnCtrlEnterPress { get; set; }

        [Parameter] public EventCallback<string> OnSelect { get; set; }

        [Parameter] public EventCallback<string> OnToolbarButtonClick { get; set; }

        [JSInvokable]
        public async Task HandleRendered()
        {
            _editorRendered = true;
            if (!string.IsNullOrWhiteSpace(Value) && HtmlChanged.HasDelegate)
            {
                Html = await GetHTML();
                await HtmlChanged.InvokeAsync(Html);
            }
        }

        [JSInvokable]
        public async Task HandleInput(string value)
        {
            _value = value;
            _wattingUpdate = false;

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }

            if (HtmlChanged.HasDelegate)
            {
                Html = await GetHTML();
                await HtmlChanged.InvokeAsync(Html);
            }
        }

        [JSInvokable]
        public void HandleFocus(string value)
        {
            if (OnFocus.HasDelegate)
            {
                OnFocus.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleBlur(string value)
        {
            if (OnBlur.HasDelegate)
            {
                OnBlur.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleEscPress(string value)
        {
            if (OnEscPress.HasDelegate)
            {
                OnEscPress.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleCtrlEnterPress(string value)
        {
            if (OnCtrlEnterPress.HasDelegate)
            {
                OnCtrlEnterPress.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleSelect(string value)
        {
            if (OnSelect.HasDelegate)
            {
                OnSelect.InvokeAsync(value);
            }
        }

        [JSInvokable]
        public void HandleToolbarButtonClick(string btnName)
        {
            if (OnToolbarButtonClick.HasDelegate)
            {
                OnToolbarButtonClick.InvokeAsync(btnName);
            }
        }
    }
}
