// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDirectModels;

[Serializable]
public class ProductSeriesDirect : BaseSerializeEntity, ISerializable
{
    #region Public and private fields, properties, constructor

    public long Id { get; set; }
    public Guid Uuid { get; set; }
    public ScaleEntity Scale { get; set; }
    public DateTime CreateDate { get; set; }
    public SsccDirect Sscc { get; set; }
    public int CountUnit { get; set; }
    public decimal TotalNetWeight { get; set; }
    public decimal TotalTareWeight { get; set; }
    public bool IsMarked { get; set; }

    #endregion

    #region Constructor and destructor

    public ProductSeriesDirect()
    {
        Id = 0;
        IsMarked = false;
        Uuid = Guid.Empty;
        CreateDate = DateTime.MinValue;
        CountUnit = 0;
        TotalNetWeight = 0;
        TotalTareWeight = 0;
        Sscc = new();
        Scale = new();
    }

    public ProductSeriesDirect(ScaleEntity scale) : this()
    {
        Scale = scale;
        Load();
    }

    #endregion

    #region Public and private methods

    public void Load()
    {
        if (Scale == null || Scale.IdentityId == default) 
        {
            throw new("Equipment instance not identified. Set [Scale].");
        }

        SqlConnect.ExecuteReader(SqlQueries.DbScales.Functions.GetCurrentProductSeriesV2,
            new SqlParameter("@SCALE_ID", SqlDbType.VarChar, 38) { Value = Scale.IdentityId }, (SqlDataReader reader) =>
            {
                byte count = 0;
                while (reader.Read())
                {
                    if (count > 0)
                        throw new($"{nameof(count)} > 0 ({count})");
                    count++;
                    if (reader[0] is long longId)
                        Id = longId;
                    else if (reader[0] is int intId)
                        Id = intId;
                    CreateDate = reader.GetDateTime(1);
                    Uuid = reader.GetGuid(2);
                    Sscc = new(reader.GetString(3));
                    CountUnit = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                    TotalNetWeight = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                    TotalTareWeight = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);
                }
            });
    }

    #endregion
}
