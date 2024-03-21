using Ws.Database.EntityFramework.Models.Ready;

namespace Ws.Database.EntityFramework;

public abstract class Program
{
    public static void Main(string[] args)
    {
        using (WsDbContext db = new())
        {
            Box box = new()
            {
                Name = "Hello3",
                Uid1C = Guid.NewGuid(),
                Weight = 1000m
            };
            db.Add(box);
            db.SaveChanges();
        }
    }
}