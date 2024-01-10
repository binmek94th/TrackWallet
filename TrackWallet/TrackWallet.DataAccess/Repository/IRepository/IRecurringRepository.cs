using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IRecurringTransactionRepository : IRepository<RecurringTransaction>
{
    void Update(RecurringTransaction obj);
}