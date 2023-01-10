//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.Windows.Forms;

//namespace ScalesUI.Forms;

//public partial class OrderDetailForm : Form
//{
//    #region Public and private fields and properties

//    //private UserSessionHelper UserSession => UserSessionHelper.Instance;

//    #endregion

//    #region Constructor and destructor

//    public OrderDetailForm()
//    {
//        InitializeComponent();
//    }

//    #endregion

//    #region Public and private methods

//    private void OrderDetailForm_Load(object sender, EventArgs e)
//    {
//        //try
//        //{
//        //    TopMost = !UserSession.Debug.IsDebug;
//        //    listBox1.Items.Clear();
//        //    foreach (string prop in UserSession.Order.ToString().Split('\n'))
//        //    {
//        //        listBox1.Items.Add(prop);
//        //    }
//        //}
//        //catch (Exception ex)
//        //{
//        //    GuiUtils.WpfForm.CatchException(this, ex);
//        //}
//    }

//    private void BtnCancel_Click(object sender, EventArgs e)
//    {
//        //try
//        //{
//        //    DialogResult = DialogResult.Cancel;
//        //    Close();
//        //}
//        //catch (Exception ex)
//        //{
//        //    GuiUtils.WpfForm.CatchException(this, ex);
//        //}
//    }

//    private void BtnOrderReset_Click(object sender, EventArgs e)
//    {
//        //try
//        //{
//        //    UserSession.Order?.SetStatus(OrderStatus.Paused);
//        //    UserSession.Order = null;
//        //    DialogResult = DialogResult.Retry;
//        //    Close();
//        //}
//        //catch (Exception ex)
//        //{
//        //    GuiUtils.WpfForm.CatchException(this, ex);
//        //}
//    }

//    private void BtnOrderComplitrd_Click(object sender, EventArgs e)
//    {
//        //try
//        //{
//        //    UserSession.Order?.SetStatus(OrderStatus.Performed);
//        //    UserSession.Order = null;
//        //    DialogResult = DialogResult.Retry;
//        //    Close();
//        //}
//        //catch (Exception ex)
//        //{
//        //    GuiUtils.WpfForm.CatchException(this, ex);
//        //}
//    }

//    private void BtnOk_Click(object sender, EventArgs e)
//    {
//        //try
//        //{
//        //    UserSession.Order?.SetStatus(OrderStatus.InProgress);
//        //    UserSession.Order = null;
//        //    DialogResult = DialogResult.OK;
//        //    Close();
//        //}
//        //catch (Exception ex)
//        //{
//        //    GuiUtils.WpfForm.CatchException(this, ex);
//        //}
//    }

//    #endregion
//}
