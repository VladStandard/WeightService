using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightServices.Common.Zpl;

namespace ZplCommonLib
{
    public class ZebraState
    {
        public ZebraState()
        {

        }

        public void LoadResponse(string request, string msg)
        {
            Message = msg;
            var noErrors = false;
            var noWarnings = false;

            if (request == ZplPipeClass.ZplHostStatusReturn())
            {
                if (msg.Contains("PRINTER STATUS"))
                {
                    foreach (var item in msg.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                    {
                        if (item.Contains("ERRORS:") && item.Contains("0 00000000 00000000"))
                        {
                            noErrors = true;
                        }
                        if (item.Contains("WARNINGS:") && item.Contains("0 00000000 00000000"))
                        {
                            noWarnings = true;
                        }
                    }
                }
            }

            if (request == ZplPipeClass.ZplGetOdometerUserLabel())
            {
                try
                {
                    OdometerUserLabel = Int32.Parse(msg);
                }
                catch
                {

                }
            }

            if (request == ZplPipeClass.ZplPeelerState())
            {

            }

            IsAlive = noErrors && noWarnings;

        }

        public string Message { get; private set; }
        public bool IsAlive { get; set; } = false;
        public int OdometerUserLabel { get; private set; }
        public string Peeled { get; private set; }
    }

}
