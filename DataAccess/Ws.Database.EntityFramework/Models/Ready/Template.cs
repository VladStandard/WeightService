using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Templates)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Templates}_NAME", IsUnique = true)]
public sealed class Template : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("BODY")]
    [StringLength(10240, MinimumLength = 1, ErrorMessage = "Body must be between 1 and 10240 characters")]
    public string Body { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    // public virtual ICollection<PlusTemplatesFk> PlusTemplatesFks { get; set; } = new List<PlusTemplatesFk>();
}
