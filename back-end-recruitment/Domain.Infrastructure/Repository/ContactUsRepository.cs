using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Domain.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;
using System.Diagnostics.Metrics;

namespace Domain.Infrastructure.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly DbRecruitmentContext _db;
        private readonly FilterHandler _filterHandler;

        public ContactUsRepository(DbRecruitmentContext db, FilterHandler filterHandler)
        {
            _db = db;
            _filterHandler = filterHandler;
        }

        public async Task<bool> CreateAsync(ContactUs entity)
        {
            await _db.ContactUs.AddAsync(entity);
            var row = await _db.SaveChangesAsync();
            return row > 0;
        }

        public async Task<bool> DeleteAsync(ContactUs entity)
        {
            _db.ContactUs.Remove(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }
        public async Task<bool> ProcessRangeAsync(List<long> ids)
        {
            var contacts = _db.ContactUs.Where(x => ids.Contains(x.Id));
            foreach (var contact in contacts)
            {
                contact.Processed = true;
            }
            var row = await _db.SaveChangesAsync();
            return row == ids.Count();
        }
        public Task<bool> DeleteRangeAsync(List<ContactUs> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContactUs>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ContactUs?> GetByIdAsync(long id)
        {
            var news = await _db.ContactUs.FindAsync(id);

            return news;
        }

        public async Task<bool> UpdateAsync(ContactUs entity)
        {
            _db.ContactUs.Update(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<DataObject<ContactUs>> GetPagingAsync(FilterContactUs filter)
        {
            var contacts = _db.ContactUs
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .Where(x => x.Processed == filter.Processed && (string.IsNullOrEmpty(filter.KeyWord) || x.Message.Contains(filter.KeyWord)));

            var total = await contacts.CountAsync();
            if (filter.PageIndex > 0)
            {
                contacts = contacts
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize);
            }
            return new DataObject<ContactUs>(await contacts.ToListAsync(), total);
        }
    }
}
