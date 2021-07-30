// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControlBlazor.Data;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DeviceControlBlazor.Components
{
    public partial class Security : BaseRazorEntity
    {
        #region Public and private methods

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            string jsonString = JsonConvert.SerializeObject(AppSettings.Identity, 
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.None });
            await JsRuntime.InvokeVoidAsync("TestingBlazor.renderJson", "jsonRender", jsonString);
        }

        private void InvokeSecuredApi()
        {
            string url = "api/requireauthentication";
            _ = JsRuntime.InvokeVoidAsync("open", url, "_blank");
        }

        #endregion
    }
}