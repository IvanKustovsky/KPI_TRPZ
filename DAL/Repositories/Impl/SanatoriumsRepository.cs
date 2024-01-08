using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Impl
{
    public class SanatoriumRepository : BaseRepository<Sanatorium>, ISanatoriumRepository
    {
        public SanatoriumRepository(DbContext context) : base(context)
        {
        }
    }
}
