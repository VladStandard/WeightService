// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using static DataCore.Localizations.LocaleCore;

namespace DataCore.Localizations;

public static partial class LocaleData
{
    public static class Methods
    {
        public static string GetItemTitle(TableBase table)
        {
            string result = string.Empty;

            SqlTableScaleEnum tableSystem = SqlUtils.GetTableScale(table.Name);
            {
                switch (tableSystem)
                {
                    case SqlTableScaleEnum.Accesses:
                        result = Strings.ItemAccess;
                        break;
                    case SqlTableScaleEnum.Logs:
                        result = Strings.ItemLog;
                        break;
                }
            }

            SqlTableScaleEnum tableScale = SqlUtils.GetTableScale(table.Name);
            {
                switch (tableScale)
                {
                    case SqlTableScaleEnum.BarCodesTypes:
                        result = DeviceControl.ItemBarCodeType;
                        break;
                    case SqlTableScaleEnum.Contragents:
                        result = DeviceControl.ItemContragent;
                        break;
                    case SqlTableScaleEnum.Hosts:
                        result = DeviceControl.ItemHost;
                        break;
                    case SqlTableScaleEnum.PlusLabels:
                        result = DeviceControl.ItemLabel;
                        break;
                    case SqlTableScaleEnum.Nomenclatures:
                        result = DeviceControl.ItemNomenclature;
                        break;
                    case SqlTableScaleEnum.Orders:
                        result = DeviceControl.ItemOrder;
                        break;
                    case SqlTableScaleEnum.OrdersWeighings:
                        result = DeviceControl.ItemOrderWeighing;
                        break;
                    case SqlTableScaleEnum.PlusObsolete:
                        result = DeviceControl.ItemPlu;
                        break;
                    case SqlTableScaleEnum.Plus:
                        result = DeviceControl.ItemPlu;
                        break;
                    case SqlTableScaleEnum.PlusScales:
                        result = DeviceControl.ItemPluScale;
                        break;
                    case SqlTableScaleEnum.Printers:
                        result = Print.Name;
                        break;
                    case SqlTableScaleEnum.PrintersResources:
                        result = Print.Resource;
                        break;
                    case SqlTableScaleEnum.PrintersTypes:
                        result = Print.Type;
                        break;
                    case SqlTableScaleEnum.ProductSeries:
                        result = DeviceControl.ItemProductSeries;
                        break;
                    case SqlTableScaleEnum.ProductionFacilities:
                        result = DeviceControl.ItemProductionFacility;
                        break;
                    case SqlTableScaleEnum.Scales:
                        result = DeviceControl.ItemScale;
                        break;
                    case SqlTableScaleEnum.TemplatesResources:
                        result = DeviceControl.ItemTemplateResource;
                        break;
                    case SqlTableScaleEnum.Templates:
                        result = DeviceControl.ItemTemplate;
                        break;
                    case SqlTableScaleEnum.PlusWeighings:
                        result = DeviceControl.ItemWeithingFact;
                        break;
                    case SqlTableScaleEnum.WorkShops:
                        result = DeviceControl.ItemWorkshop;
                        break;
                    case SqlTableScaleEnum.Empty:
                        break;
                    case SqlTableScaleEnum.Organizations:
                        result = DeviceControl.ItemOrganization;
                        break;
                    case SqlTableScaleEnum.BarCodes:
                        break;
                }
            }
            return result;
        }

        public static string GetItemTitle(TableBase table, int itemId)
        {
            return $"{GetItemTitle(table)}. ID: {itemId}";
        }

        public static string GetItemTitle(TableBase table, Guid itemUid)
        {
            return $"{GetItemTitle(table)}. UID: {itemUid}";
        }

        public static string GetSectionTitle(TableBase table)
        {
            string result = string.Empty;

            SqlTableScaleEnum tableSystem = SqlUtils.GetTableScale(table.Name);
            {
	            result = tableSystem switch
	            {
		            SqlTableScaleEnum.Accesses => Strings.SectionAccess,
		            SqlTableScaleEnum.Logs => Strings.SectionLog,
		            _ => result
	            };
            }

            SqlTableScaleEnum tableScale = SqlUtils.GetTableScale(table.Name);
            {
	            result = tableScale switch
	            {
		            SqlTableScaleEnum.BarCodes => DeviceControl.SectionBarCodes,
		            SqlTableScaleEnum.BarCodesTypes => DeviceControl.SectionBarCodeTypes,
		            SqlTableScaleEnum.Contragents => DeviceControl.SectionContragents,
		            SqlTableScaleEnum.Hosts => DeviceControl.SectionHosts,
		            SqlTableScaleEnum.PlusLabels => DeviceControl.SectionLabels,
		            SqlTableScaleEnum.Nomenclatures => DeviceControl.SectionNomenclatures,
		            SqlTableScaleEnum.Orders => DeviceControl.SectionOrders,
		            SqlTableScaleEnum.OrdersWeighings => DeviceControl.SectionOrdersWeighings,
		            SqlTableScaleEnum.PlusObsolete => DeviceControl.SectionPlus,
		            SqlTableScaleEnum.Plus => DeviceControl.SectionPlus,
		            SqlTableScaleEnum.PlusScales => DeviceControl.SectionPlusScales,
		            SqlTableScaleEnum.Printers => Print.Name,
		            SqlTableScaleEnum.PrintersResources => Print.Resources,
		            SqlTableScaleEnum.PrintersTypes => Print.Types,
		            SqlTableScaleEnum.ProductSeries => DeviceControl.SectionProductSeries,
		            SqlTableScaleEnum.ProductionFacilities => DeviceControl.SectionProductionFacilities,
		            SqlTableScaleEnum.Scales => DeviceControl.SectionScales,
		            SqlTableScaleEnum.TemplatesResources => DeviceControl.SectionTemplateResources,
		            SqlTableScaleEnum.Templates => DeviceControl.SectionTemplates,
		            SqlTableScaleEnum.PlusWeighings => DeviceControl.SectionPlusWeighings,
		            SqlTableScaleEnum.WorkShops => DeviceControl.SectionWorkShops,
		            SqlTableScaleEnum.Organizations => DeviceControl.SectionOrganizations,
		            _ => result
	            };
            }

            return result;
        }
    }
}
