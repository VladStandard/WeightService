using System.ComponentModel.DataAnnotations;

namespace Ws.Database.EntityFramework.Common;

public abstract class EfEntityBase
{
    [Key]
    [Column(SqlColumns.Uid)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [NotMapped] public virtual bool IsNew => Id.Equals(Guid.Empty);
}