using MongoDB.Driver;

public class CounterService
{
    private readonly IMongoCollection<Counter> _counterCollection;

    public CounterService(IMongoDatabase database)
    {
        _counterCollection = database.GetCollection<Counter>("Counters");
    }

    public async Task<long> GetNextSequenceValueAsync(string counterName)
    {
        var filter = Builders<Counter>.Filter.Eq(c => c.Id, counterName);
        var update = Builders<Counter>.Update.Inc(c => c.Value, 1);
        var options = new FindOneAndUpdateOptions<Counter>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        var counter = await _counterCollection.FindOneAndUpdateAsync(filter, update, options);
        return counter.Value;
    }
}
