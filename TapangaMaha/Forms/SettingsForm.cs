using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using WeightCore.DAL;
using WeightCore.Zpl;
using TapangaMaha.Common;

namespace TapangaMaha.Forms
{
    public partial class SettingsForm : Form
    {

        private readonly SessionState _ws = SessionState.Instance;


        public SettingsForm()
        {
            InitializeComponent();
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                Thread.Sleep(10);
                Application.DoEvents();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка закрытия!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void btnSaveOption_Click(object sender, EventArgs e)
        {
            try
            {

                _ws.CurrentScale.ZebraPrinter.Ip = fieldZebraTcpAddress.Text;
                _ws.CurrentScale.ZebraPrinter.Port = short.Parse(fieldZebraTcpPort.Text);
                _ws.CurrentScale.Description = fieldDescription.Text;
                _ws.CurrentScale.ScaleFactor = short.Parse(fieldScaleFactor.Text);
                _ws.CurrentScale.Save();

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка сохранения настроек!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }

            // Закрыть форму.
            btnClose_Click(sender, e);
        }


        private void btnUploadResources_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ws.CurrentPLU == null)
                {
                    const string message = "Не определен PLU";
                    const string caption = "Операция недоступна!";
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // в выгрузке шрифтов что-то идет не так
                // очистим шрифты
                //ZplPipeUtils.ZplCommandPipeByIP(
                //    _ws.CurrentScale.ZebraIP, 
                //    _ws.CurrentScale.ZebraPort, 
                //    ZplPipeUtils.ZplFontsClear());


                //foreach (KeyValuePair<string, string> font in _ws.CurrentPLU.Template.Fonts)
                //{
                //    string zpl = ZplPipeUtils.ZplFontDownloadCommand(font.Key, font.Value);
                //    ZplPipeUtils.ZplCommandPipeByIP(
                //        _ws.CurrentScale.ZebraIP,
                //        _ws.CurrentScale.ZebraPort,
                //         zpl
                //    );
                //    Thread.Sleep(7000);
                //}

                // очистим логотипы
                ZplPipeUtils.ZplCommandPipeByIp(
                    _ws.CurrentScale.ZebraPrinter.Ip,
                    _ws.CurrentScale.ZebraPrinter.Port,
                    ZplPipeUtils.ZplLogoClear());

                foreach (KeyValuePair<string, string> img in _ws.CurrentPLU.Template.Logo)
                {
                    string zpl = ZplPipeUtils.ZplLogoDownloadCommand(img.Key, img.Value);
                    ZplPipeUtils.ZplCommandPipeByIp(
                        _ws.CurrentScale.ZebraPrinter.Ip,
                        _ws.CurrentScale.ZebraPrinter.Port,
                        zpl
                    );
                    Thread.Sleep(3000);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка выгрузки ресурсов текущего шаблона!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            try
            {

                UseWaitCursor = true;
                Thread.Sleep(10);
                Application.DoEvents();

                ZplPipeUtils.ZplCommandPipeByIp(
                    _ws.CurrentScale.ZebraPrinter.Ip,
                    _ws.CurrentScale.ZebraPrinter.Port,
                    ZplPipeUtils.ZplCalibration());
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка калибровки!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        #region Commented

        //https://developer.zebra.com/thread/33293
        //string zplImageData = string.Empty;
        //byte[] binaryData = System.IO.File.ReadAllBytes(@Server.MapPath("Temp/abcd.png"));
        //foreach (Byte b in binaryData)

        //        {
        //            string hexRep = String.Format("{0:X}", b);
        //            if (hexRep.Length == 1)
        //                hexRep = "0" + hexRep;
        //            zplImageData += hexRep;
        //        }

        //string s = @"^XA^MNY~DYE:COMPOUND.PNG,P,P," + binaryData.Length + ",," + zplImageData + @"^XZ^FO28,260^IME:COMPOUND.PNG^FS^XZ";
        //RawPrinterHelper.SendStringToPrinter(@"\\info-0004\ZDesigner 105SL 203DPI Zebra", s);

        #endregion

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            _ws.CurrentScale.Load();

            if (_ws?.CurrentScale != null)
            {
                fieldZebraTcpAddress.Text = _ws.CurrentScale.ZebraPrinter.Ip;
                fieldZebraTcpPort.Text = _ws.CurrentScale.ZebraPrinter.Port.ToString();
                fieldDescription.Text = _ws.CurrentScale.Description;
                fieldScaleFactor.Text = _ws.CurrentScale.ScaleFactor.ToString();
            }


            string xmlInput = String.Empty;
            if (_ws.PluList.Count > 0)
            {
                Random rnd = new Random();
                int i = rnd.Next(0, _ws.PluList.Count);
                _ws.CurrentPLU = _ws.PluList[i];
                _ws.CurrentPLU.LoadTemplate();

                var weighingFact = WeighingFactEntity.New(
                    _ws.CurrentScale,
                    _ws.CurrentPLU,
                    _ws.ProductDate,
                    _ws.Kneading,
                    _ws.CurrentScale.ScaleFactor,
                    _ws.CurrentPLU.GoodsFixWeight,
                    _ws.CurrentPLU.GoodsTareWeight
                    );


                xmlInput = weighingFact.SerializeObject();
            }
            fieldCurrentWeightFact.Text = xmlInput;

        }

        private void btnPrintList_Click(object sender, EventArgs e)
        {
            try
            {

                UseWaitCursor = true;
                Thread.Sleep(10);
                Application.DoEvents();

                ZplPipeUtils.ZplCommandPipeByIp(
                    _ws.CurrentScale.ZebraPrinter.Ip,
                    _ws.CurrentScale.ZebraPrinter.Port,
                    ZplPipeUtils.ZplPrintDirectory());

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка калибровки!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                UseWaitCursor = false;
            }

        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(fieldCurrentWeightFact.Text);
        }
    }
}
