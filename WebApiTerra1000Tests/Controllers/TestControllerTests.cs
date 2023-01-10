// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using RestSharp;
using WebApiCore.Enums;
using WebApiCore.Models.WebRequests;
using WebApiCore.Models.WebResponses;

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
                    await WebResponseUtils.GetInfoAsync(url, request);
                }
            }
        });
    }
}