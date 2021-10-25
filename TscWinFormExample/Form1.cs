using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TSCSDK;


namespace TSCLIB_DLL_IN_C_Sharp
{
    public partial class Form1 : Form
    {

        TSCSDK.driver driver = new TSCSDK.driver();

        public Form1()
        {

            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fieldName.Text))
                return;
            if (string.IsNullOrEmpty(fieldCmd.Text))
                return;

            driver.openport(fieldName.Text);
            //string WT1 = "TSC Printers";
            //string B1 = "20080101";
            //byte[] result_utf8 = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,\"ARIAL.TTF\",0,12,12,\"utf8 test Wörter auf Deutsch\"");

            //driver.about();
            //driver.openport("TSC TE210");
            //driver.sendcommand("SIZE 100 mm, 120 mm");
            //driver.sendcommand("SPEED 4");
            //driver.sendcommand("DENSITY 12");
            //driver.sendcommand("DIRECTION 1");
            //driver.sendcommand("SET TEAR ON");
            //driver.sendcommand("CODEPAGE UTF-8");
            driver.clearbuffer();
            //driver.downloadpcx("UL.PCX", "UL.PCX");
            //driver.windowsfont(40, 490, 48, 0, 0, 0, "Arial", "Windows Font Test");
            //driver.windowsfontunicode(40, 550, 48, 0, 0, 0, "Arial", "Windows Unicode Test");
            //driver.sendcommand("PUTPCX 40,40,\"UL.PCX\"");
            //driver.sendcommand(result_utf8);
            driver.sendcommand(fieldCmd.Text);
            //driver.barcode("40", "300", "128", "80", "1", "0", "2", "2", B1);
            //driver.printerfont("40", "440", "0", "0", "15", "15", WT1);
            //driver.printlabel("1", "1");
            driver.closeport();


        }

    }
}