// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WsLabelCore.Gui;

public partial class PasswordForm : Form
{
    #region Public and private fields, properties, constructor

    private ushort UnlockCode => (ushort)(DateTime.Now.Hour * 100 + DateTime.Now.Minute);
    private ushort UserCode { get; set; }
    private UserSessionHelper UserSession => UserSessionHelper.Instance;

    #endregion

    #region Constructor and destructor

    public PasswordForm()
    {
        InitializeComponent();
        TopMost = !UserSession.Debug.IsDevelop;
    }

    #endregion

    #region Private methods

    private void PasswordForm_Load(object sender, EventArgs e)
    {
        ShowPin();
    }

    private void PasswordForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        DialogResult = Equals(UserCode, UnlockCode) ? DialogResult.OK : DialogResult.Cancel;
    }

    private void PasswordForm_KeyUp(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.NumPad0:
            case Keys.D0:
                BtnNum_Click(buttonNum0, e);
                break;
            case Keys.NumPad1:
            case Keys.D1:
                BtnNum_Click(buttonNum1, e);
                break;
            case Keys.NumPad2:
            case Keys.D2:
                BtnNum_Click(buttonNum2, e);
                break;
            case Keys.NumPad3:
            case Keys.D3:
                BtnNum_Click(buttonNum3, e);
                break;
            case Keys.NumPad4:
            case Keys.D4:
                BtnNum_Click(buttonNum4, e);
                break;
            case Keys.NumPad5:
            case Keys.D5:
                BtnNum_Click(buttonNum5, e);
                break;
            case Keys.NumPad6:
            case Keys.D6:
                BtnNum_Click(buttonNum6, e);
                break;
            case Keys.NumPad7:
            case Keys.D7:
                BtnNum_Click(buttonNum7, e);
                break;
            case Keys.NumPad8:
            case Keys.D8:
                BtnNum_Click(buttonNum8, e);
                break;
            case Keys.NumPad9:
            case Keys.D9:
                BtnNum_Click(buttonNum9, e);
                break;
            case Keys.Escape:
            case Keys.Enter:
                BtnClose_Click(sender, e);
                break;
        }
    }

    private void BtnNum_Click(object sender, EventArgs e)
    {
        string numStr = (string)(sender as Control)?.Tag;
        if (int.TryParse(numStr, out int num))
        {
            UserCode = ushort.Parse($"{UserCode}{num}");
            if (Equals(UserCode, UnlockCode))
            {
                Close();
                return;
            }
            if (UserCode.ToString().Length > 3)
                UserCode = 0;
            ShowPin();
        }
    }

    private void ShowPin()
    {
        if (UserCode == 0)
        {
            fieldValue.Text = "....";
            return;
        }
        string x = UserCode.ToString();
        string y = Regex.Replace(x, "[0-9]", "*");
        fieldValue.Text = y;
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        UserCode = 0;
        ShowPin();
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion
}