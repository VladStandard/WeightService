// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace ZplSdkExamples.Utils;

internal class BluetoothHelper
{

    public static bool IsBluetoothSupported()
    {
        OperatingSystem osVersion = Environment.OSVersion;
        if (osVersion.Version.Major >= 10)
        {
            return true;
        }
        return false;
    }
}