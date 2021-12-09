using System;
using System.IO;
using System.Text;


namespace FontLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments CommandLine = new Arguments(args);

            System.Console.WriteLine("Printer: " + CommandLine["Printer"]);
            System.Console.WriteLine("Font: " + CommandLine["Font"]);
            System.Console.WriteLine("test: " + CommandLine["test"]);
            System.Console.WriteLine("list: " + CommandLine["list"]);

            if ((CommandLine["Printer"]!=null)&&(CommandLine["Font"] != null)) {
               DoCoreTask(CommandLine);
            } else
            {
                Console.Out.WriteLine("Example: FontLoader.exe -printer=\"ZDesigner ZD420 - 203dpi ZPL\"  -font=\"D:\\!vsprojects\\WeigthService2\\resources\\Fonts\\cour.ttf\" --Test --list");
            }

            //Console.Out.WriteLine("Arguments parsed. Press a key");
            //Console.Read();
            Environment.Exit(0);
        }


        static void DoCoreTask(Arguments CommandLine)
        {

            string printerName = CommandLine["Printer"];
            string filePath = CommandLine["Font"];

            try
            {

                string namettf = Path.GetFileNameWithoutExtension(filePath).ToUpper();
                byte[] b = File.ReadAllBytes(filePath);
                //ushort crc = Crc16.ComputeChecksum(b);
                //string converted = Convert.ToBase64String(b);
                string converted = ByteArrayToString(b);

                string ZPLCommand = $"^XA^IDE:{namettf}.TTF^FS^XZ";
                log.Debug(ZPLCommand);
                RawPrinterHelper.SendStringToPrinter((string)printerName, ZPLCommand);

                ZPLCommand = $"~DUE:{namettf}.TTF,{b.Length},{converted}";
                log.Debug(ZPLCommand);
                RawPrinterHelper.SendStringToPrinter((string)printerName, ZPLCommand);

                if (CommandLine["list"] != null)
                {
                    ZPLCommand = $"^XA^WDE:*.TTF*^XZ";
                    log.Debug(ZPLCommand);
                    RawPrinterHelper.SendStringToPrinter((string)printerName, ZPLCommand);
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
                    log.Debug(ZPLCommand);
                    RawPrinterHelper.SendStringToPrinter((string)printerName, ZPLCommand);
                }

                System.Console.WriteLine($"Font {filePath} download.");

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

        }


        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }


    }

}
