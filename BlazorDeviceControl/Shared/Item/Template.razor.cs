// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Template
    {
        #region Public and private fields and properties

        public TemplateEntity TemplateItem { get => (TemplateEntity)Item; set => Item = value; }
        public List<TypeEntity<string>> TemplateCategories { get; set; }
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
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
                        TemplateItem = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(new FieldListEntity(new Dictionary<string, object>
                        { { ShareEnums.DbField.Id.ToString(), Id } }), null);
                        if (Id != null && TableAction == ShareEnums.DbTableAction.New)
                            TemplateItem.Id = (int)Id;
                        TemplateCategories = AppSettings.DataReference.GetTemplateCategories();
                        ButtonSettings = new ButtonSettingsEntity(false, false, false, false, false, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        private void OnChange(object value, string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                lock (_locker)
                {
                    switch (name)
                    {
                        case "TemlateCategories":
                            if (value is string strValue)
                            {
                                TemplateItem.CategoryId = strValue;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"{LocalizationCore.Strings.Main.MethodError} [{nameof(OnChange)}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsHelper.Delay
                };
                NotificationService.Notify(msg);
                AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
            finally
            {
                StateHasChanged();
            }
        }

        #endregion
    }
}
