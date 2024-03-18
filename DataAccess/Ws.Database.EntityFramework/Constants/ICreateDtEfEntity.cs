using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ws.Database.EntityFramework.Constants;

public interface ICreateDtEfEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ReadOnly(true)]
    [Column(SqlColumns.CreateDt)]
    public DateTime CreateDt { get; }
}