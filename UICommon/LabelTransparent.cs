using System.Windows.Forms;
// https://stackoverflow.com/questions/605920/reasons-for-why-a-winforms-label-does-not-want-to-be-transparent

namespace UICommon
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
                var parms = base.CreateParams;
                parms.ExStyle |= 0x20;  // Turn on WS_EX_TRANSPARENT
                return parms;
            }
        }
    }
}