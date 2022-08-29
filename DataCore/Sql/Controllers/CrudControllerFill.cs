// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.QueriesModels;
using DataCore.Sql.TableDwhModels;
using DataCore.Sql.Tables;

namespace DataCore.Sql.Controllers;

public partial class CrudController
{
	#region Public and private methods

	private void FillReferences<T>(T? item) where T : TableModel, new()
	{
		FillReferencesSystem(item);
		FillReferencesDatas(item);
		FillReferencesScales(item);
		FillReferencesDwh(item);
	}

	private void FillReferencesSystem<T>(T? item) where T : TableModel, new()
	{
		if (item == null) return;
		switch (item)
		{
			case LogEntity log:
				log.App = log.App?.IdentityUid == null ? new() : GetItemByUid<AppEntity>(log.App.IdentityUid);
				log.Host = log.Host?.IdentityId == null ? new() : GetItemById<HostEntity>(log.Host.IdentityId);
				log.LogType = log.LogType?.IdentityUid == null ? new() : GetItemByUid<LogTypeEntity>(log.LogType.IdentityUid);
				break;
		}
	}

	private void FillReferencesDatas<T>(T? item) where T : TableModel, new()
	{
		if (item == null) return;
		switch (item)
		{
			case DeviceModel device:
				ScaleEntity? scale = GetItemById<ScaleEntity>(device.Scales.IdentityId);
				if (scale is not null)
					device.Scales = scale;
				break;
		}
	}

	private void FillReferencesScales<T>(T? item) where T : TableModel, new()
	{
		if (item == null) return;
		switch (item)
		{
			case BarCodeEntity barcode:
				barcode.BarcodeType = GetItemByUid<BarCodeTypeEntity>(barcode.BarcodeType?.IdentityUid);
				barcode.Contragent = GetItemByUid<ContragentEntity>(barcode.Contragent?.IdentityUid);
				barcode.Nomenclature = GetItemById<TableScaleModels.NomenclatureEntity>(barcode.Nomenclature?.IdentityId);
				break;
			case OrderWeighingEntity orderWeighing:
				OrderEntity? order = GetItemByUid<OrderEntity>(orderWeighing.Order.IdentityUid);
				if (order is not null)
					orderWeighing.Order = order;
				PluWeighingEntity? pluWeighing1 = GetItemByUid<PluWeighingEntity>(orderWeighing.PluWeighing.IdentityUid);
				if (pluWeighing1 is not null)
					orderWeighing.PluWeighing = pluWeighing1;
				break;
			case PluEntity plu:
				TemplateEntity? template = GetItemById<TemplateEntity>(plu.Template.IdentityId);
				if (template is not null)
					plu.Template = template;
				TableScaleModels.NomenclatureEntity? nomenclature = GetItemById<TableScaleModels.NomenclatureEntity>(plu.Nomenclature.IdentityId);
				if (nomenclature is not null)
					plu.Nomenclature = nomenclature;
				break;
			case PluLabelEntity pluLabel:
				PluWeighingEntity? pluWeighing2 = GetItemByUid<PluWeighingEntity>(pluLabel.PluWeighing?.IdentityUid);
				if (pluWeighing2 is not null)
					pluLabel.PluWeighing = pluWeighing2;
				break;
			case PluObsoleteEntity pluObsolete:
				TemplateEntity? template1 = GetItemById<TemplateEntity>(pluObsolete.Template.IdentityId);
				if (template1 is not null)
					pluObsolete.Template = template1;
				ScaleEntity? scale1 = GetItemById<ScaleEntity>(pluObsolete.Scale.IdentityId);
				if (scale1 is not null)
					pluObsolete.Scale = scale1;
				TableScaleModels.NomenclatureEntity? nomenclature2 = GetItemById<TableScaleModels.NomenclatureEntity>(pluObsolete.Nomenclature.IdentityId);
				if (nomenclature2 is not null)
					pluObsolete.Nomenclature = nomenclature2;
				break;
			case PluScaleEntity pluScale:
				PluEntity? plu2 = GetItemByUid<PluEntity>(pluScale.Plu.IdentityUid);
				if (plu2 is not null)
					pluScale.Plu = plu2;
				ScaleEntity? scale2 = GetItemById<ScaleEntity>(pluScale.Scale.IdentityId);
				if (scale2 is not null)
					pluScale.Scale = scale2;
				break;
			case PluWeighingEntity pluWeighing:
				PluScaleEntity? pluScale2 = GetItemByUid<PluScaleEntity>(pluWeighing.PluScale.IdentityUid);
				if (pluScale2 is not null)
					pluWeighing.PluScale = pluScale2;
				ProductSeriesEntity? productSeries = GetItemById<ProductSeriesEntity>(pluWeighing.Series.IdentityId);
				if (productSeries is not null)
					pluWeighing.Series = productSeries;
				break;
			case PrinterEntity printer:
				PrinterTypeEntity? printerType = GetItemById<PrinterTypeEntity>(printer.PrinterType.IdentityId);
				if (printerType is not null)
					printer.PrinterType = printerType;
				break;
			case PrinterResourceEntity printerResource:
				PrinterEntity? printer2 = GetItemById<PrinterEntity>(printerResource.Printer.IdentityId);
				if (printer2 is not null)
					printerResource.Printer = printer2;
				TemplateResourceEntity? templateResource2 = GetItemById<TemplateResourceEntity>(printerResource.Resource.IdentityId);
				if (templateResource2 is not null)
					printerResource.Resource = templateResource2;
				if (string.IsNullOrEmpty(printerResource.Resource.Description))
					printerResource.Resource.Description = printerResource.Resource.Name;
				break;
			case ProductSeriesEntity product:
				ScaleEntity? scale3 = GetItemById<ScaleEntity>(product.Scale.IdentityId);
				if (scale3 is not null)
					product.Scale = scale3;
				break;
			case ScaleEntity scale:
				scale.TemplateDefault = GetItemById<TemplateEntity>(scale.TemplateDefault?.IdentityId);
				scale.TemplateSeries = GetItemById<TemplateEntity>(scale.TemplateSeries?.IdentityId);
				scale.PrinterMain = GetItemById<PrinterEntity>(scale.PrinterMain?.IdentityId);
				scale.PrinterShipping = GetItemById<PrinterEntity>(scale.PrinterShipping?.IdentityId);
				scale.Host = GetItemById<HostEntity>(scale.Host?.IdentityId);
				scale.WorkShop = GetItemById<WorkShopEntity>(scale.WorkShop?.IdentityId);
				break;
			case TaskEntity task:
				TaskTypeEntity? taskType = GetItemByUid<TaskTypeEntity>(task.TaskType.IdentityUid);
				if (taskType is not null)
					task.TaskType = taskType;
				ScaleEntity? scale4 = GetItemById<ScaleEntity>(task.Scale.IdentityId);
				if (scale4 is not null)
					task.Scale = scale4;
				break;
			case WorkShopEntity workshop:
				ProductionFacilityEntity? productionFacility = GetItemById<ProductionFacilityEntity>(workshop.ProductionFacility.IdentityId);
				if (productionFacility is not null)
					workshop.ProductionFacility = productionFacility;
				break;
		}
	}

