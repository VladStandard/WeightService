// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDirectModels;

[Serializable]
public class PrinterDirect : SqlSerializeBase, ISerializable
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static PrinterDirect _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static PrinterDirect Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields, properties, constructor

	[XmlElement] public long Id { get; set; } = default;
	[XmlElement] public string Name { get; set; } = string.Empty;
	[XmlElement] public string Ip { get; set; } = string.Empty;
	[XmlElement] public short Port { get; set; } = default;
	[XmlElement] public string Mac { get; set; } = string.Empty;
	[XmlElement] public string Password { get; set; } = string.Empty;
	[XmlElement] public string PrinterType { get; set; } = string.Empty;

    #endregion

    #region Constructor and destructor

    public PrinterDirect()
    {
        Load(default);
    }

    public PrinterDirect(long id)
    {
        Load(id);
    }

    #endregion

    #region Public and private methods

    public void Load(long id)
    {
        Id = id;
        if (Id == default) return;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        string query = @"
SELECT x.Id,x.Name,x.Ip,x.Port,x.Password,x.Mac,y.Name as PrinterType
FROM [db_scales].[ZebraPrinter] x
INNER JOIN[db_scales].[ZebraPrinterType] y
ON x.[PrinterTypeId] = y.Id
WHERE x.[Id] = @ID
                ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
        using (SqlCommand cmd = new(query))
        {
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ID", Id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Ip = SqlConnect.GetValueAsString(reader, "Ip");
                    Port = SqlConnect.GetValueAsNotNullable<short>(reader, "Port");
                    Mac = SqlConnect.GetValueAsString(reader, "Mac");
                    Name = SqlConnect.GetValueAsString(reader, "Name");
                    Password = SqlConnect.GetValueAsString(reader, "Password");
                    PrinterType = SqlConnect.GetValueAsString(reader, "PrinterType");
                }
            }
            reader.Close();
        }
        con.Close();
    }

    #endregion
}
