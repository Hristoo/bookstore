using BookStore.Models.Models;

namespace BookStore2
{
    public class DataflowClass
    {
        public Dictionary<int, Purchase> _purcases;

        public DataflowClass(Dictionary<int, Purchase> purcases)
        {
            _purcases = purcases;
        }
    }
}
