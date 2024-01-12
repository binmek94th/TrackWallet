using TrackWallet.Models;

namespace TrackWallet.DataAccess.Repository.IRepository;

public interface ILoanAndDebtRepository : IRepository<LoanAndDebt>
{
    void Update(LoanAndDebt obj);
}