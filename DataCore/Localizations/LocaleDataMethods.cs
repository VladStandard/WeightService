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

            ProjectsEnums.TableSystem tableSystem = ProjectsEnums.GetTableSystem(table.Name);
            {
                switch (tableSystem)
                {
                    case ProjectsEnums.TableSystem.Accesses:
                        result = Strings.ItemAccess;
                        break;
                    case ProjectsEnums.TableSystem.Logs:
                        result = Strings.ItemLog;
                        break;
                }
            }

            ProjectsEnums.TableScale tableScale = ProjectsEnums.GetTableScale(table.Name);
            {
                switch (tableScale)
                {
                    case ProjectsEnums.TableScale.BarCodeTypes:
                        result = DeviceControl.ItemBarCodeType;
                        break;
                    case ProjectsEnums.TableScale.Contragents:
                        result = DeviceControl.ItemContragent;
                        break;
                    case ProjectsEnums.TableScale.Hosts:
                        result = DeviceControl.ItemHost;
                        break;
                    case ProjectsEnums.TableScale.PlusLabels:
                        result = DeviceControl.ItemLabel;
                        break;
                    case ProjectsEnums.TableScale.Nomenclatures:
                        result = DeviceControl.ItemNomenclature;
                        break;
                    case ProjectsEnums.TableScale.Orders:
                        result = DeviceControl.ItemOrder;
                        break;
                    case ProjectsEnums.TableScale.OrdersWeighings:
                        result = DeviceControl.ItemOrderWeighing;
                        break;
                    case ProjectsEnums.TableScale.PlusObsolete:
                        result = DeviceControl.ItemPlu;
                        break;
                    case ProjectsEnums.TableScale.Plus:
                        result = DeviceControl.ItemPlu;
                        break;
                    case ProjectsEnums.TableScale.PlusScales:
                        result = DeviceControl.ItemPluScale;
                        break;
                    case ProjectsEnums.TableScale.Printers:
                        result = Print.Name;
                        break;
                    case ProjectsEnums.TableScale.PrintersResources:
                        result = Print.Resource;
                        break;
                    case ProjectsEnums.TableScale.PrintersTypes:
                        result = Print.Type;
                        break;
                    case ProjectsEnums.TableScale.ProductSeries:
                        result = DeviceControl.ItemProductSeries;
                        break;
                    case ProjectsEnums.TableScale.ProductionFacilities:
                        result = DeviceControl.ItemProductionFacility;
                        break;
                    case ProjectsEnums.TableScale.Scales:
                        result = DeviceControl.ItemScale;
                        break;
                    case ProjectsEnums.TableScale.TemplatesResources:
                        result = DeviceControl.ItemTemplateResource;
                        break;
                    case ProjectsEnums.TableScale.Templates:
                        result = DeviceControl.ItemTemplate;
                        break;
                    case ProjectsEnums.TableScale.PlusWeighings:
                        result = DeviceControl.ItemWeithingFact;
                        break;
                    case ProjectsEnums.TableScale.Workshops:
                        result = DeviceControl.ItemWorkshop;
                        break;
                    case ProjectsEnums.TableScale.Default:
                        break;
                    case ProjectsEnums.TableScale.Organizations:
                        result = DeviceControl.ItemOrganization;
                        break;
                    case ProjectsEnums.TableScale.BarCodes:
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

            ProjectsEnums.TableSystem tableSystem = ProjectsEnums.GetTableSystem(table.Name);
            {
                switch (tableSystem)
                {
                    case ProjectsEnums.TableSystem.Accesses:
                        result = Strings.SectionAccess;
                        break;
                    case ProjectsEnums.TableSystem.Logs:
                        result = Strings.SectionLog;
                        break;
                }
            }

            ProjectsEnums.TableScale tableScale = ProjectsEnums.GetTableScale(table.Name);
            {
                switch (tableScale)
                {
                    case ProjectsEnums.TableScale.Default:
                        break;
                    case ProjectsEnums.TableScale.BarCodes:
                        result = DeviceControl.SectionBarCodes;
                        break;
                    case ProjectsEnums.TableScale.BarCodeTypes:
                        result = DeviceControl.SectionBarCodeTypes;
                        break;
                    case ProjectsEnums.TableScale.Contragents:
                        result = DeviceControl.SectionContragents;
                        break;
                    case ProjectsEnums.TableScale.Hosts:
                        result = DeviceControl.SectionHosts;
                        break;
                    case ProjectsEnums.TableScale.PlusLabels:
                        result = DeviceControl.SectionLabels;
                        break;
                    case ProjectsEnums.TableScale.Nomenclatures:
                        result = DeviceControl.SectionNomenclatures;
                        break;
                    case ProjectsEnums.TableScale.Orders:
                        result = DeviceControl.SectionOrders;
                        break;
                    case ProjectsEnums.TableScale.OrdersWeighings:
                        result = DeviceControl.SectionOrdersWeighings;
                        break;
                    case ProjectsEnums.TableScale.PlusObsolete:
                        result = DeviceControl.SectionPlus;
                        break;
                    case ProjectsEnums.TableScale.Plus:
                        result = DeviceControl.SectionPlus;
                        break;
                    case ProjectsEnums.TableScale.PlusScales:
                        result = DeviceControl.SectionPlusScales;
                        break;
                    case ProjectsEnums.TableScale.Printers:
                        result = Print.Name;
                        break;
                    case ProjectsEnums.TableScale.PrintersResources:
                        result = Print.Resources;
                        break;
                    case ProjectsEnums.TableScale.PrintersTypes:
                        result = Print.Types;
                        break;
                    case ProjectsEnums.TableScale.ProductSeries:
                        result = DeviceControl.SectionProductSeries;
                        break;
                    case ProjectsEnums.TableScale.ProductionFacilities:
                        result = DeviceControl.SectionProductionFacilities;
                        break;
                    case ProjectsEnums.TableScale.Scales:
                        result = DeviceControl.SectionScales;
                        break;
                    case ProjectsEnums.TableScale.TemplatesResources:
                        result = DeviceControl.SectionTemplateResources;
                        break;
                    case ProjectsEnums.TableScale.Templates:
                        result = DeviceControl.SectionTemplates;
                        break;
                    case ProjectsEnums.TableScale.PlusWeighings:
                        result = DeviceControl.SectionPlusWeighings;
                        break;
                    case ProjectsEnums.TableScale.Workshops:
                        result = DeviceControl.SectionWorkShops;
                        break;
                    case ProjectsEnums.TableScale.Organizations:
                        result = DeviceControl.SectionOrganizations;
                        break;
                }
            }

            return result;
        }
    }
}
