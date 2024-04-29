using OOP.ClassLibrary;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

class Program
{
    public static List<Category> Categories { get; set; } = new();
    public static void Main(string[] args)
    {

        //var ornek1 = new Example("Taner Saydam"); //object
        //var ornek2 = new Example("Taner Saydam"); //object
        //var ornek3 = new Example("Taner Saydam"); //object
        //var ornek4 = new Example("Taner Saydam"); //object
        //ornek1.Name = "asdasd";
        //ornek2.Name = "asdasd";
        //ornek3.Name = "asdasd";
        //ornek4.Name = "asdasd";
        //class lar newlendiği zaman instance yani örneği türetilir

        // ornek1.Method();

        //var object2 = new AbstractExample(); abstract classlar newlenemez

        //var user = new User();
        //user.Name = "";

        //new IUser(); interfaceler newlenemez

        // IUser taner = new Product();


        //IDatabase db = new ElasticSearch();
        //db.Connect();

        // Kodu çalıştır
        // kodu daha iyi kodla
        // kodu daha hızlı çalıştır

        //hepsinin ana odak noktası kodun okunabilir olması
        //okunabilir kod daha az yazılan koddan daha iyidir
        //


        //Category category = new();
        //category.Name = "Test";
        //category.Count = 10;

        //Categories.Add(category);

        //var list = Categories;

        //Categories.Add(category);

        //Category category = new();
        // Category.ChangeName("Taner Saydam");
        //Console.WriteLine(Category.Name);

        //Category category2 = new();
        //Category.ChangeName("Ahmet");
        //Console.WriteLine(Category.Name);

        //  Category category = new("Taner Saydam");
        //category.ChangeName("Taner Saydam",10);
        //        Console.WriteLine(category.Name);

        //object name = new Category();

        // Car car = new Car();
        //car.Name = "Kara Şimşek";
        //car.Brand = "asdsad";
        //car.Model = "X Serisi";

        //Category category = new Category();

        //var testObject = new
        //{
        //    Name = "Taner",
        //    LastName = "Saydam",
        //    Age = 34,
        //    City = "Kayseri"
        //};

        //Console.WriteLine(testObject.Name);

        //testObject.Name = "asdasd";
       
        //string[] names = new string[3]; //array
        //int[] ages = [1, 2, 3];

        //names[2] = "Taner Saydam";
        //names[0] = "Ahmet";
        //ages[3] = 30;

        //array de sabit sayıda kayıt olmak zorunda
        //kayıt ekleyemezsiniz
        //sadece mevcut kaydı güncelleyebilirsiniz
        //kayıt silemezsiniz

        //List<string> names2 = new();
        //names2.Add("Taner"); //ekleme
        //names2.Add("Seval");       
        //names2.Add("Ahmet");


        //names2[0] = "Seval";//güncelleme

        //names2.Remove("Seval"); //silme

        //var list = names2.ToList(); //listeye çevirmek için

        //names2.Where(p => p == "Seval").First();

        //names2.First();

        //var list2 = names2.OrderBy(o => o).ToList();

        //names2.AddRange(new List<string>() { "isim 1", "İsim 2" });

        //names2.RemoveRange(0, 2);

        //GenericTest<string> genericTest1 = new();
        //GenericTest<int> genericTest2 = new();
        //GenericTest<decimal> genericTest3 = new();

        List<NewProduct> products = new();

        products.Add(
            new NewProduct() 
            { 
                Name = "Domates", 
                Price = 10, 
                Stock = 20 
            }
            );

        var product1 = new NewProduct();
        product1.Name = "Salatalık";
        product1.Stock = 300;
        product1.Price = 15.20m;

        products.Add(product1);

        var product2 = new NewProduct()
        {
            Name = "Domates",
            Price = 5,
            Stock = 3
        };

        products.Add(product2);

        int totalStock = products.Sum(s => s.Stock);
        decimal totalAmount = products.Sum(s=> s.Stock*s.Price);

        NewProduct enPahaliUrunum1 = products.OrderBy(p => p.Price).Last();
        NewProduct enPahaliUrunum2 = products.OrderByDescending(p => p.Price).First();

        var last = products.Last();

        foreach(var p in products)
        {
            Console.WriteLine(p.Name);
        }

        products.ForEach(p => Console.WriteLine(p.Name));
        products.ForEach(p =>
        {
            Console.WriteLine(p.Name);
        });

        void Test()
        {
            Console.WriteLine("Merhaba");
        }

        void Test2() => Console.WriteLine("asdasd");

        int Calculate1(int count) => 15 + 20 + count;

        int Calculate2(int count)
        {
            return 15 + 20 + count;
        }

        NewProduct? expected = products.FirstOrDefault(p=> p.Name == "Taner");

        //Console.WriteLine(expected!.Price);

        if (expected is not null)
        {
            Console.WriteLine(expected.Price);
        }

       // NewProduct? expected2 = products.SingleOrDefault(p=> p.Name == "Domates");

        HashSet<NewProduct> products2 = new();
        //products2.Add(product2);
        //products2.Add(product2);
        //products2.Add(product2);

        products.Add(product2);

        products2 = products.ToHashSet();   //linq metotları      
    }
}

public class NewProduct
{
    public string Name { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }    
}

//static => method, property, değişken, class larda kullanılabiliyor


//erişim belirleyici | access modifier
//public private internal => class larda 
//değişken ve metotlarda public private internal protected 

public class GenericTest<T>
{
    public T Value { get; set; }
}
public class Category : Car
{
    public string Name { get; set; } = string.Empty;
    public Category(string name)
    {
        Name = name;
        //Brand => protected sadece interit edilmiş class içerisinde kullanılmasına müsade etme
    }

    public Category()
    {
        Name = "Değer yok!";
    }
    
    public int Count { get; set; }

    public void ChangeName(string name)
    {
        Console.WriteLine("Birinci overload kullanıldı");
    }

    public void ChangeName(string name, int age)
    {
        Console.WriteLine("İkinci overload kullanıldı");
    }
}
public interface IDatabase
{
    void Connect();
}

public class MSSQL : IDatabase
{
    public void Connect()
    {
        Console.WriteLine("MSSQL ile connection yapıldı...");
    }
}

public class PostgreSQL : IDatabase
{
    public void Connect()
    {
        Console.WriteLine("PostgreSQL ile connection yapıldı...");
    }
}

public class ElasticSearch : IDatabase
{
    public void Connect()
    {
        Console.WriteLine("Elasticsearch ile connection yapıldı...");
    }
}

public class Example
{
    public string _name; //değişken
    public int Age { get; set; }// => property sondaki kısma get set bloğu
    public Example(string name) //constructor
    {
        if (name.Length > 3)
        {
            //this.name = name;?
            _name = name;
            Age = 5;
        }
        else
        {
            throw new ArgumentException();
        }

    }

    public void Method()
    {
        //işlemler
        //return "asdasd";
    }
}

public abstract class AbstractExample //classlarda yapılan her şeyi yapabilir.
{

}

public class User : SharedAbstract, IUser, IExample //inherit => bir classın başka bir classa dahil olması manasına gelir
{

}

public class Product : IUser //inherit sadece bir defa yapılabilir
{
    public int Age { get; set; }

    public void Method()
    {
        //throw new NotImplementedException();
    }
}

public abstract class SharedAbstract : IUser //implement
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void Method()
    {
        //throw new NotImplementedException();
    }
}

public interface IUser
{
    int Age { get; set; } //bu bir imza
    void Method(); //bu bir imza
}

public interface IExample
{

}
