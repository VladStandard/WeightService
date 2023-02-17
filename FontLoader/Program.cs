// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;

namespace FontLoader;

internal static class Program
{
    private static void Main(string[] args)
    {
        Arguments CommandLine = new(args);

        Console.WriteLine("Printer: " + CommandLine["Printer"]);
        Console.WriteLine("Font: " + CommandLine["Font"]);
        Console.WriteLine("test: " + CommandLine["test"]);
        Console.WriteLine("list: " + CommandLine["list"]);

        if ((CommandLine["Printer"] != null) && (CommandLine["Font"] != null))
        {
            DoCoreTask(CommandLine);
        }
        else
        {
            Console.Out.WriteLine("Example: FontLoader.exe -printer=\"ZDesigner ZD420 - 203dpi ZPL\"  -font=\"D:\\!vsprojects\\WeigthService2\\resources\\Fonts\\cour.ttf\" --Test --list");
        }

        //Console.Out.WriteLine("Arguments parsed. Press a key");
        //Console.Read();
        Environment.Exit(0);
    }

    private static void DoCoreTask(Arguments CommandLine)
    {
        string printerName = CommandLine["Printer"];
        string filePath = CommandLine["Font"];

        try
        {
            string namettf = Path.GetFileNameWithoutExtension(filePath).ToUpper();
            byte[] b = File.ReadAllBytes(filePath);
            //ushort crc = Crc16.ComputeChecksum(b);
            //string converted = Convert.ToBase64String(b);
            string converted = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ByteArrayToString(b);

            string ZPLCommand = $"^XA^IDE:{namettf}.TTF^FS^XZ";
            RawPrinterHelper.SendStringToPrinter(printerName, ZPLCommand);

            ZPLCommand = $"~DUE:{namettf}.TTF,{b.Length},{converted}";
            RawPrinterHelper.SendStringToPrinter(printerName, ZPLCommand);

            if (CommandLine["list"] != null)
            {
                ZPLCommand = $"^XA^WDE:*.TTF*^XZ";
                RawPrinterHelper.SendStringToPrinter(printerName, ZPLCommand);
            }

            if (CommandLine["test"] != null)
            {
                ZPLCommand = $"^XA" +
                             $"^CI28" +
                             $"^CWZ,E:{namettf}.TTF" +
                             $"^FO10,40" +
                             $"^CFW,18,12" +
                             $"^FB250,7,0,C,0" +
                             $"^FDTEST label\\&for font:\\&E:{ namettf}.TTF!" +
                             $"^FS" +
                             $"^FO290,40" +
                             $"^CFW,12,18" +
                             $"^FB250,7,0,L,0" +
                             $"^FDTEST label\\&for font:\\&E:{ namettf}.TTF!" +
                             $"^FS" +
                             $"^XZ";
                RawPrinterHelper.SendStringToPrinter(printerName, ZPLCommand);
            }

            Console.WriteLine($"Font {filePath} download.");
        }
        catch (Exception)
        {
            throw;
        }
    }
}