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
using Ws.Domain.Models.Entities.Print;
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

    private ViewPallet Pallet => PalletContext.CurrentPallet;

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
        Paragraph paragraph = new($"Паллетная карта № {Pallet.Counter}");
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
        
        cell = new ICell().Add(new Paragraph(Pallet.ProdDt.ToString("dd.MM.yyyy")).SetFontSize(40).SetBold()).SetBorder(Border.NO_BORDER);
        dateTimeTable.AddCell(cell);
        
        cell = new ICell().Add(new Paragraph(Pallet.ProdDt.ToString("HH:mm:ss")).SetFontSize(30)).SetBorder(Border.NO_BORDER);
        dateTimeTable.AddCell(cell);
        
        doc.Add(dateTimeTable);
        
        ITable infoTable = new ITable(UnitValue.CreatePercentArray([60, 40]))
            .SetWidth(UnitValue.CreatePercentValue(100))
            .SetBorderCollapse(BorderCollapsePropertyValue.COLLAPSE)
            .SetMarginBottom(10);
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Вес нетто")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell(Pallet.WeightNet.ToString());
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Вес брутто + Вес паллеты")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell(Pallet.WeightBrut.ToString());
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Вес паллеты")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell("0");
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Количество коробок")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell(Pallet.LabelCount.ToString());
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Склад")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell(Pallet.Warehouse);
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Линия")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell(Pallet.Line);
        
        infoTable.AddCell(new ICell().Add(new Paragraph("Замес")).SetBorder(Border.NO_BORDER));
        infoTable.AddCell(Pallet.Kneading.ToString());
        
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
        
        pluTable.AddCell(new Paragraph(Pallet.Plu).SetBold().SetFontSize(30));
        pluTable.AddCell(Pallet.WeightNet.ToString());
        pluTable.AddCell(Pallet.WeightBrut.ToString());
        pluTable.AddCell("840");
        pluTable.AddCell(Pallet.LabelCount.ToString());

        pluTable.AddCell(new Paragraph("Всего").SetBold());
        pluTable.AddCell(new Paragraph(Pallet.WeightNet.ToString()).SetBold());
        pluTable.AddCell(new Paragraph(Pallet.WeightBrut.ToString()).SetBold());
        pluTable.AddCell(new Paragraph("840").SetBold());
        pluTable.AddCell(new Paragraph(Pallet.LabelCount.ToString()).SetBold());
        
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
