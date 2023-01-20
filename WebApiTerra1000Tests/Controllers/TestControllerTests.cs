// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using RestSharp;
using WsStorage.Enums;
using WsWebApi.Models.WebRequests;
using WsWebApi.Utils;
using WsWebApiTests.Utils;

namespace WebApiTerra1000Tests.Controllers;

[TestFixture]
internal class TestControllerTests
{
    [Test]
    public void GetListInfo_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestTerra1000().GetListInfo(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtilsTests.GetInfoAsync(url, request);
                }
            }
        });
    }
}