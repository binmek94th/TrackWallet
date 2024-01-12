namespace TrackWallet.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IUserSelectedCategoryRepository UserSelectedCategory { get; }
    IWalletRepository Wallet { get; }
    IBudgetRepository Budget { get;  }
    IEventRepository Event { get; }
    IGoalRepository Goal { get; }
    IRecurringTransactionRepository RecurringTransaction { get; }
    IOccasionRepository Occasion { get; }
    ILoanAndDebtRepository LoanAndDebt { get; }
    void Save();
}