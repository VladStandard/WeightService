// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeight.Gui;

public partial class NumberInputForm : Form
{
    #region Public and private fields, properties, constructor

    private int _InputValueShadow;
    public int InputValue { get; set; }

    #endregion

    #region Constructor and destructor

    public NumberInputForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Private methods

    private void PasswordForm_KeyUp(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.NumPad0:
            case Keys.D0:
                btnNum_Click(btnNum0, e);
                break;
            case Keys.NumPad1:
            case Keys.D1:
                btnNum_Click(btnNum1, e);
                break;
            case Keys.NumPad2:
            case Keys.D2:
                btnNum_Click(btnNum2, e);
                break;
            case Keys.NumPad3:
            case Keys.D3:
                btnNum_Click(btnNum3, e);
                break;
            case Keys.NumPad4:
            case Keys.D4:
                btnNum_Click(btnNum4, e);
                break;
            case Keys.NumPad5:
            case Keys.D5:
                btnNum_Click(btnNum5, e);
                break;
            case Keys.NumPad6:
            case Keys.D6:
                btnNum_Click(btnNum6, e);
                break;
            case Keys.NumPad7:
            case Keys.D7:
                btnNum_Click(btnNum7, e);
                break;
            case Keys.NumPad8:
            case Keys.D8:
                btnNum_Click(btnNum8, e);
                break;
            case Keys.NumPad9:
            case Keys.D9:
                btnNum_Click(btnNum9, e);
                break;
            case Keys.Escape:
            case Keys.Enter:
                btnClose_Click(sender, e);
                break;
        }
    }

    private void btnNum_Click(object sender, EventArgs e)
    {
        string num = (string)(sender as Control)?.Tag;
        _InputValueShadow = int.Parse(_InputValueShadow + num);
        ShowPin(_InputValueShadow);
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
        _InputValueShadow = 0;
        ShowPin(_InputValueShadow);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        InputValue = _InputValueShadow;
        DialogResult = (InputValue != 0) ? DialogResult.OK : DialogResult.Cancel;
        Close();
    }

    private void NumberInputForm_Shown(object sender, EventArgs e)
    {
        _InputValueShadow = 0;
        ShowPin(InputValue);
    }

    #endregion
}
