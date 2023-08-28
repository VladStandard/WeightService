namespace WsStorageCore.Tables.TableDirectModels;

[Serializable]
public sealed class NomenclatureDirect : WsSqlSerializeBase
{
	#region Public and private fields, properties, constructor

	[XmlElement] public long Id { get; set; } = default;
	[XmlElement] public string? Name { get; set; } = string.Empty;
	[XmlElement] public DateTime CreateDate { get; set; } = default;
	[XmlElement] public DateTime ChangeDt { get; set; } = default;
	[XmlElement] public string? RRefID { get; set; } = default;
	[XmlElement] public string? Code { get; set; } = default;
	[XmlElement] public bool IsMarked { get; set; } = false;
	[XmlElement] public string NameFull { get; set; } = "";
	[XmlElement] public string Description { get; set; } = string.Empty;
	[XmlElement] public string Comment { get; set; } = string.Empty;
	[XmlElement] public string Brand { get; set; } = string.Empty;
	[XmlElement] public string GUID_Mercury { get; set; } = string.Empty;
	[XmlElement] public string NomenclatureType { get; set; } = string.Empty;
	[XmlElement] public string VATRate { get; set; } = string.Empty;


	public NomenclatureDirect()
    {
        Load();
    }


	/// <param name="id"></param>
	public NomenclatureDirect(long id)
    {
        Id = id;
        Load();
    }

	#endregion

	#region Public and private methods

	public override bool Equals(object obj)
	{
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
		if (obj is not NomenclatureDirect item)
        {
            return false;
        }
        return Id.Equals(item.Id);
    }

    public override int GetHashCode() => 
	    Id.GetHashCode();

    public void Load()
    {
        if (Id == default) return;
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        string query = "SELECT * FROM [db_scales].[GetNomenclature] (@Id);";
        using (SqlCommand cmd = new(query))
        {
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@Id", Id);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Id = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
                    Name = SqlConnect.GetValueAsString(reader, "Name");
                    CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate");
                    ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt");
                    RRefID = SqlConnect.GetValueAsString(reader, "RRefID");
                    Code = SqlConnect.GetValueAsString(reader, "Code");
                    IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "Marked");
                    NameFull = SqlConnect.GetValueAsString(reader, "NameFull");
                    Description = SqlConnect.GetValueAsString(reader, "Description");
                    Comment = SqlConnect.GetValueAsString(reader, "Comment");
                    Brand = SqlConnect.GetValueAsString(reader, "Brand");
                    GUID_Mercury = SqlConnect.GetValueAsString(reader, "GUID_Mercury");
                    NomenclatureType = SqlConnect.GetValueAsString(reader, "NomenclatureType");
                    VATRate = SqlConnect.GetValueAsString(reader, "VATRate");
                }
            }
            reader.Close();
        }
        con.Close();
    }

    public void Save()
    {
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        string query = @"
DECLARE @ID int; 
EXECUTE  [db_scales].[SetNomenclature] 
@1CRRefID
,@Code
,@Marked
,@Name
,@NameFull
,@Description
,@Comment
,@Brand
,@GUID_Mercury
,@NomenclatureType
,@VATRate
,@ID OUTPUT
SELECT @ID as ID";
        using (SqlCommand cmd = new(query))
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue($"@1CRRefID", RRefID ?? (object)DBNull.Value);  // @1CRRefID
            cmd.Parameters.AddWithValue($"@Code ", Code ?? (object)DBNull.Value);  // 
            cmd.Parameters.AddWithValue($"@Marked", IsMarked);  // 
            cmd.Parameters.AddWithValue($"@Name", Name ?? (object)DBNull.Value);  // 
            cmd.Parameters.AddWithValue($"@NameFull", NameFull ?? (object)DBNull.Value);  // 
            cmd.Parameters.AddWithValue($"@Description", Description ?? (object)DBNull.Value);  // 
            cmd.Parameters.AddWithValue($"@Comment", Comment ?? (object)DBNull.Value);  // 
            cmd.Parameters.AddWithValue($"@Brand", Brand ?? (object)DBNull.Value);  // 
            cmd.Parameters.AddWithValue($"@GUID_Mercury", GUID_Mercury ?? (object)DBNull.Value);  // 
            cmd.Parameters.AddWithValue($"@NomenclatureType", NomenclatureType ?? (object)DBNull.Value);  // 
            cmd.Parameters.AddWithValue($"@VATRate", VATRate ?? (object)DBNull.Value);  // 
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Id = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
                }
            }
            reader.Close();
        }
        con.Close();
    }

    public List<NomenclatureDirect> GetList()
    {
        List<NomenclatureDirect> result = new();
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        string query = "SELECT * FROM [db_scales].[GetNomenclature] (default);";
        using (SqlCommand cmd = new(query))
        {
	        cmd.Connection = con;
	        using SqlDataReader reader = cmd.ExecuteReader();
	        if (reader.HasRows)
	        {
		        while (reader.Read())
		        {
			        NomenclatureDirect nomenclature = new()
			        {
				        Id = SqlConnect.GetValueAsNotNullable<int>(reader, "Id"),
				        Name = SqlConnect.GetValueAsString(reader, "Name"),
				        CreateDate = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "CreateDate"),
				        ChangeDt = SqlConnect.GetValueAsNotNullable<DateTime>(reader, "ChangeDt"),
				        RRefID = SqlConnect.GetValueAsString(reader, "1CRRefID"),
				        Code = SqlConnect.GetValueAsString(reader, "Code"),
				        IsMarked = SqlConnect.GetValueAsNotNullable<bool>(reader, "Marked"),
				        NameFull = SqlConnect.GetValueAsString(reader, "NameFull"),
				        Description = SqlConnect.GetValueAsString(reader, "Description"),
				        Comment = SqlConnect.GetValueAsString(reader, "Comment"),
				        Brand = SqlConnect.GetValueAsString(reader, "Brand"),
				        GUID_Mercury = SqlConnect.GetValueAsString(reader, "GUID_Mercury"),
				        NomenclatureType = SqlConnect.GetValueAsString(reader, "NomenclatureType"),
				        VATRate = SqlConnect.GetValueAsString(reader, "VATRate")
			        };
			        result.Add(nomenclature);
		        }
	        }
	        reader.Close();
        }
        con.Close();
        return result;
    }

    #endregion
}