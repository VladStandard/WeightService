//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using BlazorResourcesVs.Data;
//using DataCore;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace BlazorResourcesVs.Shared
//{
//    public partial class Creatio
//    {
//        #region Public and private fields and properties

//        public List<WebSiteEntity> LocalSites { get; set; }
//        public WebSiteEntity WebSite { get; set; }

//        #endregion

//        #region Public and private methods

//        protected override async Task OnInitializedAsync()
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            await base.OnInitializedAsync().ConfigureAwait(false);
//            await FillDataAsync().ConfigureAwait(false);
//        }

//        private async Task FillDataAsync()
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            var sites = new List<WebSiteEntity>
//            {
//                new WebSiteEntity(LocaleData.Resources.CreatioCreDevDmName, LocaleData.Resources.CreatioCreDevDmLink, LocaleData.Resources.CreatioCreDevDmDev),
//                new WebSiteEntity(LocaleData.Resources.CreatioCreDevIaName, LocaleData.Resources.CreatioCreDevIaLink, LocaleData.Resources.CreatioCreDevIaDev),
//                new WebSiteEntity(LocaleData.Resources.CreatioCreTestName, LocaleData.Resources.CreatioCreTestLink, LocaleData.Resources.CreatioCreTestDev),
//            };
//            LocalSites = sites;
//        }

//        private async Task RowSelectAsync(WebSiteEntity site)
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            WebSite = site;
//        }

//        private async Task RowDoubleClickAsync(object args)
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

//            //NavigationManager.NavigateTo(WebSite.Link, true);
//        }

//        #endregion
//    }
//}