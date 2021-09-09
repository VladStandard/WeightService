// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace WeightCore.Zpl
{
    /// <summary>
    /// ZPLConverterHelper to assist in converting bitmap images into zpl strings.  Context of conversion output zpl strings will be either a string
    /// output ^GF[A] zpl command input or an entire valid ZPL label including headers and footers.
    /// </summary>
    public class ZplConverterHelper
    {
        // defaults black monochromatic threshold to 50%
        private int _blackLimit = 380;
        private int _total;
        private int _widthBytes;
        private bool _compressHex;

        private static readonly Dictionary<int, string> MapCode = new()
        {
            {1, "G"},
            {2, "H"},
            {3, "I"},
            {4, "J"},
            {5, "K"},
            {6, "L"},
            {7, "M"},
            {8, "N"},
            {9, "O" },
            {10, "P"},
            {11, "Q"},
            {12, "R"},
            {13, "S"},
            {14, "T"},
            {15, "U"},
            {16, "V"},
            {17, "W"},
            {18, "X"},
            {19, "Y"},
            {20, "g"},
            {40, "h"},
            {60, "i"},
            {80, "j" },
            {100, "k"},
            {120, "l"},
            {140, "m"},
            {160, "n"},
            {180, "o"},
            {200, "p"},
            {220, "q"},
            {240, "r"},
            {260, "s"},
            {280, "t"},
            {300, "u"},
            {320, "v"},
            {340, "w"},
            {360, "x"},
            {380, "y"},
            {400, "z" }
        };

        /// <summary>
        /// Converts a Bitmap into a ZPL ^GF[A] command input.  Can also specify ZPL header and footer to allow easy printing of label.
        /// </summary>
        /// <param name="image">Bitmap containing image source for ^GF[A] command input string.</param>
        /// <param name="addHeaderFooter">if true surrounds the command input string with the ZPL headers and footers required to generate valid ZPL.</param>
        /// <returns>^GF[A] command input string</returns>
        public string ConvertFromImage(Bitmap image, bool addHeaderFooter = true)
        {
            string hexAscii = CreateBody(image);
            if (_compressHex)
            {
                hexAscii = EncodeHexAscii(hexAscii);
            }

            string zplCode = "^GFA," + _total + "," + _total + "," + _widthBytes + ", " + hexAscii;

            if (addHeaderFooter)
            {
                string header = "^XA " + "^FO0,0^GFA," + _total + "," + _total + "," + _widthBytes + ", ";
                string footer = "^FS" + "^XZ";
                zplCode = header + zplCode + footer;
            }
            return zplCode;
        }

        public string CreateGRF(Bitmap image, string imageName, bool addHeaderFooter = true)
        {
            BitmapData imgData = null;
            byte[] pixels;
            int x, y, width;
            StringBuilder sb;
            IntPtr ptr;

            try
            {
                imgData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
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

        /// <summary>
        /// Driver for generating command input string.
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns></returns>
        private string CreateBody(Bitmap bitmapImage)
        {
            StringBuilder sb = new();
            int height = bitmapImage.Height;
            int width = bitmapImage.Width;
            int rgb, red, green, blue, index = 0;
            char[] auxBinaryChar = new char[] { '0', '0', '0', '0', '0', '0', '0', '0' };
            _widthBytes = width / 8;
            if (width % 8 > 0)
            {
                _widthBytes = ((int)(width / 8)) + 1;
            }
            else
            {
                _widthBytes = width / 8;
            }
            _total = _widthBytes * height;
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    rgb = bitmapImage.GetPixel(w, h).ToArgb();
                    red = (rgb >> 16) & 0x000000FF;
                    green = (rgb >> 8) & 0x000000FF;
                    blue = (rgb) & 0x000000FF;
                    char auxChar = '1';
                    int totalColor = red + green + blue;
                    if (totalColor > _blackLimit)
                    {
                        auxChar = '0';
                    }
                    auxBinaryChar[index] = auxChar;
                    index++;
                    if (index == 8 || w == (width - 1))
                    {
                        sb.Append(FourByteBinary(new string(auxBinaryChar)));
                        auxBinaryChar = new char[] { '0', '0', '0', '0', '0', '0', '0', '0' };
                        index = 0;
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts binary into integer representation of two hex digits 
        /// </summary>
        /// <param name="binaryStr"></param>
        /// <returns></returns>
        private string FourByteBinary(string binaryStr)
        {
            int value = Convert.ToInt32(binaryStr, 2);
            if (value > 15)
            {
                return Convert.ToString(value, 16).ToUpper();
            }
            else
            {
                return "0" + Convert.ToString(value, 16).ToUpper();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string EncodeHexAscii(string code)
        {
            int maxlinea = _widthBytes * 2;
            StringBuilder sbCode = new();
            StringBuilder sbLinea = new();
            string previousLine = null;
            int counter = 1;
            char aux = code.ElementAt(0);
            bool firstChar = false;
            for (int i = 1; i < code.Length; i++)
            {
                if (firstChar)
                {
                    aux = code.ElementAt(i);
                    firstChar = false;
                    continue;
                }
                if (code.ElementAt(i) == '\n')
                {
                    if (counter >= maxlinea && aux == '0')
                    {
                        sbLinea.Append(",");
                    }
                    else if (counter >= maxlinea && aux == 'F')
                    {
                        sbLinea.Append("!");
                    }
                    else if (counter > 20)
                    {
                        int multi20 = counter / 20 * 20;
                        int resto20 = counter % 20;
                        sbLinea.Append(MapCode[multi20]);
                        if (resto20 != 0)
                        {
                            sbLinea.Append(MapCode[resto20]).Append(aux);
                        }
                        else
                        {
                            sbLinea.Append(aux);
                        }
                    }
                    else
                    {
                        sbLinea.Append(MapCode[counter]).Append(aux);
                    }
                    counter = 1;
                    firstChar = true;
                    sbCode.Append(sbLinea.ToString().Equals(previousLine) ? ":" : sbLinea.ToString());
                    previousLine = sbLinea.ToString();
                    sbLinea.Length = 0;
                    continue;
                }
                if (aux == code.ElementAt(i))
                {
                    counter++;
                }
                else
                {
                    if (counter > 20)
                    {
                        int multi20 = counter / 20 * 20;
                        int resto20 = counter % 20;
                        sbLinea.Append(MapCode[multi20]);
                        if (resto20 != 0)
                        {
                            sbLinea.Append(MapCode[resto20]).Append(aux);
                        }
                        else
                        {
                            sbLinea.Append(aux);
                        }
                    }
                    else
                    {
                        sbLinea.Append(MapCode[counter]).Append(aux);
                    }
                    counter = 1;
                    aux = code.ElementAt(i);
                }
            }
            return sbCode.ToString();
        }

        public void SetCompressHex(bool compressHex)
        {
            _compressHex = compressHex;
        }

        /// <summary>
        /// Sets black pixel threshold for comparison of zpl pixels which determining whether to render or ignore pixels.  
        /// </summary>
        /// <param name="percentage">threshold percentage for comparison of pixels</param>
        /// <remarks>100+ percentage values will generate entirely black label.</remarks>
        public void SetBlacknessLimitPercentage(int percentage)
        {
            _blackLimit = percentage * 768 / 100;
        }


        public void SendToPrinter(string ipAddress, int port, string[] ZPLCommand)
        {

            TcpClient client = new();
            client.Connect(ipAddress, port);
            NetworkStream stream = client.GetStream();

            foreach (string commandLine in ZPLCommand)
            {
                stream.Write(ASCIIEncoding.ASCII.GetBytes(commandLine), 0, commandLine.Length);
                stream.Flush();
            }

            stream.Close();
            client.Close();
        }

        public void FontsClear(string ZebraIP, int ZebraPort)
        {
            string zplCommand = "^XA^IDE:*.TTF^FS^XZ";
            SendToPrinter(ZebraIP, ZebraPort, zplCommand.Split('\n'));
        }

        public void LogoClear(string ZebraIP, int ZebraPort)
        {
            string zplCommand = "^XA^IDE:*.GRF^FS^XZ";
            SendToPrinter(ZebraIP, ZebraPort, zplCommand.Split('\n'));
        }

        public void FontsUpload(string ZebraIP, int ZebraPort, Dictionary<string, string> Fonts)
        {
            foreach (KeyValuePair<string, string> fnt in Fonts)
            {
                string zplImageData = string.Empty;
                byte[] binaryData = Convert.FromBase64String(fnt.Value);
                string namettf = fnt.Key;

                using TcpClient client = new();
                client.Connect(ZebraIP, ZebraPort);
                using (NetworkStream stream = client.GetStream())
                {
                    string ZPLCommand = $"^XA^MNN^LL500~DYE:{namettf}.TTF,B,T," + binaryData.Length + ",,";
                    stream.Write(ASCIIEncoding.ASCII.GetBytes(ZPLCommand), 0, ZPLCommand.Length);
                    stream.Flush();
                    stream.Write(binaryData, 0, binaryData.Length);
                    stream.Flush();
                    ZPLCommand = "^XZ";
                    stream.Write(ASCIIEncoding.ASCII.GetBytes(ZPLCommand), 0, ZPLCommand.Length);
                    stream.Flush();
                    stream.Close();
                }
                client.Close();
            }
        }


        public string FixBase64ForImage(string Image)
        {
            StringBuilder sbText = new(Image, Image.Length);
            sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
            return sbText.ToString();
        }

        public void LogoUpload(string ZebraIP, int ZebraPort, Dictionary<string, string> Logo)
        {
            foreach (KeyValuePair<string, string> fnt in Logo)
            {
                byte[] bitmapData = Convert.FromBase64String(FixBase64ForImage(fnt.Value));
                using System.IO.MemoryStream bmpStream = new(bitmapData);
                //
                //"~DYR:2033.PNG,p,p,2312,40,:B64:iVBO
                //Rw0KGgoAAAANSUhEUgAA
                //AUAAAACeAQMAAAB5HUEC
                //AAAABlBMVEUAAAD"
                //
                //^XA^FO20,20^XGR:2033.PNG^XZ
                //
                Image image = Image.FromStream(bmpStream);
                Bitmap bitmap = new(image);
                ZplConverterHelper zp = new();
                //String zplCommand = zp.ConvertFromImage(bitmap);

                string nameimage = fnt.Key;
                string zplCommand = zp.CreateGRF(bitmap, nameimage);
                SendToPrinter(ZebraIP, ZebraPort, zplCommand.Split('\n'));
            }
        }

        public void Сalibration(string zebraIP, int zebraPort)
        {
            string zplCommand = 
                "! U1 setvar \"media.type\" \"label\"" +
                "! U1 setvar \"media.sense_mode\" \"gap\"" +
                "~JC^XA^JUS^XZ";
            SendToPrinter(zebraIP, zebraPort, zplCommand.Split('\n'));
        }
    }
}
