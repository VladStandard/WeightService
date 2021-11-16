// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WeightCore.Managers
{
    public class ManagerEntity
    {
        #region Public and private fields and properties - Manager

        public int WaitReopen { get; set; }
        public int WaitRequest { get; set; }
        public int WaitResponse { get; set; }
        public int WaitException { get; set; }
        public int WaitClose { get; set; }
        public string ExceptionMsg { get; set; }
        public bool IsInit { get; set; }
        public bool IsResponse { get; set; }
        public object Locker { get; private set; } = new();

        #endregion

        #region Public and private methods

        public void Init(int waitReopen = 1_000, int waitResponse = 500, int waitRequest = 250, int waitClose = 2_000, int waitException = 1_000)
        {
            WaitReopen = waitReopen == 0 ? 5_000 : waitReopen;
            WaitResponse = waitResponse == 0 ? 500 : waitResponse;
            WaitRequest = waitRequest == 0 ? 250 : waitRequest;
            WaitClose = waitClose == 0 ? 5_000 : waitClose;
            WaitException = waitException == 0 ? 5_000 : waitException;
        }

        #endregion

    }
}
