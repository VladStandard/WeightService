// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using RimDev.Stuntman.Core;
using System.Collections.Generic;

namespace BlazorCore.Models
{
    public class StuntmanUsersEntity
    {
        public List<StuntmanUser> Users { get; set; }

        public StuntmanUsersEntity()
        {
            Users = new List<StuntmanUser>();
            Users.Add(new StuntmanUser("user-1", "User 1").AddClaim("given_name", "John").AddClaim("family_name", "Doe"));
            Users.Add(new StuntmanUser("user-2", "User 2").SetAccessToken("123").AddClaim("given_name", "Mary").AddClaim("family_name", "Smith"));
        }
    }
}
