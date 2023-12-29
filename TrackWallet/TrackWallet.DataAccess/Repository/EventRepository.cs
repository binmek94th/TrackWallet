using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class EventRepository : Repository<Event>, IEventRepository
{
    private ApplicationDbContext _db;
    
    public EventRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(Event obj)
    {
        _db.Events.Update(obj);
    }

    
}