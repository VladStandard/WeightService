// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Utils;
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
using static DataCore.ShareEnums;
using TableDirectModels = DataCore.Sql.TableDirectModels;
using TableScaleModels = DataCore.Sql.TableScaleModels;

namespace WeightCore.Zpl
{
    public static class ZplUtils
    {
        #region Public and private fields and properties

        public static char[] DigitsCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static char[] SpecialCharacters = { ' ', ',', '.', '-', '~', '!', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '=',
            '"', '№', ';', ':', '?', '/', '|', '\\', '{', '}', '<', '>' };

        #endregion

        #region pipe's

        public static string XmlCompatibleReplace(string xmlInput)
        {
            string result = xmlInput;
            if (string.IsNullOrEmpty(result))
                return result;
            // TableDirectModels.
            result = result.Replace(nameof(TableDirectModels.HostDirect), "HostEntity");
            result = result.Replace(nameof(TableDirectModels.NomenclatureDirect), "NomenclatureEntity");
            result = result.Replace(nameof(TableDirectModels.OrderDirect), "OrderEntity");
            result = result.Replace(nameof(TableDirectModels.PluDirect), "PluEntity");
            result = result.Replace(nameof(TableDirectModels.PrinterDirect), "ZebraPrinterEntity");
            result = result.Replace(nameof(TableDirectModels.ProductionFacilityDirect), "ProductionFacilityEntity");
            result = result.Replace(nameof(TableDirectModels.ProductSeriesDirect), "ProductSeriesEntity");
            result = result.Replace(nameof(TableDirectModels.SsccDirect), "SsccEntity");
            result = result.Replace(nameof(TableDirectModels.TaskDirect), "TaskEntity");
            result = result.Replace(nameof(TableDirectModels.TemplateDirect), "TemplateEntity");
            result = result.Replace(nameof(TableDirectModels.WeighingFactDirect), "WeighingFactEntity");
            result = result.Replace(nameof(TableDirectModels.WorkShopDirect), "WorkShopEntity");
            result = result.Replace(nameof(TableDirectModels.ZplLabelDirect), "ZplLabelEntity");
            // TableScaleModels.
            result = result.Replace(nameof(TableScaleModels.PrinterEntity), "PrinterEntity");
            result = result.Replace(nameof(TableScaleModels.ScaleEntity), "ScaleEntity");
            result = result.Replace(nameof(TableScaleModels.TemplateEntity), "TemplateEntity");
            // Result.
            return result;
        }

        public static string XsltTransformation(string xslInput, string xmlInput)
        {
            string result;

            using (StringReader stringReaderXslt = new(xslInput.Trim()))
            {
                using StringReader stringReaderXml = new(xmlInput.Trim());
                using XmlReader xmlReaderXslt = XmlReader.Create(stringReaderXslt);
                using XmlReader xmlReaderXml = XmlReader.Create(stringReaderXml);
                XslCompiledTransform xslt = new();
                xslt.Load(xmlReaderXslt);
                using StringWriter stringWriter = new();
                // Use OutputSettings of xsl, so it can be output as HTML.
                using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xslt.OutputSettings);
                xslt.Transform(xmlReaderXml, xmlWriter);
                result = stringWriter.ToString();
                result = ConvertStringToHex(result);
            }
            
            return result;
        }

        //public static string ZplCmdByIp(string ip, int port, string zplCommand)
        //{
        //    StringBuilder result = new();
        //    try
        //    {
        //        string zpl = ConvertStringToHex(zplCommand);
        //        string info = InterplayToPrinter(ip, port, zpl.Split('\n'), out string errorMessage);
        //        result.AppendLine(info);
        //        result.AppendLine(errorMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.AppendLine(zplCommand);
        //        result.AppendLine(ex.Message);
        //    }
        //    return result.ToString();
        //}

        //public static string ZplCmdByRaw(string printerName, string zplCommand)
        //{
        //    StringBuilder result = new();
        //    try
        //    {
        //        string zpl = ConvertStringToHex(zplCommand);
        //        RawPrinterHelper.SendStringToPrinter(printerName, zpl);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.AppendLine(zplCommand);
        //        result.AppendLine(ex.Message);
        //    }
        //    return result.ToString();
        //}
        
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

        public static string ZplHostStatusReturn => $"~HS";

        public static string ZplClearPrintBuffer => $"^XA~JA^XZ";

        public static string ZplFontDownloadCommand(string ttfName, byte[] b, bool addHeaderFooter = true)
        {
            string converted = StringUtils.ByteArrayToString(b);
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
            string converted = StringUtils.ByteArrayToString(b);
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

        private static string FixBase64ForImage(string image)
        {
            StringBuilder sbText = new(image, image.Length);
            sbText.Replace("\r\n", string.Empty);
            sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }

        #endregion

        #region Other methods

        public static string InterplayToPrinter(string ip, int port, string[] zplCommand, out string errorMessage, 
            int receiveTimeout = 1_000, int sendTimeout = 100)
        {
            errorMessage = @"";
            StringBuilder response = new();

            using (TcpClient client = new())
            {
                //client.NoDelay = true;
                client.ReceiveTimeout = receiveTimeout;
                client.SendTimeout = sendTimeout;

                client.Connect(ip, port);
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

        public static bool IsDigit(char value) => DigitsCharacters.Contains(value);

        public static bool IsSpecial(char value, bool isExcludeTop = true)
        {
            if (isExcludeTop && value == '^')
                return false;
            return SpecialCharacters.Contains(value);
        }

        public static string ConvertStringToHex(string zplInput)
        {
            StringBuilder result = new();
            Dictionary<char, string> unicodeCharacterList = new();
            // Search substring [^FH^FD].
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

        /// <summary>
        /// Replace ZPL resource.
        /// </summary>
        /// <param name="value"></param>
        public static string PrintCmdReplaceZplResources(string value)
        {
            string result = value;
            if (string.IsNullOrEmpty(result))
                return result;
            
            List<TemplateResourceEntity> resources = DataAccessHelper.Instance.Crud.GetEntities<TemplateResourceEntity>(
                new FieldListEntity(new Dictionary<string, object> { { $"{nameof(TemplateResourceEntity.Type)}", "ZPL" } }),
                new FieldOrderEntity(DbField.Name, DbOrderDirection.Asc)).ToList();
            foreach (TemplateResourceEntity resource in resources)
            {
                string resourceHex = ConvertStringToHex(resource.ImageData.ValueUnicode);
                result = result.Replace($"[{resource.Name}]", resourceHex);
            }
            return result;
        }
        
        #endregion
    }
}