using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

namespace Ws.Database.EntityFramework;

public abstract class Program
{
    public static void Main(string[] args)
    {
        using (WsDbContext db = new())
        {
            BoxEntity boxEntity = new()
            {
                Name = "Hello3",
                Uid1C = Guid.NewGuid(),
                Weight = 1000m
            };
            db.Add(boxEntity);
            db.SaveChanges();
        }
    }
}