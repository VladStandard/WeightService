// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using System.Xml;
using System.Xml.Xsl;
using WeightCore.Print.Native;

namespace WeightCore.Zpl
{
    public static class ZplPipeUtils
    {
        #region Public and private fields and properties

        public static char[] DigitsCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static char[] SpecialCharacters = { 
            ' ', ',', '.', '-', 
            '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
            '"', '№', ';', ':', '?',
            '/', '|', '\\', '{', '}', '<', '>'
        };

        #endregion

        #region pipe's

        public static string FakePipe(string zplCommand) => zplCommand;

        public static void XmlReplace(ref string xmlInput)
        {
            xmlInput = xmlInput.Replace(nameof(BarCodeDirect), "BarCodeEntity");
            xmlInput = xmlInput.Replace(nameof(ContregentDirect), "ContregentEntity");
            xmlInput = xmlInput.Replace(nameof(HostDirect), "HostEntity");
            xmlInput = xmlInput.Replace(nameof(LogDirect), "LogEntity");
            xmlInput = xmlInput.Replace(nameof(NomenclatureDirect), "NomenclatureEntity");
            xmlInput = xmlInput.Replace(nameof(OrderDirect), "OrderEntity");
            xmlInput = xmlInput.Replace(nameof(PluDirect), "PluEntity");
            xmlInput = xmlInput.Replace(nameof(ProductionFacilityDirect), "ProductionFacilityEntity");
            xmlInput = xmlInput.Replace(nameof(ProductSeriesDirect), "ProductSeriesEntity");
            xmlInput = xmlInput.Replace(nameof(ScaleDirect), "ScaleEntity");
            xmlInput = xmlInput.Replace(nameof(SsccDirect), "SsccEntity");
            xmlInput = xmlInput.Replace(nameof(TaskDirect), "TaskEntity");
            xmlInput = xmlInput.Replace(nameof(TemplateDirect), "TemplateEntity");
            xmlInput = xmlInput.Replace(nameof(WeighingFactDirect), "WeighingFactEntity");
            xmlInput = xmlInput.Replace(nameof(WorkShopDirect), "WorkShopEntity");
            xmlInput = xmlInput.Replace(nameof(ZebraPrinterHelper), "ZebraPrinterEntity");
            xmlInput = xmlInput.Replace(nameof(ZplLabelDirect), "ZplLabelEntity");
        }

        public static string XsltTransformationPipe(string xslInput, string xmlInput, bool useEntityReplace)
        {
            if (useEntityReplace)
                XmlReplace(ref xmlInput);
            string result;
            using (StringReader stringReaderXslt = new(xslInput.Trim())) // xslInput is a string that contains xsl
            {
                using StringReader stringReaderXml = new(xmlInput.Trim()); // xmlInput is a string that contains xml
                using XmlReader xmlReaderXslt = XmlReader.Create(stringReaderXslt);
                using XmlReader xmlReaderXml = XmlReader.Create(stringReaderXml);
                XslCompiledTransform xslt = new();
                xslt.Load(xmlReaderXslt);
                using StringWriter stringWriter = new();
                using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings); // use OutputSettings of xsl, so it can be output as HTML
                xslt.Transform(xmlReaderXml, xmlWriter);
                result = stringWriter.ToString();
                result = ToCodePoints(result);
            }
            return result;
        }

        //[Obsolete(@"Deprecated method")]
        //public string ZplCommandPipe(string zplCommand)
        //{
        //    var outMsg = new StringBuilder();
        //    try
        //    {
        //        string _zpl = ToCodePoints(zplCommand);
        //        string _errorMessage = string.Empty;
        //        string info = InterplayToPrinter(IpAddress, Port, _zpl.Split('\n'), out _errorMessage);
        //        outMsg.AppendLine(info);
        //        outMsg.AppendLine(_errorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        outMsg.AppendLine(zplCommand);
        //        outMsg.AppendLine(ex.Message);
        //    }
        //    return outMsg.ToString();
        //}

