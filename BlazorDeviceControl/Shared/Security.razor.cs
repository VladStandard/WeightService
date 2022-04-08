//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.JSInterop;
//using Newtonsoft.Json;
//using System.Threading.Tasks;

//namespace BlazorDeviceControl.Shared
//{
//    public partial class Security
//    {
//        #region Public and private fields and properties

//        [Inject] public AuthenticationStateProvider AuthenticationState { get; private set; }
//        private readonly object _locker = new();

//        #endregion

//        #region Public and private methods

//        protected override async Task OnAfterRenderAsync(bool firstRender)
//        {
//            await base.OnAfterRenderAsync(firstRender);

//            AuthenticationState authenticationState = AuthenticationState.GetAuthenticationStateAsync().Result;
//            System.Security.Principal.IIdentity identity = authenticationState?.User?.Identity;

//            string jsonString = JsonConvert.SerializeObject(identity, 
//                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.None });
//            await JsRuntime.InvokeVoidAsync("TestingBlazor.renderJson", "jsonRender", jsonString);
//        }

//        private void InvokeSecuredApi()
//        {
//            _ = Task.Run(async () =>
//            {
//                string url = "api/requireauthentication";
//                await JsRuntime.InvokeVoidAsync("open", url, "_blank");
//            }).ConfigureAwait(true);
//        }

//        #endregion
//    }
//}
