namespace WsDataCore.Protocols
{
    public static class MdSerialPortsUtils
    {
        private static List<string> _comPorts = new();

        public static List<string> ComPorts
        {
            get
            {
                if (_comPorts.Count == 0)
                {
                    _comPorts = GetListComPorts();
                }
                return _comPorts;
            }
        }

        #region Public Methods

        public static string GenerateComPort(int number)
        {
            return $"COM{number}";
        }
        
        #endregion

        #region Private Methods

        private static List<string> GetListComPorts()
        {
            return Enumerable.Range(1, 255).Select(i => $"COM{i}").ToList();
        }

        #endregion
    }
}