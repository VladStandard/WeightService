using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Xsl;



namespace ZplSender
{


    public class ZplPipeClass
    {

        #region publuc methods
        public String IpAddress { get; set; }

        public Int32 Port { get; set; }


        public string XsltTransformationPipe(string xslInput, string xmlInput)
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

        public string ZplCommandPipe(string zplCommand)
        {
            StringBuilder outMsg = new StringBuilder();

            try
            {

                string _zpl = ToCodePoints(zplCommand);
                string _errorMessage = String.Empty;
                string info = InterplayToPrinter(IpAddress, Port, _zpl.Split('\n'), out _errorMessage);
                outMsg.AppendLine(info);
                outMsg.AppendLine(_errorMessage);

            } catch (Exception ex)
            {
                outMsg.AppendLine(zplCommand);
                outMsg.AppendLine(ex.Message);
            }

            return outMsg.ToString();

        }

        public string ZplCommandPipeByIP (string _ip, int _port, string zplCommand)
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

        public string ZplCommandPipeByRAW (string _lptName, string _zplCommand)
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

        #region get example command
        public static String ZplFontsClear()
        {
           return @"^XA^IDE:*.TTF^FS^XZ";
           
        }
        public static String LogoClear()
        {
            return @"^XA^IDE:*.GRF^FS^XZ";
        }

        public static String Сalibration()
        {
            return
                "! U1 setvar \"media.type\" \"label\"" +
                "! U1 setvar \"media.sense_mode\" \"gap\"" +
                "~JC^XA^JUS^XZ";
        }


        #endregion

        public string FakePipe(string zplCommand)
        {
            return zplCommand;
        }


        #endregion

        #region ZPL ancillary private methods

     
        private static string InterplayToPrinter(string _ipAddress, int _port, string[] ZPLCommand, out string errorMessage, int _receiveTimeout = 1000, int _sendTimeout = 100)
        {
            errorMessage = @"successfully";
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
                            }
                        }
                    }
                }
            }

            return response.ToString();

        }

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

        #endregion

        #region NetLoader interface ancillary private methods

        public ZplPipeClass()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("System.Web.Helpers"))
            {
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Test.System.Web.Helpers.dll"))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            }

            return null;
        }

        public string ShowMethods()
        {
            StringBuilder outMsg = new StringBuilder();

            foreach (var method in typeof(ZplPipeClass).GetMethods())
            {
                if (method.IsPublic)
                {

                    var parameters = method.GetParameters();
                    var parameterDescriptions = string.Join
                        (", ", method.GetParameters()
                                     .Select(x => x.ParameterType + " " + x.Name)
                                     .ToArray());

                    outMsg.AppendLine($"{method.ReturnType} {method.Name} ({parameterDescriptions})");
                }

            }
            return outMsg.ToString();
        }
        public string ShowProperties()
        {
            StringBuilder outMsg = new StringBuilder();

            foreach (var property in typeof(ZplPipeClass).GetProperties())
            {
                string r = property.CanRead ? "get;" : "";
                string w = property.CanWrite ? "set;" : "";
                outMsg.AppendLine($"{property.PropertyType} {property.Name} ({r} {w})");
            }
            return outMsg.ToString();
        }


        //public delegate void ExternalEventHandler(String Source, String Message, String Data);

        //public event ExternalEventHandler TestEvent;

        #endregion

    }
}
