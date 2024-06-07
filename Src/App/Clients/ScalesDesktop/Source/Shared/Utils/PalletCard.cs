using iText.Barcodes;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Properties;
using Ws.Domain.Models.Entities.Print;
using Border = iText.Layout.Borders.Border;
using ICell = iText.Layout.Element.Cell;
using IImage = iText.Layout.Element.Image;
using ITable = iText.Layout.Element.Table;
using Paragraph = iText.Layout.Element.Paragraph;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ScalesDesktop.Source.Shared.Utils;

public class PalletCard
{
    private const string FontPath = "C:\\Windows\\Fonts\\arial.ttf";

    public static string CreateBase64(ViewPallet palletView)
    {
        MemoryStream stream = new();
        PdfWriter writer = new(stream);
        PdfDocument pdf = new(writer);
        Document document = new(pdf);
        Generate(document, pdf, palletView);
        document.Close();
        return Convert.ToBase64String(stream.ToArray());
    }

    private static void Generate(Document doc, PdfDocument pdf, ViewPallet palletView)
    {
        SetDocumentFont(doc);
        AddPalletCardHeader(doc, palletView);
        AddDateTimeTable(doc, palletView);
        AddInfoTable(doc, palletView);
        AddPluTable(doc, palletView);
        AddBarcodeImage(doc, pdf);
    }

    private static void SetDocumentFont(Document doc)
    {
        try
        {
            doc.SetFont(PdfFontFactory.CreateFont(FontPath, "Identity-H"));
        }
        catch
        {
            // pass font error
        }
    }

    private static void AddPalletCardHeader(Document doc, ViewPallet palletView)
    {
        Paragraph paragraph = new Paragraph($"Паллетная карта № {palletView.Counter}")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetBold()
            .SetFontSize(16);
        doc.Add(paragraph);
    }

    private static void AddDateTimeTable(Document doc, ViewPallet palletView)
    {
        ITable dateTimeTable = new ITable(UnitValue.CreatePercentArray([60, 40]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);

        dateTimeTable.AddCell(new ICell().Add(new Paragraph("Дата")).SetBorder(Border.NO_BORDER));
        dateTimeTable.AddCell(new ICell().Add(new Paragraph("Время формирования")).SetBorder(Border.NO_BORDER));
        dateTimeTable.AddCell(new ICell().Add(new Paragraph(palletView.ProdDt.ToString("dd.MM.yyyy")).SetFontSize(40).SetBold()).SetBorder(Border.NO_BORDER));
        dateTimeTable.AddCell(new ICell().Add(new Paragraph(palletView.ProdDt.ToString("HH:mm:ss")).SetFontSize(30)).SetBorder(Border.NO_BORDER));

        doc.Add(dateTimeTable);
    }

    private static void AddInfoTable(Document doc, ViewPallet palletView)
    {
        ITable infoTable = new ITable(UnitValue.CreatePercentArray([60, 40]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);

        AddInfoTableRow(infoTable, "Вес нетто", palletView.WeightNet.ToString() ?? "");
        AddInfoTableRow(infoTable, "Вес брутто + Вес паллеты", palletView.WeightBrut.ToString() ?? "");
        AddInfoTableRow(infoTable, "Вес паллеты", "0");
        AddInfoTableRow(infoTable, "Количество коробок", palletView.LabelCount.ToString());
        AddInfoTableRow(infoTable, "Склад", palletView.Warehouse ?? "");
        AddInfoTableRow(infoTable, "Линия", palletView.Line ?? "");
        AddInfoTableRow(infoTable, "Замес", palletView.Kneading.ToString());

        doc.Add(infoTable);
    }

    private static void AddInfoTableRow(ITable table, string label, string value)
    {
        table.AddCell(new ICell().Add(new Paragraph(label)).SetBorder(Border.NO_BORDER));
        table.AddCell(value);
    }

    private static void AddPluTable(Document doc, ViewPallet palletView)
    {
        ITable pluTable = new ITable(UnitValue.CreatePercentArray([60, 10, 10, 10, 10]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);

        AddPluTableHeaders(pluTable);
        AddPluTableData(pluTable, palletView);

        doc.Add(pluTable);
    }

    private static void AddPluTableHeaders(ITable pluTable)
    {
        pluTable.AddCell("Номенклатура");
        pluTable.AddCell("Масса нетто кг.");
        pluTable.AddCell("Масса брутто кг.");
        pluTable.AddCell("Кол-во единиц товара шт.");
        pluTable.AddCell("Коробки шт.");
    }

    private static void AddPluTableData(ITable pluTable, ViewPallet palletView)
    {
        pluTable.AddCell(new Paragraph(palletView.Plu).SetBold().SetFontSize(30));
        pluTable.AddCell(palletView.WeightNet.ToString());
        pluTable.AddCell(palletView.WeightBrut.ToString());
        pluTable.AddCell("840");
        pluTable.AddCell(palletView.LabelCount.ToString());

        pluTable.AddCell(new Paragraph("Всего").SetBold());
        pluTable.AddCell(new Paragraph(palletView.WeightNet.ToString()).SetBold());
        pluTable.AddCell(new Paragraph(palletView.WeightBrut.ToString()).SetBold());
        pluTable.AddCell(new Paragraph("840").SetBold());
        pluTable.AddCell(new Paragraph(palletView.LabelCount.ToString()).SetBold());
    }

    private static void AddBarcodeImage(Document doc, PdfDocument pdf)
    {
        Barcode128 barcode = new(pdf);
        barcode.SetCode("(00)146071002341025233");
        Rectangle? barcodeSize = barcode.GetBarcodeSize();
        IImage barcodeImage = new(barcode.CreateFormXObject(pdf));
        float scaleFactor = PageSize.A4.GetWidth() / barcodeSize.GetWidth();
        barcodeImage.ScaleAbsolute(barcodeSize.GetWidth() * scaleFactor, barcodeSize.GetHeight() * scaleFactor);
        barcodeImage.SetMarginTop(barcodeImage.GetImageHeight() * 3);
        doc.Add(barcodeImage);
    }
}