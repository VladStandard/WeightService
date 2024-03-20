using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Common;

public abstract class EfEntityBase
{
    [Key]
    [Column(SqlColumns.Uid)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; private set; }
}