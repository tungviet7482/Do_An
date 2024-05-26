using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;
using Transaction = Demo.Domain.Entity.Transaction;

namespace Domain.Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly DbRecruitmentContext _db;
        private readonly UserManager<User> _userManager;

        public TransactionRepository(DbRecruitmentContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(Transaction entity)
        {
            await _db.Transactions.AddAsync(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteAsync(Transaction entity)
        {
            entity.Deleted = true;
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public Task<bool> DeleteRangeAsync(List<Transaction> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            var transactions = await _db.Transactions.Where(x => !x.Deleted).ToListAsync();

            return transactions;
        }

        public async Task<Demo.Domain.Entity.Transaction?> GetByIdAsync(int id)
        {
            var transaction = await _db.Transactions.FindAsync(id);
            if (transaction != null)
            {
                return transaction.Deleted ? null : transaction;
            }
            return transaction;
        }

        public async Task<bool> UpdateAsync(Transaction entity)
        {
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<List<UserTransaction>> GetPagingAsync(int pageIndex, int pageSize, string keyWord)
        {
            var query = from t in _db.Transactions
                        join u in _db.Users
                        on t.UserId equals u.Id
                        where t.Deleted == false
                        select new UserTransaction
                        {
                            Id = t.Id,
                            TransactionDate = t.TransactionDate,
                            Fee = t.Fee,
                            Message = t.Message,
                            Status = t.Status,
                            StartDate = t.StartDate,
                            EndDate = t.EndDate,
                            UserId = t.UserId,
                            Deleted = t.Deleted,
                            FullName = u.FullName,
                            Email = u.Email,
                        };

            var list = await query
                .Where(x => string.IsNullOrEmpty(keyWord) || x.FullName!.Contains(keyWord))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            foreach (var i in list)
            {
                i.TypeUser = GetRoleByUserId(i.UserId ?? 0);
            }
            return list;
        }

        public async Task<int> GetTotalTransaction()
        {
            return await _db.Transactions.Where(x => x.Deleted).CountAsync();
        }

        public async Task<int> GetTotalTransactionByKeyword(string keyWord)
        {
            var query = from t in _db.Transactions
                        join u in _db.Users
                        on t.UserId equals u.Id
                        where t.Deleted == false
                        select new { t.Id, u.FullName};

            var total = await query
                .Where(x => string.IsNullOrEmpty(keyWord) || x.FullName!.Contains(keyWord)).CountAsync();

            return total;
        }

        public string? GetRoleByUserId(int userId)
        {
            var role = from ur in _db.UserRoles
                       join r in _db.Roles
                       on ur.RoleId equals r.Id
                       where ur.UserId == userId
                       select r.Name;
            return role.FirstOrDefault();
        }
    }
}
