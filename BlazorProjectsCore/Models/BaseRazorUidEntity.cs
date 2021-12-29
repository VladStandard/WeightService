//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataShareCore;
//using DataShareCore.DAL.Interfaces;
//using DataShareCore.DAL.Models;
//using Microsoft.AspNetCore.Components;
//using System;
//using System.Threading.Tasks;

//namespace BlazorProjectsCore.Models
//{
//    public class BaseRazorUidEntity : BaseRazorEntity
//    {
//        #region Public and private fields and properties

//        [Parameter] public Guid Uid { get; set; }
//        public IBaseUidEntity UidItem
//        {
//            get => (BaseUidEntity)Item;
//            set
//            {
//                Item = (BaseEntity)value;
//            }
//        }
//        public IBaseUidEntity UidParentItem
//        {
//            get => (BaseUidEntity)ParentItem;
//            set
//            {
//                ParentItem = (BaseEntity)value;
//            }
//        }

//        #endregion

//        #region Public and private methods

//        public async Task ItemSelectAsync(IBaseUidEntity item)
//        {
//            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemSelectAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
//                new Task(async() => {
//                    UidItem = item;
//                    await GuiRefreshWithWaitAsync();
//                }), true);
//        }

//        #endregion
//    }
//}
