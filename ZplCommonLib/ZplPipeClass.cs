using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using WeightServices.Common;
using ZplCommonLib.Native;

namespace ZplCommonLib
{
    public static class ZplPipeClass
    {
        #region pipe's

        public static string FakePipe(string zplCommand)
        {
            return zplCommand;
        }

        public static string XsltTransformationPipe(string xslInput, string xmlInput)
        {
            string result;
            using (var srt = new StringReader(xslInput.Trim())) // xslInput is a string that contains xsl
            using (var sri = new StringReader(xmlInput.Trim())) // xmlInput is a string that contains xml
            {
                using (var xrt = XmlReader.Create(srt))
                using (var xri = XmlReader.Create(sri))
                {
                    XslCompiledTransform xslt = new XslCompiledTransform();
                    xslt.Load(xrt);
                    using (StringWriter sw = new StringWriter())
                    using (XmlWriter xwo = XmlWriter.Create(sw, xslt.OutputSettings)) // use OutputSettings of xsl, so it can be output as HTML
                    {
                        xslt.Transform(xri, xwo);
                        result = sw.ToString();
                        result = ToCodePoints(result);
                    }
                }
            }
            return result;
        }

        public static string ZplCommandPipeByIP(string _ip, int _port, string zplCommand)
        {
            StringBuilder outMsg = new StringBuilder();

            try
            {

                string _zpl = ToCodePoints(zplCommand);
                string _errorMessage = String.Empty;
                string info = InterplayToPrinter(_ip, _port, _zpl.Split('\n'), out _errorMessage);
                outMsg.AppendLine(info);
                outMsg.AppendLine(_errorMessage);

            }
            catch (Exception ex)
            {
                outMsg.AppendLine(zplCommand);
                outMsg.AppendLine(ex.Message);
            }

            return outMsg.ToString();

        }

        public static string ZplCommandPipeByRAW(string _lptName, string _zplCommand)
        {
            StringBuilder outMsg = new StringBuilder();

            try
            {
                string _zpl = ToCodePoints(_zplCommand);
                RawPrinterHelper.SendStringToPrinter(_lptName, _zpl);
            }
            catch (Exception ex)
            {
                outMsg.AppendLine(_zplCommand);
                outMsg.AppendLine(ex.Message);
            }

            return outMsg.ToString();

        }
        
        #endregion

        #region zpl command's
        
        public static string ZplHostQuery(string prm = "ES")
        {
            return $"~HQ{prm}";
        }

        public static string ZplFontsClear()
        {
            return @"^XA^IDE:*.TTF^FS^XZ";
        }

        public static string ZplLogoClear()
        {
            return @"^XA^IDE:*.GRF^FS^XZ";
        }

        public static string ZplCalibration()
        {
            return
                "! U1 setvar \"media.type\" \"label\"\r\n" +
                "! U1 setvar \"media.sense_mode\" \"gap\"\r\n" +
                "~JC^XA^JUS^XZ";
        }

        public static string ZplFilesDelete(string mask = "E:*.*")
        {
            return $"! U1 do \"file.delete\" \"{mask}\"\r\n";
        }

        public static string ZplFilesList(string mask = "E:*.*")
        {
            return $"! U1 do \"file.dir\" \"{mask}\"\r\n";
        }

        public static string ZplSetOdometerUserLabel(int value = 1)
        {
            return $"! U1 setvar \"odometer.user_label_count\" \"{value}\"\r\n";
        }

        public static string ZplGetOdometerUserLabel()
        {
            return $"! U1 getvar \"odometer.user_label_count\"\r\n";
        }


        public static string ZplPrintDirectory(string mask = "E:*.*")
        {
            return $"^XA^WD{mask}^XZ";
        }

        public static string ZplPowerOnReset()
        {
            return $"~JR";
        }

        public static string ZplPeelerState()
        {
            return $"! U1 getvar \"sensor.peeler\"\r\n";
        }

        public static string ZplPrintConfigurationLabel()
        {
            return $"~WC";
        }

        public static string ZplHostStatusReturn()
        {
            return $"~HS";
        }

        public static string ZplClearPrintBuffer()
        {
            return $"^XA~JA^XZ";
        }



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
            using (System.IO.MemoryStream bmpStream = new System.IO.MemoryStream(b))
            {
                var img = System.Drawing.Image.FromStream(bmpStream);
                var bitmapData = new System.Drawing.Bitmap(img);
                return ZplLogoDownloadCommand(imageName, bitmapData, addHeaderFooter);
            }
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

            var zplCode = $"~DGE:{imageName.ToUpper().Replace(".BMP", "")}.GRF,{width * y},{width}," + sb;

            if (addHeaderFooter)
            {
                zplCode = "^XA " + zplCode + "^XZ";
            }

            return zplCode;

        }

        public static string ZplContentAsBase64(string base64string)
        {
            //с наскока не сработало
            byte[] b = System.Text.Encoding.ASCII.GetBytes(base64string);
            Crc16Ccitt crc = new Crc16Ccitt(InitialCrcValue.Zeros);
            byte[] c = crc.ComputeChecksumBytes(b);

            //Encode the compressed data into Base64. No whitespace or line breaks allowed.
            //Convert the Base64 string to a byte array according to ASCII encoding().
            //Calculate the CRC over that byte array. The Initial CRC Value must be zero.

            return $"{b.Length},:B64:{base64string}:{ByteConverter.ByteArrayToString(c)}";

        }

        private static string FixBase64ForImage(string Image)
        {
            var sbText = new StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", string.Empty);
            sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }

        #endregion

        #region ZPL ancillary private methods

        public static string InterplayToPrinter(string _ipAddress, int _port, string[] ZPLCommand, out string errorMessage, int _receiveTimeout = 1000, int _sendTimeout = 100)
        {
            errorMessage = @"";
            StringBuilder response = new StringBuilder();

            using (var client = new TcpClient())
            {
                //client.NoDelay = true;
                client.ReceiveTimeout = _receiveTimeout;
                client.SendTimeout = _sendTimeout;

                client.Connect(_ipAddress, _port);
                using (var stream = client.GetStream())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.AutoFlush = true;
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            try
                            {
                                foreach (var commandLine in ZPLCommand)
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
                                SocketException sockEx = ex.InnerException as SocketException;
                                if (sockEx != null)
                                {
                                    errorMessage = @"(" + sockEx.NativeErrorCode.ToString() + ") Exception = " + ex.Message;
                                }
                                throw ex;
                            }
                        }
                    }
                }
            }

            return response.ToString();

        }

        public static string ToCodePoints(string zplInput)
        {
            var ret = new StringBuilder();
            var unicodeCharacterList = new Dictionary<char, string>();
            foreach (var ch in zplInput)
            {
                if (!unicodeCharacterList.ContainsKey(ch))
                {
                    var bytes = Encoding.UTF8.GetBytes(ch.ToString());
                    if (bytes.Length > 1)
                    {
                        var hexCode = string.Empty;
                        foreach (var b in bytes)
                        {
                            hexCode += $"_{BitConverter.ToString(new byte[] { b }).ToLower()}";
                        }

                        unicodeCharacterList[ch] = hexCode;
                    }
                    else
                        unicodeCharacterList[ch] = ch.ToString();

                    ret.Append(unicodeCharacterList[ch]);
                }
                else
                    ret.Append(unicodeCharacterList[ch]);
            }
            return ret.ToString();
        }

        #endregion
    }
}