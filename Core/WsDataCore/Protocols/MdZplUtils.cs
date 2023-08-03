// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Protocols;

public static class MdZplUtils
{
    public static TcpClient TcpClientSendData(string hostname, int port, List<MdZplExchangeModel> zplExchanges)
    {
        using TcpClient client = new();
        //client.ReceiveTimeout = 0;
        //client.SendTimeout = 0;
        client.Connect(hostname, port);
        using (NetworkStream stream = client.GetStream())
        {
            foreach (MdZplExchangeModel zpl in zplExchanges)
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
