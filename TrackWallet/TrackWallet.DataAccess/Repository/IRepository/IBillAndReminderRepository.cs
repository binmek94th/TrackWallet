using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IBillAndReminderRepository : IRepository<BillAndReminder>
{
    void Update(BillAndReminder obj);
}