        public static string ZplCommandPipeByIp(string ip, int port, string zplCommand)
        {
            StringBuilder outMsg = new();
            try
            {

                string zpl = ToCodePoints(zplCommand);
                string info = InterplayToPrinter(ip, port, zpl.Split('\n'), out string errorMessage);
                outMsg.AppendLine(info);
                outMsg.AppendLine(errorMessage);
            }
            catch (Exception ex)
            {
                outMsg.AppendLine(zplCommand);
                outMsg.AppendLine(ex.Message);
            }
            return outMsg.ToString();
        }

        public static string ZplCommandPipeByRaw(string lptName, string zplCommand)
        {
            StringBuilder outMsg = new();
            try
            {
                string zpl = ToCodePoints(zplCommand);
                RawPrinterHelper.SendStringToPrinter(lptName, zpl);
            }
            catch (Exception ex)
            {
                outMsg.AppendLine(zplCommand);
                outMsg.AppendLine(ex.Message);
            }
            return outMsg.ToString();
        }
        
        #endregion

        #region Commands
        
        public static string ZplHostQuery(string prm = "ES") => $"~HQ{prm}";

        public static string ZplFontsClear() => @"^XA^IDE:*.TTF^FS^XZ";

        public static string ZplLogoClear() => @"^XA^IDE:*.GRF^FS^XZ";

        public static string ZplCalibration() => "! U1 setvar \"media.type\" \"label\"\r\n" + "! U1 setvar \"media.sense_mode\" \"gap\"\r\n" + "~JC^XA^JUS^XZ";

        public static string ZplFilesDelete(string mask = "E:*.*") => $"! U1 do \"file.delete\" \"{mask}\"\r\n";

        public static string ZplFilesList(string mask = "E:*.*") => $"! U1 do \"file.dir\" \"{mask}\"\r\n";

        public static string ZplSetOdometerUserLabel(int value = 1) => $"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n";

        public static string ZplGetOdometerUserLabel() => $"! U1 getvar \"odometer.user_label_count\"\r\n";

        public static string ZplPrintDirectory(string mask = "E:*.*") => $"^XA^WD{mask}^XZ";

        public static string ZplPowerOnReset() => $"~JR";

        public static string ZplPeelerState() => $"! U1 getvar \"sensor.peeler\"\r\n";

        public static string ZplPrintConfigurationLabel() => $"~WC";

        public static string ZplHostStatusReturn() => $"~HS";

        public static string ZplClearPrintBuffer() => $"^XA~JA^XZ";

        public static string ZplFontDownloadCommand(string ttfName, byte[] b, bool addHeaderFooter = true)
        {
            string converted = ByteConverter.ByteArrayToString(b);
            // так чейт по сети не работает
            //string zplCode = $"~DUE:{ttfName.ToUpper().Replace(".TTF", "")}.TTF,{b.Length},{converted}";
            string zplCode = $"~DYE:{ttfName.ToUpper().Replace(".TTF", "")}.TTF,B,T,{b.Length},,{converted}";
            if (addHeaderFooter)
            {
                zplCode = "^XA " + zplCode + "^XZ";
            }
            return zplCode;
        }

        public static string ZplFontDownloadCommand(string ttfName, string Value, bool addHeaderFooter = true)
        {
            //var binaryData = Convert.FromBase64String(Value);
            //return ZplFontDownloadCommand(ttfName, binaryData);
            byte[] b = Convert.FromBase64String(Value);
            string converted = ByteConverter.ByteArrayToString(b);
            //string zplCode = $"~DUE:{ttfName.ToUpper().Replace(".TTF", "")}.TTF,{b.Length},{converted}";
            string zplCode = $"~DYE:{ttfName.ToUpper().Replace(".TTF", "")}.TTF,B,T,{b.Length},,{converted}";
            if (addHeaderFooter)
            {
                zplCode = "^XA " + zplCode + "^XZ";
            }
            return zplCode;
        }

