using RssWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssWeb.Repositories
{
    public interface ISourceRepository
    {
        Task<IEnumerable<RssSource>> GetAllAsync();

        Task<IEnumerable<RssSource>> AddRangeAsync(IEnumerable<RssSource> sources);

        Task<RssSource> GetById(int id);
    }
}
