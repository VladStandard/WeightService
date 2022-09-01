// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Text.Json;

namespace BlazorDeviceControl.Razors;

public partial class IdentityFetchData : RazorPageModel
{
	[Inject] IHttpClientFactory HttpClientFactory { get; set; }
	[Inject] Microsoft.Identity.Web.ITokenAcquisition TokenAcquisitionService { get; set; }
	private string UserDisplayName { get; set; }
	private List<MailMessageModel>? Messages { get; set; }
	private HttpClient _httpClient { get; set; }

	protected override async Task OnInitializedAsync()
	//protected override void OnInitialized()
	{
		//base.OnInitialized();
		
		Messages = new();
		UserDisplayName = string.Empty;

		_httpClient = HttpClientFactory.CreateClient();

		// get a token
		var token = await TokenAcquisitionService.GetAccessTokenForUserAsync(new string[] { "User.Read", "Mail.Read" });

		// make API call
		_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		HttpResponseMessage dataRequest = await _httpClient.GetAsync("https://graph.microsoft.com/beta/me");

		if (dataRequest.IsSuccessStatusCode)
		{
			JsonDocument userData = System.Text.Json.JsonDocument.Parse(await dataRequest.Content.ReadAsStreamAsync());
			UserDisplayName = userData.RootElement.GetProperty("displayName").GetString();
		}

		HttpResponseMessage mailRequest = await _httpClient.GetAsync("https://graph.microsoft.com/beta/me/messages?$select=subject,receivedDateTime,sender&$top=10");

		if (mailRequest.IsSuccessStatusCode)
		{
			JsonDocument mailData = System.Text.Json.JsonDocument.Parse(await mailRequest.Content.ReadAsStreamAsync());
			JsonElement.ArrayEnumerator messagesArray = mailData.RootElement.GetProperty("value").EnumerateArray();

			foreach (JsonElement m in messagesArray)
			{
				MailMessageModel message = new MailMessageModel();
				message.Subject = m.GetProperty("subject").GetString();
				message.Sender = m.GetProperty("sender").GetProperty("emailAddress").GetProperty("address").GetString();
				message.ReceivedTime = m.GetProperty("receivedDateTime").GetDateTime();
				Messages.Add(message);
			}
		}
	}
}

public class MailMessageModel
{
	public string Subject;
	public string Sender;
	public DateTime ReceivedTime;
}
