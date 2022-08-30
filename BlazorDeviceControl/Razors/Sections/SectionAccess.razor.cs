// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionAccess : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private List<AccessEntity> ItemsCast
    {
        get { return Items == null ? new() : Items.Select(x => (AccessEntity)x).ToList(); }
        set
        {
            if (!value.Any())
                Items = null;
            else
            {
                Items = new();
                Items.AddRange(value);
            }
        }
    }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Accesses);
        ItemsCast = new();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActions(new()
        {
            () =>
            {
                ItemsCast = AppSettings.DataAccess.Crud.GetListAcesses(IsShowMarked, IsShowOnlyTop);

				ButtonSettings = new(true, false, true, true, true, false, false);
            }
        });
    }

    public void RowRender(RowRenderEventArgs<AccessEntity> args)
    {
        args.Attributes.Add("class", UserSettings.Identity.GetColorAccessRights(args.Data.Rights));
        //RowCounter += 1;
    }

    #endregion
}
