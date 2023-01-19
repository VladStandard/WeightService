// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace WeightCore.Gui;

public class ControlPluModel
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
    /// <param name="buttonPlu"></param>
    /// <param name="labelPluNumber"></param>
    /// <param name="labelPluType"></param>
    /// <param name="labelPluCode"></param>
    /// <param name="labelPluTemplate"></param>
    /// <param name="labelPluValidate"></param>
    public ControlPluModel(Button buttonPlu, Label labelPluNumber, Label labelPluType, Label labelPluCode, Label labelPluTemplate, Label labelPluValidate)
    {
        ButtonPlu = buttonPlu;
        LabelPluNumber = labelPluNumber;
        LabelPluType = labelPluType;
        LabelPluCode = labelPluCode;
        LabelPluTemplate = labelPluTemplate;
        LabelPluValidate = labelPluValidate;
    }

    #endregion

    #region Public and private methods

    public void SetupSizes()
    {
        int height = ButtonPlu.Height / 4 - 4;
        int width = ButtonPlu.Width / 2 - 4;

        LabelPluNumber.Width = width;
        LabelPluNumber.Height = height;
        LabelPluNumber.Left = 2;
        LabelPluNumber.Top = 2;

        LabelPluType.Height = height;
        LabelPluType.Top = 2;

        if (LabelPluValidate.Text == @"OK")
        {
            LabelPluType.Width = width;
            LabelPluType.Left = ButtonPlu.Width / 2 + 2;

        } 
        else
        {
            LabelPluType.Width = (width + 4) / 2 - 4;
            LabelPluType.Left = ButtonPlu.Width / 2 + 2;

            LabelPluValidate.Width = (width + 4) / 2 - 4;
            LabelPluValidate.Height = height;
            LabelPluValidate.Left = ButtonPlu.Width / 2 + ButtonPlu.Width / 4 + 2;
            LabelPluValidate.Top = 2;
        }



        LabelPluCode.Width = width;
        LabelPluCode.Height = height;
        LabelPluCode.Left = 2;
        LabelPluCode.Top = ButtonPlu.Height / 4 * 3 + 2;

        LabelPluTemplate.Width = width;
        LabelPluTemplate.Height = height;
        LabelPluTemplate.Left = ButtonPlu.Width / 2 + 2;
        LabelPluTemplate.Top = ButtonPlu.Height / 4 * 3 + 2;
    }

    #endregion
}
