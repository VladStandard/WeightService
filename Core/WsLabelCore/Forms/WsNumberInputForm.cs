// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Forms;

public sealed partial class WsNumberInputForm : Form
{
    #region Public and private fields, properties, constructor

    private int _inputValueShadow;
    public int InputValue { get; set; }

    #endregion

    #region Constructor and destructor

    public WsNumberInputForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Private methods

    private void PasswordForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.D0:
            case Keys.NumPad0:
                btnNum_Click(btnNum0, e);
                break;
            case Keys.D1:
            case Keys.NumPad1:
                btnNum_Click(btnNum1, e);
                break;
            case Keys.D2:
            case Keys.NumPad2:
                btnNum_Click(btnNum2, e);
                break;
            case Keys.D3:
            case Keys.NumPad3:
                btnNum_Click(btnNum3, e);
                break;
            case Keys.D4:
            case Keys.NumPad4:
                btnNum_Click(btnNum4, e);
                break;
            case Keys.D5:
            case Keys.NumPad5:
                btnNum_Click(btnNum5, e);
                break;
            case Keys.D6:
            case Keys.NumPad6:
                btnNum_Click(btnNum6, e);
                break;
            case Keys.D7:
            case Keys.NumPad7:
                btnNum_Click(btnNum7, e);
                break;
            case Keys.D8:
            case Keys.NumPad8:
                btnNum_Click(btnNum8, e);
                break;
            case Keys.D9:
            case Keys.NumPad9:
                btnNum_Click(btnNum9, e);
                break;
            case Keys.Enter:
            case Keys.Escape:
                btnClose_Click(sender, e);
                break;
        
        }
    }

    private void btnNum_Click(object sender, EventArgs e)
    {
        string num = (string)(sender as Control)?.Tag;
        _inputValueShadow = int.Parse(_inputValueShadow + num);
        ShowPin(_inputValueShadow);
    }

    private void ShowPin(int value)
    {
        if (value == 0)
        {
            lbPIn.Text = "";
            return;
        }

        string x = value.ToString();
        lbPIn.Text = x;

    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        _inputValueShadow = 0;
        ShowPin(_inputValueShadow);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        InputValue = _inputValueShadow;
        DialogResult = (InputValue != 0) ? DialogResult.OK : DialogResult.Cancel;
        Close();
    }

    private void NumberInputForm_Shown(object sender, EventArgs e)
    {
        _inputValueShadow = 0;
        ShowPin(InputValue);
    }

    #endregion
}