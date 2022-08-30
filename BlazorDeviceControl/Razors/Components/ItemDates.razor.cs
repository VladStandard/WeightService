// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Components;

public partial class ItemDates : RazorBase
{
    #region Public and private fields, properties, constructor

    private string CreateDt { get; set; }
	private string ChangeDt { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
    {
        base.OnInitialized();

        CreateDt = string.Empty;
        ChangeDt = string.Empty;
    }

   protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActions(new()
        {
            () =>
            {
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
            case ProjectsEnums.TableScale.BarCodeTypes:
                BarCodeTypeEntity barcodeType = AppSettings.DataAccess.Crud.GetItemByUidNotNull<BarCodeTypeEntity>(IdentityUid);
                CreateDt = barcodeType.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = barcodeType.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Contragents:
                ContragentEntity contragent = AppSettings.DataAccess.Crud.GetItemByUidNotNull<ContragentEntity>(IdentityUid);
                CreateDt = contragent.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = contragent.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Hosts:
                HostEntity host = AppSettings.DataAccess.Crud.GetItemByIdNotNull<HostEntity>(IdentityId);
                CreateDt = host.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = host.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Nomenclatures:
                NomenclatureEntity nomenclature = AppSettings.DataAccess.Crud.GetItemByIdNotNull<NomenclatureEntity>(IdentityId);
                CreateDt = nomenclature.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = nomenclature.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Orders:
                OrderEntity order = AppSettings.DataAccess.Crud.GetItemByIdNotNull<OrderEntity>(IdentityId);
                CreateDt = order.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = order.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.OrdersWeighings:
                OrderWeighingEntity orderWeighing = AppSettings.DataAccess.Crud.GetItemByUidNotNull<OrderWeighingEntity>(IdentityUid);
                CreateDt = orderWeighing.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = orderWeighing.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Organizations:
                OrganizationEntity organization = AppSettings.DataAccess.Crud.GetItemByIdNotNull<OrganizationEntity>(IdentityId);
                CreateDt = organization.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = organization.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Plus:
                PluEntity plu = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluEntity>(IdentityUid);
                CreateDt = plu.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = plu.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusLabels:
                PluLabelEntity pluLabel = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluLabelEntity>(IdentityUid);
                CreateDt = pluLabel.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluLabel.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusObsolete:
                PluObsoleteEntity pluObsolete = AppSettings.DataAccess.Crud.GetItemByIdNotNull<PluObsoleteEntity>(IdentityId);
                CreateDt = pluObsolete.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluObsolete.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusScales:
                PluScaleEntity pluScale = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluScaleEntity>(IdentityUid);
                CreateDt = pluScale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluScale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusWeighings:
	            PluWeighingEntity weithingFact = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluWeighingEntity>(IdentityUid);
	            CreateDt = weithingFact.CreateDt.ToString(CultureInfo.InvariantCulture);
	            ChangeDt = weithingFact.ChangeDt.ToString(CultureInfo.InvariantCulture);
	            break;
            case ProjectsEnums.TableScale.Printers:
                PrinterEntity printer = AppSettings.DataAccess.Crud.GetItemByIdNotNull<PrinterEntity>(IdentityId);
                CreateDt = printer.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printer.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PrintersResources:
                PrinterResourceEntity printerResource = AppSettings.DataAccess.Crud.GetItemByIdNotNull<PrinterResourceEntity>(IdentityId);
                CreateDt = printerResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printerResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.ProductionFacilities:
                ProductionFacilityEntity productionFacility = AppSettings.DataAccess.Crud.GetItemByIdNotNull<ProductionFacilityEntity>(IdentityId);
                CreateDt = productionFacility.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = productionFacility.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.ProductSeries:
                ProductSeriesEntity productSeries = AppSettings.DataAccess.Crud.GetItemByIdNotNull<ProductSeriesEntity>(IdentityId);
                CreateDt = productSeries.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Scales:
                ScaleEntity scale = AppSettings.DataAccess.Crud.GetItemByIdNotNull<ScaleEntity>(IdentityId);
                CreateDt = scale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = scale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Templates:
                TemplateEntity template = AppSettings.DataAccess.Crud.GetItemByIdNotNull<TemplateEntity>(IdentityId);
                CreateDt = template.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = template.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.TemplatesResources:
                TemplateResourceEntity templateResource = AppSettings.DataAccess.Crud.GetItemByIdNotNull<TemplateResourceEntity>(IdentityId);
                CreateDt = templateResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = templateResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Workshops:
                WorkShopEntity workshop = AppSettings.DataAccess.Crud.GetItemByIdNotNull<WorkShopEntity>(IdentityId);
                CreateDt = workshop.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = workshop.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
        }
    }

    private void SetDtFromTableSystem()
    {
        switch (ProjectsEnums.GetTableSystem(Table.Name))
        {
            case ProjectsEnums.TableSystem.Accesses:
                AccessEntity access = AppSettings.DataAccess.Crud.GetItemByUid<AccessEntity>(IdentityUid);
                CreateDt = access.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = access.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableSystem.Logs:
                LogEntity log = AppSettings.DataAccess.Crud.GetItemByUid<LogEntity>(IdentityUid);
                CreateDt = log.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
        }
    }

    #endregion
}
