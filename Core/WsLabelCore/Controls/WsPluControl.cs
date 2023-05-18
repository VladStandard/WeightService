// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

public sealed class WsPluControl : UserControl
{
    #region Public and private fields, properties, constructor

    public Button ButtonPlu { get; }
    private Label LabelPluNumber { get; }
    private Label LabelPluType { get; }
    private Label LabelPluCode { get; }
    private Label LabelPluTemplate { get; }
    private Label LabelPluValidate { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="viewPluScale"></param>
    /// <param name="buttonPlu"></param>
    /// <param name="labelPluNumber"></param>
    /// <param name="labelPluType"></param>
    /// <param name="labelPluCode"></param>
    /// <param name="labelPluTemplate"></param>
    /// <param name="labelPluValidate"></param>
    public WsPluControl(WsSqlViewPluScaleModel viewPluScale, 
        Button buttonPlu, Label labelPluNumber, Label labelPluType, Label labelPluCode, Label labelPluTemplate, Label labelPluValidate)
    {
        ButtonPlu = buttonPlu;
        LabelPluNumber = labelPluNumber;
        LabelPluType = labelPluType;
        LabelPluCode = labelPluCode;
        LabelPluTemplate = labelPluTemplate;
        LabelPluValidate = labelPluValidate;
        LabelPluValidate.Tag = LabelPluTemplate.Tag = LabelPluCode.Tag = LabelPluType.Tag = LabelPluNumber.Tag = 
            ButtonPlu.Tag = viewPluScale;
    }

    #endregion

    #region Public and private methods

    public void SetupSizes()
    {
        int shiftLeft = 10;
        int shiftTop = 10;
        int height = ButtonPlu.Height / 4 - shiftLeft * 2;
        int width = ButtonPlu.Width / 2 - shiftLeft * 2;

        LabelPluNumber.Width = width;
        LabelPluNumber.Height = height;
        LabelPluNumber.Left = shiftLeft;
        LabelPluNumber.Top = shiftTop;

        LabelPluType.Height = height;
        LabelPluType.Top = shiftTop;

        if (LabelPluValidate.Text == @"OK")
        {
            LabelPluType.Width = width;
            LabelPluType.Left = ButtonPlu.Width / 2 + shiftLeft;
        }
        else
        {
            LabelPluType.Width = (width + shiftLeft * 2) / 2 - shiftLeft * 2;
            LabelPluType.Left = ButtonPlu.Width / 2 + shiftLeft;

            LabelPluValidate.Width = (width + shiftLeft * 2) / 2 - shiftLeft * 2;
            LabelPluValidate.Height = height;
            LabelPluValidate.Left = ButtonPlu.Width / 2 + ButtonPlu.Width / 4 + shiftLeft;
            LabelPluValidate.Top = shiftTop;
        }

        LabelPluCode.Width = width;
        LabelPluCode.Height = height;
        LabelPluCode.Left = shiftLeft;
        LabelPluCode.Top = ButtonPlu.Height / 4 * 3 + shiftTop;

        LabelPluTemplate.Width = width;
        LabelPluTemplate.Height = height;
        LabelPluTemplate.Left = ButtonPlu.Width / 2 + shiftLeft;
        LabelPluTemplate.Top = ButtonPlu.Height / 4 * 3 + shiftTop;
    }

    #endregion
}
