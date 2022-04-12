namespace BlazorAppTemplate202112.Pages
{
    public partial class RadzenPage
    {
        public IEnumerable<string>? ListComPorts { get; set; }

        private List<string> GetListComPorts()
        {
            List<string> result = new();
            for (int i = 1; i < 256; i++)
            {
                result.Add($"COM{i}");
            }
            return result;
        }

        public RadzenPage()
        {
            ListComPorts = GetListComPorts();
        }
    }
}
