namespace BookStore.Caches
{
    public class Subcribe2Cache<TKey, TValue> 
    {
        public Dictionary<TKey, TValue> _dictionary;

        public Subcribe2Cache()
        {
            _dictionary = new Dictionary<TKey, TValue>();   
        }

        public Task GetAll(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}