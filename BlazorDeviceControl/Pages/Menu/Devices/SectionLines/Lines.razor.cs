// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Scales;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionLines;

public sealed partial class Lines : RazorComponentSectionBase<ScaleModel>
{
    #region Public and private fields, properties, constructor

    public Lines() : base()
    {
        SqlCrudConfigSection.AddOrders(new() { Name = nameof(ScaleModel.Description), Direction = SqlOrderDirection.Asc });
    }

    #endregion

    #region Public and private

    #endregion
}
