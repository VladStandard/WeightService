// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsMassaCore.Models;

public readonly struct ResponseMassaModel
{
    #region Public and private fields, properties, constructor

    public int Weight { get; }
    public int ScaleFactor { get; }
    public bool IsStable { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ResponseMassaModel()
    {
        Weight = default;
        ScaleFactor = default;
        IsStable = default;
    }
    
    public ResponseMassaModel(IReadOnlyList<byte> response)
    {
        if (response.Count < 10) return;
        Weight = BitConverter.ToInt32(response.Skip(6).Take(4).ToArray(), 0);
        //Division = response[10];
        ScaleFactor = response[10] switch
        {
            0x00 => 10_000,
            0x01 => 1_000,
            0x02 => 0_100,
            0x03 => 0_010,
            0x04 => 0_001,
            _ => 0,
        };
        IsStable = response[11] == 0x01;
        //Net = response[12];
        //Zero = response[13];
        //Tare = BitConverter.ToInt32(response.Skip(14).Take(4).ToArray(), 0);
    }

    #endregion
}