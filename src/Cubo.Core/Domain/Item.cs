using System;

namespace Cubo.Core.Domain
{
    public class Item
    {
        public Guid Id { get; protected set; }

        public string Key { get; protected set; }

        public string Value { get; protected set; }

        protected Item()
        {
        }

        public Item(string key, string value)
        {
            Key = key.ToLowerInvariant();
            Value = value;
        }
    }
}