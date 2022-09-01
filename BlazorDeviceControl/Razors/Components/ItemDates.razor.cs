// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Components;

public partial class ItemDates : RazorPageModel
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
                    case TableSystemModel:
                        SetDtFromTableSystem();
                        break;
                    case TableScaleModel:
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
                BarCodeTypeModel barcodeType = AppSettings.DataAccess.Crud.GetItemByUidNotNull<BarCodeTypeModel>(IdentityUid);
                CreateDt = barcodeType.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = barcodeType.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Contragents:
                ContragentModel contragent = AppSettings.DataAccess.Crud.GetItemByUidNotNull<ContragentModel>(IdentityUid);
                CreateDt = contragent.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = contragent.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Hosts:
                HostModel host = AppSettings.DataAccess.Crud.GetItemByIdNotNull<HostModel>(IdentityId);
                CreateDt = host.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = host.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Nomenclatures:
                NomenclatureModel nomenclature = AppSettings.DataAccess.Crud.GetItemByIdNotNull<NomenclatureModel>(IdentityId);
                CreateDt = nomenclature.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = nomenclature.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Orders:
                OrderModel order = AppSettings.DataAccess.Crud.GetItemByIdNotNull<OrderModel>(IdentityId);
                CreateDt = order.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = order.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.OrdersWeighings:
                OrderWeighingModel orderWeighing = AppSettings.DataAccess.Crud.GetItemByUidNotNull<OrderWeighingModel>(IdentityUid);
                CreateDt = orderWeighing.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = orderWeighing.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Organizations:
                OrganizationModel organization = AppSettings.DataAccess.Crud.GetItemByIdNotNull<OrganizationModel>(IdentityId);
                CreateDt = organization.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = organization.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Plus:
                PluModel plu = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluModel>(IdentityUid);
                CreateDt = plu.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = plu.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusLabels:
                PluLabelModel pluLabel = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluLabelModel>(IdentityUid);
                CreateDt = pluLabel.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluLabel.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusObsolete:
                PluObsoleteModel pluObsolete = AppSettings.DataAccess.Crud.GetItemByIdNotNull<PluObsoleteModel>(IdentityId);
                CreateDt = pluObsolete.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluObsolete.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusScales:
                PluScaleModel pluScale = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluScaleModel>(IdentityUid);
                CreateDt = pluScale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluScale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PlusWeighings:
	            PluWeighingModel weithingFact = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluWeighingModel>(IdentityUid);
	            CreateDt = weithingFact.CreateDt.ToString(CultureInfo.InvariantCulture);
	            ChangeDt = weithingFact.ChangeDt.ToString(CultureInfo.InvariantCulture);
	            break;
            case ProjectsEnums.TableScale.Printers:
                PrinterModel printer = AppSettings.DataAccess.Crud.GetItemByIdNotNull<PrinterModel>(IdentityId);
                CreateDt = printer.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printer.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.PrintersResources:
                PrinterResourceModel printerResource = AppSettings.DataAccess.Crud.GetItemByIdNotNull<PrinterResourceModel>(IdentityId);
                CreateDt = printerResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printerResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.ProductionFacilities:
                ProductionFacilityModel productionFacility = AppSettings.DataAccess.Crud.GetItemByIdNotNull<ProductionFacilityModel>(IdentityId);
                CreateDt = productionFacility.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = productionFacility.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.ProductSeries:
                ProductSeriesModel productSeries = AppSettings.DataAccess.Crud.GetItemByIdNotNull<ProductSeriesModel>(IdentityId);
                CreateDt = productSeries.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Scales:
                ScaleModel scale = AppSettings.DataAccess.Crud.GetItemByIdNotNull<ScaleModel>(IdentityId);
                CreateDt = scale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = scale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Templates:
                TemplateModel template = AppSettings.DataAccess.Crud.GetItemByIdNotNull<TemplateModel>(IdentityId);
                CreateDt = template.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = template.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.TemplatesResources:
                TemplateResourceModel templateResource = AppSettings.DataAccess.Crud.GetItemByIdNotNull<TemplateResourceModel>(IdentityId);
                CreateDt = templateResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = templateResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableScale.Workshops:
                WorkShopModel workshop = AppSettings.DataAccess.Crud.GetItemByIdNotNull<WorkShopModel>(IdentityId);
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
                AccessModel access = AppSettings.DataAccess.Crud.GetItemByUid<AccessModel>(IdentityUid);
                CreateDt = access.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = access.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProjectsEnums.TableSystem.Logs:
                LogModel log = AppSettings.DataAccess.Crud.GetItemByUid<LogModel>(IdentityUid);
                CreateDt = log.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
        }
    }

    #endregion
}
