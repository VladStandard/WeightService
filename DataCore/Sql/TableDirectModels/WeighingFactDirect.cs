//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCore.Sql.TableDirectModels;

///// <summary>
///// Table WeighingFact.
///// </summary>
//[Serializable]
//public class WeighingFactDirect : BaseSerializeEntity, ISerializable
//{
//	#region Public and private fields, properties, constructor

//	[XmlElement] public long Id { get; set; }
//	[XmlElement] public TemplateEntity Template { get; set; } = new();
//	[XmlElement] public ScaleEntity Scale { get; set; }
//	[XmlElement] public string ProductSeries { get; set; } = string.Empty;
//	[XmlElement] public PluScaleEntity PluScale { get; set; } = new();
//    [XmlElement] public DateTime ProductDate { get; set; }
//	[XmlElement] public int KneadingNumber { get; set; }
//	[XmlElement] public decimal NetWeight { get; set; }
//	[XmlElement] public decimal TareWeight { get; set; }
//	[XmlElement(IsNullable = true)] public int? ScaleFactor { get; set; }
//	[XmlElement] public DateTime RegDate { get; set; }
//	[XmlElement] public SsccDirect Sscc { get; set; }

//	[XmlElement] public DateTime ExpirationDate
//    {
//        get => ProductDate.AddDays(PluScale == null ? 30 : PluScale.Plu.ShelfLifeDays);
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string ProductDateBarCode
//    {
//        get => $"{ProductDate:yyMMdd}";
//        // This code need for print labels.
//        set => _ = value;
//    }
// 	[XmlElement] public string ProductTimeBarCode
//    {
//        get => $"{ProductDate:HHmmss}";
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string ProductDateRus
//    {
//        get => $"{ProductDate:dd.MM.yyyy}";
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string LotNumberPretty
//    {
//        get => $"{ProductDate:yyMM}";
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string ProductTimePrettyBarCode
//    {
//        get => $"{ProductDate:HHmm}";
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string ProductTimePrettyRus
//    {
//        get => $"{ProductDate:HH:mm}";
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string NetWeightKgPretty2
//    {
//        get => $"{NetWeight:00.000}".Replace(',', '.').Split('.')[0];
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string NetWeightKgPretty3
//    {
//        get => $"{NetWeight:000.000}".Replace(',', '.').Split('.')[0];
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string NetWeightKgPretty1Dot3Eng
//    {
//        get => $"{NetWeight:0.000}".Replace(',', '.');
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string NetWeightKgPretty2Dot3Eng
//    {
//        get => $"{NetWeight:#0.000}".Replace(',', '.');
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string NetWeightKgPretty1Dot3Rus
//    {
//        get => $"{NetWeight:0.000}".Replace('.', ',');
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string NetWeightKgPretty2Dot3Rus
//    {
//        get => $"{NetWeight:#0.000}".Replace('.', ',');
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string NetWeightGrPretty2
//    {
//        get => $"{NetWeight:#.00}".Replace(',', '.').Split('.')[1];
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public string NetWeightGrPretty3
//    {
//        get => $"{NetWeight:#.000}".Replace(',', '.').Split('.')[1];
//        // This code need for print labels.
//        set => _ = value;
//    }
//	[XmlElement] public decimal GrossWeight => NetWeight + TareWeight;
//    public string ProductSscc
//    {
//        get => Sscc.Sscc;
//        // This code need for print labels.
//        set => _ = value;
//    }

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//	public WeighingFactDirect()
//    {
//        NetWeight = 0;
//        TareWeight = 0;
//        ScaleFactor = 1000;
//        RegDate = DateTime.Now;
//        ProductDate = DateTime.Now.Date;
//        Template = new();
//        Scale = new();
//        ProductSeries = string.Empty;
//        Sscc = new();
//    }

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//	/// <param name="scale"></param>
//	/// <param name="pluScale"></param>
//	/// <param name="productDate"></param>
//	/// <param name="kneadingNumber"></param>
//	/// <param name="scaleFactor"></param>
//	/// <param name="netWeight"></param>
//	/// <param name="tareWeight"></param>
//	public WeighingFactDirect(ScaleEntity scale, PluScaleEntity pluScale, DateTime productDate, int kneadingNumber,
//        int? scaleFactor, decimal netWeight, decimal tareWeight)
//    {
//        ScaleFactor = scaleFactor;
//        Scale = scale;
//        PluScale = pluScale;
//        ProductDate = productDate;
//        KneadingNumber = kneadingNumber;
//        NetWeight = netWeight;
//        TareWeight = tareWeight;
//        Sscc = new();
//    }

