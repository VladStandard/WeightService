// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WeightCore.Managers
{
    public class ManagerWaitConfig
    {
        #region Public and private fields and properties

        public ushort WaitClose { get; set; }
        public ushort WaitException { get; set; }
        public ushort WaitReopen { get; set; }
        public ushort WaitRequest { get; set; }
        public ushort WaitResponse { get; set; }

        #endregion

        #region Constructor and destructor

        public ManagerWaitConfig(ushort waitReopen, ushort waitRequest, ushort waitResponse, ushort waitClose, ushort waitException)
        {
            WaitReopen = waitReopen == 0 ? (ushort)1_000 : waitReopen;
            WaitRequest = waitRequest == 0 ? (ushort)250 : waitRequest;
            WaitResponse = waitResponse == 0 ? (ushort)500 : waitResponse;
            WaitClose = waitClose == 0 ? (ushort)0_200 : waitClose;
            WaitException = waitException == 0 ? (ushort)1_000 : waitException;
        }

        public ManagerWaitConfig() : this(1_000, 1_000, 1_000, 1_000, 1_000) { }

        #endregion
    }
}
