// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace WsStorageCore.TableDirectModels;

[Serializable]
public sealed class SsccDirect : WsSqlSerializeBase
{
	#region Public and private fields, properties, constructor

	[XmlElement("SSCC")] public string Sscc { get; set; } = string.Empty;
    [XmlElement("GLN")] public string Gln { get; set; } = string.Empty;
	[XmlElement("UnitID")] public int UnitId { get; set; }
    [XmlElement("UnitType")] public byte UnitType { get; set; }
    [XmlElement("SynonymSSCC")] public string SynonymSscc => $"({Sscc.Substring(0, 2)}){Sscc.Substring(2, 17)}";
    [XmlElement("Check")] public int Check { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public SsccDirect() { }

    public SsccDirect(string sscc)
    {
        Sscc = sscc;
        Gln = sscc.Substring(3, 9);
        UnitId = int.Parse(sscc.Substring(12, 7));
        UnitType = byte.Parse(sscc.Substring(2, 1));
        Check = int.Parse(sscc.Substring(19, 1));
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{SynonymSscc}";

    #endregion
}