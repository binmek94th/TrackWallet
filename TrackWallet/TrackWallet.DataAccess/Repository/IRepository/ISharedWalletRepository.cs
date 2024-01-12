using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface ISharedWalletRepository : IRepository<SharedWallet>
{
    void Update(SharedWallet obj);
}