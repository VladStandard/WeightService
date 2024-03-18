using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ws.Database.EntityFramework.Constants;

public class EfEntityBase
{
    [Key]
    [Column(SqlColumns.Uid)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Uid { get; private set; }
}