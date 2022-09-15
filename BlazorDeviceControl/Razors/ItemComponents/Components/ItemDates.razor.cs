// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Components;

public partial class ItemDates<TItem> : RazorPageItemBase<TItem> where TItem : SqlTableBase, new ()
{
    #region Public and private fields, properties, constructor

    private string CreateDt { get; set; }
	private string ChangeDt { get; set; }

	public ItemDates()
	{
		CreateDt = string.Empty;
		ChangeDt = string.Empty;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            SetDtFromTableScale
        });
    }

    private void SetDtFromTableScale()
    {
        switch (Item)
        {
			case AccessModel:
				AccessModel access = AppSettings.DataAccess.GetItemByUidNotNull<AccessModel>(IdentityUid);
				CreateDt = access.CreateDt.ToString(CultureInfo.InvariantCulture);
				ChangeDt = access.ChangeDt.ToString(CultureInfo.InvariantCulture);
				break;
			case LogModel:
				LogModel log = AppSettings.DataAccess.GetItemByUidNotNull<LogModel>(IdentityUid);
				CreateDt = log.CreateDt.ToString(CultureInfo.InvariantCulture);
				break;
            case BarCodeTypeModel:
                BarCodeTypeModel barcodeType = AppSettings.DataAccess.GetItemByUidNotNull<BarCodeTypeModel>(IdentityUid);
                CreateDt = barcodeType.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = barcodeType.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ContragentModel:
                ContragentModel contragent = AppSettings.DataAccess.GetItemByUidNotNull<ContragentModel>(IdentityUid);
                CreateDt = contragent.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = contragent.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case HostModel:
                HostModel host = AppSettings.DataAccess.GetItemByIdNotNull<HostModel>(IdentityId);
                CreateDt = host.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = host.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case NomenclatureModel:
                NomenclatureModel nomenclature = AppSettings.DataAccess.GetItemByIdNotNull<NomenclatureModel>(IdentityId);
                CreateDt = nomenclature.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = nomenclature.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case OrderModel:
                OrderModel order = AppSettings.DataAccess.GetItemByIdNotNull<OrderModel>(IdentityId);
                CreateDt = order.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = order.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case OrderWeighingModel:
                OrderWeighingModel orderWeighing = AppSettings.DataAccess.GetItemByUidNotNull<OrderWeighingModel>(IdentityUid);
                CreateDt = orderWeighing.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = orderWeighing.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case OrganizationModel:
                OrganizationModel organization = AppSettings.DataAccess.GetItemByIdNotNull<OrganizationModel>(IdentityId);
                CreateDt = organization.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = organization.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case PluModel:
                PluModel plu = AppSettings.DataAccess.GetItemByUidNotNull<PluModel>(IdentityUid);
                CreateDt = plu.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = plu.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case PluLabelModel:
                PluLabelModel pluLabel = AppSettings.DataAccess.GetItemByUidNotNull<PluLabelModel>(IdentityUid);
                CreateDt = pluLabel.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluLabel.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            //case PluObsoleteModel:
            //    PluObsoleteModel pluObsolete = AppSettings.DataAccess.GetItemByIdNotNull<PluObsoleteModel>(IdentityId);
            //    CreateDt = pluObsolete.CreateDt.ToString(CultureInfo.InvariantCulture);
            //    ChangeDt = pluObsolete.ChangeDt.ToString(CultureInfo.InvariantCulture);
            //    break;
            case PluScaleModel:
                PluScaleModel pluScale = AppSettings.DataAccess.GetItemByUidNotNull<PluScaleModel>(IdentityUid);
                CreateDt = pluScale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluScale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case PluWeighingModel:
	            PluWeighingModel weithingFact = AppSettings.DataAccess.GetItemByUidNotNull<PluWeighingModel>(IdentityUid);
	            CreateDt = weithingFact.CreateDt.ToString(CultureInfo.InvariantCulture);
	            ChangeDt = weithingFact.ChangeDt.ToString(CultureInfo.InvariantCulture);
	            break;
            case PrinterModel:
                PrinterModel printer = AppSettings.DataAccess.GetItemByIdNotNull<PrinterModel>(IdentityId);
                CreateDt = printer.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printer.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case PrinterResourceModel:
                PrinterResourceModel printerResource = AppSettings.DataAccess.GetItemByIdNotNull<PrinterResourceModel>(IdentityId);
                CreateDt = printerResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printerResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProductionFacilityModel:
                ProductionFacilityModel productionFacility = AppSettings.DataAccess.GetItemByIdNotNull<ProductionFacilityModel>(IdentityId);
                CreateDt = productionFacility.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = productionFacility.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ProductSeriesModel:
                ProductSeriesModel productSeries = AppSettings.DataAccess.GetItemByIdNotNull<ProductSeriesModel>(IdentityId);
                CreateDt = productSeries.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
            case ScaleModel:
                ScaleModel scale = AppSettings.DataAccess.GetItemByIdNotNull<ScaleModel>(IdentityId);
                CreateDt = scale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = scale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case TemplateModel:
                TemplateModel template = AppSettings.DataAccess.GetItemByIdNotNull<TemplateModel>(IdentityId);
                CreateDt = template.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = template.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case TemplateResourceModel:
                TemplateResourceModel templateResource = AppSettings.DataAccess.GetItemByIdNotNull<TemplateResourceModel>(IdentityId);
                CreateDt = templateResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = templateResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case WorkShopModel:
                WorkShopModel workshop = AppSettings.DataAccess.GetItemByIdNotNull<WorkShopModel>(IdentityId);
                CreateDt = workshop.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = workshop.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
        }
    }

    #endregion
}
