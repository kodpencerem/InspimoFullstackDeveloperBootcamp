using DependencyInjection.New.WebAPI;

var builder = WebApplication.CreateBuilder(args);
//Service register is start here

builder.Services.AddMyDependencInjection();

builder.Services.AddEndpointsApiExplorer(); //extension methods
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

//Service register is stop here
var app = builder.Build();
//Middleware register is start here

app.UseSwagger();
app.UseSwaggerUI();

//Middleware register is stop here

app.MapControllers();
app.Run();




class Test
{
    public void Method()
    {
        //_product.Name = "Ahmet";
        //Console.WriteLine(_product.Name);

        //builder.Services.AddTransient<ProductService>();
        //builder.Services.AddTransient<Product>();
        //builder.Services.AddTransient<Product>();
        //builder.Services.AddScoped<Test>();
        //builder.Services.AddSingleton<Product>();

        //app.MapGet("create-product", (Product product, Test test, ProductService productService) =>
        //{
        //    //using(var product = new Product())
        //    //{

        //    //}
        //    product.Name = "Elif";
        //    test.Method();
        //    Console.WriteLine($"Name here: {product.Name}");


        //    //ProductService productService = new();
        //    productService.Create(product);

        //    return Results.Ok();
        //});

        //app.MapGet("update-product", (Product product, Test test1) =>
        //{
        //    // Test test = new(new Product());
        //    return Results.Ok();
        //});

        //app.Urls.Add("https://localhost:8888");
        //app.Urls.Add("https://localhost:7777");
        //app.Urls.Add("http://localhost:5555");
    }
}

