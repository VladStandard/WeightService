//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.DAL.DataModels;
//using Microsoft.AspNetCore.Components;
//using System;

//namespace BlazorDeviceControl.Shared.Item
//{
//    public partial class Device
//    {
//        #region Public and private fields and properties

//        public DeviceEntity? ItemCast { get => Item == null ? null : (DeviceEntity)Item; set => Item = value; }
//        [Parameter] public EventCallback CallbackActionSaveAsync { get; set; }
//        [Parameter] public EventCallback CallbackActionCancelAsync { get; set; }
//        private string _state = @"Отключено";
//        private readonly object _locker = new();

//        #endregion

//        #region Public and private methods

//        private void Change<T>(T value, string name) where T : class
//        {
//            if (name.Equals("State"))
//            {
//                _state = Convert.ToBoolean(value) ? @"Работает" : @"Отключено";
//            }
//            StateHasChanged();
//        }

//        #endregion
//    }
//}
