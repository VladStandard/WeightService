using System.Windows.Forms;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using SystemColors=System.Drawing.SystemColors;

namespace Ws.LabelCore.Controls;

/// <summary>
/// WinForms-контрол одной ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class FormPluControl : UserControl
{
    #region Public and private fields, properties, constructor
    private SqlPluRepository PluRepository { get; } = new();
    
    private FontsSettingsHelper FontsSettings => FontsSettingsHelper.Instance;
    private Label LabelPlu { get; }
    private Label LabelTemplate { get; }
    private Action<object, EventArgs> ActionPluSelect { get; }
    private const short ShiftLeft = 0;
    private const short ShiftTop = 0;
    private short LabelHeight => (short)(Height / 3 - ShiftLeft * 2);
    //private int LabelWidth => Width / 2 - ShiftLeft * 2;

    public FormPluControl(SqlViewPluLineModel viewPluScale, Action<object, EventArgs> actionPluSelect)
    {
        Name = $"WsPluControl{viewPluScale.PluNumber}";
        Font = FontsSettingsHelper.Instance.FontLabelsBlack;
        AutoSize = false;
        Dock = DockStyle.Fill;
        Visible = true;
        Location = new(0, 0);
        BackColor = SystemColors.Control;
        ActionPluSelect = actionPluSelect;
        Tag = viewPluScale;
        Click += PluSelect;
        // Создать метку ПЛУ линии.
        LabelPlu = CreateLabelPlu(viewPluScale);
        LabelPlu.Parent = this;
        LabelPlu.Tag = viewPluScale;
        LabelPlu.Click += PluSelect;
        // Создать метку информации ПЛУ линии.
        LabelTemplate = CreateLabelPluTemplate(viewPluScale);
        LabelTemplate.Parent = this;
        LabelTemplate.Tag = viewPluScale;
        LabelTemplate.Click += PluSelect;
    }

    #endregion

    #region Public and private methods

    private void PluSelect(object sender, EventArgs e) => ActionPluSelect(sender, e);

    /// <summary>
    /// Создать метку ПЛУ линии.
    /// </summary>
    private Label CreateLabelPlu(SqlViewPluLineModel viewPluScale) => new()
    {
        Font = FontsSettings.FontLabelsBlack,
        AutoSize = false,
        Text = $@"{viewPluScale.PluNumber} | {(viewPluScale.PluIsWeight ? LocaleCore.LabelPrint.PluIsWeight : LocaleCore.LabelPrint.PluIsPiece)} | {viewPluScale.PluName}",
        Visible = true,
        TextAlign = ContentAlignment.MiddleCenter,
        FlatStyle = FlatStyle.Flat,
        Dock = DockStyle.None,
        BackColor = Color.Transparent,
        BorderStyle = BorderStyle.FixedSingle,
    };

    /// <summary>
    /// Создать метку информации ПЛУ линии.
    /// </summary>
    private Label CreateLabelPluTemplate(SqlViewPluLineModel viewPluScale)
    {
        List<string> validates = PluRepository.GetEnumerableValidatesViewPluLine(viewPluScale).ToList();
        return new()
        {
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = validates.Any() ? string.Join(" | ", validates) : LocaleCore.LabelPrint.CheckAllPassed,
            Visible = true,
            TextAlign = ContentAlignment.MiddleCenter,
            FlatStyle = FlatStyle.Flat,
            Dock = DockStyle.None,
            BackColor = validates.Any() ? Color.Yellow : Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
        };
    }

    /// <summary>
    /// Настроить размеры контрола.
    /// </summary>
    public void SetupSizes()
    {
        LabelPlu.Width = Width - ShiftLeft * 2;
        LabelPlu.Height = Height - ShiftTop * 2 - LabelHeight;
        LabelPlu.Left = ShiftLeft;
        LabelPlu.Top = ShiftTop;

        LabelTemplate.Width = Width - ShiftLeft * 2;
        LabelTemplate.Height = LabelHeight;
        LabelTemplate.Left = ShiftLeft;
        LabelTemplate.Top = LabelPlu.Top + LabelPlu.Height;
    }

    public override string ToString() =>
        $"{Left},{Top} {Width}x{Height} | {LabelPlu.Left},{LabelPlu.Top} {LabelPlu.Width} x {LabelPlu.Height} | {LabelTemplate.Left},{LabelTemplate.Top} {LabelTemplate.Width} x {LabelTemplate.Height}";

    #endregion
}