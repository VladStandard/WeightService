using System.Globalization;
using iText.Barcodes;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Properties;
using Ws.Desktop.Models.Features.Pallets.Output;
using Border = iText.Layout.Borders.Border;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using ICell = iText.Layout.Element.Cell;
using IImage = iText.Layout.Element.Image;
using ITable = iText.Layout.Element.Table;
using Paragraph = iText.Layout.Element.Paragraph;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ScalesDesktop.Source.Shared.Utils;

public abstract class PalletCard
{
    private const string FontPath = "C:\\Windows\\Fonts\\arial.ttf";
    private const ushort BarcodeHeight = 120;

    public static string CreateBase64(PalletInfo palletView)
    {
        MemoryStream stream = new();
        PdfWriter writer = new(stream);
        PdfDocument pdf = new(writer);
        Document document = new(pdf);
        Generate(document, pdf, palletView);
        document.Close();
        return Convert.ToBase64String(stream.ToArray());
    }

    private static void Generate(Document doc, PdfDocument pdf, PalletInfo palletView)
    {
        SetDocumentFont(doc);
        AddPalletCardHeader(doc, palletView);
        AddDateTimeTable(doc, palletView);
        AddInfoTable(doc, palletView);
        AddPluTable(doc, palletView);
        AddBarcodeImage(doc, pdf, palletView.Barcode);
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

    private static void AddPalletCardHeader(Document doc, PalletInfo palletView)
    {
        Paragraph paragraph = new Paragraph($"Паллетная карта № {palletView.Number}")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetBold()
            .SetFontSize(16);
        doc.Add(paragraph);
    }

    private static void AddDateTimeTable(Document doc, PalletInfo palletView)
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

    private static void AddInfoTable(Document doc, PalletInfo palletView)
    {
        ITable infoTable = new ITable(UnitValue.CreatePercentArray([60, 40]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);

        AddInfoTableRow(infoTable, "Вес нетто", palletView.WeightNet.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, "Вес брутто", palletView.WeightBrutto.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, "Вес паллеты", palletView.WeightTray.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, "Вес брутто + Вес паллеты", (palletView.WeightBrutto + palletView.WeightTray).ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, "Количество коробок", palletView.BoxCount.ToString());
        AddInfoTableRow(infoTable, "Склад", palletView.Warehouse);
        AddInfoTableRow(infoTable, "Линия", palletView.Arm);
        AddInfoTableRow(infoTable, "Сдатчик", palletView.PalletMan.DisplayShortName);
        AddInfoTableRow(infoTable, "Замес", string.Join(",", palletView.Kneadings.Select(k => k.ToString()).ToArray()));

        doc.Add(infoTable);
    }

    private static void AddInfoTableRow(ITable table, string label, string value)
    {
        table.AddCell(new ICell().Add(new Paragraph(label)).SetBorder(Border.NO_BORDER));
        table.AddCell(value);
    }

    private static void AddPluTable(Document doc, PalletInfo palletView)
    {
        ITable pluTable = new ITable(UnitValue.CreatePercentArray([60, 10, 10, 10, 10]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);

        AddPluTableHeaders(pluTable);
        foreach(PluPalletInfo plu in palletView.Plus)
            AddPluTableData(pluTable, plu);
        AddTableFooter(pluTable, palletView);

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

    private static void AddTableFooter(ITable pluTable, PalletInfo palletView)
    {
        pluTable.AddCell(new Paragraph("Всего").SetBold());
        pluTable.AddCell(new Paragraph(palletView.WeightNet.ToString(CultureInfo.InvariantCulture)).SetBold());
        pluTable.AddCell(new Paragraph(palletView.WeightBrutto.ToString(CultureInfo.InvariantCulture)).SetBold());
        pluTable.AddCell(new Paragraph(palletView.PieceCount.ToString()).SetBold());
        pluTable.AddCell(new Paragraph(palletView.BoxCount.ToString()).SetBold());
    }

    private static void AddPluTableData(ITable pluTable, PluPalletInfo plu)
    {
        pluTable.AddCell(new Paragraph(plu.Name).SetBold().SetFontSize(30));
        pluTable.AddCell(plu.WeightNet.ToString(CultureInfo.InvariantCulture));
        pluTable.AddCell(plu.WeightBrutto.ToString(CultureInfo.InvariantCulture));
        pluTable.AddCell(plu.PieceCount.ToString());
        pluTable.AddCell(plu.BoxCount.ToString());
    }

    private static void AddBarcodeImage(Document doc, PdfDocument pdf, string barcodeCode)
    {
        Barcode128 barcode = new(pdf);
        barcode.SetCode(barcodeCode);
        Rectangle? barcodeSize = barcode.GetBarcodeSize();
        IImage barcodeImage = new(barcode.CreateFormXObject(pdf));
        float scaleFactor = PageSize.A4.GetWidth() / barcodeSize.GetWidth();
        float tooBigScaleFactor = barcodeSize.GetHeight() * scaleFactor / BarcodeHeight;
        if (tooBigScaleFactor > 1) scaleFactor /= tooBigScaleFactor;
        barcodeImage.ScaleAbsolute(barcodeSize.GetWidth() * scaleFactor, barcodeSize.GetHeight() * scaleFactor);
        barcodeImage.SetMarginTop(10);
        barcodeImage.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        doc.Add(barcodeImage);
    }
}