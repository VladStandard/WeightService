using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Xsl;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    /// <summary>
    /// Send ZPL command into socket.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <param name="zplCommand"></param>
    /// 
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ZplPipe(string ip, int port, string zplCommand)
    {

        string zpl = ToCodePoints(zplCommand);

        //SendToPrinter(ip, port, zplCommand.Split('\n'));
        //SqlContext.Pipe.Send("--------------------------");

        string errorMessage = String.Empty;
        string info = InterplayToPrinter(ip, port, zpl.Split('\n'), out errorMessage);

        SqlContext.Pipe.Send(zpl);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(info);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(errorMessage);

        //var client = new TcpClient();
        //client.Connect(ip, port);
        //var stream = client.GetStream();
        //stream.Write(Encoding.ASCII.GetBytes(zpl), 0, zpl.Length);
        //stream.Flush();
        //stream.Close();
        //client.Close();
    }

    /// <summary>
    /// Send ZPL command into socket with XSLT trasforms.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <param name="xslInput"></param>
    /// <param name="xmlInput"></param>
    /// 
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ZplPipeNow(string ip, int port, string xslInput, string xmlInput)
    {
        string zpl = XSLTTransformProc(xslInput, xmlInput);
        zpl = ToCodePoints(zpl);

        //SendToPrinter(ip, port, zpl.Replace("\n","").Split('\n'));
        string errorMessage = String.Empty;
        string info = InterplayToPrinter(ip, port, zpl.Split('\n'), out errorMessage);

        SqlContext.Pipe.Send(zpl);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(info);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(errorMessage);


        //var client = new TcpClient();
        //client.Connect(ip, port);
        //var stream = client.GetStream();

        //stream.Write(Encoding.ASCII.GetBytes(zpl), 0, zpl.Length);
        //stream.Flush();

        //stream.Close();
        //client.Close();
    }

    /// <summary>
    /// Build ZPL command through XSLT trasforms.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="xslInput"></param>
    /// <param name="xmlInput"></param>
    /// 
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void XSLTTransform(string xslInput, string xmlInput)
    {
        string result = XSLTTransformProc(xslInput, xmlInput);
        SqlContext.Pipe.Send(result);
    }

    /// <summary>
    /// Сalibration ZPL printer.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="xslInput"></param>
    /// <param name="xmlInput"></param>
    /// 
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ZplСalibration(string ip, int port)
    {
        var zplCommand =
            "! U1 setvar \"media.type\" \"label\" " +
            "! U1 setvar \"media.sense_mode\" \"gap\" " +
            "END " +
            "~JC^XA^JUS^XZ\n";
        //SendToPrinter(ip, port, zplCommand.Split('\n'));
        string errorMessage = String.Empty;
        string info = InterplayToPrinter(ip, port, zplCommand.Split('\n'), out errorMessage);
        SqlContext.Pipe.Send(zplCommand);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(info);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(errorMessage);
    }

    /// <summary>
    /// Get host status (~HS) printer.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="xslInput"></param>
    /// <param name="xmlInput"></param>
    /// 
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ZplHostStatus(string ip, int port)
    {
        var zplCommand = "~HS";
        SqlContext.Pipe.Send(zplCommand);
        string errorMessage = String.Empty;
        string info = InterplayToPrinter(ip, port, zplCommand.Split('\n'), out errorMessage);
        SqlContext.Pipe.Send(info);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(errorMessage);
    }

    /// <summary>
    /// Clear FONTS.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <param name="mask" default="E:*TTF"></param>
    /// 
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ZplFontsClear(string ip, int port, string mask)
    {
        var zplCommand = $"^XA^ID{mask}^FS^XZ";
        //   SendToPrinter(ip, port, zplCommand.Split('\n'));

        string errorMessage = String.Empty;
        string info = InterplayToPrinter(ip, port, zplCommand.Split('\n'), out errorMessage);

        SqlContext.Pipe.Send(zplCommand);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(info);
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send(errorMessage);
    }

    /// <summary>
    /// Font upload.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <param name="ttfname" default = "E:CURL.TTF"></param>
    /// <param name="ttfdata" ></param>
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ZplFontUpload(string ip, int port, string ttfname, SqlBytes ttfdata)
    {
        //byte[] b = Convert.FromBase64String(ttfdata);
        var binaryData = ttfdata.Buffer;

        using (var client = new TcpClient())
        {
            client.Connect(ip, port);
            using (var stream = client.GetStream())
            {
                var ZPLCommand = $"^XA^MNN^LL500~DYE:{ttfname}.TTF,B,T," + binaryData.Length + ",,";
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
        SqlContext.Pipe.Send(@"-------------");
        SqlContext.Pipe.Send($"Font {ttfname} upload." );

    }

    /// <summary>
    /// Clear LOGO.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <param name="mask"></param>
    /// 
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ZplLogoClear(string ip, int port, string mask = "E:*.GRF")
    {
        string zplCommand = $"^XA^ID{mask}^FS^XZ";
        SendToPrinter(ip, port, zplCommand.Split('\n'));
    }

    /// <summary>
    /// Logo upload.
    /// Редактировал: 2020-07-15 Dmitry Ivakin.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    /// <param name="ttfname" default = "E:CURL.TTF"></param>
    /// <param name="ttfdata" ></param>
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ZplLogoUpload(string ip, int port, string logoname, SqlBytes logodata)
    {
        string errorMessage = @"successfully";
        int _receiveTimeout = 1000;
        int _sendTimeout = 100;

        byte[] b = logodata.Buffer;
        using (System.IO.MemoryStream bmpStream = new System.IO.MemoryStream(b))
        {
            var image = System.Drawing.Image.FromStream(bmpStream,false,true);
            var bitmapData = new System.Drawing.Bitmap(image);

            System.Drawing.Imaging.BitmapData imgData = null;
            byte[] pixels;
            int x, y, width;
            StringBuilder sb;
            IntPtr ptr;

            try
            {
                imgData = bitmapData.LockBits(
                    new System.Drawing.Rectangle(0, 0, bitmapData.Width, bitmapData.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format1bppIndexed);

                //if (bitmapData.Width % 8 > 0)
                //{
                //    var remaining = bitmapData.Width % 8;
                //    var newbmp = ResizeImage(bitmapData, bitmapData.Width + remaining, bitmapData.Height);
                //    bitmapData.Dispose();
                //    bitmapData = newbmp;
                //}
                //width = (bitmapData.Width ) / 8;

                width = (bitmapData.Width + 7) / 8;
                pixels = new byte[width];
                sb = new StringBuilder(width * bitmapData.Height * 2);
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
                    if (imgData != null) bitmapData.UnlockBits(imgData);
                    image.Dispose();
                }
            }

            var zplCommand = $"~DGE:{logoname.ToUpper().Replace(".BMP", "")}.GRF,{width * y},{width}," + sb;
            zplCommand = "^XA " + zplCommand + "^XZ";

            errorMessage = @"successfully";
            StringBuilder response = new StringBuilder();

            using (var client = new TcpClient())
            {
                //client.NoDelay = true;
                client.ReceiveTimeout = _receiveTimeout;
                client.SendTimeout = _sendTimeout;

                client.Connect(ip, port);
                using (var stream = client.GetStream())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.AutoFlush = true;
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            try
                            {
                                foreach (var commandLine in zplCommand.Split('\n'))
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
                            }
                        }
                    }
                }
            }

            SqlContext.Pipe.Send(@"-------------");
            SqlContext.Pipe.Send(errorMessage);

        }
    }

    public static Bitmap ResizeImage(Image image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var oldRect = new Rectangle(0, 0, image.Width, image.Height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.FillRectangle(Brushes.White, destRect);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, oldRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }

        return destImage;
    }

    private static string FixBase64ForImage(string Image)
    {
        var sbText = new StringBuilder(Image, Image.Length);
        sbText.Replace("\r\n", string.Empty);
        sbText.Replace(" ", string.Empty);
        return sbText.ToString();
    }

    private static string InterplayToPrinter(string ipAddress, int port, string[] ZPLCommand, out string errorMessage, int _receiveTimeout = 1000, int _sendTimeout = 100)
    {
        errorMessage = @"successfully";
        StringBuilder response = new StringBuilder();

        using (var client = new TcpClient())
        {
            //client.NoDelay = true;
            client.ReceiveTimeout = _receiveTimeout;
            client.SendTimeout = _sendTimeout;

            client.Connect(ipAddress, port);
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
                        }
                    }
                }
            }
        }
        return response.ToString();
    }
        
    private static void SendToPrinter(string ipAddress, int port, string[] ZPLCommand)
    {
        foreach (string zpl in ZPLCommand)
        {
            Thread.Sleep(20);
            using (var client = new TcpClient())
            {
                //client.NoDelay = true;
                //client.ReceiveTimeout = 300;
                ////client.SendTimeout = 100;

                client.Connect(ipAddress, port);
                using (var stream = client.GetStream())
                {
                    stream.Write(ASCIIEncoding.ASCII.GetBytes(zpl), 0, zpl.Length);
                    stream.Flush();
                }
            }
        }
    }

    private static string XSLTTransformProc(string xslInput, string xmlInput)
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

    //public string FixBase64ForImage(string Image)
    //{
    //    var sbText = new StringBuilder(Image, Image.Length);
    //    sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
    //    return sbText.ToString();
    //}

    //private string CreateGRF(Bitmap image, string imageName, bool addHeaderFooter = true)
    //{
    //    System.Drawing.Imaging.BitmapData imgData = null;
    //    byte[] pixels;
    //    int x, y, width;
    //    StringBuilder sb;
    //    IntPtr ptr;

    //    try
    //    {
    //        imgData = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format1bppIndexed);
    //        width = (image.Width + 7) / 8;
    //        pixels = new byte[width];
    //        sb = new StringBuilder(width * image.Height * 2);
    //        ptr = imgData.Scan0;
    //        for (y = 0; y < image.Height; y++)
    //        {
    //            System.Runtime.InteropServices.Marshal.Copy(ptr, pixels, 0, width);
    //            for (x = 0; x < width; x++)
    //                sb.AppendFormat("{0:X2}", (byte)~pixels[x]);
    //            ptr = (IntPtr)(ptr.ToInt64() + imgData.Stride);
    //        }
    //    }
    //    finally
    //    {
    //        if (image != null)
    //        {
    //            if (imgData != null) image.UnlockBits(imgData);
    //            image.Dispose();
    //        }
    //    }

    //    var zplCode = $"~DG{imageName.ToUpper().Replace(".BMP", "")}.GRF,{width * y},{width}," + sb;

    //    if (addHeaderFooter)
    //    {
    //        zplCode = "^XA " + zplCode + "^XZ";
    //    }

    //    return zplCode;
    //}

    private static string ToCodePoints(string zplInput)
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
}