        public static string ZplLogoDownloadCommand(string imageName, string image, bool addHeaderFooter = true)
        {
            byte[] b = Convert.FromBase64String(FixBase64ForImage(image));
            using MemoryStream bmpStream = new(b);
            System.Drawing.Image img = System.Drawing.Image.FromStream(bmpStream);
            System.Drawing.Bitmap bitmapData = new(img);
            return ZplLogoDownloadCommand(imageName, bitmapData, addHeaderFooter);
        }

        public static string ZplLogoDownloadCommand(string imageName, System.Drawing.Bitmap image, bool addHeaderFooter = true)
        {

            System.Drawing.Imaging.BitmapData imgData = null;
            byte[] pixels;
            int x, y, width;
            StringBuilder sb;
            IntPtr ptr;

            try
            {
                imgData = image.LockBits(
                    new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format1bppIndexed);

                width = (image.Width + 7) / 8;
                pixels = new byte[width];
                sb = new StringBuilder(width * image.Height * 2);
                ptr = imgData.Scan0;
                for (y = 0; y < image.Height; y++)
                {
                    Marshal.Copy(ptr, pixels, 0, width);
                    for (x = 0; x < width; x++)
                        sb.AppendFormat("{0:X2}", (byte)~pixels[x]);
                    ptr = (IntPtr)(ptr.ToInt64() + imgData.Stride);
                }
            }
            finally
            {
                if (image != null)
                {
                    if (imgData != null) image.UnlockBits(imgData);
                    image.Dispose();
                }
            }

            string zplCode = $"~DGE:{imageName.ToUpper().Replace(".BMP", "")}.GRF,{width * y},{width}," + sb;
            if (addHeaderFooter)
            {
                zplCode = "^XA " + zplCode + "^XZ";
            }
            return zplCode;
        }

        //public static string ZplContentAsBase64(string base64String)
        //{
        //    //с наскока не сработало
        //    byte[] b = Encoding.ASCII.GetBytes(base64String);
        //    Crc16Ccitt crc = new(PrintInitialCrcValue.Zeros);
        //    byte[] c = crc.ComputeChecksumBytes(b);
        //    //Encode the compressed data into Base64. No whitespace or line breaks allowed.
        //    //Convert the Base64 string to a byte array according to ASCII encoding().
        //    //Calculate the CRC over that byte array. The Initial CRC Value must be zero.
        //    return $"{b.Length},:B64:{base64String}:{ByteConverter.ByteArrayToString(c)}";
        //}

        private static string FixBase64ForImage(string image)
        {
            StringBuilder sbText = new(image, image.Length);
            sbText.Replace("\r\n", string.Empty);
            sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }

        #endregion

        #region Other methods

