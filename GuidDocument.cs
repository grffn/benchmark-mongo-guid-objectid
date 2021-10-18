using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace benchmark_mongo_guid_objectid
{
    public class GuidDocument : IDocument<Guid>
    { 
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }
    }
}
