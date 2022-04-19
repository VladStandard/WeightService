// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Localization;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using static DataCore.ShareEnums;

namespace BlazorCore.Models
{
    public class ItemFieldControlEntity
    {
        #region Public and private fields and properties

        public AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;

        #endregion

        #region Public and private methods

        public bool ProcessChecks(NotificationService? notificationService, BaseEntity? item, string field)
        {
            bool result = item != null;
            string detailAddition = Environment.NewLine;
            switch (item)
            {
                case AccessEntity access:
                    Access(ref result, ref detailAddition, access);
                    break;
                case BarCodeTypeEntityV2 barCodeType:
                    BarcodeType(ref result, ref detailAddition, barCodeType);
                    break;
                case ContragentEntityV2 contragent:
                    Contragent(ref result, ref detailAddition, contragent);
                    break;
                case HostEntity host:
                    Host(ref result, ref detailAddition, host);
                    break;
                case LabelEntity label:
                    Label(ref result, ref detailAddition, label);
                    break;
                case NomenclatureEntity nomenclature:
                    Nomenclature(ref result, ref detailAddition, nomenclature);
                    break;
                case OrderEntity order:
                    Order(ref result, ref detailAddition, order);
                    break;
                case OrderStatusEntity orderStatus:
                    OrderStatus(ref result, ref detailAddition, orderStatus);
                    break;
                case OrderTypeEntity orderType:
                    OrderType(ref result, ref detailAddition, orderType);
                    break;
                case PluEntity plu:
                    Plu(ref result, ref detailAddition, plu);
                    break;
                case PrinterEntity printer:
                    Printer(ref result, ref detailAddition, printer);
                    break;
                case PrinterResourceEntity printerResource:
                    PrinterResource(ref result, ref detailAddition, printerResource);
                    break;
                case PrinterTypeEntity printerType:
                    PrinterType(ref result, ref detailAddition, printerType);
                    break;
                case ProductionFacilityEntity productionFacility:
                    ProductionFacility(ref result, ref detailAddition, productionFacility);
                    break;
                case ProductSeriesEntity productSeries:
                    ProductSeries(ref result, ref detailAddition, productSeries);
                    break;
                case ScaleEntity scale:
                    Scale(ref result, ref detailAddition, scale);
                    break;
                case TaskEntity task:
                    Task(ref result, ref detailAddition, task);
                    break;
                case TaskTypeEntity taskType:
                    TaskType(ref result, ref detailAddition, taskType);
                    break;
                case TemplateResourceEntity templateResource:
                    TemplateResource(ref result, ref detailAddition, templateResource);
                    break;
                case TemplateEntity template:
                    Template(ref result, ref detailAddition, template);
                    break;
                case WeithingFactEntity weithingFact:
                    WeithingFact(ref result, ref detailAddition, weithingFact);
                    break;
                case WorkShopEntity workshop:
                    Workshop(ref result, ref detailAddition, workshop);
                    break;
            }
            if (!result)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = Core.Strings.DataControl,
                    Detail = $"{Core.Strings.DataControlField} [{field}]!" +
                        (Equals(detailAddition, Environment.NewLine) ? string.Empty : detailAddition),
                    Duration = AppSettingsHelper.Delay
                };
                notificationService?.Notify(msg);
                return false;
            }
            return true;
        }

        public static void Access(ref bool result, ref string detailAddition, AccessEntity access)
        {
            if (access.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(access.User))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldUser}" + Environment.NewLine;
                result = false;
            }
            if (access.Rights > 3)
            {
                detailAddition += $"{Core.Strings.FieldIsNotInRange}: {Core.Strings.Main.AccessRights}" + Environment.NewLine;
                result = false;
            }
        }

        public static void BarcodeType(ref bool result, ref string detailAddition, BarCodeTypeEntityV2 barCodeType)
        {
            if (barCodeType.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(barCodeType.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        public static void Contragent(ref bool result, ref string detailAddition, ContragentEntityV2 contragent)
        {
            if (contragent.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(contragent.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        public static void Host(ref bool result, ref string detailAddition, HostEntity host)
        {
            if (host.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(host.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
            if (Equals(host.IdRRef, Guid.Empty))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldIdRRef}" + Environment.NewLine;
                result = false;
            }
            if (string.IsNullOrEmpty(host.Ip))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldIpAddress}" + Environment.NewLine;
                result = false;
            }
        }

        public static void Label(ref bool result, ref string detailAddition, LabelEntity label)
        {
            if (label.EqualsDefault())
                result = false;
            if (label.Label.Length == 0)
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldLabel}" + Environment.NewLine;
                result = false;
            }
        }

        public static void Nomenclature(ref bool result, ref string detailAddition, NomenclatureEntity nomenclature)
        {
            if (nomenclature.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(nomenclature.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        private static void Order(ref bool result, ref string detailAddition, OrderEntity order)
        {
            if (order.EqualsDefault())
                result = false;
        }

        private static void OrderStatus(ref bool result, ref string detailAddition, OrderStatusEntity orderStatus)
        {
            if (orderStatus.EqualsDefault())
                result = false;
        }

        private static void OrderType(ref bool result, ref string detailAddition, OrderTypeEntity orderType)
        {
            if (orderType.EqualsDefault())
                result = false;
        }

        public void Plu(ref bool result, ref string detailAddition, PluEntity plu)
        {
            if (plu.EqualsDefault())
                result = false;
            PluEntity[]? pluEntities = AppSettings.DataAccess.Crud.GetEntities<PluEntity>(
                new FieldListEntity(new Dictionary<string, object?> {
                    { $"Scale.{DbField.IdentityId}", plu.Scale.IdentityId },
                    { DbField.Plu.ToString(), plu.Plu }
                }), null);
            if (pluEntities != null && pluEntities.Any() && !pluEntities.Where(x => x.IdentityId.Equals(plu.IdentityId)).Select(x => x).Any())
            {
                detailAddition += $"{Core.Strings.TablePluHavingPlu}: {plu.Plu}" + Environment.NewLine;
                result = false;
            }
        }

        private static void Printer(ref bool result, ref string detailAddition, PrinterEntity printer)
        {
            if (printer.EqualsDefault())
                result = false;
        }

        private static void PrinterResource(ref bool result, ref string detailAddition, PrinterResourceEntity printerResource)
        {
            if (printerResource.EqualsDefault())
                result = false;
        }

        public static void PrinterType(ref bool result, ref string detailAddition, PrinterTypeEntity printerType)
        {
            if (printerType.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(printerType.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        private static void ProductionFacility(ref bool result, ref string detailAddition, ProductionFacilityEntity productionFacility)
        {
            if (productionFacility.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(productionFacility.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        private static void ProductSeries(ref bool result, ref string detailAddition, ProductSeriesEntity productSeries)
        {
            if (productSeries.EqualsDefault())
                result = false;
        }

        private static void Scale(ref bool result, ref string detailAddition, ScaleEntity scale)
        {
            if (scale.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(scale.Description))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldDescription}" + Environment.NewLine;
                result = false;
            }
        }

        private static void Task(ref bool result, ref string detailAddition, TaskEntity task)
        {
            if (task.EqualsDefault())
                result = false;
        }

        private static void TaskType(ref bool result, ref string detailAddition, TaskTypeEntity taskType)
        {
            if (taskType.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(taskType.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        private static void TemplateResource(ref bool result, ref string detailAddition, TemplateResourceEntity templateResource)
        {
            if (templateResource.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(templateResource.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        public static void Template(ref bool result, ref string detailAddition, TemplateEntity template)
        {
            if (template.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(template.Title))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldTitle}" + Environment.NewLine;
                result = false;
            }
            if (string.IsNullOrEmpty(template.CategoryId))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldCategory}" + Environment.NewLine;
                result = false;
            }
        }

        public static void WeithingFact(ref bool result, ref string detailAddition, WeithingFactEntity weithingFact)
        {
            if (weithingFact.EqualsDefault())
                result = false;
        }

        private static void Workshop(ref bool result, ref string detailAddition, WorkShopEntity workshop)
        {
            if (workshop.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(workshop.Name))
            {
                detailAddition += $"{Core.Strings.FieldIsEmpty}: {Core.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        #endregion
    }
}
