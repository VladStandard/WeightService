using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using TapangaMaha.Common;

namespace TapangaMaha.Forms
{

    enum Direction {forward, back }

    public partial class PrintForm : Form
    {

        private Thread counterThread;
        private readonly SessionState sessionState = SessionState.Instance;

        public PrintForm()
        {
            InitializeComponent();


        }

        private void ShowProductDate()
        {
            lbProdDate.Text = sessionState.ProductDate.ToString("dd.MM.yyyy");
        }

        private void ShowKneading()
        {
            SetControlText(lKneadingValue, sessionState.Kneading.ToString());
            //lKneadingValue.Text = sessionState.Kneading.ToString();
        }

        private void ShowPalletSize()
        {
            lbPalletSize.Text = sessionState.PalletSize.ToString();
        }



        private void RotateKneading(Direction direction)
        {
            if (direction == Direction.back)
            {
                sessionState.Kneading--;
                if (sessionState.Kneading < SessionState.KneadingMinValue)
                    sessionState.Kneading = SessionState.KneadingMinValue;

            }
            if (direction == Direction.forward)
            {
                sessionState.Kneading++;
                if (sessionState.Kneading > SessionState.KneadingMaxValue)
                    sessionState.Kneading = SessionState.KneadingMaxValue;

            }
        }

        private void RotatePalletSize(Direction direction)
        {
            if (direction == Direction.back)
            {
                sessionState.PalletSize--;
                if (sessionState.PalletSize < SessionState.PalletSizeMinValue)
                    sessionState.PalletSize = SessionState.PalletSizeMinValue;

            }
            if (direction == Direction.forward)
            {
                sessionState.PalletSize++;
                if (sessionState.PalletSize > SessionState.PalletSizeMaxValue)
                    sessionState.PalletSize = SessionState.PalletSizeMaxValue;
            }
        }

        private void RotateProductDate(Direction direction)
        {
            if (direction == Direction.back)
            {
                sessionState.ProductDate = sessionState.ProductDate.AddDays(-1);
                if (sessionState.ProductDate < SessionState.ProductDateMinValue)
                    sessionState.ProductDate = SessionState.ProductDateMinValue;

            }
            if (direction == Direction.forward)
            {
                sessionState.ProductDate = sessionState.ProductDate.AddDays(1);
                if (sessionState.ProductDate > SessionState.ProductDateMaxValue)
                    sessionState.ProductDate = SessionState.ProductDateMaxValue;

            }
        }
        private void btnKneadingPrev_Click(object sender, EventArgs e)
        {
            RotateKneading(Direction.back);
            ShowKneading();

        }

        private void btnKneadingNext_Click(object sender, EventArgs e)
        {
            RotateKneading(Direction.forward);
            ShowKneading();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RotateProductDate(Direction.back);
            ShowProductDate();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RotateProductDate(Direction.forward);
            ShowProductDate();

        }

        private void btnPalletSizePrev_Click(object sender, EventArgs e)
        {
            RotatePalletSize(Direction.back);
            ShowPalletSize();

        }

        private void btnPalletSizeNext_Click(object sender, EventArgs e)
        {
            RotatePalletSize(Direction.forward);
            ShowPalletSize();
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            ShowKneading(); 
            ShowProductDate();
            ShowPalletSize();
            lbPluName.Text = sessionState.CurrentPLU.ToString();
        }


        private void btnPalletSize10_Click(object sender, EventArgs e)
        {
            int n = sessionState.PalletSize == 1 ? 9 : 10;
            
            for (int i = 0; i < n; i++) { 
                RotatePalletSize(Direction.forward);
                ShowPalletSize();
            }

        }




        private void button3_KeyDown(object sender, KeyEventArgs e)
        {
            counterThread = new Thread( t =>
            {
                Random rnd = new Random();
                while (true)
                {
                    //RotateKneading(Direction.forward);
                    SetControlText(lKneadingValue,  (rnd.Next(1,100)).ToString());
                    Thread.Sleep(100);
                }
            })
            { IsBackground = true };
            counterThread.Start();
        }

        private void button3_KeyUp(object sender, KeyEventArgs e)
        {
            counterThread.Abort();
        }

        void SetControlText(Control control, string text)
        {
            control.BeginInvoke(
                new MethodInvoker(() =>
                {
                    control.Text = text;
                })
            );
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
