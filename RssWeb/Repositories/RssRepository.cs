using RssWeb.Context;
using RssWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RssWeb.Repositories
{
    public class RssRepository : IRssRepository
    {
        private readonly RSScontext _context;
        private bool _disposed;

        /// <inheritdoc/>
        public RssRepository(RSScontext context) => _context = context;

        /// <inheritdoc/>
        public async Task<IEnumerable<RSS>> GetAllAsync()
        {
            return await Task.Run(() => _context.RSSs.ToList()).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<RSS> GetByIdAsync(string title, DateTime dateTime)
        {
            var rss = await _context.RSSs.FirstOrDefaultAsync(r => r.Headline == title && r.Date == dateTime);
            return rss;
        }

        /// <inheritdoc/>
        public async Task AddAsync(RSS rss)
        {
            _context.RSSs.Add(rss);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RSS>> AddRangeAsync(IEnumerable<RSS> rss)
        {
            _context.RSSs.AddRange(rss);

            await _context.SaveChangesAsync();

            return rss;
        }

        public IEnumerable<RSS> AddRange(IEnumerable<RSS> rss)
        {
            _context.RSSs.AddRange(rss);

            _context.SaveChanges();

            return rss;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RSS>> SelectAllAsync()
        {
            return await Task.Run(() => _context.RSSs.ToList()).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(RSS rss)
        {
            if (!(await _context.RSSs.ContainsAsync(rss))) return;

            _context.RSSs.Remove(rss);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<RSS>> SelectWhereAsync(Predicate<RSS> predicate)
        {
            var condition = new Func<RSS, bool>(predicate);

            return await Task.Run(() => _context.RSSs.Where(condition).ToList()).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}