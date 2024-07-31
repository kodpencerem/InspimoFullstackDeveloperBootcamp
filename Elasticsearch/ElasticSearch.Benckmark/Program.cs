using BenchmarkDotNet.Running;
using Bogus;
using ElasticSearch.Benckmark;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello world!");

        //SeedData.Seed();

        BenchmarkRunner.Run<BenchmarkService>();
    }
}
public static class SeedData
{
    public static void Seed()
    {
        ElasticSearchService elasticSearchService = new();
        ApplicationDbContext context = new();

        elasticSearchService.CreateIndex();

        List<Product> products = new();

        for (int i = 0; i < 1000; i++)
        {
            Faker faker = new();
            string description = "";
            for (int j = 0; j < 200; j++)
            {
                description += faker.Commerce.ProductDescription() + " ";
            }

            Product product = new()
            {
                Name = faker.Commerce.ProductName(),
                Description = description,
                Price = Convert.ToDecimal(faker.Commerce.Price(100, 50000, 2, ""))
            };

            var result = elasticSearchService.Add(product);

            products.Add(product);
        }

        context.AddRange(products);
        context.SaveChanges();

        Console.WriteLine("Seed data başarıyla oluşturuldu");
        Console.ReadLine();
    }
}
