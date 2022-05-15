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
        [Inject] private IJSRuntime Js { get; set; }

        public async Task CreateVditor()
        {
            await Js.InvokeVoidAsync("vditorBlazor.createVditor", _ref, DotNetObjectReference.Create(this), this.Value, this.Options);
        }

        public ValueTask<string> GetValue()
        {
            return Js.InvokeAsync<string>("vditorBlazor.getValue", _ref);
        }

        public ValueTask<string> GetHTML()
        {
            return Js.InvokeAsync<string>("vditorBlazor.getHTML", _ref);
        }

        public async Task SetValue(string value, bool clearStack = false)
        {
            await Js.InvokeVoidAsync("vditorBlazor.setValue", _ref, value, clearStack);
        }

        public async Task InsertValue(string value, bool render = true)
        {
            await Js.InvokeVoidAsync("vditorBlazor.insertValue", _ref, value, render);
        }

        public async Task Disabled()
        {
            await Js.InvokeVoidAsync("vditorBlazor.disabled", _ref);
        }

        public async Task Destroy()
        {
            await Js.InvokeVoidAsync("vditorBlazor.destroy", _ref);
        }
    } 
}
