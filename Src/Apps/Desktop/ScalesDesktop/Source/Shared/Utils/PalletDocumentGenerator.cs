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

public abstract class PalletDocumentGenerator
{
    private const string FontPath = "C:\\Windows\\Fonts\\arial.ttf";
    private const ushort BarcodeHeight = 120;

    public static string CreateBase64(PalletInfo pallet)
    {
        MemoryStream stream = new();
        PdfWriter writer = new(stream);
        PdfDocument pdf = new(writer);
        Document document = new(pdf);

        SetDocumentFont(document, FontPath);
        FillDocumentWithContent(document, pdf, pallet);

        document.Close();
        return Convert.ToBase64String(stream.ToArray());
    }

    # region Generating card

    private static void FillDocumentWithContent(Document doc, PdfDocument pdf, PalletInfo pallet)
    {
        doc.Add(GenerateHeader(pallet));
        doc.Add(GenerateDateTimeTable(pallet));
        doc.Add(GenerateInfoTable(pallet));
        doc.Add(GeneratePluTable(pallet));
        doc.Add(GenerateBarcode(pdf, pallet.Barcode));
    }

    private static Paragraph GenerateHeader(PalletInfo pallet) =>
        new Paragraph($"Паллетная карта № {pallet.Number}")
            .SetTextAlignment(TextAlignment.CENTER).SetBold().SetFontSize(16).SetMarginBottom(10);

    private static ITable GenerateDateTimeTable(PalletInfo pallet)
    {
        ITable dateTimeTable = CreateTable([60f, 40f]);

        dateTimeTable.AddCell(new ICell().Add(new Paragraph("Дата")).SetBorder(Border.NO_BORDER));
        dateTimeTable.AddCell(new ICell().Add(new Paragraph("Время формирования")).SetBorder(Border.NO_BORDER));
        dateTimeTable.AddCell(new ICell().Add(new Paragraph(pallet.ProdDt.ToString("dd.MM.yyyy")).SetFontSize(40).SetBold()).SetBorder(Border.NO_BORDER));
        dateTimeTable.AddCell(new ICell().Add(new Paragraph(pallet.ProdDt.ToString("HH:mm:ss")).SetFontSize(30)).SetBorder(Border.NO_BORDER));

        return dateTimeTable;
    }

    private static ITable GenerateInfoTable(PalletInfo pallet)
    {
        ITable infoTable = CreateTable([60f, 40f]).SetMarginBottom(20);

        AddInfoTableRow(infoTable, "Вес нетто", pallet.WeightNet.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, "Вес брутто", pallet.WeightBrutto.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, "Вес паллеты", pallet.WeightTray.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, "Вес брутто + Вес паллеты", (pallet.WeightBrutto + pallet.WeightTray).ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, "Количество коробок", pallet.BoxCount.ToString());
        AddInfoTableRow(infoTable, "Склад", pallet.Warehouse);
        AddInfoTableRow(infoTable, "Линия", pallet.Arm);
        AddInfoTableRow(infoTable, "Сдатчик", pallet.PalletMan.DisplayFullName);
        AddInfoTableRow(infoTable, "Замес", string.Join(",", pallet.Kneadings.Select(k => k.ToString()).ToArray()));

        return infoTable;
    }

    private static void AddInfoTableRow(ITable table, string label, string value)
    {
        table.AddCell(new ICell().Add(new Paragraph(label)).SetBorder(Border.NO_BORDER));
        table.AddCell(value);
    }

    private static ITable GeneratePluTable(PalletInfo pallet)
    {
        ITable pluTable = CreateTable([60f, 10f, 10f, 10f, 10f]).SetMarginBottom(20);

        AddPluTableHeaders(pluTable);
        foreach(PluPalletInfo plu in pallet.Plus)
            AddPluTableData(pluTable, plu);
        AddPluTableFooter(pluTable, pallet);

        return pluTable;
    }

    private static void AddPluTableHeaders(ITable table)
    {
        table.AddCell("Номенклатура");
        table.AddCell("Масса нетто кг.");
        table.AddCell("Масса брутто кг.");
        table.AddCell("Кол-во единиц товара шт.");
        table.AddCell("Коробки шт.");
    }

    private static void AddPluTableFooter(ITable table, PalletInfo pallet)
    {
        table.AddCell(new Paragraph("Всего").SetBold());
        table.AddCell(new Paragraph(pallet.WeightNet.ToString(CultureInfo.InvariantCulture)).SetBold());
        table.AddCell(new Paragraph(pallet.WeightBrutto.ToString(CultureInfo.InvariantCulture)).SetBold());
        table.AddCell(new Paragraph(pallet.BundleCount.ToString()).SetBold());
        table.AddCell(new Paragraph(pallet.BoxCount.ToString()).SetBold());
    }

    private static void AddPluTableData(ITable table, PluPalletInfo plu)
    {
        table.AddCell(new Paragraph(plu.Name).SetBold().SetFontSize(30));
        table.AddCell(plu.WeightNet.ToString(CultureInfo.InvariantCulture));
        table.AddCell(plu.WeightBrutto.ToString(CultureInfo.InvariantCulture));
        table.AddCell(plu.BundleCount.ToString());
        table.AddCell(plu.BoxCount.ToString());
    }

    private static IImage GenerateBarcode(PdfDocument pdf, string barcodeCode)
    {
        Barcode128 barcode = new(pdf);
        barcode.SetCode(barcodeCode);
        Rectangle? barcodeSize = barcode.GetBarcodeSize();
        if (barcodeSize == null) throw new InvalidOperationException("Failed to get the barcode size.");

        IImage barcodeImage = new(barcode.CreateFormXObject(pdf));
        float scaleFactor = CalculateBarcodeScaleFactor(barcodeSize, BarcodeHeight);
        barcodeImage.ScaleAbsolute(barcodeSize.GetWidth() * scaleFactor, barcodeSize.GetHeight() * scaleFactor);
        barcodeImage.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        return barcodeImage;
    }

    # endregion

    # region Utils

    private static void SetDocumentFont(Document doc, string fontPath)
    {
        try
        {
            doc.SetFont(PdfFontFactory.CreateFont(fontPath, "Identity-H"));
        }
        catch
        {
            // pass font error
        }
    }

    private static ITable CreateTable(float[] columnWidths) =>
        new ITable(UnitValue.CreatePercentArray(columnWidths))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE);

    private static float CalculateBarcodeScaleFactor(Rectangle barcodeSize, float targetBarcodeHeight)
    {
        float pageWidth = PageSize.A4.GetWidth();
        float scaleFactor = pageWidth / barcodeSize.GetWidth();
        float tooBigScaleFactor = barcodeSize.GetHeight() * scaleFactor / targetBarcodeHeight;
        if (tooBigScaleFactor > 1) scaleFactor /= tooBigScaleFactor;
        return scaleFactor;
    }

    # endregion
}