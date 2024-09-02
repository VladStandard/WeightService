// SYSTEM

global using System.Linq.Expressions;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;

// EXTERNAL
global using Ws.DeviceControl.Models.Shared;

global using Ws.Shared.Enums;
global using Ws.Shared.Constants;
global using Ws.Shared.Extensions;
global using Ws.Shared.Api.ApiException;

global using Ws.Database.EntityFramework;

global using Ws.DeviceControl.Models.Auth;
global using Ws.DeviceControl.Models.Shared;

global using Ws.DeviceControl.Api.App.Common;
global using Ws.DeviceControl.Api.App.Shared;
global using Ws.DeviceControl.Api.App.Shared.Internal;
global using Ws.DeviceControl.Api.App.Shared.Extensions;
global using Ws.DeviceControl.Api.App.Shared.Expressions;

// NUGETS
global using FluentValidation.Results;