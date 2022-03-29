// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemScale
    {
        #region Public and private fields and properties

        public ScaleEntity? ItemCast { get => Item == null ? null : (ScaleEntity)Item; set => Item = value; }
        public string PluTitle { get; set; } = string.Empty;
        public List<PluEntity>? PluItems { get; set; } = null;
        public List<PrinterEntity>? PrinterItems { get; set; } = null;
        public List<TemplateEntity>? TemplatesDefaultItems { get; set; } = null;
        public List<TemplateEntity>? TemplatesSeriesItems { get; set; } = null;
        public List<WorkshopEntity>? WorkshopItems { get; set; } = null;
        public List<TypeEntity<string>>? ComPorts { get; set; }
        public List<HostEntity>? HostItems { get; set; } = null;
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (_locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Scales);
                        ItemCast = null;
                        ComPorts = null;
                        PluItems = null;
                        TemplatesDefaultItems = null;
                        TemplatesSeriesItems = null;
                        WorkshopItems = null;
                        PrinterItems = null;
                        HostItems = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        ItemCast = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.Id.ToString(), Id } }), null);
                        if (Id != null && TableAction == DbTableAction.New)
                            ItemCast.Id = (long)Id;
                        // ComPorts
                        ComPorts = new List<TypeEntity<string>>();
                        for (int i = 1; i < 256; i++)
                        {
                            ComPorts.Add(new TypeEntity<string>($"COM{i}", $"COM{i}"));
                        }
                        // ScaleFactor
                        ItemCast.ScaleFactor ??= 1000;
                        // PLU.
                        PluTitle = $"{@LocalizationData.DeviceControl.SectionPlus}  [{LocalizationCore.Strings.Main.DataLoading}]";
                        PluItems = AppSettings.DataAccess.Crud.GetEntities<PluEntity>(
                            new FieldListEntity(new Dictionary<string, object?> {
                            { DbField.Marked.ToString(), false },
                            { "Scale.Id", ItemCast.Id },
                            }), new FieldOrderEntity(DbField.Plu, DbOrderDirection.Asc))?.ToList();
                        PluTitle = $"{@LocalizationData.DeviceControl.SectionPlus}  [{PluItems?.Count} {@LocalizationData.DeviceControl.DataRecords}]";
                        // Other.
                        TemplatesDefaultItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.Marked.ToString(), false } }),
                            null)?.ToList();
                        TemplatesSeriesItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.Marked.ToString(), false } }),
                            null)?.ToList();
                        WorkshopItems = AppSettings.DataAccess.Crud.GetEntities<WorkshopEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.Marked.ToString(), false } }),
                            null)?.ToList();
                        PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.Marked.ToString(), false } }),
                            null)?.ToList();
                        HostItems = AppSettings.DataAccess.Crud.GetEntities<HostEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                            new FieldOrderEntity(DbField.Name, DbOrderDirection.Asc))
                        ?.ToList();
                        ButtonSettings = new(false, false, false, false, false, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
