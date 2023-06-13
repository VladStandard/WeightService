// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Data;

namespace WsStorageCore.TableDirectModels;

[Serializable]
public sealed class ProductSeriesDirect : WsSqlSerializeBase
{
	#region Public and private fields, properties, constructor

	[XmlElement] public long Id { get; set; }
	[XmlElement] public Guid Uuid { get; set; }
	[XmlElement] public WsSqlScaleModel Scale { get; set; }
	[XmlElement] public DateTime CreateDate { get; set; }
	[XmlElement] public SsccDirect Sscc { get; set; }
	[XmlElement] public int CountUnit { get; set; }
	[XmlElement] public decimal TotalWeightNetto { get; set; }
	[XmlElement] public decimal TotalWeightTare { get; set; }
	[XmlElement] public bool IsMarked { get; set; }

    #endregion

    #region Constructor and destructor

    public ProductSeriesDirect()
    {
        Id = 0;
        IsMarked = false;
        Uuid = Guid.Empty;
        CreateDate = DateTime.MinValue;
        CountUnit = 0;
        TotalWeightNetto = 0;
        TotalWeightTare = 0;
        Sscc = new();
        Scale = new();
    }

    public ProductSeriesDirect(WsSqlScaleModel scale) : this()
    {
        Scale = scale;
        Load();
    }

    #endregion

    #region Public and private methods

    public void Load()
    {
        if (Scale is null || Scale.IsNew) 
        {
            throw new("Equipment instance not identified. Set [Scale].");
        }

        SqlConnect.ExecuteReader(WsSqlQueriesScales.Functions.GetCurrentProductSeriesV2,
            new("@SCALE_ID", SqlDbType.VarChar, 38) { Value = Scale.IdentityValueId }, reader =>
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
                    TotalWeightNetto = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5);
                    TotalWeightTare = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6);
                }
            });
    }

    #endregion
}