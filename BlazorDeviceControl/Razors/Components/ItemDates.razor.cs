// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Components;

public partial class ItemDates : RazorPageBase
{
    #region Public and private fields, properties, constructor

    private string CreateDt { get; set; }
	private string ChangeDt { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
    {
        base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
		        CreateDt = string.Empty;
		        ChangeDt = string.Empty;
			}
		});
    }

   protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
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
        switch (SqlUtils.GetTableScale(Table.Name))
        {
            case SqlTableScaleEnum.BarCodeTypes:
                BarCodeTypeModel barcodeType = AppSettings.DataAccess.GetItemByUidNotNull<BarCodeTypeModel>(IdentityUid);
                CreateDt = barcodeType.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = barcodeType.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Contragents:
                ContragentModel contragent = AppSettings.DataAccess.GetItemByUidNotNull<ContragentModel>(IdentityUid);
                CreateDt = contragent.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = contragent.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Hosts:
                HostModel host = AppSettings.DataAccess.GetItemByIdNotNull<HostModel>(IdentityId);
                CreateDt = host.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = host.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Nomenclatures:
                NomenclatureModel nomenclature = AppSettings.DataAccess.GetItemByIdNotNull<NomenclatureModel>(IdentityId);
                CreateDt = nomenclature.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = nomenclature.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Orders:
                OrderModel order = AppSettings.DataAccess.GetItemByIdNotNull<OrderModel>(IdentityId);
                CreateDt = order.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = order.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.OrdersWeighings:
                OrderWeighingModel orderWeighing = AppSettings.DataAccess.GetItemByUidNotNull<OrderWeighingModel>(IdentityUid);
                CreateDt = orderWeighing.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = orderWeighing.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Organizations:
                OrganizationModel organization = AppSettings.DataAccess.GetItemByIdNotNull<OrganizationModel>(IdentityId);
                CreateDt = organization.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = organization.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Plus:
                PluModel plu = AppSettings.DataAccess.GetItemByUidNotNull<PluModel>(IdentityUid);
                CreateDt = plu.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = plu.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.PlusLabels:
                PluLabelModel pluLabel = AppSettings.DataAccess.GetItemByUidNotNull<PluLabelModel>(IdentityUid);
                CreateDt = pluLabel.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluLabel.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.PlusObsolete:
                PluObsoleteModel pluObsolete = AppSettings.DataAccess.GetItemByIdNotNull<PluObsoleteModel>(IdentityId);
                CreateDt = pluObsolete.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluObsolete.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.PlusScales:
                PluScaleModel pluScale = AppSettings.DataAccess.GetItemByUidNotNull<PluScaleModel>(IdentityUid);
                CreateDt = pluScale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = pluScale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.PlusWeighings:
	            PluWeighingModel weithingFact = AppSettings.DataAccess.GetItemByUidNotNull<PluWeighingModel>(IdentityUid);
	            CreateDt = weithingFact.CreateDt.ToString(CultureInfo.InvariantCulture);
	            ChangeDt = weithingFact.ChangeDt.ToString(CultureInfo.InvariantCulture);
	            break;
            case SqlTableScaleEnum.Printers:
                PrinterModel printer = AppSettings.DataAccess.GetItemByIdNotNull<PrinterModel>(IdentityId);
                CreateDt = printer.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printer.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.PrintersResources:
                PrinterResourceModel printerResource = AppSettings.DataAccess.GetItemByIdNotNull<PrinterResourceModel>(IdentityId);
                CreateDt = printerResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = printerResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.ProductionFacilities:
                ProductionFacilityModel productionFacility = AppSettings.DataAccess.GetItemByIdNotNull<ProductionFacilityModel>(IdentityId);
                CreateDt = productionFacility.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = productionFacility.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.ProductSeries:
                ProductSeriesModel productSeries = AppSettings.DataAccess.GetItemByIdNotNull<ProductSeriesModel>(IdentityId);
                CreateDt = productSeries.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Scales:
                ScaleModel scale = AppSettings.DataAccess.GetItemByIdNotNull<ScaleModel>(IdentityId);
                CreateDt = scale.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = scale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Templates:
                TemplateModel template = AppSettings.DataAccess.GetItemByIdNotNull<TemplateModel>(IdentityId);
                CreateDt = template.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = template.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.TemplatesResources:
                TemplateResourceModel templateResource = AppSettings.DataAccess.GetItemByIdNotNull<TemplateResourceModel>(IdentityId);
                CreateDt = templateResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = templateResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableScaleEnum.Workshops:
                WorkShopModel workshop = AppSettings.DataAccess.GetItemByIdNotNull<WorkShopModel>(IdentityId);
                CreateDt = workshop.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = workshop.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
        }
    }

    private void SetDtFromTableSystem()
    {
        switch (SqlUtils.GetTableSystem(Table.Name))
        {
            case SqlTableSystemEnum.Accesses:
                AccessModel access = AppSettings.DataAccess.GetItemByUid<AccessModel>(IdentityUid);
                CreateDt = access.CreateDt.ToString(CultureInfo.InvariantCulture);
                ChangeDt = access.ChangeDt.ToString(CultureInfo.InvariantCulture);
                break;
            case SqlTableSystemEnum.Logs:
                LogModel log = AppSettings.DataAccess.GetItemByUid<LogModel>(IdentityUid);
                CreateDt = log.CreateDt.ToString(CultureInfo.InvariantCulture);
                break;
        }
    }

    #endregion
}
