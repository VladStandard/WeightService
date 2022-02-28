// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
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

        public bool ProcessChecks(NotificationService notificationService, BaseEntity? item, string field)
        {
            bool result = item != null;
            string detailAddition = Environment.NewLine;
            switch (item)
            {
                case BarcodeTypeEntity barCodeType:
                    BarcodeType(ref result, ref detailAddition, barCodeType);
                    break;
                case ContragentEntity contragent:
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
                case WorkshopEntity workshop:
                    Workshop(ref result, ref detailAddition, workshop);
                    break;
            }
            if (!result)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = LocalizationCore.Strings.DataControl,
                    Detail = $"{LocalizationCore.Strings.DataControlField} [{field}]!" +
                        (Equals(detailAddition, Environment.NewLine) ? string.Empty : detailAddition),
                    Duration = AppSettingsHelper.Delay
                };
                notificationService.Notify(msg);
                return false;
            }
            return true;
        }

        public void BarcodeType(ref bool result, ref string detailAddition, BarcodeTypeEntity barCodeType)
        {
            if (barCodeType.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(barCodeType.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        public void Contragent(ref bool result, ref string detailAddition, ContragentEntity contragent)
        {
            if (contragent.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(contragent.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        public void Host(ref bool result, ref string detailAddition, HostEntity host)
        {
            if (host.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(host.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
            if (Equals(host.IdRRef, Guid.Empty))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldIdRRef}" + Environment.NewLine;
                result = false;
            }
            if (string.IsNullOrEmpty(host.Ip))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldIpAddress}" + Environment.NewLine;
                result = false;
            }
        }

        public void Label(ref bool result, ref string detailAddition, LabelEntity label)
        {
            if (label.EqualsDefault())
                result = false;
            if (label.Label.Length == 0)
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldLabel}" + Environment.NewLine;
                result = false;
            }
        }

        public void Nomenclature(ref bool result, ref string detailAddition, NomenclatureEntity nomenclature)
        {
            if (nomenclature.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(nomenclature.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        private void Order(ref bool result, ref string detailAddition, OrderEntity order)
        {
            if (order.EqualsDefault())
                result = false;
        }

        private void OrderStatus(ref bool result, ref string detailAddition, OrderStatusEntity orderStatus)
        {
            if (orderStatus.EqualsDefault())
                result = false;
        }

        private void OrderType(ref bool result, ref string detailAddition, OrderTypeEntity orderType)
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
                    { "Scale.Id", plu.Scale.Id },
                    { DbField.Plu.ToString(), plu.Plu }
                }), null);
            if (pluEntities != null && pluEntities.Any() && !pluEntities.Where(x => x.Id.Equals(plu.Id)).Select(x => x).Any())
            {
                detailAddition += $"{LocalizationCore.Strings.TablePluHavingPlu}: {plu.Plu}" + Environment.NewLine;
                result = false;
            }
        }

        private void Printer(ref bool result, ref string detailAddition, PrinterEntity printer)
        {
            if (printer.EqualsDefault())
                result = false;
        }

        private void PrinterResource(ref bool result, ref string detailAddition, PrinterResourceEntity printerResource)
        {
            if (printerResource.EqualsDefault())
                result = false;
        }

        public void PrinterType(ref bool result, ref string detailAddition, PrinterTypeEntity printerType)
        {
            if (printerType.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(printerType.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        private void ProductionFacility(ref bool result, ref string detailAddition, ProductionFacilityEntity productionFacility)
        {
            if (productionFacility.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(productionFacility.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        private void ProductSeries(ref bool result, ref string detailAddition, ProductSeriesEntity productSeries)
        {
            if (productSeries.EqualsDefault())
                result = false;
        }

        private void Scale(ref bool result, ref string detailAddition, ScaleEntity scale)
        {
            if (scale.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(scale.Description))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldDescription}" + Environment.NewLine;
                result = false;
            }
        }

        private void Task(ref bool result, ref string detailAddition, TaskEntity task)
        {
            if (task.EqualsDefault())
                result = false;
        }

        private void TaskType(ref bool result, ref string detailAddition, TaskTypeEntity taskType)
        {
            if (taskType.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(taskType.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        private void TemplateResource(ref bool result, ref string detailAddition, TemplateResourceEntity templateResource)
        {
            if (templateResource.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(templateResource.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        public void Template(ref bool result, ref string detailAddition, TemplateEntity template)
        {
            if (template.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(template.Title))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldTitle}" + Environment.NewLine;
                result = false;
            }
            if (string.IsNullOrEmpty(template.CategoryId))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldCategory}" + Environment.NewLine;
                result = false;
            }
        }

        public void WeithingFact(ref bool result, ref string detailAddition, WeithingFactEntity weithingFact)
        {
            if (weithingFact.EqualsDefault())
                result = false;
        }

        private void Workshop(ref bool result, ref string detailAddition, WorkshopEntity workshop)
        {
            if (workshop.EqualsDefault())
                result = false;
            if (string.IsNullOrEmpty(workshop.Name))
            {
                detailAddition += $"{LocalizationCore.Strings.FieldIsEmpty}: {LocalizationCore.Strings.FieldName}" + Environment.NewLine;
                result = false;
            }
        }

        #endregion
    }
}
