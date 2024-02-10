using System.Text;
using Append.Blazor.Printing;
using Blazorise;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Font.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Paragraph = iText.Layout.Element.Paragraph;
using IText = iText.Layout.Element.Text;

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
        
        GeneratePalletCardHtml(document);
        document.Close();
        
        string base64String = Convert.ToBase64String(stream.ToArray());
        await PrintingService.Print(new(base64String) {Base64 = true});
    }

    private void GeneratePalletCardHtml(Document doc)
    {
        Paragraph paragraph = new("Паллетная карта № 00165896");
        paragraph.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
        paragraph.SetBold();
        paragraph.SetFontSize(16);
        doc.Add(paragraph);
    }

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}
