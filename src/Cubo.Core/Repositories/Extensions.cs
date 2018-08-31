﻿using Cubo.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Cubo.Core.Repositories
{
    public static class Extensions
    {
        public static async Task<Bucket> GetOrFailAsync(this IBucketRepository repository, string name)
        {
            var bucket = await repository.GetAsync(name);

            if (bucket == null)
            {
                throw new Exception($"Bucket: '{name}' was not found.");
            }

            return bucket;
        }
    }
}