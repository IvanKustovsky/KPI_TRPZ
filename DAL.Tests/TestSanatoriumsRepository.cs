using DAL.Entities;
using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests.Repositories
{
    public class TestSanatoriumRepository : BaseRepository<Sanatorium>, ISanatoriumRepository
    {
        public TestSanatoriumRepository(DbContext context) : base(context)
        {
        }
    }
}
