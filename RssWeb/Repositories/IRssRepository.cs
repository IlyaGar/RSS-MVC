using RssWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssWeb.Repositories
{
    public interface IRssRepository
    {
        /// <summary>
        /// Return the rss item.
        /// </summary>
        /// <param name="title">Title of the rss item.</param>
        /// <param name="dateTime">DateTime of the rss item.</param>
        /// <returns>RSS.</returns>
        Task<RSS> GetByIdAsync(string title, DateTime dateTime);

        /// <summary>
        /// Return rss item.
        /// </summary>
        /// <returns>All RSS.</returns>
        Task<IEnumerable<RSS>> GetAllAsync();

        /// <summary>
        /// Add new rss item in the repository.
        /// </summary>
        /// <param name="rss">New rss item to add.</param>
        Task AddAsync(RSS rss);


        /// <summary>
        /// Adds a collection of rss items.
        /// </summary>
        /// <param name="rss">The specific collection of rss items for adding.</param>
        /// <returns>A task that represents the asynchronous add operation.
        /// The task result contains the collection of ids of the rss items written to the database.</returns>
        Task<IEnumerable<RSS>> AddRangeAsync(IEnumerable<RSS> rss);

        IEnumerable<RSS> AddRange(IEnumerable<RSS> rss);

        /// <summary>
        /// Provides a collection of all rss items.
        /// </summary>
        /// <returns>A task that represents the asynchronous select operation.
        /// The task result contains the collection of all rss items from the database.</returns>
        Task<IEnumerable<RSS>> SelectAllAsync();

        /// <summary>
        /// Provides a collection of rss item that satisfy the condition.
        /// </summary>
        /// <param name="predicate">The condition that collection of rss items must satisfy</param>
        /// <returns>A task that represents the asynchronous select operation.
        /// The task result contains the collection of rss items from the database that satisfy a condition.</returns>
        Task<IEnumerable<RSS>> SelectWhereAsync(Predicate<RSS> predicate);

        /// <summary>
        /// Delete record of the rss item in the repository.
        /// </summary>
        /// <param name="rss">Rss item for delete.</param>
        Task DeleteAsync(RSS rss);
    }
}
