namespace TrackWallet.Models.ViewModel;

public class HomeVM
{
    public List<Wallet> Wallet { get; set; }
    public List<Transaction> Transaction { get; set; }
    public List<SharedWallet> SharedWallets { get; set; }
}