// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusGroups;

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    [Obsolete(@"Use GetNomenclatureList")]
    private List<NomenclatureModel> GetNomenclatureDeprecatedList(XElement xml)
    {
        List<NomenclatureModel> nomenclatures = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return nomenclatures;

        XmlNodeList list = xmlDocument.DocumentElement.GetElementsByTagName("Nomenclature");
        foreach (XmlNode node in list)
        {
            NomenclatureModel nomenclature = new();
            BrandModel brand = new();
            PluGroupModel nomenclatureGroup = new();
            try
            {
                nomenclature.ParseResult.Status = ParseStatus.Success;
                brand.ParseResult.Status = ParseStatus.Success;
                nomenclatureGroup.ParseResult.Status = ParseStatus.Success;
                // Set properties.
                SetItemPropertyFromXmlAttribute(node, nomenclature, "Guid");
                SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.IsMarked));
                //SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.IsGroup));
                SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.Name));
                SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.Code));
                //SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.FullName));
                SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.Description));
                //SetItemPropertyFromXmlAttribute(node, brand, nameof(brand.Code));
                SetItemPropertyFromXmlAttribute(node, brand, "BrandGuid");
                SetItemPropertyFromXmlAttribute(node, nomenclatureGroup, "GroupGuid");
                //SetItemPropertyFromXmlAttribute(node, , "BoxTypeGuid");
                //SetItemPropertyFromXmlAttribute(node, , "PackageTypeGuid");
                //SetItemPropertyFromXmlAttribute(node, , "ClipTypeGuid");

                if (string.IsNullOrEmpty(nomenclature.ParseResult.Exception))
                    nomenclature.ParseResult.Message = "Is success";
            }
            catch (Exception ex)
            {
                nomenclature.ParseResult.Status = ParseStatus.Error;
                nomenclature.ParseResult.Exception = ex.Message;
                if (ex.InnerException is not null)
                    nomenclature.ParseResult.InnerException = ex.InnerException.Message;
            }
            nomenclatures.Add(nomenclature);
        }

        return nomenclatures;
    }

    #endregion
}