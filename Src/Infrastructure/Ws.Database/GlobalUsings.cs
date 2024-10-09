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
global using Ws.Database.Common;
global using Ws.Database.Shared.Constants;

global using Ws.Database.Entities.Print.Labels;
global using Ws.Database.Entities.Print.LabelsZpl;
global using Ws.Database.Entities.Print.Pallets;
global using Ws.Database.Entities.Ref.Lines;
global using Ws.Database.Entities.Ref.PalletMen;
global using Ws.Database.Entities.Ref.Printers;
global using Ws.Database.Entities.Ref.ProductionSites;
global using Ws.Database.Entities.Ref.Users;
global using Ws.Database.Entities.Ref.Warehouses;
global using Ws.Database.Entities.Ref1C.Boxes;
global using Ws.Database.Entities.Ref1C.Brands;
global using Ws.Database.Entities.Ref1C.Bundles;
global using Ws.Database.Entities.Ref1C.Characteristics;
global using Ws.Database.Entities.Ref1C.Clips;
global using Ws.Database.Entities.Ref1C.Nestings;
global using Ws.Database.Entities.Ref1C.Plus;
global using Ws.Database.Entities.Zpl.Templates;
global using Ws.Database.Entities.Zpl.ZplResources;

global using Ws.Database.Views.Diag.DatabaseTables;

// 5. Modules
global using Ws.Shared.Utils;