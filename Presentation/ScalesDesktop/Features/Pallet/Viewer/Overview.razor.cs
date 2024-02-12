using System.Text;
using Append.Blazor.Printing;
using Blazorise;
using iText.Barcodes;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Font.Constants;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Border = iText.Layout.Borders.Border;
using Paragraph = iText.Layout.Element.Paragraph;
using IText = iText.Layout.Element.Text;
using ITable = iText.Layout.Element.Table;
using ICell = iText.Layout.Element.Cell;
using IImage = iText.Layout.Element.Image;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class Overview : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private IPrintingService PrintingService { get; set; } = null!;

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    private async Task PrintPalletCard()
    {
        MemoryStream stream = new();
        PdfWriter writer = new(stream);
        PdfDocument pdf = new(writer);
        Document document = new(pdf);

        try
        {
            document.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H"));
        }
        catch
        {
            // can not set font
        }
        
        GeneratePalletCardHtml(document, pdf);
        document.Close();
        
        string base64String = Convert.ToBase64String(stream.ToArray());
        await PrintingService.Print(new(base64String) {Base64 = true});
    }

    private void GeneratePalletCardHtml(Document doc, PdfDocument pdf)
    {
        Paragraph paragraph = new("Паллетная карта № 00165896");
        paragraph.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
        paragraph.SetBold();
        paragraph.SetFontSize(16);
        doc.Add(paragraph);
        
        ITable dateTimeTable = new ITable(UnitValue.CreatePercentArray([60, 40]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);

        ICell cell = new ICell().Add(new Paragraph("Дата")).SetBorder(Border.NO_BORDER);
        dateTimeTable.AddCell(cell);
        
        cell = new ICell().Add(new Paragraph("Время формирования")).SetBorder(Border.NO_BORDER);
        dateTimeTable.AddCell(cell);
        
        cell = new ICell().Add(new Paragraph("15.01.2024").SetFontSize(40).SetBold()).SetBorder(Border.NO_BORDER);
        dateTimeTable.AddCell(cell);
        
        cell = new ICell().Add(new Paragraph("0:00:00").SetFontSize(30)).SetBorder(Border.NO_BORDER);
        dateTimeTable.AddCell(cell);
        
        doc.Add(dateTimeTable);
        
        ITable infoTable = new ITable(UnitValue.CreatePercentArray([60, 40]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Вес нетто")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell("294,000");
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Вес брутто + Вес паллеты")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell("348,601");
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Вес паллеты")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell("0,001");
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Количество коробок")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell("140");
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Склад")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell("Склад фасовка 4");
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Линия")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell("Линия 14");
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Замес")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell("0");
        
        doc.Add(infoTable);
        
        ITable pluTable = new ITable(UnitValue.CreatePercentArray([60, 10, 10, 10, 10]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);
        
        pluTable.AddCell("Номенклатура");
        pluTable.AddCell("Масса нетто кг.");
        pluTable.AddCell("Масса брутто кг.");
        pluTable.AddCell("Кол-во единиц товара шт.");
        pluTable.AddCell("Коробки шт.");
        
        pluTable.AddCell(new Paragraph("Владимирский в.к 350 г").SetBold().SetFontSize(40));
        pluTable.AddCell("294,000");
        pluTable.AddCell("348,600");
        pluTable.AddCell("840");
        pluTable.AddCell("140");

        pluTable.AddCell(new Paragraph("Всего").SetBold());
        pluTable.AddCell(new Paragraph("294,000").SetBold());
        pluTable.AddCell(new Paragraph("348,600").SetBold());
        pluTable.AddCell(new Paragraph("840").SetBold());
        pluTable.AddCell(new Paragraph("140").SetBold());
        
        doc.Add(pluTable);
        
        Barcode128 barcode = new(pdf);
        barcode.SetCode("(00)146071002341025233");
        Rectangle? barcodeSize = barcode.GetBarcodeSize();
        IImage barcodeImage = new(barcode.CreateFormXObject(pdf));
        float scaleFactor = PageSize.A4.GetWidth() / barcodeSize.GetWidth();
        barcodeImage.ScaleAbsolute(barcodeSize.GetWidth() * scaleFactor, barcodeSize.GetHeight() * scaleFactor);
        barcodeImage.SetMarginTop(barcodeImage.GetImageHeight() * 3);
        doc.Add(barcodeImage);

    }

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}
