namespace benchmark_mongo_guid_objectid
{
    public interface IDocument<TDocumentId> where TDocumentId : struct
    {
        public TDocumentId Id { get; set; }
    }
}