//	/// <summary>
//	/// Constructor.
//	/// </summary>
//	public WeighingFactDirect(SerializationInfo info, StreamingContext context)
//	{
//		Id = info.GetInt64(nameof(Id));
//		Template = (TemplateEntity)info.GetValue(nameof(Template), typeof(TemplateEntity));
//		//Scale = (ScaleEntity)info.GetValue(nameof(Scale), typeof(ScaleEntity));
//		//ProductSeries = info.GetString(nameof(ProductSeries));
//		PluScale = (PluScaleEntity)info.GetValue(nameof(PluScale), typeof(PluScaleEntity));
//		ProductDate = info.GetDateTime(nameof(ProductDate));
//		KneadingNumber = info.GetInt32(nameof(KneadingNumber));
//        NetWeight = info.GetDecimal(nameof(NetWeight));
//        TareWeight = info.GetDecimal(nameof(TareWeight));
//        ScaleFactor = info.GetInt32(nameof(ScaleFactor));
//        RegDate = info.GetDateTime(nameof(RegDate));
//        //Sscc = (SsccDirect)info.GetValue(nameof(Sscc), typeof(SsccDirect));
//	}

//    #endregion

//    #region Public and private methods

//    private void SaveReader(SqlDataReader reader)
//    {
//        if (reader.Read())
//        {
//            RegDate = reader.GetDateTime(1);
//            Id = reader.GetInt32(3);
//            XDocument xDoc = XDocument.Parse(reader.GetString(2));
//            SsccDirect sscc = new();
//            XElement? element = xDoc.Root?.Element("Item");
//            if (element != null)
//            {
//                if (element.Attribute("SSCC") is { } attributeSscc)
//                    sscc.Sscc = attributeSscc.Value;
//                if (element.Attribute("GLN") is { } attributeGln)
//                    sscc.Gln = attributeGln.Value;
//                if (element.Attribute("UnitID") is { } attributeUnitId)
//                    sscc.UnitId = int.Parse(attributeUnitId.Value);
//                if (element.Attribute("UnitType") is { } attributeUnitType)
//                    sscc.UnitType = byte.Parse(attributeUnitType.Value);
//                //if (element.Attribute("SynonymSSCC") is { } attributeSynonymSscc)
//                //    sscc.SynonymSSCC = attributeSynonymSscc.Value;
//                if (element.Attribute("Check") is { } attributeCheck)
//                    sscc.Check = int.Parse(attributeCheck.Value);
//            }

//            Sscc = sscc;
//        }
//    }

//    public void Save()
//    {
//        SqlParameter[] parameters = {
//            new("@ScaleID", SqlDbType.VarChar, 38) { Value = Scale.IdentityId },
//            new("@PLU", SqlDbType.Int) { Value = PluScale.Plu.Number },
//            new("@NetWeight", SqlDbType.Decimal) { Value = NetWeight },
//            new("@TareWeight", SqlDbType.Decimal) { Value = TareWeight },
//            new("@ProductDate", SqlDbType.Date) { Value = ProductDate },
//            new("@Kneading", SqlDbType.Int) { Value = KneadingNumber },
//        };
//        SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.WeithingFacts.Save, parameters, SaveReader);
//    }

//    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
//    {
//	    base.GetObjectData(info, context);
//	    info.AddValue(nameof(Id), Id);
//	    info.AddValue(nameof(Template), Template);
//	    info.AddValue(nameof(Scale), Scale);
//	    info.AddValue(nameof(ProductSeries), ProductSeries);
//	    info.AddValue(nameof(PluScale), PluScale);
//	    info.AddValue(nameof(ProductDate), ProductDate);
//	    info.AddValue(nameof(KneadingNumber), KneadingNumber);
//	    info.AddValue(nameof(NetWeight), NetWeight);
//	    info.AddValue(nameof(TareWeight), TareWeight);
//	    info.AddValue(nameof(ScaleFactor), ScaleFactor);
//	    info.AddValue(nameof(RegDate), RegDate);
//	    info.AddValue(nameof(Sscc), Sscc);
//	}
    
//    #endregion
//}
