using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    private ApplicationDbContext _db;
    
    public NotificationRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(Notification obj)
    {
        _db.Notifications.Update(obj);
    }
    

    
}