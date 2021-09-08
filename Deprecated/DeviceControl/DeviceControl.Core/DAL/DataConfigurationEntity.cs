namespace DeviceControl.Core.DAL
{
    public class DataConfigurationEntity
    {
        public bool OrderAsc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public DataConfigurationEntity(bool orderAsc = true, int pageNo = 0, int pageSize = 10)
        {
            OrderAsc = orderAsc;
            PageNo = pageNo;
            PageSize = pageSize;
        }

        public override string ToString()
        {
            return $"{nameof(OrderAsc)}: {OrderAsc}. " +
                   $"{nameof(PageNo)}: {PageNo}. " +
                   $"{nameof(PageSize)}: {PageSize}.";
        }
    }
}
