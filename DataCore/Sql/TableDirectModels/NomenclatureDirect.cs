// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDirectModels;

[Serializable]
public class NomenclatureDirect : BaseSerializeEntity, ISerializable
{
    #region Public and private fields, properties, constructor

    public long Id { get; set; } = default;
    public string? Name { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; } = default;
    public DateTime ChangeDt { get; set; } = default;
    public string? RRefID { get; set; } = default;
    public string? Code { get; set; } = default;
    public bool IsMarked { get; set; } = false;
    public string NameFull { get; set; } = "";
    public string Description { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string GUID_Mercury { get; set; } = string.Empty;
    public string NomenclatureType { get; set; } = string.Empty;
    public string VATRate { get; set; } = string.Empty;

	/// <summary>
	/// Constructor.
	/// </summary>
	public NomenclatureDirect()
    {
        Load();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
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
        if (obj is not NomenclatureDirect item)
        {
            return false;
        }
        return Id.Equals(item.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

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
