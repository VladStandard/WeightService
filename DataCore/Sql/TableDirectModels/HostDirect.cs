//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCore.Sql.TableDirectModels;

//[Serializable]
//public class HostDirect : SqlSerializeBase, ISerializable
//{
//	#region Public and private fields, properties, constructor

//	[XmlElement] public long Id { get; set; }
//	[XmlElement] public long ScaleId { get; set; }
//	[XmlElement] public string? Name { get; set; }
//    [XmlIgnore] public string? HostName { get; set; }
//    [XmlElement] public string? Ip { get; set; }
//    [XmlElement] public string? Mac { get; set; }
//    [XmlElement] public Guid IdRRef { get; set; }
//    [XmlElement] public bool IsMarked { get; set; }

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//	public HostDirect()
//    {
//        Id = 0;
//        ScaleId = 0;
//        Name = string.Empty;
//        HostName = string.Empty;
//        Ip = string.Empty;
//        Mac = string.Empty;
//        IdRRef = Guid.Empty;
//        IsMarked = false;
//    }

//    #endregion

//    #region Public and private methods

//    public override string ToString() =>
//        $"{nameof(Name)}: {Name}. " +
//        $"{nameof(HostName)}: {HostName}. " +
//        $"{nameof(Ip)}: {Ip}. " +
//        $"{nameof(Mac)}: {Mac}. ";

//    #endregion
//}
