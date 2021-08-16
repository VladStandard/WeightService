// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using BlazorCore.DAL.TableModels;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class EntityDates
    {
        #region Public and private fields and properties

        private BaseEntity _item;
        [Parameter] public BaseEntity Item
        {
            get => _item;
            set
            {
                _item = value;
                DtCreate = string.Empty;
                DtModify = string.Empty;

                if (_item != null && _item is ScalesEntity scalesItem)
                {
                    DtCreate = scalesItem.CreateDate.ToString();
                    DtModify = scalesItem.ModifiedDate.ToString();
                }
                else if (_item != null && _item is ZebraPrinterEntity printerItem)
                {
                    DtCreate = printerItem.CreateDate.ToString();
                    DtModify = printerItem.ModifiedDate.ToString();
                }
            }
        }
        public string DtCreate { get; private set; }
        public string DtModify { get; private set; }

        #endregion

        #region Public and private methods


        #endregion
    }
}