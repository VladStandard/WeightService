// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsMassa.Models;

public class ResponseMassaModel
{
    #region Public and private fields, properties, constructor

    public int Weight { get; }
    private byte Division { get; }
    public int ScaleFactor => Division switch
    {
        0x00 => 10000,
        0x01 => 1000,
        0x02 => 100,
        0x03 => 10,
        0x04 => 1,
        _ => 1_000,
    };
    public byte IsStable { get; }
    private byte Net { get; }
    private byte Zero { get; }
    public int Tare { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ResponseMassaModel()
    {
        Weight = default;
        Division = default;
        IsStable = default;
        Net = default;
        Zero = default;
        Tare = default;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="response"></param>
    public ResponseMassaModel(IReadOnlyList<byte> response)
    {
        if (response.Count < 10)
            return;
        Weight = BitConverter.ToInt32(response.Skip(6).Take(4).ToArray(), 0);
        Division = response[10];
        IsStable = response[11];
        Net = response[12];
        Zero = response[13];
        Tare = BitConverter.ToInt32(response.Skip(14).Take(4).ToArray(), 0);
    }

    #endregion
}