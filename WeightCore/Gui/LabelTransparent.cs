// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
// https://stackoverflow.com/questions/605920/reasons-for-why-a-winforms-label-does-not-want-to-be-transparent

namespace WeightCore.Gui
{

    public class LabelTransparent : Label
    {
        public LabelTransparent()
        {
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                parms.ExStyle |= 0x20;  // Turn on WS_EX_TRANSPARENT
                return parms;
            }
        }
    }
}