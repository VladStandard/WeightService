﻿using Ws.Database.Core.Entities.Ref.Users;
using Ws.Database.Core.Helpers;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.User;

internal class UserService(SqlUserRepository userRepo) : IUserService
{
    [Session] public IEnumerable<UserEntity> GetAll() => userRepo.GetAll();

    [Session] public UserEntity GetItemByUid(Guid uid) => userRepo.GetByUid(uid);

    [Session] public UserEntity GetItemByNameOrCreate(string username)
    {
        UserEntity user = userRepo.GetItemByUsername(username);

        user.Name = username;
        user.LoginDt = DateTime.Now;

        SqlCoreHelper.SaveOrUpdate(user);
        return user;
    }
}