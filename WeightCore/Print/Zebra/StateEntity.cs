﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using WeightCore.Zpl;

namespace WeightCore.Print.Zebra
{
    public class StateEntity
    {
        public string Message { get; private set; }
        public bool IsAlive { get; set; } = false;
        public int OdometerUserLabel { get; private set; }
        public string Peeled { get; private set; }

        public StateEntity()
        {
            //
        }

        public void LoadResponse(string request, string msg)
        {
            Message = msg;
            bool noErrors = false;
            bool noWarnings = false;

            if (request == ZplPipeUtils.ZplHostStatusReturn())
            {
                if (msg.Contains("PRINTER STATUS"))
                {
                    foreach (string item in msg.Split(new string[] { "\r\n" }, StringSplitOptions.None))
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

            if (request == ZplPipeUtils.ZplGetOdometerUserLabel())
            {
                try
                {
                    OdometerUserLabel = int.Parse(msg);
                }
                catch
                {

                }
            }

            if (request == ZplPipeUtils.ZplPeelerState())
            {

            }

            IsAlive = noErrors && noWarnings;

        }
    }
}