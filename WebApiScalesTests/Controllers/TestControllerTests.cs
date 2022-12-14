// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using RestSharp;
using WebApiCore.Enums;
using WebApiCore.Models.WebRequests;
using WebApiCore.Models.WebResponses;

namespace WebApiScalesTests.Controllers;

[TestFixture]
internal class TestControllerTests
{
    [Test]
    public void GetListException_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListException(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtils.GetExceptionAsync(url, request);
                }
            }
        });
    }

    [Test]
    public void GetListExceptionV1_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListExceptionV1(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtils.GetExceptionAsync(url, request);
                }
            }
        });
    }

    [Test]
    public void GetListExceptionV2_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListExceptionV2(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtils.GetExceptionAsync(url, request);
                }
            }
        });
    }

    [Test]
    public void GetListExceptionV3_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListExceptionV3(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtils.GetExceptionAsync(url, request);
                }
            }
        });
    }

    [Test]
    public void GetListInfo_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListInfo(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtils.GetInfoAsync(url, request);
                }
            }
        });
    }

    [Test]
    public void GetListInfoV1_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListInfoV1(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtils.GetInfoAsync(url, request);
                }
            }
        });
    }

    [Test]
    public void GetListInfoV2_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListInfoV2(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtils.GetInfoAsync(url, request);
                }
            }
        });
    }

    [Test]
    public void GetListInfoV3_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListInfoV3(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtils.GetInfoAsync(url, request);
                }
            }
        });
    }
}
