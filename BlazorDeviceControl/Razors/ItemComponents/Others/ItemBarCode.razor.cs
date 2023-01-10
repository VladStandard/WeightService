// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Enums;
using DataCore.Sql.TableScaleModels.BarCodes;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemBarCode : RazorComponentItemBase<BarCodeModel>
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = DataContext.GetItemNotNullable<BarCodeModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
                    SqlItemCast = SqlItemNew<BarCodeModel>();
                }

                ButtonSettings = new(false, false, false, false, false, false, true);
            }
        });
    }

    private string GetBarcodeTop(FormatType formatType) => DataFormatUtils.GetContent(SqlItemCast.ValueTop, formatType);

    private string GetBarcodeTop(string formatString) => GetBarcodeTop(DataUtils.GetFormatType(formatString));

    private string GetBarcodeRight(FormatType formatType) => DataFormatUtils.GetContent(SqlItemCast.ValueRight, formatType);

    private string GetBarcodeRight(string formatString) => GetBarcodeRight(DataUtils.GetFormatType(formatString));

    private string GetBarcodeBottom(FormatType formatType) => DataFormatUtils.GetContent(SqlItemCast.ValueBottom, formatType);

    private string GetBarcodeBottom(string formatString) => GetBarcodeBottom(DataUtils.GetFormatType(formatString));

    #endregion
}