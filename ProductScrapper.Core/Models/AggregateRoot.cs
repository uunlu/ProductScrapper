using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.Core.Models
{
    public class AggregateRoot
    {
        public AggregateRoot()
        {
            if (DateCreated == null)
                DateCreated = DateTime.UtcNow;

            DateModified = DateTime.UtcNow;
            IsActive = true;
            Id = Guid.NewGuid().ToString();
        }
        [BsonId]
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; private set; } 
        public DateTime DateModified { get; private set; }
    }
}
