// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;
// ReSharper disable InconsistentNaming

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPluCharacteristics(Response1cShortModel response, List<PluCharacteristicModel> pluCharacteristicsDb, 
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

    private void AddResponse1cPluCharacteristicsFks(Response1cShortModel response, List<PluCharacteristicsFkModel> pluCharacteristicsFksDb, 
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
                PluCharacteristic = pluCharacteristicDb,
            };

            // Find by Identity -> Update exists.
            PluCharacteristicsFkModel? pluCharacteristicFkDb = pluCharacteristicsFksDb.Find(item =>
                Equals(item.Plu.Uid1c, pluCharacteristicsFk.Plu.Uid1c) &&
                Equals(item.PluCharacteristic.Uid1c, pluCharacteristicsFk.PluCharacteristic.Uid1c));
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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cPluCharacteristics(ISessionFactory sessionFactory, XElement xml, string format) =>
        NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<PluCharacteristicModel> pluCharacteristicsDb = DataContext.GetListNotNullable<PluCharacteristicModel>(sqlCrudConfig);
            List<PluCharacteristicsFkModel> pluCharacteristicsFksDb = DataContext.GetListNotNullable<PluCharacteristicsFkModel>(sqlCrudConfig);
            List<PluCharacteristicModel> pluCharacteristicsXml = GetXmlPluCharacteristicsList(xml);
            foreach (PluCharacteristicModel pluCharacteristicXml in pluCharacteristicsXml)
            {
                if (pluCharacteristicXml.ParseResult.Status == ParseStatus.Success)
                    AddResponse1cPluCharacteristics(response, pluCharacteristicsDb, pluCharacteristicXml);
                if (pluCharacteristicXml.ParseResult.Status == ParseStatus.Success)
                    AddResponse1cPluCharacteristicsFks(response, pluCharacteristicsFksDb, pluCharacteristicXml);
                if (pluCharacteristicXml.ParseResult.Status == ParseStatus.Error)
                    AddResponse1cException(response, pluCharacteristicXml.Uid1c, 
                        pluCharacteristicXml.ParseResult.Exception, pluCharacteristicXml.ParseResult.InnerException);
            }
        }, format, false);

    #endregion
}