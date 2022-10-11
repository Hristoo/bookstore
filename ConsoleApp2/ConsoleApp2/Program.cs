
using MessagePack;
using Newtonsoft.Json;

var product = new Product();

product.Name = "Apple";
product.ExpiryDate = new DateTime(2023, 02, 01);
product.Price = 3.99M;
product.Sizes = new List<string>() { "small", "medium" };

var product1 = new Product();
product1.Name = "Apple";
product1.ExpiryDate = new DateTime(2023, 02, 01);
product1.Price = 3.99M;
product1.Sizes = new List<string>() { "small", "medium" };


var list = new List<Product>()
{
    product1,product
};

//string output = JsonConvert.SerializeObject(list, Formatting.Indented);

//Console.WriteLine(output);

//var prod = JsonConvert.DeserializeObject<List<Product>>(output);

//Console.WriteLine(prod);

byte[] bytes = MessagePackSerializer.Serialize(list);

Console.WriteLine(bytes.Length);


var result = MessagePackSerializer.Deserialize<List<Product>>(bytes);

foreach (var product2 in result)
{
    Console.WriteLine(product2);
}

var json = MessagePackSerializer.SerializeToJson(list);

[MessagePackObject]
public record Product
{
    [Key(0)]
    public string  Name { get; set; }

    [Key(1)]
    public DateTime ExpiryDate { get; set; }

    [Key(2)]
    public decimal Price { get; set; }

    [Key(3)]
    public List<string> Sizes { get; set; }
}