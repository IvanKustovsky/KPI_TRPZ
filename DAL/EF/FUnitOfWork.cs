
using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork 
    { 
        private readonly SanatoriumDbContext _context;
        private SanatoriumRepository _sanatoriumRepository;
        private VoucherRepository _voucherRepository;

        public EFUnitOfWork(DbContextOptions options)
        {
            _context = new SanatoriumDbContext(options);
        }

        public ISanatoriumRepository Sanatoriums
        {
            get
            {
                if (_sanatoriumRepository == null) {
                    _sanatoriumRepository = new SanatoriumRepository(_context);
                }
                return _sanatoriumRepository;
            }
        }

        public IVoucherRepository Vouchers
        {
            get
            {
                _voucherRepository ??= new VoucherRepository(_context);
                return _voucherRepository;
            }
        }
    
        public void Save()
        {
            _context.SaveChanges();
        }
    
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
                _context.Dispose();
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
