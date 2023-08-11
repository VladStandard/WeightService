// Global using directives

global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Threading;
global using FluentValidation.Results;
global using MDSoft.NetUtils;
global using NSubstitute;
global using NUnit.Framework;
global using WsDataCore.Enums;
global using WsLocalizationCore.Utils;
global using WsStorageCore.Common;
global using WsStorageCore.Helpers;
global using WsStorageCore.Models;
global using WsStorageCore.Utils;
global using WsStorageCore.Tables.TableConfModels.DeviceSettings;
global using WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;
global using WsStorageCore.Tables.TableDiagModels.Logs;
global using WsStorageCore.Tables.TableDiagModels.LogsTypes;
global using WsStorageCore.Tables.TableDiagModels.LogsWebs;
global using WsStorageCore.Tables.TableDiagModels.LogsWebsFks;
global using WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;
global using WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;
global using WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusBundlesFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusCharacteristicsFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusGroupsFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusNestingFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;
global using WsStorageCore.Tables.TableScaleFkModels.PrintersResourcesFks;
global using WsStorageCore.Tables.TableScaleModels.Access;
global using WsStorageCore.Tables.TableScaleModels.Apps;
global using WsStorageCore.Tables.TableScaleModels.BarCodes;
global using WsStorageCore.Tables.TableScaleModels.Boxes;
global using WsStorageCore.Tables.TableScaleModels.Brands;
global using WsStorageCore.Tables.TableScaleModels.Bundles;
global using WsStorageCore.Tables.TableScaleModels.Clips;
global using WsStorageCore.Tables.TableScaleModels.Contragents;
global using WsStorageCore.Tables.TableScaleModels.Devices;
global using WsStorageCore.Tables.TableScaleModels.DeviceTypes;
global using WsStorageCore.Tables.TableScaleModels.Orders;
global using WsStorageCore.Tables.TableScaleModels.OrdersWeighings;
global using WsStorageCore.Tables.TableScaleModels.Organizations;
global using WsStorageCore.Tables.TableScaleModels.Plus;
global using WsStorageCore.Tables.TableScaleModels.PlusCharacteristics;
global using WsStorageCore.Tables.TableScaleModels.PlusGroups;
global using WsStorageCore.Tables.TableScaleModels.PlusLabels;
global using WsStorageCore.Tables.TableScaleModels.PlusScales;
global using WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;
global using WsStorageCore.Tables.TableScaleModels.PlusWeighings;
global using WsStorageCore.Tables.TableScaleModels.Printers;
global using WsStorageCore.Tables.TableScaleModels.PrintersTypes;
global using WsStorageCore.Tables.TableScaleModels.ProductionFacilities;
global using WsStorageCore.Tables.TableScaleModels.ProductSeries;
global using WsStorageCore.Tables.TableScaleModels.Scales;
global using WsStorageCore.Tables.TableScaleModels.Tasks;
global using WsStorageCore.Tables.TableScaleModels.TasksTypes;
global using WsStorageCore.Tables.TableScaleModels.Templates;
global using WsStorageCore.Tables.TableScaleModels.TemplatesResources;
global using WsStorageCore.Tables.TableScaleModels.Versions;
global using WsStorageCore.Tables.TableScaleModels.WorkShops;