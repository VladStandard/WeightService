// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace WsLabelCore.Controls;

/// <summary>
/// Контрол ПЛУ.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed class WsPluControl : UserControl
{
    #region Public and private fields, properties, constructor

    private WsFontsSettingsHelper FontsSettings => WsFontsSettingsHelper.Instance;
    private Label LabelPlu { get; }
    private Label LabelTemplate { get; }
    private Action<object, EventArgs> ActionPluSelect { get; }

    public WsPluControl(WsSqlViewPluLineModel viewPluScale, Action<object, EventArgs> actionPluSelect)
    {
        Name = $"WsPluControl{viewPluScale.PluNumber}";
        Font = WsFontsSettingsHelper.Instance.FontLabelsBlack;
        AutoSize = false;
        Dock = DockStyle.Fill;
        Visible = true;
        Location = new(0, 0);
        BackColor = System.Drawing.SystemColors.Control;
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
    /// <param name="viewPluScale"></param>
    /// <returns></returns>
    private Label CreateLabelPlu(WsSqlViewPluLineModel viewPluScale) => new()
    {
        Font = FontsSettings.FontLabelsBlack,
        AutoSize = false,
        Text = $@"{viewPluScale.PluNumber} | {(viewPluScale.PluIsWeight ? LocaleCore.Scales.PluIsWeight : LocaleCore.Scales.PluIsPiece)} | {viewPluScale.PluName}",
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
    /// <param name="viewPluScale"></param>
    /// <returns></returns>
    private Label CreateLabelPluTemplate(WsSqlViewPluLineModel viewPluScale)
    {
        List<string> validates = WsSqlPluController.Instance.ValidateViewPluLine(viewPluScale);
        return new()
        {
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = validates.Any() ? string.Join(" | ", validates) : LocaleCore.Scales.CheckAllPassed,
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
        short shiftLeft = 0;
        short shiftTop = 0;
        short height = (short)(Height / 3 - shiftLeft * 2);
        //int width = Width / 2 - shiftLeft * 2;

        LabelPlu.Width = Width - shiftLeft * 2;
        LabelPlu.Height = Height - shiftTop * 2 - height;
        LabelPlu.Left = shiftLeft;
        LabelPlu.Top = shiftTop;

        LabelTemplate.Width = Width - shiftLeft * 2;
        LabelTemplate.Height = height;
        LabelTemplate.Left = shiftLeft;
        LabelTemplate.Top = LabelPlu.Top + LabelPlu.Height;
    }

    public override string ToString() =>
        $"{Left},{Top} {Width}x{Height} | {LabelPlu.Left},{LabelPlu.Top} {LabelPlu.Width} x {LabelPlu.Height} | {LabelTemplate.Left},{LabelTemplate.Top} {LabelTemplate.Width} x {LabelTemplate.Height}";

    #endregion
}