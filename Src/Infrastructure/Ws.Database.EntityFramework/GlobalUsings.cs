// 1. .NET
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Reflection;

// 2. Microsoft
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;

// 3. External
// pass

// 4. Internal
global using Ws.Database.EntityFramework.Common;
global using Ws.Database.EntityFramework.Constants;
global using Ws.Database.EntityFramework.Extensions;
global using Ws.Database.EntityFramework.Interceptors;
global using Ws.Database.EntityFramework.Models;

// 5. Modules
global using Ws.Shared.Utils;