
using EntitiesLib;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TapangaMaha.Common;
using TapangaMaha.Forms;
using WeightServices.Common;
using WeightServices.Common.Zpl;
using WeightServices.Entities;
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace TapangaMaha
{
    public partial class MainForm : Form
    {
        #region Private fields and properties

        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Thread _datetimeThread;
        private readonly SessionState _sessionState = SessionState.Instance;
        private int _currentPage;
        private readonly int _rowCount = 5;
        private readonly int _columnCount = 4;
        private readonly int _pageSize = 20;

        #endregion

        #region Constructor and destructor

        public MainForm()
        {
            InitializeComponent();
            GridCustomizatorClass.GridCustomizator(this.PluListGrid, this._columnCount, this._rowCount);

            _datetimeThread = new Thread(t =>
            {
                while (true)
                {
                    SetControlText(lbCurrentTime, DateTime.Now.ToString(@"dd.MM.yyyy HH:mm:ss"));
                    Thread.Sleep(250);
                }
            }
            )
            { IsBackground = true };
            _datetimeThread.Start();
        }

        #endregion

        #region Private methods

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {

            if (_currentPage > 0) _currentPage--; else _currentPage = 0;

            PluEntity[] pluEntities = _sessionState.PluList.Skip(_currentPage * _pageSize).Take(_pageSize).ToArray();
            Control[,] controls = ControlBuilder(pluEntities, this._columnCount, this._rowCount);
            GridCustomizatorClass.PageBuilder(this.PluListGrid, controls);

            lbCurrentPage.Text = $"Cтр. {_currentPage}";

            _log.Info(lbCurrentPage.Text);

        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            int countPage = (_sessionState.PluList.Count() / _pageSize);

            if (_currentPage < countPage) _currentPage++;
            else _currentPage = countPage;

            PluEntity[] pluEntities = _sessionState.PluList.Skip(_currentPage * _pageSize).Take(_pageSize).ToArray();
            Control[,] controls = ControlBuilder(pluEntities, this._columnCount, this._rowCount);
            GridCustomizatorClass.PageBuilder(this.PluListGrid, controls);

            lbCurrentPage.Text = $"Cтр. {_currentPage}";

            _log.Info(lbCurrentPage.Text);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            if (_sessionState.PluList.Count < _pageSize)
            {
                btnPrevPage.Visible = false;
                btnNextPage.Visible = false;
            }

            PluEntity[] pluEntities = _sessionState.PluList.Skip(_currentPage * _pageSize).Take(_pageSize).ToArray();
            Control[,] controls = ControlBuilder(pluEntities, this._columnCount, this._rowCount);
            GridCustomizatorClass.PageBuilder(this.PluListGrid, controls);

            lbCurrentPage.Text = $"Cтр. {_currentPage}";

        }

        private void SetControlText(Control control, string text)
        {
            if (IsHandleCreated)
            {
                control.BeginInvoke(
                new MethodInvoker(() =>
                {
                    control.Text = text;
                })
            );
            }
        }

        private Control[,] ControlBuilder(PluEntity[] pluEntities, int _X, int _Y)
        {
            Control[,] Controls = new Control[_X, _Y];
            for (int j = 0, k = 0; j < _Y; ++j)
                for (int i = 0; i < _X; ++i)
                {
                    if (k >= pluEntities.Length) break;
                    //Control btn = NewButton(pluEntities[k], _currentPage, k);
                    Control btn = NewControl(pluEntities[k], _currentPage, k);
                    Controls[i, j] = btn;
                    k++;

                }
            return Controls;
        }

        private Control NewControl(PluEntity plu, int pageNumber, int i)
        {
            var button = new Button()
            {
                Font = new Font("Arial", 18, FontStyle.Bold),
                Text = plu.GoodsName,
                Name = "btn_" + i,
                TabIndex = i + pageNumber * _pageSize,
                Dock = DockStyle.Fill,
                Size = new Size(150, 30),
                Visible = true,
                Parent = PluListGrid,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                UseVisualStyleBackColor = true,
                BackColor = SystemColors.Control
            };
            button.Click += NewButton_Click;

            var mashtabW = 0.1M;
            var mashtabH = 0.05M;
            var label = new Label()
            {
                Font = new Font("Arial", 23, FontStyle.Bold),
                Text = plu.PLU.ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = button,
                Size = new Size((int)(PluListGrid.Height * mashtabW), (int)(PluListGrid.Height * mashtabH)),
                Dock = DockStyle.None,
                Left = 3,
                Top = 3,// button.Height - (int)(PluListGrid.Height * mashtabH) - 3,
                //BackColor = Color.YellowGreen,
                BackColor = Color.FromArgb(255, 192, 255, 192),
                BorderStyle = BorderStyle.FixedSingle,
            };
            label.MouseClick += (sender, args) =>
            {
                NewButton_Click(sender, null);
            };

            return button;
        }


        private void NewButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                _sessionState.CurrentPLU = null;
                if (_sessionState.PluList?.Count >= button.TabIndex)
                {
                    PluEntity _plu = _sessionState.PluList[button.TabIndex];
                    _plu.LoadTemplate();
                    _sessionState.CurrentPLU = _plu;

                    using (var printForm = new PrintForm())
                    {

                        _sessionState.Kneading = 1;
                        _sessionState.ProductDate = DateTime.Now;
                        _sessionState.PalletSize = SessionState.PalletSizeMinValue;

                        if (printForm.ShowDialog() == DialogResult.OK)
                        {
                            _log.Info($"{_sessionState.CurrentPLU.ToString()} - {_sessionState.PalletSize} шт.");

                            ProductSeriesEntity productSeries = new ProductSeriesEntity(_sessionState.CurrentScale);
                            productSeries.New();

                            for (int j = 0; j < _sessionState.PalletSize; j++)
                            {

                                var weighingFact = WeighingFactEntity.New(
                                    _sessionState.CurrentScale,
                                    _sessionState.CurrentPLU,
                                    _sessionState.ProductDate,
                                    _sessionState.Kneading,
                                    _sessionState.CurrentScale.ScaleFactor,
                                    _sessionState.CurrentPLU.GoodsFixWeight,
                                    _sessionState.CurrentPLU.GoodsTareWeight

                                );

                                weighingFact.Save();
                                TemplateEntity template = weighingFact.PLU.Template;
                                _sessionState.CurrentWeighingFact = weighingFact;

                                if (weighingFact != null && template != null)
                                {
                                    try
                                    {
                                        string xmlInput = weighingFact.SerializeObject();

                                        // заменил один вызов на другой
                                        // хочу сохранять полученный  ZPL в таблицу Labels
                                        //_sessionState.zebraDeviceEntity.SendAsync(template.XslContent, xmlInput);
                                        //
                                        string zplContent = ZplPipeClass.XsltTransformationPipe(template.XslContent, xmlInput);
                                        _sessionState.zebraDeviceEntity.SendAsync(zplContent);
                                        ZplLabel zplLabel = new ZplLabel();
                                        zplLabel.WeighingFactId = weighingFact.Id;
                                        zplLabel.Content = zplContent;
                                        zplLabel.Save();

                                        //string zplContent = ZplPipeClass.XsltTransformationPipe(template.XslContent, xmlInput);
                                        ////ZplPipeClass.FakePipe(zplContent);
                                        //ZplPipeClass.ZplCommandPipeByIP(sessionState.CurrentScale.ZebraIP, sessionState.CurrentScale.ZebraPort, zplContent);
                                    }
                                    catch (Exception ex)
                                    {
                                        _log.Error($"Ошибка создания этикетки. {ex.Message}.");
                                    }
                                }
                            }

                            productSeries.Load();
                            productSeries.Plu = _sessionState.CurrentPLU;
                            productSeries.LoadTemplate(_sessionState.CurrentScale.TemplateIdSeries);
                            if (productSeries.Template != null)
                            {
                                try
                                {
                                    string xmlInput = productSeries.SerializeObject();
                                    _sessionState.zebraDeviceEntity.SendAsync(productSeries.Template.XslContent, xmlInput);

                                    //string zplContent = ZplPipeClass.XsltTransformationPipe(productSeries.Template.XslContent, xmlInput);
                                    //ZplPipeClass.ZplCommandPipeByIP(sessionState.CurrentScale.ZebraIP, sessionState.CurrentScale.ZebraPort, zplContent);
                                }
                                catch (Exception ex)
                                {
                                    _log.Error($"Ошибка создания этикетки. {ex.Message}.");
                                }
                            }
                        }
                    }
                }

                //DialogResult result = MessageBox.Show("Hi! You ready CLOSE FORM?!", "TNT", MessageBoxButtons.YesNo);
                //if (result == System.Windows.Forms.DialogResult.Yes)
                //{
                //    this.Close();
                //}

            }

        }


        private Button NewButton(PluEntity plu, int pageNumber, int i)
        {

            Button btn = new Button
            {
                Font = new Font("Arial", 24, FontStyle.Bold),
                Text = plu.ToString(),
                Name = $"btn_{i}",
                TabIndex = i + pageNumber * this._pageSize,
                Dock = DockStyle.Fill,
                Size = new Size(150, 30),
                Visible = true,
                //Parent = panel,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                UseVisualStyleBackColor = true
            };

            btn.Click +=  NewButton_Click;
            return btn;

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (PasswordForm pinForm = new PasswordForm())
            {
                if (pinForm.ShowDialog() == DialogResult.OK)
                {
                    //pinForm.Close();
                    using (var settingsForm = new SettingsForm())
                    {
                        if (settingsForm.ShowDialog() == DialogResult.OK)
                        {
                        }
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _datetimeThread.Abort();
        }

        private void lbCurrentTime_Click(object sender, EventArgs e)
        {
            Form messageBox = ServiceMessagesWindow.BuildServiceMessagesWindow(this);
        }

        #endregion

        private void PluListGrid_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
