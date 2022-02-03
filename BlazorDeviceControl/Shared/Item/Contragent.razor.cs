// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.TableScaleModels;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Contragent
    {
        #region Public and private fields and properties

        public ContragentEntity ContragentItem { get => (ContragentEntity)Item; set => Item = value; }

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}
