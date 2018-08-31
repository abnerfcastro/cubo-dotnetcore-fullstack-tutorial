using Cubo.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cubo.Core.Repositories
{
    public interface IBucketRepository
    {
        Task<Bucket> GetAsync(string name);

        Task<IEnumerable<string>> GetNamesAsync();

        Task AddAsync(Bucket bucket);

        Task UpdateAsync(Bucket bucket);

        Task RemoveAsync(string name);
    }
}