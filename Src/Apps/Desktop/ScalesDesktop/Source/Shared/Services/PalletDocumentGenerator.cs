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

namespace ScalesDesktop.Source.Shared.Services;

public class PalletDocumentGenerator(IStringLocalizer<WsDataResources> wsDataLocalizer)
{
    private const string FontPath = "C:\\Windows\\Fonts\\arial.ttf";
    private const ushort BarcodeHeight = 120;

    public string CreateBase64(PalletInfo pallet)
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

    private void FillDocumentWithContent(Document doc, PdfDocument pdf, PalletInfo pallet)
    {
        doc.Add(GenerateHeader(pallet));
        doc.Add(GenerateDateTimeTable(pallet));
        doc.Add(GenerateInfoTable(pallet));
        doc.Add(GeneratePluTable(pallet));
        doc.Add(GenerateBarcode(pdf, pallet.Barcode));
    }

    private Paragraph GenerateHeader(PalletInfo pallet) =>
        new Paragraph($"{wsDataLocalizer["ColPalletCard"]} №{pallet.Number}")
            .SetTextAlignment(TextAlignment.CENTER).SetBold().SetFontSize(16).SetMarginBottom(10);

    private ITable GenerateDateTimeTable(PalletInfo pallet)
    {
        ITable dateTimeTable = CreateTable([60f, 40f]);

        dateTimeTable.AddCell(new ICell().Add(new Paragraph(wsDataLocalizer["ColDate"])).SetBorder(Border.NO_BORDER));
        dateTimeTable.AddCell(new ICell().SetBorder(Border.NO_BORDER));
        dateTimeTable.AddCell(new ICell().Add(new Paragraph(pallet.ProdDt.ToString("dd.MM.yyyy")).SetFontSize(40).SetBold()).SetBorder(Border.NO_BORDER));
        return dateTimeTable;
    }

    private ITable GenerateInfoTable(PalletInfo pallet)
    {
        ITable infoTable = CreateTable([60f, 40f]).SetMarginBottom(20);

        AddInfoTableRow(infoTable, wsDataLocalizer["ColNetWeight"], pallet.WeightNet.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, wsDataLocalizer["ColGrossWeight"], pallet.WeightBrutto.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, wsDataLocalizer["ColTrayWeight"], pallet.WeightTray.ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, $"{wsDataLocalizer["ColGrossWeight"]} + {wsDataLocalizer["ColTrayWeight"]}", (pallet.WeightBrutto + pallet.WeightTray).ToString(CultureInfo.InvariantCulture));
        AddInfoTableRow(infoTable, wsDataLocalizer["ColBoxCount"], pallet.BoxCount.ToString());
        AddInfoTableRow(infoTable, wsDataLocalizer["ColWarehouse"], pallet.Warehouse);
        AddInfoTableRow(infoTable, wsDataLocalizer["ColLine"], pallet.Arm);
        AddInfoTableRow(infoTable, wsDataLocalizer["ColPalletMan"], pallet.PalletMan.DisplayFullName);
        AddInfoTableRow(infoTable, wsDataLocalizer["ColKneading"], string.Join(",", pallet.Kneadings.Select(k => k.ToString()).ToArray()));

        return infoTable;
    }

    private static void AddInfoTableRow(ITable table, string label, string value)
    {
        table.AddCell(new ICell().Add(new Paragraph(label)).SetBorder(Border.NO_BORDER));
        table.AddCell(value);
    }

    private ITable GeneratePluTable(PalletInfo pallet)
    {
        ITable pluTable = CreateTable([60f, 10f, 10f, 10f, 10f]).SetMarginBottom(20);

        AddPluTableHeaders(pluTable);
        foreach(PluPalletInfo plu in pallet.Plus)
            AddPluTableData(pluTable, plu);
        AddPluTableFooter(pluTable, pallet);

        return pluTable;
    }

    private void AddPluTableHeaders(ITable table)
    {
        table.AddCell(wsDataLocalizer["ColNomenclature"]);
        table.AddCell($"{wsDataLocalizer["ColNetWeight"]} {wsDataLocalizer["MeasureKg"]}");
        table.AddCell($"{wsDataLocalizer["ColGrossWeight"]} {wsDataLocalizer["MeasureKg"]}");
        table.AddCell($"{wsDataLocalizer["ColProductUnitsCount"]} {wsDataLocalizer["MeasurePc"]}");
        table.AddCell($"{wsDataLocalizer["ColBoxes"]} {wsDataLocalizer["MeasurePc"]}");
    }

    private void AddPluTableFooter(ITable table, PalletInfo pallet)
    {
        table.AddCell(new Paragraph(wsDataLocalizer["ColTotal"]).SetBold());
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