	private void FillReferencesDwh<T>(T? item) where T : TableModel, new()
	{
		if (item == null) return;
		InformationSystemEntity? informationSystem;
		switch (item)
		{
			case BrandEntity brand:
				informationSystem = GetItemById<InformationSystemEntity>(brand.InformationSystem.IdentityId);
				if (informationSystem is not null)
					brand.InformationSystem = informationSystem;
				break;
			case TableDwhModels.NomenclatureEntity nomenclature:
				StatusEntity? status = GetItemById<StatusEntity>(nomenclature.Status.IdentityId);
				if (status is not null)
					nomenclature.Status = status;
				break;
			case NomenclatureGroupEntity nomenclatureGroup:
				informationSystem = GetItemById<InformationSystemEntity>(nomenclatureGroup.InformationSystem.IdentityId);
				if (informationSystem is not null)
					nomenclatureGroup.InformationSystem = informationSystem;
				break;
			case NomenclatureLightEntity nomenclatureLight:
				informationSystem = GetItemById<InformationSystemEntity>(nomenclatureLight.InformationSystem.IdentityId);
				if (informationSystem is not null)
					nomenclatureLight.InformationSystem = informationSystem;
				break;
			case NomenclatureTypeEntity nomenclatureType:
				informationSystem = GetItemById<InformationSystemEntity>(nomenclatureType.InformationSystem.IdentityId);
				if (informationSystem is not null)
					nomenclatureType.InformationSystem = informationSystem;
				break;
		}
	}

	#endregion
}
