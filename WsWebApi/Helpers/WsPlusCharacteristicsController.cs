// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsWebApi.Helpers;

internal class WsPlusCharacteristicsController : WsWebControllerBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsPlusCharacteristicsController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsPlusCharacteristicsController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public WsPlusCharacteristicsController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    private List<PluCharacteristicModel> GetXmlPluCharacteristicsList(XElement xml) =>
        GetNodesListCore<PluCharacteristicModel>(xml, LocaleCore.WebService.XmlItemCharacteristic, (xmlNode, itemXml) =>
        {
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "NomenclatureGuid");
        });

    private void AddResponse1cPluCharacteristics(WsResponse1cShortModel response, List<PluCharacteristicModel> pluCharacteristicsDb,
        PluCharacteristicModel pluCharacteristicXml)
    {
        try
        {
            // Find by Identity -> Update exists.
            PluCharacteristicModel? itemDb = pluCharacteristicsDb.Find(x => x.IdentityValueUid.Equals(pluCharacteristicXml.IdentityValueUid));
            if (UpdateItem1cDb(response, pluCharacteristicXml.Uid1c, pluCharacteristicXml, itemDb, true)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluCharacteristicXml.Uid1c, pluCharacteristicXml, true);

            // Update db list.
            if (isSave && !pluCharacteristicsDb.Select(x => x.IdentityValueUid).Contains(pluCharacteristicXml.IdentityValueUid))
                pluCharacteristicsDb.Add(pluCharacteristicXml);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluCharacteristicXml.Uid1c, ex);
        }
    }

    private void AddResponse1cPluCharacteristicsFks(WsResponse1cShortModel response, List<PluCharacteristicsFkModel> pluCharacteristicsFksDb,
        PluCharacteristicModel pluCharacteristicXml)
    {
        try
        {
            if (Equals(pluCharacteristicXml.NomenclatureGuid, Guid.Empty)) return;

            if (!GetPluDb(response, pluCharacteristicXml.NomenclatureGuid, pluCharacteristicXml.Uid1c,
                    LocaleCore.WebService.FieldNomenclature, out PluModel? pluDb)) return;
            if (!GetPluCharacteristicDb(response, pluCharacteristicXml.Uid1c, pluCharacteristicXml.Uid1c,
                    LocaleCore.WebService.FieldNomenclatureCharacteristic, out PluCharacteristicModel? pluCharacteristicDb)) return;
            if (pluDb is null || pluCharacteristicDb is null) return;

            PluCharacteristicsFkModel pluCharacteristicsFk = new()
            {
                IdentityValueUid = Guid.NewGuid(),
                Plu = pluDb,
                Characteristic = pluCharacteristicDb,
            };

            // Find by Identity -> Update exists.
            PluCharacteristicsFkModel? pluCharacteristicFkDb = pluCharacteristicsFksDb.Find(item =>
                Equals(item.Plu.Uid1c, pluCharacteristicsFk.Plu.Uid1c) &&
                Equals(item.Characteristic.Uid1c, pluCharacteristicsFk.Characteristic.Uid1c));
            if (UpdateItemDb(response, pluCharacteristicXml.Uid1c, pluCharacteristicsFk, pluCharacteristicFkDb, false)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluCharacteristicXml.Uid1c, pluCharacteristicsFk, false);

            // Update db list.
            if (isSave && !pluCharacteristicsFksDb.Select(x => x.IdentityValueUid).Contains(pluCharacteristicsFk.IdentityValueUid))
                pluCharacteristicsFksDb.Add(pluCharacteristicsFk);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, pluCharacteristicXml.Uid1c, ex);
        }
    }

    public ContentResult NewResponse1cPluCharacteristics(XElement xml, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1cCore<WsResponse1cShortModel>(response =>
        {
            List<PluCharacteristicModel> pluCharacteristicsDb = WsDataContext.GetListNotNullable<PluCharacteristicModel>(SqlCrudConfig);
            List<PluCharacteristicsFkModel> pluCharacteristicsFksDb = WsDataContext.GetListNotNullable<PluCharacteristicsFkModel>(SqlCrudConfig);
            List<PluCharacteristicModel> pluCharacteristicsXml = GetXmlPluCharacteristicsList(xml);
            foreach (PluCharacteristicModel pluCharacteristicXml in pluCharacteristicsXml)
            {
                if (pluCharacteristicXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluCharacteristics(response, pluCharacteristicsDb, pluCharacteristicXml);
                if (pluCharacteristicXml.ParseResult.IsStatusSuccess)
                    AddResponse1cPluCharacteristicsFks(response, pluCharacteristicsFksDb, pluCharacteristicXml);
                if (pluCharacteristicXml.ParseResult.IsStatusError)
                    AddResponse1cException(response, pluCharacteristicXml.Uid1c,
                        pluCharacteristicXml.ParseResult.Exception, pluCharacteristicXml.ParseResult.InnerException);
            }
        }, format, isDebug, sessionFactory);

    #endregion
}