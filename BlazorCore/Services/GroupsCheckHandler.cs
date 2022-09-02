//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.VisualBasic;

//namespace BlazorCore.Services;

//public class GroupsCheckHandler : AuthorizationHandler<GroupsCheckRequirement>
//{
//	private readonly ITokenAcquisition tokenAcquisition;
//	private readonly IMSGraphService graphService;

//	public GroupsCheckHandler(ITokenAcquisition tokenAcquisition, IMSGraphService MSGraphService)
//	{
//		this.tokenAcquisition = tokenAcquisition;
//		this.graphService = MSGraphService;
//	}
//	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
//		GroupsCheckRequirement requirement)
//	{
//		string accessToken = await tokenAcquisition.GetAccessTokenOnBehalfOfUserAsync(new[] { Constants.ScopeUserRead, Constants.ScopeDirectoryReadAll });

//		User me = await graphService.GetMeAsync(accessToken);

//		IList<Group> groups = await graphService.GetMyMemberOfGroupsAsync(accessToken);

//		var result = false;
//		foreach (var group in groups)
//		{
//			if (requirement.groups.Equals(group.Id))
//			{
//				result = true;
//			}
//		}

//		if (result)
//		{
//			context.Succeed(requirement);
//		}

//	}
//}
