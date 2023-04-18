namespace BlazorResourcesVs.Data;

public class WebSiteEntity
{
	public string Name { get; set; }
	public string Link { get; set; }
	public string Dev { get; set; }

	public WebSiteEntity(string name, string link, string dev)
	{
		Name = name;
		Link = link;
		Dev = dev;
	}
}