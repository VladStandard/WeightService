// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using System.Globalization;

namespace BlazorDeviceControl.Shared.Component;

public partial class ItemDates
{
    #region Public and private fields, properties, constructor

    private string CreateDt { get; set; } = string.Empty;
    private string ChangeDt { get; set; } = string.Empty;

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SetParametersWithAction(new()
        {
            () =>
            {
	            Console.WriteLine(Table);
                // Name: Accesses.
                switch (Table)
                {
                    case TableSystemEntity:
                        SetDtFromTableSystem();
                        break;
                    case TableScaleEntity:
                        SetDtFromTableScale();
                        break;
                }
            }
        });
    }

    private void SetDtFromTableScale()
    {
        switch (ProjectsEnums.GetTableScale(Table.Name))
        {
            case ProjectsEnums.TableScale.Default:
                break;
            case ProjectsEnums.TableScale.BarCodeTypes:
                BarCodeTypeV2Entity barcodeType = AppSettings.DataAccess.Crud.GetEntityByUid<BarCodeTypeV2Entity>(IdentityUid);
                CreateDt = barcodeType.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = barcodeType.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Contragents:
                ContragentV2Entity contragent = AppSettings.DataAccess.Crud.GetEntityByUid<ContragentV2Entity>(IdentityUid);
                CreateDt = contragent.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = contragent.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Hosts:
                HostEntity host = AppSettings.DataAccess.Crud.GetEntityById<HostEntity>(IdentityId);
                CreateDt = host.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = host.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Labels:
                LabelEntity label = AppSettings.DataAccess.Crud.GetEntityById<LabelEntity>(IdentityId);
                CreateDt = label.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Nomenclatures:
                NomenclatureEntity nomenclature = AppSettings.DataAccess.Crud.GetEntityById<NomenclatureEntity>(IdentityId);
                CreateDt = nomenclature.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = nomenclature.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Orders:
                OrderEntity order = AppSettings.DataAccess.Crud.GetEntityById<OrderEntity>(IdentityId);
                CreateDt = order.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = order.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            //case ProjectsEnums.TableScale.OrdersStatuses:
            //    OrderStatusEntity orderStatus = AppSettings.DataAccess.Crud.GetEntityById<OrderStatusEntity>(IdentityId);
            //    break;
            //case ProjectsEnums.TableScale.OrdersTypes:
            //    OrderTypeEntity orderType = AppSettings.DataAccess.Crud.GetEntityById<OrderTypeEntity>(IdentityId);
            //    break;
            case ProjectsEnums.TableScale.Organizations:
                OrganizationEntity organization = AppSettings.DataAccess.Crud.GetEntityById<OrganizationEntity>(IdentityId);
                CreateDt = organization.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = organization.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Plus:
                PluEntity plu = AppSettings.DataAccess.Crud.GetEntityById<PluEntity>(IdentityId);
                CreateDt = plu.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = plu.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusV2:
                PluV2Entity pluV2 = AppSettings.DataAccess.Crud.GetEntityById<PluV2Entity>(IdentityId);
                CreateDt = pluV2.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluV2.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PluRefs:
                PluRefV2Entity pluRefV2 = AppSettings.DataAccess.Crud.GetEntityById<PluRefV2Entity>(IdentityId);
                CreateDt = pluRefV2.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluRefV2.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Printers:
                PrinterEntity printer = AppSettings.DataAccess.Crud.GetEntityById<PrinterEntity>(IdentityId);
                CreateDt = printer.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printer.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PrintersResources:
                PrinterResourceEntity printerResource = AppSettings.DataAccess.Crud.GetEntityById<PrinterResourceEntity>(IdentityId);
                CreateDt = printerResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printerResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            //case ProjectsEnums.TableScale.PrintersTypes:
            //    PrinterTypeEntity printerType = AppSettings.DataAccess.Crud.GetEntityById<PrinterTypeEntity>(IdentityId);
            //    break;
            case ProjectsEnums.TableScale.ProductionFacilities:
                ProductionFacilityEntity productionFacility = AppSettings.DataAccess.Crud.GetEntityById<ProductionFacilityEntity>(IdentityId);
                CreateDt = productionFacility.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = productionFacility.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.ProductSeries:
                ProductSeriesEntity productSeries = AppSettings.DataAccess.Crud.GetEntityById<ProductSeriesEntity>(IdentityId);
                CreateDt = productSeries.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Scales:
                ScaleEntity scale = AppSettings.DataAccess.Crud.GetEntityById<ScaleEntity>(IdentityId);
                CreateDt = scale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = scale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Templates:
                TemplateEntity template = AppSettings.DataAccess.Crud.GetEntityById<TemplateEntity>(IdentityId);
                CreateDt = template.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = template.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.TemplatesResources:
                TemplateResourceEntity templateResource = AppSettings.DataAccess.Crud.GetEntityById<TemplateResourceEntity>(IdentityId);
                CreateDt = templateResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = templateResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.WeithingFacts:
                WeithingFactEntity weithingFact = AppSettings.DataAccess.Crud.GetEntityById<WeithingFactEntity>(IdentityId);
                CreateDt = weithingFact.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = weithingFact.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Workshops:
                WorkShopEntity workshop = AppSettings.DataAccess.Crud.GetEntityById<WorkShopEntity>(IdentityId);
                CreateDt = workshop.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = workshop.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
        }
    }

    private void SetDtFromTableSystem()
    {
        switch (ProjectsEnums.GetTableSystem(Table.Name))
        {
            case ProjectsEnums.TableSystem.Default:
                break;
            case ProjectsEnums.TableSystem.Accesses:
                AccessEntity access = AppSettings.DataAccess.Crud.GetEntityById<AccessEntity>(IdentityId);
                CreateDt = access.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = access.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableSystem.Logs:
                LogEntity log = AppSettings.DataAccess.Crud.GetEntityByUid<LogEntity>(IdentityUid);
                CreateDt = log.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
        }
    }

    #endregion
}
