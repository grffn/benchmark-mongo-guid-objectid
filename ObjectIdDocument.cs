using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace benchmark_mongo_guid_objectid
{
    public class ObjectIdDocument : IDocument<ObjectId>
    {

        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }
    }
}
