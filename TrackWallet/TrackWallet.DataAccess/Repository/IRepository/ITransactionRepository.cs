using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface ITransactionRepository : IRepository<Transaction>
{
    void Update(Transaction obj);
}