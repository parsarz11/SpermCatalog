using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpermCatalog.DataAccess.Entities
{
    public class RangeFilter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Category { get; set; }
        public string? UserId { get; set; }
        public string? HerdId { get; set; }
        public string? HerdName { get; set; }
        public string Index { get; set; }
        public double MinValue { get; set; } = -9999999;
        public double MaxValue { get; set; } = 9999999;

        public DateOnly FilterDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
