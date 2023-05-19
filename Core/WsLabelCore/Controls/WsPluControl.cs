// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

[DebuggerDisplay("{ToString()}")]
public sealed class WsPluControl : UserControl
{
    #region Public and private fields, properties, constructor

    private Label LabelPlu { get; }
    private Label LabelTemplateCode { get; }
    private Action<object, EventArgs> ActionPluSelect { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="viewPluScale"></param>
    /// <param name="labelPlu"></param>
    /// <param name="labelTemplateCode"></param>
    /// <param name="actionPluSelect"></param>
    public WsPluControl(WsSqlViewPluScaleModel viewPluScale, Label labelPlu, Label labelTemplateCode,
        Action<object, EventArgs> actionPluSelect)
    {
        Font = WsFontsSettingsHelper.Instance.FontLabelsBlack;
        AutoSize = false;
        Dock = DockStyle.Fill;
        Visible = true;
        Location = new(0, 0);
        BackColor = System.Drawing.SystemColors.Control;

        ActionPluSelect = actionPluSelect;
        Tag = viewPluScale;
        Click += PluSelect;

        LabelPlu = labelPlu;
        LabelPlu.Parent = this;
        LabelPlu.Tag = viewPluScale;
        LabelPlu.Click += PluSelect;

        LabelTemplateCode = labelTemplateCode;
        LabelTemplateCode.Parent = this;
        LabelTemplateCode.Tag = viewPluScale;
        LabelTemplateCode.Click += PluSelect;

        void PluSelect(object sender, EventArgs e) => ActionPluSelect(sender, e);
    }


    #endregion

    #region Public and private methods

    /// <summary>
    /// Настроить размеры контрола.
    /// </summary>
    public void SetupSizes()
    {
        int shiftLeft = 4;
        int shiftTop = 4;
        int height = Height / 3 - shiftLeft * 2;
        int width = Width / 2 - shiftLeft * 2;

        LabelPlu.Width = Width - shiftLeft * 2;
        LabelPlu.Height = Height - shiftTop * 2 - height;
        LabelPlu.Left = shiftLeft;
        LabelPlu.Top = shiftTop;

        LabelTemplateCode.Width = Width - shiftLeft * 2;
        LabelTemplateCode.Height = height;
        LabelTemplateCode.Left = shiftLeft;
        LabelTemplateCode.Top = LabelPlu.Top + LabelPlu.Height;
    }

    public override string ToString() =>
        $"{Left},{Top} {Width}x{Height} | {LabelPlu.Left},{LabelPlu.Top} {LabelPlu.Width} x {LabelPlu.Height} | {LabelTemplateCode.Left},{LabelTemplateCode.Top} {LabelTemplateCode.Width} x {LabelTemplateCode.Height}";

    #endregion
}