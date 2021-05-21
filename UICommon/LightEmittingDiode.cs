using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UICommon
{
    public class LightEmittingDiode
    {

        private Color _colorOn;
        public Color ColorOn
        {
            get => _colorOn;
            set
            {
                _colorOn = value;
                _colorOff = LighterColor(_colorOn, 75);
            }
        }

        private Color _colorOff;

        private string _description = "Description";
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                LedDraw();
            }
        }

        private Control _owner;

        private bool _state = false;
        public bool State
        {
            get => _state;
            set
            {
                _state = value;
                if (_state)
                {
                    LedDraw();
                }
                else
                {
                    LedDraw();
                }
                StartCheckChangesTask();
            }
        }

        public LightEmittingDiode(Control _owner)
        {
            this._owner = _owner;
            ColorOn = Color.Green;
            _colorOff = this._owner.BackColor;

        }

        private Color DarkerColor(Color color, float correctionfactory = 50f)
        {
            const float hundredpercent = 100f;
            return Color.FromArgb((int)(((float)color.R / hundredpercent) * correctionfactory), (int)(((float)color.G / hundredpercent) * correctionfactory), (int)(((float)color.B / hundredpercent) * correctionfactory));
        }

        private Color LighterColor(Color color, float correctionfactory = 50f)
        {
            correctionfactory = correctionfactory / 100f;
            const float rgb255 = 255f;
            return Color.FromArgb((int)((float)color.R + ((rgb255 - (float)color.R) * correctionfactory)), (int)((float)color.G + ((rgb255 - (float)color.G) * correctionfactory)), (int)((float)color.B + ((rgb255 - (float)color.B) * correctionfactory)));
        }

        public LightEmittingDiode(Control owner, Color _ColorOn)
        {
            ColorOn = _ColorOn;
            State = false;
        }

        #region CheckCanges

        bool taskDone = false;
        Task checkChangesTask = null;

        private int _checkChangesTimeoutMsec = 10000;
        public int CheckChangesTimeoutMsec
        {
            get => _checkChangesTimeoutMsec;
            set
            {
                _checkChangesTimeoutMsec = value;
                if (_checkChanges)
                {
                    StartCheckChangesTask();
                }
            }
        }

        private bool _checkChanges;
        public bool CheckChanges
        {
            get => _checkChanges;
            set
            {
                _checkChanges = value;
                if (_checkChanges)
                {
                    StartCheckChangesTask();
                }
            }
        }

        private async void StartCheckChangesTask()
        {

            // если поток существует 
            // удаляем поток
            if (checkChangesTask != null)
            {
                taskDone = true;
                await checkChangesTask;
                checkChangesTask = null;
            }

            // если не тебуется контроля поступления сигналов
            // ничего делать не будем
            if (!_checkChanges)
            {
                return;
            }

            // создаем его заново
            // если требуется, т.е. CheckChanges==true
            taskDone = false;
            checkChangesTask = Task.Run(SendUpdateTask);
        }

        private async Task SendUpdateTask()
        {
            int iter = 10;
            for (int i = 0; i < iter; i++)
            {
                if (taskDone) return;
                await Task.Delay(CheckChangesTimeoutMsec / iter);
            }
            DrawNoCheck();
        }

        #endregion

        #region Draw elements
        private void DrawNoCheck()
        {
            if (_owner == null) return;
            Graphics graphics = _owner.CreateGraphics();
            var center = GetCenter();
            var radius = GetRadius();
            var alpha = GetAlpha(radius);
            var circle = GetCircle(center, radius);

            // выключаем LED полюбасу 
            SolidBrush penBack = new SolidBrush(_owner.BackColor);
            graphics.FillEllipse(penBack, circle);

            // рисуем крестик полосками "Х"
            Pen p = new Pen(Color.Red);
            int radiusCorrect = 6;
            radius = radius - radiusCorrect;
            p.Width = 9;
            graphics.DrawLine(p, center.X - radius, center.Y + radius, center.X + radius, center.Y - radius);
            graphics.DrawLine(p, center.X - radius, center.Y - radius, center.X + radius, center.Y + radius);

            // окантовка LED
            Pen pRound = new Pen(DarkerColor(this._owner.BackColor, 75));
            pRound.Width = 4;
            graphics.DrawEllipse(pRound, circle);

            DrawTxt(graphics);
            DrawBorder(graphics);
        }

        private void LedDraw()
        {
            if (_owner == null) return;

            Color color = _colorOff;
            if (_state)
            {
                color = ColorOn;
            }

            Graphics graphics = _owner.CreateGraphics();
            var center = GetCenter();
            var radius = GetRadius();
            var alpha = GetAlpha(radius);
            var circle = GetCircle(center, radius);

            // выключаем LED полюбасу 
            SolidBrush penBack = new SolidBrush(_owner.BackColor);
            graphics.FillEllipse(penBack, circle);

            // включаем LED если надо
            Pen p = new Pen(color);
            graphics.DrawEllipse(p, circle);
            SolidBrush sb = new SolidBrush(color);
            graphics.FillEllipse(sb, circle);

            // окантовка LED
            Pen pRound = new Pen(DarkerColor(color, 75));
            pRound.Width = 4;
            graphics.DrawEllipse(pRound, circle);

            DrawTxt(graphics);
            DrawBorder(graphics);

        }

        private void DrawTxt(Graphics graphics)
        {
            // вывести текст
            SolidBrush penBack = new SolidBrush(_owner.BackColor);
            using (Font font1 = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point))
            {
                Rectangle drawRect1 = new Rectangle(3, 3, _owner.Width - 6, 22);

                // Create a StringFormat object with the each line of text, and the block
                // of text centered on the page.
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                // Draw the text and the surrounding rectangle.
                graphics.FillRectangle(penBack, drawRect1);
                graphics.DrawString(Description, font1, Brushes.Black, drawRect1, stringFormat);
            }

        }

        private void DrawBorder(Graphics graphics)
        {
            Pen p0 = new Pen(DarkerColor(_owner.BackColor, 65));
            p0.Width = 2;
            Rectangle drawRect2 = new Rectangle(3, 3, _owner.Width - 6, _owner.Height - 6);
            graphics.DrawRectangle(p0, drawRect2);

        }

        private Point GetCenter()
        {
            return new Point((int)(_owner.Width * 0.5), (int)(_owner.Height * 0.5 + 9));
        }
        private float GetRadius()
        {
            return (float)(Math.Min(_owner.Width, _owner.Height) * 0.25);
        }
        private float GetAlpha(float radius)
        {
            const float cutOutLen = 0;
            return (float)(Math.Asin(1f * (radius - cutOutLen) / radius) / Math.PI * 180);
        }
        private RectangleF GetCircle(Point center, float radius)
        {

            return new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }

        #endregion

    }
}

