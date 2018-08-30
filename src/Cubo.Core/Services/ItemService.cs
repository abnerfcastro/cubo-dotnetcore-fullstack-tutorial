using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cubo.Core.Repositories;
using Newtonsoft.Json;

namespace Cubo.Core.Services
{
    public class ItemService : IITemService
    {
        private readonly IBucketRepository _bucketRepository;

        public ItemService(IBucketRepository bucketRepository)
        {
            _bucketRepository = bucketRepository;
        }

        public async Task<ItemDTO> GetAsync(string bucketName, string key)
        {
            var bucket = await _bucketRepository.GetOrFailAsync(bucketName);
            var item = bucket.GetItemOrFail(key);

            return new ItemDTO();
        }

        public async Task<IEnumerable<string>> GetKeysAsync(string bucketName)
        {
            var bucket = await _bucketRepository.GetOrFailAsync(bucketName);

            return bucket.Items.Select(x => x.Key);
        }

        public async Task AddAsync(string bucketName, string key, object value)
        {
            var bucket = await _bucketRepository.GetOrFailAsync(bucketName);
            bucket.AddItem(key, JsonConvert.SerializeObject(value));
            await _bucketRepository.UpdateAsync(bucket);
        }

        public async Task RemoveAsync(string bucketName, string key)
        {
            var bucket = await _bucketRepository.GetOrFailAsync(bucketName);
            bucket.RemoveItem(key);
            await _bucketRepository.UpdateAsync(bucket);
        }
    }
}