// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsPrintCore.Zpl;

namespace FontLoader;

internal static class Program
{
    private static void Main(string[] args)
    {
        Arguments commandLine = new(args);

        Console.WriteLine("Printer: " + commandLine["Printer"]);
        Console.WriteLine("Font: " + commandLine["Font"]);
        Console.WriteLine("test: " + commandLine["test"]);
        Console.WriteLine("list: " + commandLine["list"]);

        if ((commandLine["Printer"] != null) && (commandLine["Font"] != null))
        {
            DoCoreTask(commandLine);
        }
        else
        {
            Console.Out.WriteLine("Example: FontLoader.exe -printer=\"ZDesigner ZD420 - 203dpi ZPL\"  -font=\"D:\\!vsprojects\\WeigthService2\\resources\\Fonts\\cour.ttf\" --Test --list");
        }

        //Console.Out.WriteLine("Arguments parsed. Press a key");
        //Console.Read();
        Environment.Exit(0);
    }

    private static void DoCoreTask(Arguments commandLine)
    {
        string printerName = commandLine["Printer"];
        string filePath = commandLine["Font"];

        try
        {
            string namettf = Path.GetFileNameWithoutExtension(filePath).ToUpper();
            byte[] b = File.ReadAllBytes(filePath);
            //ushort crc = Crc16.ComputeChecksum(b);
            //string converted = Convert.ToBase64String(b);
            string converted = ZplUtils.ByteArrayToString(b);

            string zplCommand = $"^XA^IDE:{namettf}.TTF^FS^XZ";
            RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);

            zplCommand = $"~DUE:{namettf}.TTF,{b.Length},{converted}";
            RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);

            if (commandLine["list"] != null)
            {
                zplCommand = $"^XA^WDE:*.TTF*^XZ";
                RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);
            }

            if (commandLine["test"] != null)
            {
                zplCommand = @$"
^XA
^CI28
^CWZ,E:{namettf}.TTF
^FO10,40
^CFW,18,12
^FB250,7,0,C,0
^FDTEST label\\&for font:\\&E:{ namettf}.TTF!
^FS
^FO290,40
^CFW,12,18
^FB250,7,0,L,0
^FDTEST label\\&for font:\\&E:{ namettf}.TTF!
^FS
^XZ";
                RawPrinterHelper.SendStringToPrinter(printerName, zplCommand);
            }

            Console.WriteLine($"Font {filePath} download.");
        }
        catch (Exception)
        {
            throw;
        }
    }
}