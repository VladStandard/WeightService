// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Linq;
using WsLocalizationCore.Utils;
using WsStorageCore.TableScaleModels.Brands;
using WsWebApiCore.Models;
using WsWebApiCore.Utils;

namespace WsWebApiCoreTests.Utils;

[TestFixture]
public sealed class WsContentUtilsTests
{
    private XElement? GetXmlBrands()
    {
        XDocument xDocument = XDocument.Parse(@"
<?xml version=""1.0"" encoding=""UTF-8""?>
<Brands Count=""24"">
	<Brand GUID=""5a159c70-70e6-11e6-80ce-a4bf01016d50"" IsMarked=""0"" Name=""Бамбушки"" Code=""000000005"" />
	<Brand GUID=""ceee208c-3985-11e4-9409-001e6722b449"" IsMarked=""0"" Name=""ВладимирскийСтандарт"" Code=""000000001"" />
	<Brand GUID=""d539dc32-3985-11e4-9409-001e6722b449"" IsMarked=""0"" Name=""ВладПродукт"" Code=""000000002"" />
	<Brand GUID=""4c3b6bfc-61ef-11e5-941b-001e6722b449"" IsMarked=""0"" Name=""Буслав"" Code=""000000003"" />
	<Brand GUID=""9d3c1d5c-973d-11e5-941d-001e6722b449"" IsMarked=""0"" Name=""Вот такая"" Code=""000000004"" />
	<Brand GUID=""f469476c-7a08-11e8-a214-a4bf0139eb1c"" IsMarked=""0"" Name=""Тешенки"" Code=""000000006"" />
	<Brand GUID=""87bd6166-b5b8-11e8-a214-a4bf0139eb1c"" IsMarked=""0"" Name=""Телятино"" Code=""000000007"" />
	<Brand GUID=""515d884b-449a-11e9-a216-a4bf0139eb1b"" IsMarked=""0"" Name=""Докторский стандарт"" Code=""000000008"" />
	<Brand GUID=""6939b998-449a-11e9-a216-a4bf0139eb1b"" IsMarked=""0"" Name=""Любительский стандарт"" Code=""000000009"" />
	<Brand GUID=""a68fc355-8b77-11e9-a217-a4bf0139eb1b"" IsMarked=""0"" Name=""Очумелли"" Code=""000000010"" />
	<Brand GUID=""4d9ceffd-aa1d-11e9-a217-a4bf0139eb1b"" IsMarked=""0"" Name=""ВладФиш"" Code=""000000011"" />
	<Brand GUID=""fd85fe39-225c-11ea-a21b-a4bf0139eb1b"" IsMarked=""0"" Name=""ВладБиф"" Code=""000000012"" />
	<Brand GUID=""5d4d88d1-bc70-11ea-a221-a4bf0139eb1b"" IsMarked=""0"" Name=""ОССЕТИАН"" Code=""000000013"" />
	<Brand GUID=""e45f38e9-d7d0-11ea-a221-a4bf0139eb1b"" IsMarked=""0"" Name=""Дисней"" Code=""000000014"" />
	<Brand GUID=""ab979e68-1ab8-11eb-a22d-00155d8a4602"" IsMarked=""0"" Name=""Огогонь"" Code=""000000015"" />
	<Brand GUID=""dd809fa4-3945-11eb-a22e-00155d8a4602"" IsMarked=""0"" Name=""ВладСтандарт"" Code=""000000016"" />
	<Brand GUID=""e8b651b8-a27e-11eb-bcd9-00155d8a4603"" IsMarked=""0"" Name=""Чудо-печка"" Code=""000000017"" />
	<Brand GUID=""90fe9c12-0024-11ec-bd11-00155df8551b"" IsMarked=""0"" Name=""Буль-Буль"" Code=""000000018"" />
	<Brand GUID=""1b2e853d-bb15-11ec-bd1a-00155d8a460f"" IsMarked=""0"" Name=""Владимирские пельмени"" Code=""000000019"" />
	<Brand GUID=""ed4fbb5b-fd12-11ec-bd1b-00155d8a460f"" IsMarked=""1"" Name=""тест"" Code=""000000020"" />
	<Brand GUID=""17bc261f-1a40-11ed-bd1c-00155d8a460f"" IsMarked=""0"" Name=""Дубравицы"" Code=""000000021"" />
	<Brand GUID=""210186a5-1a40-11ed-bd1c-00155d8a460f"" IsMarked=""0"" Name=""Закряжье"" Code=""000000022"" />
	<Brand GUID=""5bdc2388-35b4-11ed-bd20-00155d8a460f"" IsMarked=""0"" Name=""Красная птица"" Code=""000000023"" />
	<Brand GUID=""7519943a-35b4-11ed-bd20-00155d8a460f"" IsMarked=""0"" Name=""Владушка"" Code=""000000024"" />
</Brands>
            ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t'));
        XElement? xElement = xDocument.Root;
        return xElement;
    }


    [Test]
    public void GetXmlBrands_Exists()
    {
        Assert.DoesNotThrow(() =>
        {
            XElement? xElement = GetXmlBrands();
            Assert.That(xElement, Is.Not.Null);
            TestContext.WriteLine(xElement);
        });
    }

    [Test]
    public void WsContentUtils_GetNodesListCore_Brands()
    {
        Assert.DoesNotThrow(() =>
        {
            XElement? xElement = GetXmlBrands();
            Assert.That(xElement, Is.Not.Null);
            if (xElement is not null)
            {
                List<WsXmlContentRecord<WsSqlBrandModel>> brands = WsServiceContentUtils.GetNodesListCore<WsSqlBrandModel>(xElement, WsLocaleCore.WebService.XmlItemBrand,
                    (xmlNode, itemXml) =>
                    {
                        WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
                        WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
                        WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
                        WsServiceContentUtils.SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
                    });
                foreach (WsXmlContentRecord<WsSqlBrandModel> brand in brands)
                {
                    TestContext.WriteLine(brand.Item);
                    TestContext.WriteLine(brand.Content);
                    //Assert.That(brand);
                }
            }
            //WsContentUtils.GetXmlAttributeBool<PluModel>(XmlNomenclatures, LocaleCore.WebService.XmlItemNomenclature, (xmlNode, itemXml) =>
            //{
            //});
        });
    }
}