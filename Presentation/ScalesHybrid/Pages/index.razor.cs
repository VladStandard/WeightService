using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace ScalesHybrid.Pages;

public partial class Index : ComponentBase
{
    private SqlHostEntity host { get; set; }
}