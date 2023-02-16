// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusCharacteristics;

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    private List<PluCharacteristicModel> GetXmlPluCharacteristicsList(XElement xml) =>
        GetNodesListCore<PluCharacteristicModel>(xml, "Characteristic", (xmlNode, itemXml) =>
        {
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "AttachmentsCount");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "NomenclatureGuid");
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPluCharacteristics(Response1cShortModel response, List<PluCharacteristicModel> itemsDb, 
        PluCharacteristicModel pluCharacteristicXml)
    {
        try
        {
            // Find by Identity -> Update exists.
            PluCharacteristicModel? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(pluCharacteristicXml.IdentityValueUid));
            if (UpdateItemDb(response, pluCharacteristicXml.Uid1c, pluCharacteristicXml, itemDb, true)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, pluCharacteristicXml.Uid1c, pluCharacteristicXml, true);

            // Update db list.
            if (isSave && !itemsDb.Select(x => x.IdentityValueUid).Contains(pluCharacteristicXml.IdentityValueUid))
                itemsDb.Add(pluCharacteristicXml);
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
            List<PluCharacteristicModel> itemsDb = DataContext.GetListNotNullable<PluCharacteristicModel>(sqlCrudConfig);
            List<PluCharacteristicModel> itemsXml = GetXmlPluCharacteristicsList(xml);
            foreach (PluCharacteristicModel itemXml in itemsXml)
            {
                switch (itemXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cPluCharacteristics(response, itemsDb, itemXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, itemXml.Uid1c, itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
                        break;
                }
            }
        }, format, false);

    #endregion
}