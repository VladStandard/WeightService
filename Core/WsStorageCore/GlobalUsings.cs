// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// Global using directives

global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.IO;
global using System.Linq;
global using System.Net;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.Serialization;
global using System.Runtime.Serialization.Formatters.Binary;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Unicode;
global using System.Threading;
global using System.Xml;
global using System.Xml.Linq;
global using System.Xml.Serialization;
global using System.Xml.Xsl;
global using FluentNHibernate.Cfg;
global using FluentNHibernate.Cfg.Db;
global using FluentNHibernate.Conventions;
global using FluentNHibernate.Mapping;
global using FluentValidation;
global using FluentValidation.Results;
global using MDSoft.NetUtils;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Data.SqlClient;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;
global using NHibernate;
global using NHibernate.Criterion;
global using WsDataCore.Common;
global using WsDataCore.Enums;
global using WsDataCore.Helpers;
global using WsDataCore.Memory;
global using WsDataCore.Models;
global using WsDataCore.Serialization;
global using WsDataCore.Settings.Helpers;
global using WsDataCore.Utils;
global using WsFileSystemCore.Helpers;
global using WsLocalizationCore.Utils;
global using WsPrintCore.Zpl;
global using WsStorageCore.Common;
global using WsStorageCore.Helpers;
global using WsStorageCore.Models;
global using WsStorageCore.Utils;
global using WsStorageCore.Xml;
global using WsStorageCore.Tables.TableConfModels.DeviceSettings;
global using WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;
global using WsStorageCore.Tables.TableDiagModels.Logs;
global using WsStorageCore.Tables.TableDiagModels.LogsMemories;
global using WsStorageCore.Tables.TableDiagModels.LogsTypes;
global using WsStorageCore.Tables.TableDiagModels.LogsWebs;
global using WsStorageCore.Tables.TableDiagModels.LogsWebsFks;
global using WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;
global using WsStorageCore.Tables.TableRefModels.Plus1CFk;
global using WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;
global using WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;
global using WsStorageCore.Tables.TableScaleFkModels.PlusBrandsFks;
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
global using WsStorageCore.Views.ViewDiagModels.TableSize;
global using WsStorageCore.Views.ViewRefModels.PluLines;
global using WsStorageCore.Views.ViewRefModels.PluNestings;
global using WsStorageCore.Views.ViewRefModels.PluStorageMethods;
global using static WsStorageCore.Utils.WsSqlQueriesScales.Tables;
