using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;

namespace Ws.Database.EntityFramework;

public abstract class Program
{
    public static void Main(string[] args)
    {
        using (WsDbContext db = new())
        {
            BundleEntity boxEntity = db.Bundles.Find(Guid.Empty) ?? new();
            boxEntity.Name = "Test";
            db.Update(boxEntity);
            db.SaveChanges();
        }
    }
}