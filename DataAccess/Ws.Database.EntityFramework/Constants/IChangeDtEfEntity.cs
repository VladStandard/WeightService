using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ws.Database.EntityFramework.Constants;

public interface IChangeDtEfEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [ReadOnly(true)]
    [Column(SqlColumns.ChangeDt)]
    public DateTime ChangeDt { get; }
}