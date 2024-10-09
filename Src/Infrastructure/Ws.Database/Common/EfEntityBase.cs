using System.ComponentModel.DataAnnotations;

namespace Ws.Database.Common;

public abstract class EfEntityBase
{
    [Key]
    [Column(SqlColumns.Uid)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}