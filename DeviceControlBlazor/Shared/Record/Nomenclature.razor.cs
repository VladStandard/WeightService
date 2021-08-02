// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.TableModels;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DeviceControlBlazor.Shared.Record
{
    public partial class Nomenclature
    {
        #region Public and private fields and properties

        [Parameter] public NomenclatureEntity Item { get; set; }

        #endregion

        #region Public and private methods

        private async Task RowSelectAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                //if (entity is BarCodesEntity barCodesEntity)
                //{
                //    BarCodesEntity = barCodesEntity;
                //}
                //else if (entity is NomenclatureUnitsEntity nomenclatureUnitsEntity)
                //{
                //    NomenclatureUnitsEntity = nomenclatureUnitsEntity;
                //}
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettings.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(msg.Detail);
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task RowDoubleClickAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                //if (entity is BarCodesEntity barCodesEntity)
                //{
                //    BarCodesEntity = barCodesEntity;
                //    await ActionEditAsync(EnumTable.BarCodes, BarCodesEntity).ConfigureAwait(true);
                //}
                //else if (entity is NomenclatureUnitsEntity nomenclatureUnitsEntity)
                //{
                //    NomenclatureUnitsEntity = nomenclatureUnitsEntity;
                //    await ActionEditAsync(EnumTable.NomenclatureUnits, NomenclatureUnitsEntity).ConfigureAwait(true);
                //}
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettings.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine(msg.Detail);
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task ActionEditAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync(new Task(null)).ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync(new Task(null)).ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync(new Task(null)).ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync(new Task(null)).ConfigureAwait(false);
        }

        #endregion
    }
}