// This is an independent project of an individual developer. Dear PVS-Studio, please check it
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net.Sockets;
using WsDataCore.Protocols;
using WsStorageCore.Enums;
using WsStorageCore.Tables;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;
using WsStorageCore.TableScaleModels.TemplatesResources;

namespace BlazorDeviceControl.Pages.Menu.Devices.SectionPrinters;

public sealed partial class ItemPrinter : RazorComponentItemBase<WsSqlPrinterModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlPrinterTypeModel> PrinterTypeModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlPrinterModel>(IdentityId);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<WsSqlPrinterModel>();
        PrinterTypeModels =
            ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlPrinterTypeModel>(
                WsSqlCrudConfigUtils.GetCrudConfigComboBox()
            );
        
    }

    private async Task SqlItemPrinterResourcesClear(WsSqlPrinterModel printer)
    {
        if (User?.IsInRole(UserAccessStr.Write) == false)
            return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        RunActionsWithQeustion(
            LocaleCore.Print.ResourcesClear,
            GetQuestionAdd(),
            () =>
            {
                WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
                    false,
                    false
                );
                List<WsSqlTemplateResourceModel> templateResources =
                    ContextManager.ContextList.GetListNotNullableTemplateResources(sqlCrudConfig);
                foreach (WsSqlTemplateResourceModel templateResource in templateResources)
                {
                    if (templateResource.Name.Contains("TTF"))
                    {
                        TcpClient client = MdZplUtils.TcpClientSendData(
                            printer.Ip,
                            printer.Port,
                            new() { new($"^XA^ID"), new(templateResource.Name), new($"^FS^XZ") }
                        );
                    }
                }
            }
        );
    }

    private async Task SqlItemPrinterResourcesLoad(WsSqlPrinterModel printer, string fileType)
    {
        if (User?.IsInRole(UserAccessStr.Write) == false)
            return;
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        RunActionsWithQeustion(
            LocaleCore.Print.ResourcesLoadTtf,
            GetQuestionAdd(),
            () =>
            {
                WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(
                    new WsSqlFieldOrderModel
                    {
                        Name = nameof(WsSqlTableBase.Description),
                        Direction = WsSqlOrderDirection.Asc
                    },
                    false,
                    false
                );
                List<WsSqlTemplateResourceModel> templateResources =
                    ContextManager.ContextList.GetListNotNullable<WsSqlTemplateResourceModel>(
                        sqlCrudConfig
                    );
                foreach (WsSqlTemplateResourceModel templateResource in templateResources)
                {
                    if (templateResource.Name.Contains(fileType))
                    {
                        TcpClient client = MdZplUtils.TcpClientSendData(
                            printer.Ip,
                            printer.Port,
                            new()
                            {
                                new(
                                    $"^XA^MNN^LL500~DYE:{templateResource.Name}.TTF,B,T,{templateResource.DataValue.Length},,"
                                ),
                                new(templateResource.DataValue),
                                new($"^XZ")
                            }
                        );
                    }
                }
            }
        );
    }

    #endregion
}
