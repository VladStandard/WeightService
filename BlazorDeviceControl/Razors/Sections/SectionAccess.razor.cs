// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionAccess : RazorBase
{
    #region Public and private fields, properties, constructor

    private List<AccessModel> ItemsCast
    {
        get { return Items == null ? new() : Items.Select(x => (AccessModel)x).ToList(); }
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

        Table = new TableSystemModel(ProjectsEnums.TableSystem.Accesses);
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

    public void RowRender(RowRenderEventArgs<AccessModel> args)
    {
        args.Attributes.Add("class", UserSettings.Identity.GetColorAccessRights(args.Data.Rights));
        //RowCounter += 1;
    }

    #endregion
}
