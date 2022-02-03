// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCoreTests;
using NUnit.Framework;

namespace DataCoreTests.DAL.DataModels
{
    [TestFixture]
    internal class XmlProductEntityTests
    {
        //private ProductHelper _product { get; set; } = ProductHelper.Instance;

        private string GetXmlProduct(int number)
        {
            switch (number)
            {
                case 1:
                    return @"
<Product Category=""Полуфабрикаты рубленые"" Code=""ЦБД00039761"" Description=""яКотлеты Из мраморной говядины 400 г"" Comment="""" SKU="""" DescriptionOptional=""AA=="" GUIDMercury=""f56361a6-5a1a-46b7-a7e0-480b2007785b"" Temperature=""0-6"" ProductShelfLife=""12 сут."" Brand=""ВладБиф"">
  <Units>
    <Unit Heft=""3.200"" Capacity=""0.000"" Rate=""8.000"" Threshold=""0"" OKEI=""001 "" Description=""Кор"" />
    <Unit Heft=""0.400"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Description=""шт"" IsBase=""1"" />
  </Units>
  <Barcodes>
    <Barcode Type=""ITF14"" Barcode=""14607100236440"" />
    <Barcode Type=""EAN13"" Barcode=""4607100236443"" />
  </Barcodes>
  <Box>
    <Box Description=""Коробка №2 400 грамм (380*230*230)"" Heft=""0.400"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Box>
  <Pack>
    <Pack Description=""Пакет 15 грамм"" Heft=""0.015"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Pack>
  <NameFull>яПолуфабрикаты мясные рубленые формованные непанированные категории Б. Котлеты ""Из мраморной говядины"" охлажденные ТУ 10.13.14-014-91005552-2019 (400г)</NameFull>
  <AdditionalDescriptionOfNomenclature>Срок годности: при температуре от 0°С до +6°С и относительной влажности воздуха 85±5% - 12 суток. Упаковано в модифицированной атмосфере.</AdditionalDescriptionOfNomenclature>
</Product>
                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                case 2:
                    return @"
<Product Category=""Колбаса вареная"" Code=""ЦБД00028783"" Description=""Докторская стандарт 500 г ц/ф 1 сорт"" Comment="""" SKU="""" DescriptionOptional=""AA=="" GUIDMercury=""58133a03-05cc-44a5-9d74-5c25e080709d"" Temperature=""0-6"" ProductShelfLife=""30 сут."" Brand=""ВладимирскийСтандарт"">
  <Units>
    <Unit Heft=""7.500"" Capacity=""0.000"" Rate=""15.000"" Threshold=""0"" OKEI=""001 "" Description=""Кор"" />
    <Unit Heft=""3.000"" Capacity=""0.000"" Rate=""6.000"" Threshold=""1"" OKEI=""001 "" Description=""Кор"" />
    <Unit Heft=""0.500"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Description=""шт"" IsBase=""1"" />
  </Units>
  <Barcodes>
    <Barcode Type=""ITF14"" Barcode=""14607100234507"" />
    <Barcode Type=""EAN13"" Barcode=""4607100234500"" />
  </Barcodes>
  <Box>
    <Box Description=""Коробка №2 400 грамм (380*230*230)"" Heft=""0.400"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Box>
  <Pack>
    <Pack Description=""Пакет 5  грамм"" Heft=""0.005"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Pack>
  <NameFull>Колбасные изделия вареные куриные 1 сорта. Колбаса вареная ""Докторская стандарт"" охлажденная ТУ 10.13.14-005-91005552-2016, ц/ф (500г)</NameFull>
  <AdditionalDescriptionOfNomenclature>Срок годности: 30 суток при температуре от 0°С до +6°С и относительной влажности воздуха 75%-78%. Упаковано под вакуумом.</AdditionalDescriptionOfNomenclature>
</Product>
                        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                case 3:
                    return @"
<Product Category=""Полуфабрикаты рубленые"" Code=""ЦБД00004577"" Description=""яБифштекс  &quot;Гурман&quot;  лоток 360 гр"" Comment="""" SKU="""" DescriptionOptional=""AA=="" GUIDMercury="""" Temperature=""-18"" ProductShelfLife=""90 сут"">
  <Units>
    <Unit Heft=""5.760"" Capacity=""0.000"" Rate=""16.000"" Threshold=""0"" OKEI=""001 "" Description=""Кор"" />
    <Unit Heft=""0.360"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Description=""шт"" IsBase=""1"" />
  </Units>
  <Box>
    <Box Description=""яКоробка №2 (для пельменей)"" Heft=""0.500"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Box>
  <NameFull>Полуфабрикаты рубленные яБифштекс  ""Гурман""  лоток 360 гр</NameFull>
  <AdditionalDescriptionOfNomenclature />
</Product>
                        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                case 4:
                    return @"
<Product Category=""Полуфабрикаты рубленые"" Code=""ЦБД00029982"" Description=""Котлеты Купеческие 480г"" Comment="""" SKU="""" DescriptionOptional=""AA=="" GUIDMercury=""cc026f1d-90c4-465e-82fa-6fef623fb349"" Temperature=""-18"" ProductShelfLife=""90 сут"" Brand=""ВладимирскийСтандарт"">
  <Units>
    <Unit Heft=""4.320"" Capacity=""0.000"" Rate=""9.000"" Threshold=""0"" OKEI=""001 "" Description=""Кор"" />
    <Unit Heft=""0.480"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Description=""шт"" IsBase=""1"" />
  </Units>
  <Barcodes>
    <Barcode Type=""ITF14"" Barcode=""14607100235917"" />
    <Barcode Type=""EAN13"" Barcode=""4607100235910"" />
  </Barcodes>
  <Box>
    <Box Description=""Коробка №9 200 грамм (350*235*200)"" Heft=""0.200"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Box>
  <Pack>
    <Pack Description=""Пакет 20  грамм"" Heft=""0.020"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Pack>
  <NameFull>Полуфабрикаты мясные рубленые формованные и панированные  категории Г  Котлеты ""Купеческие""  замороженные, ТУ 10.13.14-014-91005552-2019,   480г</NameFull>
  <AdditionalDescriptionOfNomenclature>Срок годности: при температуре не выше минус 18°С — 90 суток. </AdditionalDescriptionOfNomenclature>
</Product>
                        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                case 5:
                    return @"
<Product Category=""Фарш"" Code=""ЦБД00036400"" Description=""яИз свинины и говядины 400 г"" Comment="""" SKU="""" DescriptionOptional=""AA=="" GUIDMercury=""12d94183-59d2-4fac-8b8e-115964866030"" Temperature=""0-6"" ProductShelfLife=""12 сут"" Brand=""Телятино"">
  <Units>
    <Unit Heft=""3.200"" Capacity=""0.000"" Rate=""8.000"" Threshold=""0"" OKEI=""001 "" Description=""Кор"" />
    <Unit Heft=""0.400"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Description=""шт"" IsBase=""1"" />
  </Units>
  <Barcodes>
    <Barcode Type=""ITF14"" Barcode=""14607100235962"" />
    <Barcode Type=""EAN13"" Barcode=""4607100235965"" />
  </Barcodes>
  <Box>
    <Box Description=""Коробка №11 400 грамм (405*305*130)"" Heft=""0.400"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Box>
  <Pack>
    <Pack Description=""Пакет 15 грамм"" Heft=""0.015"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Pack>
  <NameFull>яПолуфабрикат мясной рубленый неформованный категории Б фарш ""Из свинины и говядины"" охлажденный, ТУ 10.13.14-014-91005552-2019, ( 400 г)</NameFull>
  <AdditionalDescriptionOfNomenclature>Срок годности: при температуре от 0 С до +6 С и относительной влажности воздуха 85±5%, упакованный с применением модифицированной газовой среды - 12 суток</AdditionalDescriptionOfNomenclature>
</Product>
                        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
                case 6:
                    return @"
<Product Category=""Полуфабрикаты рубленые"" Code=""ЦБД00036397"" Description=""яКолбаски Гриль 300 г"" Comment="""" SKU="""" DescriptionOptional=""AA=="" GUIDMercury=""73d9cfe4-5747-49e9-a52f-85381fe85e65"" Temperature=""0-6"" ProductShelfLife=""12 сут"" Brand=""Телятино"">
  <Units>
    <Unit Heft=""2.400"" Capacity=""0.000"" Rate=""8.000"" Threshold=""0"" OKEI=""001 "" Description=""Кор"" />
    <Unit Heft=""0.300"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Description=""шт"" IsBase=""1"" />
  </Units>
  <Barcodes>
    <Barcode Type=""ITF14"" Barcode=""14607100236037"" />
    <Barcode Type=""EAN13"" Barcode=""4607100236030"" />
  </Barcodes>
  <Box>
    <Box Description=""Коробка №11 400 грамм (405*305*130)"" Heft=""0.400"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Box>
  <Pack>
    <Pack Description=""Пакет 15 грамм"" Heft=""0.015"" Capacity=""0.000"" Rate=""1.000"" Threshold=""0"" OKEI=""796 "" Unit=""шт"" />
  </Pack>
  <NameFull> яПолуфабрикаты мясные рубленые формованные категории В Колбаски ""Гриль"" охлажденные, ТУ 10.13.14-014-91005552-2019, (300 г)</NameFull>
  <AdditionalDescriptionOfNomenclature>Срок годности: при температуре от 0 С до + 6 С и относительной влажности воздуха 85±5%, упакованный с применением модифицированной газовой среды - 12 суток</AdditionalDescriptionOfNomenclature>
</Product>
                        ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
            }
            return string.Empty;
        }

        [Test]
        public void XmlProductEntity_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                for (int i = 1; i < 7; i++)
                {
                    //var productEntity = _product.GetProductEntity(GetXmlProduct(i));
                    //TestContext.WriteLine(productEntity);
                    //TestContext.WriteLine();
                }
            });

            TestsUtils.MethodComplete();
        }
    }
}
