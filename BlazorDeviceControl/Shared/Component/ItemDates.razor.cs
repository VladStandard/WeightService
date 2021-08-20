// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using BlazorCore.DAL.TableModels;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ItemDates
    {
        #region Public and private fields and properties

        public string DtCreate { get; private set; }
        public string DtModify { get; private set; }
        private IBaseEntity _dtItem;
        [Parameter] public IBaseEntity DtItem
        {
            get => _dtItem;
            set
            {
                _dtItem = value;
                DtCreate = string.Empty;
                DtModify = string.Empty;

                if (_dtItem != null && _dtItem is ScaleEntity scalesItem)
                {
                    DtCreate = scalesItem.CreateDate.ToString();
                    DtModify = scalesItem.ModifiedDate.ToString();
                }
                else if (_dtItem != null && _dtItem is PluEntity pluEntity)
                {
                    DtCreate = pluEntity.CreateDate.ToString();
                    DtModify = pluEntity.ModifiedDate.ToString();
                }
                else if (_dtItem != null && _dtItem is ZebraPrinterEntity printerItem)
                {
                    DtCreate = printerItem.CreateDate.ToString();
                    DtModify = printerItem.ModifiedDate.ToString();
                }
            }
        }

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
