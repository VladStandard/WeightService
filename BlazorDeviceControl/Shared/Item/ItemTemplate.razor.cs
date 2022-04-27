// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemTemplate
    {
        #region Public and private fields and properties

        public TemplateEntity ItemCast { get => Item == null ? new() : (TemplateEntity)Item; set => Item = value; }
        public List<TypeEntity<string>>? TemplateCategories { get; set; }

        #endregion

        #region Constructor and destructor

        public ItemTemplate() : base()
        {
            //
        }

        #endregion

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
            TemplateCategories = DataSourceDicsEntity.GetTemplateCategories();
            ItemCast = new();
            ButtonSettings = new();
        }

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    switch (TableAction)
                    {
                        case DbTableAction.New:
                            ItemCast = new();
                            ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
                            ItemCast.IsMarked = false;
                            ItemCast.Title = "NEW TEMPLATE";
                            ItemCast.IdRRef = System.Guid.Empty;
                            ItemCast.CategoryId = "300 dpi";
                            ItemCast.ImageData.SetTemplateValue();
                            break;
                        default:
                            ItemCast = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                                new FieldListEntity(new Dictionary<DbField, object?>{ { DbField.IdentityId, IdentityId } }));
                            break;
                    }
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
