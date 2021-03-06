﻿using Cubo.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cubo.Core.Services
{
    public interface IITemService
    {
        Task<ItemDTO> GetAsync(string bucketName, string key);

        Task<IEnumerable<string>> GetKeysAsync(string bucketName);

        Task AddAsync(string bucketName, string key, object value);

        Task RemoveAsync(string bucketName, string key);
    }
}