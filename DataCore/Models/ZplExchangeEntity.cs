// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;

namespace DataCore.Models
{
    public class ZplExchangeEntity
    {
        #region Public and private fields and properties

        public byte[] Cmd { get; set; }
        public int Length { get => Cmd.Length; }

        #endregion

        #region Constructor and destructor

        public ZplExchangeEntity()
        {
            Cmd = Array.Empty<byte>();
        }

        public ZplExchangeEntity(string cmd)
        {
            Cmd = Encoding.ASCII.GetBytes(cmd);
        }

        public ZplExchangeEntity(byte[] cmd)
        {
            Cmd = cmd;
        }

        #endregion
    }
}
