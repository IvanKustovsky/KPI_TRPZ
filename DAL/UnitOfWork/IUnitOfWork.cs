using DAL.Repositories.Interfaces;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ISanatoriumRepository Sanatoriums { get; }
        IVoucherRepository Vouchers { get; }
        void Save();
    }
}
