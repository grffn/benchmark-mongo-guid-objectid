using System;
using System.Collections.Generic;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace benchmark_mongo_guid_objectid
{
    [GenericTypeArguments(typeof(Guid), typeof(GuidDocument))]
    [GenericTypeArguments(typeof(ObjectId), typeof(ObjectIdDocument))]
    public class Benchmarks<TDocumentId, TDocument>
        where TDocumentId : struct
        where TDocument: IDocument<TDocumentId>, new()
    {
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;

        [Params(1000, 10000)]
        public int N { get; set; }

        private TDocumentId[] _ids = new TDocumentId[10];

        [GlobalSetup]
        public void GlobalSetup()
        {
            var config = MongoUrl.Create("mongodb://localhost:27017/benchmark");
            _mongoClient = new MongoClient(config);
            _mongoDatabase = _mongoClient.GetDatabase(config.DatabaseName);
        }

        [Benchmark(Description = "Insert documents")]
        public void InsertDocuments()
        {
            for (var i = 0; i < N; i++)
            {
                _mongoDatabase.GetCollection<TDocument>("benchmark").InsertOne(new TDocument());
            }
        }

        [IterationSetup(Target = nameof(FindDocumentById))]
        public void InsertSetup()
        {
            var step = N / _ids.Length;
            for (var i = 0; i < N; i++)
            { 
                var document = new TDocument();
                _mongoDatabase.GetCollection<TDocument>("benchmark").InsertOne(document);
                if (i % step == 0)
                {
                    _ids[i / step] = document.Id;
                }
            }
        }

        [Benchmark(Description = "Find document by id")]
        public void FindDocumentById()
        {
            foreach (var id in _ids)
            {
                _mongoDatabase.GetCollection<TDocument>("benchmark").Find(find => find.Id.Equals(id));
            }
        }

        [IterationCleanup]
        public void GlobalCleanup()
        {
            _mongoDatabase.DropCollection("benchmark");
        }

    }
}
