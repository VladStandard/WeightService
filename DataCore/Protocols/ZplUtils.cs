// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net.Sockets;

namespace DataCore.Protocols;

public static class ZplUtils
{
    public static TcpClient TcpClientSendData(string hostname, int port, List<ZplExchangeEntity> zplExchanges)
    {
        using TcpClient client = new();
        //client.ReceiveTimeout = 0;
        //client.SendTimeout = 0;
        client.Connect(hostname, port);
        using (NetworkStream stream = client.GetStream())
        {
            foreach (ZplExchangeEntity zpl in zplExchanges)
            {
                stream.Write(zpl.Cmd, 0, zpl.Length);
                stream.Flush();
            }
            stream.Close();
        }
        client.Close();
        return client;
    }

}
