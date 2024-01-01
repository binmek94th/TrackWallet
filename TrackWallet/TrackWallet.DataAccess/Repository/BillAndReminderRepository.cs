using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository;

public class BillAndReminderRepository : Repository<BillAndReminder>, IBillAndReminderRepository
{
    private ApplicationDbContext _db;
    
    public BillAndReminderRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public void Update(BillAndReminder obj)
    {
        _db.BillAndReminders.Update(obj);

    }
    
}