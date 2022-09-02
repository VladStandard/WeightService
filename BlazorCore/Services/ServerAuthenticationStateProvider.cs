//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using Microsoft.AspNetCore.Components.Authorization;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace BlazorCore.Services;

//public class ServerAuthenticationStateProvider : AuthenticationStateProvider
//{
//	string UserId;
//	string Password;
//	bool IsAuthenticated = false;

//	public void LoadUser(string _UserId, string _Password)
//	{
//		UserId = _UserId;
//		Password = _Password;
//	}

//	public async Task LoadUserData()
//	{
//		var securityService = new SharedServiceLogic.Security();
//		try
//		{
//			var passwordCheck = await securityService.ValidatePassword(UserId, Password);
//			IsAuthenticated = passwordCheck == true ? true : false;
//		}
//		catch (Exception ex)
//		{
//			Console.WriteLine(ex);
//		}


//	}

//	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//	{
//		var userService = new UserService();

//		var identity = IsAuthenticated
//			? new ClaimsIdentity(await userService.GetClaims(UserId))
//			: new ClaimsIdentity();

//		var result = new AuthenticationState(new ClaimsPrincipal(identity));
//		return result;
//	}

//}