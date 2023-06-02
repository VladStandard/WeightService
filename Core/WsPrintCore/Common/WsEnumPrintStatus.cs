// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsPrintCore.Common;

public enum WsEnumPrintStatus
{
    Zero = 0,       // 00 Normal
    One = 1,        // 01 Head opened
    Two = 2,        // 02 Paper Jam
    Three = 3,      // 03 Paper Jam and head opened
    Four = 4,       // 04 Out of paper
    Five = 5,       // 05 Out of paper and head opened
    Eight = 8,      // 08 Out of ribbon
    Nine = 9,       // 09 Out of ribbon and head opened
    Ten = 10,       // 0A Out of ribbon and paper jam
    Eleven = 11,    // 0B Out of ribbon, paper jam and head opened
    Twelve = 12,    // 0C Out of ribbon and out of paper
    Thirteen = 13,  // 0D Out of ribbon, out of paper and head opened
    Sixteen = 16,   // 10 Pause
    ThirtyTwo = 32, // 20 Printing
    HundredTwentyEight = 128, // 80 Other error
}