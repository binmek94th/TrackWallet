using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IWalletRepository : IRepository<Wallet>
{
    void Update(Wallet obj);
}