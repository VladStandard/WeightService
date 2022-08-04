// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace WeightCore.Gui;

public class ControlPluEntity
{
    #region Public and private fields and properties

    public Button ButtonPlu { get; }
    private Label LabelPluNumber { get; }
    private Label LabelPluType { get; }
    private Label LabelPluCode { get; }
    private Label LabelPluDescription { get; }

    #endregion

    #region Constructor and destructor

    public ControlPluEntity(Button buttonPlu, Label labelPluNumber, Label labelPluType, Label labelPluCode, Label labelPluDescription)
    {
        ButtonPlu = buttonPlu;
        LabelPluNumber = labelPluNumber;
        LabelPluType = labelPluType;
        LabelPluCode = labelPluCode;
        LabelPluDescription = labelPluDescription;
    }

    #endregion

    #region Public and private methods

    public void SetupSizes()
    {
        LabelPluNumber.Width = ButtonPlu.Width / 2 - 4;
        LabelPluNumber.Height = ButtonPlu.Height / 4 - 4;
        LabelPluNumber.Left = 2;
        LabelPluNumber.Top = 2;
        
        LabelPluType.Width = ButtonPlu.Width / 2 - 4;
        LabelPluType.Height = ButtonPlu.Height / 4 - 4;
        LabelPluType.Left = ButtonPlu.Width / 2 + 2;
        LabelPluType.Top = 2;
        
        LabelPluCode.Width = ButtonPlu.Width / 2 - 4;
        LabelPluCode.Height = ButtonPlu.Height / 4 - 4;
        LabelPluCode.Left = 2;
        LabelPluCode.Top = ButtonPlu.Height / 4 * 3 + 2;

        LabelPluDescription.Width = ButtonPlu.Width / 2 - 4;
        LabelPluDescription.Height = ButtonPlu.Height / 4 - 4;
        LabelPluDescription.Left = ButtonPlu.Width / 2 + 2;
        LabelPluDescription.Top = ButtonPlu.Height / 4 * 3 + 2;
    }

    #endregion
}
