using FPTBook.DataAccess.Data;
using FPTBook.DataAccess.Repository.IRepository;
using FPTBook.Models;

namespace FPTBook.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}