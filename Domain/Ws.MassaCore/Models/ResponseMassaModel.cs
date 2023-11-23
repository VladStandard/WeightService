namespace Ws.MassaCore.Models;

public readonly struct ResponseMassaModel
{
    #region Public and private fields, properties, constructor

    public int Weight { get; }
    public bool IsStable { get; }

    public ResponseMassaModel()
    {
        Weight = default;
        IsStable = default;
    }
    
    public ResponseMassaModel(IReadOnlyList<byte> response)
    {
        if (response.Count < 10) return;
        Weight = BitConverter.ToInt32(response.Skip(6).Take(4).ToArray(), 0);
        IsStable = response[11] == 0x01;
        //Net = response[12];
        //Zero = response[13];
        //Tare = BitConverter.ToInt32(response.Skip(14).Take(4).ToArray(), 0);
    }

    #endregion
}