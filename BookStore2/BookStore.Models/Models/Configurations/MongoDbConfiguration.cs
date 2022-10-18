namespace BookStore.Models.Models.Configurations
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ShopingCartDatabase { get; set; }
        public string PurchaseDatabase { get; set; }

    }
}