        public static string InterplayToPrinter(string ipAddress, int port, string[] zplCommand, 
            out string errorMessage, int receiveTimeout = 1000, int sendTimeout = 100)
        {
            errorMessage = @"";
            StringBuilder response = new();

            using (TcpClient client = new())
            {
                //client.NoDelay = true;
                client.ReceiveTimeout = receiveTimeout;
                client.SendTimeout = sendTimeout;

                client.Connect(ipAddress, port);
                using NetworkStream stream = client.GetStream();
                using StreamWriter writer = new(stream);
                writer.AutoFlush = true;
                using StreamReader reader = new(stream);
                try
                {
                    foreach (string commandLine in zplCommand)
                    {
                        writer.Write(Encoding.ASCII.GetChars(Encoding.ASCII.GetBytes(commandLine)), 0, commandLine.Length);
                        writer.Flush();
                    }
                    byte[] data = new byte[256];
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);

                        if (bytes > 0)
                        {
                            response.Append(Encoding.UTF8.GetString(data, 0, bytes));
                        }
                    }
                    while (stream.DataAvailable);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is SocketException sockEx)
                    {
                        errorMessage = @"(" + sockEx.NativeErrorCode + ") Exception = " + ex.Message;
                    }
                    throw;
                }
            }
            return response.ToString();
        }

        public static bool IsCyrillic(char value)
        {
            char[] cyrillic = Enumerable
                .Range(UnicodeRanges.Cyrillic.FirstCodePoint, UnicodeRanges.Cyrillic.Length)
                .Select(ch => (char)ch)
                .ToArray();
            return Array.BinarySearch(cyrillic, value) >= 0;
        }

        public static bool IsDigit(char value)
        {
            return DigitsCharacters.Contains(value);
        }

        public static bool IsSpecial(char value, bool isExcludeTop = true)
        {
            if (isExcludeTop && value == '^')
                return false;
            return SpecialCharacters.Contains(value);
        }

        [Obsolete(@"Use ToCodePoints")]
        public static string ToCodePointsOld(string zplInput)
        {
            StringBuilder result = new();
            Dictionary<char, string> unicodeCharacterList = new();
            foreach (char ch in zplInput)
            {
                if (!unicodeCharacterList.ContainsKey(ch))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(ch.ToString());
                    if (bytes.Length > 1)
                    {
                        string hexCode = string.Empty;
                        foreach (byte b in bytes)
                        {
                            hexCode += $"_{BitConverter.ToString(new byte[] { b }).ToLower()}";
                        }
                        unicodeCharacterList[ch] = hexCode;
                    }
                    else
                        unicodeCharacterList[ch] = ch.ToString();
                    result.Append(unicodeCharacterList[ch]);
                }
                else
                // English characters.
                {
                    result.Append(unicodeCharacterList[ch]);
                }
            }
            return result.ToString();
        }

        public static string ToCodePoints(string zplInput)
        {
            StringBuilder result = new();
            Dictionary<char, string> unicodeCharacterList = new();
            // Поиск подстроки [^FH^FD].
            int isFieldData = 0;
            bool isDataStart = false;
            bool isDataEnd = false;
            foreach (char ch in zplInput)
            {
                if (isFieldData == 6)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(ch.ToString());
                    string hexCode = string.Empty;
                    foreach (byte b in bytes)
                    {
                        hexCode += $"_{BitConverter.ToString(new byte[] {b}).ToUpper()}";
                    }

                    unicodeCharacterList[ch] = hexCode;
                }
                else
                {
                    unicodeCharacterList[ch] = ch.ToString();
                }

                // Calc isFieldData. ^FH^FD
                if (isFieldData == 0 && ch == '^')
                    isFieldData = 1;
                if (isFieldData == 1 && ch == 'F')
                    isFieldData = 2;
                if (isFieldData == 2 && ch == 'H')
                    isFieldData = 3;
                if (isFieldData == 3 && ch == '^')
                    isFieldData = 4;
                if (isFieldData == 4 && ch == 'F')
                    isFieldData = 5;
                if (isFieldData == 5 && ch == 'D')
                    isFieldData = 6;

                // Reset isFieldData. ^FS
                if (isFieldData == 6 && ch == '^')
                    isFieldData = 7;
                if (isFieldData == 7 && ch == 'F')
                    isFieldData = 8;
                if (isFieldData == 8 && ch == 'S')
                {
                    isFieldData = 0;
                    isDataStart = false;
                    isDataEnd = false;
                }
                if (isFieldData < 7)
                {
                    result.Append(unicodeCharacterList[ch]);
                    if (isFieldData == 6 && !isDataStart)
                    {
                        isDataStart = true;
                        result.Append(Environment.NewLine);
                    }
                }
                else
                {
                    if (isFieldData == 7 && !isDataEnd)
                    {
                        isDataEnd = true;
                        result.Append(Environment.NewLine);
                        result.Append("^");
                    }
                    else
                        result.Append(unicodeCharacterList[ch]);
                }
            }
            return result.ToString();
        }

        #endregion
    }
}