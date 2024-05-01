using System.Text.Json;

namespace OOP.ConsoleApp2;

internal class Program
{
    static void Main(string[] args)
    {
        User user = new()
        {
            Name = "Taner Saydam",
            Email = "tanersaydam@gmail.com"
        };

        //string userString = JsonSerializer.Serialize(user);
        //object? userObject = JsonSerializer.Deserialize<User>(userString);

        var result = user.ToString();

        // { Name= "Taner Saydam", Email = "tanersaydam@gmail.com" }
        
    }

    static void Numbers()
    {

        bool isTrue = true; //0-1 0 => false 1=> true
        byte numBte = 255; //0-255
        short numShort = 5;    //-32568 + 32568     
        int numInt = 5; //2186555 +-
        long numLong = 5;
        double numDouble = 5;
        decimal numDecimal = 5; //18,2 16 karakter solda 2 karakter sağda 18,18
        float numFloat = 5; //
    }

    static void Strings()
    {
        int age = 34;
        string content = "Taner Saydam " + age + " yaşında";
        content = $"Taner Saydam {age} yaşında";

        content = string.Join(" ", "Taner", "Saydam"); //Taner Saydam

        string[] names = { "Taner","Saydam" };
        content = string.Join(" ", names);

        content = @$"
1
2
3
4
{age}
";


        List<User> users = new()
        {
           new User(){Name = "Taner Saydam", Email = "tanersaydam@gmail.com"},
           new User(){Name = "Ali Can Yücel", Email = "alicanyucel@gmail.com"}
        };

        content = string.Join("\n", users.Select(s => s.Name.Split(" ")[0]).ToList());
        content = string.Join("\n", users.Select(s => s.Name.Substring(0,3)).ToList()); Console.WriteLine(content);

        content = @"c:\\"; // c:/
        Console.WriteLine(content);

        char[] firstName = { 'T','a','n','e','r' };

        string lastName = "Saydam";
        char[] c = lastName.ToCharArray();
        


    }

    static decimal Multiply(params int[] number)
    {
        decimal total = 0;
        foreach (var num in number)
        {
            total *= num;
        }

        total = total * 5;
        total *= 5;
        total += 5;
        total -= 5;
        total /= 5;

        return total;
    }

    static void Sum(int a, int b, out int c)
    {
        //başka işlemler
        c = a + b;
        Console.WriteLine("C'nin işlem sonrası değeri: " + c);
    }

    private static void OutRefKeywords()
    {
        int a = 1;
        int b = 2;
        Sum(a, b, out int c);
        Console.WriteLine("C'nin son değeri: " + c);
    }
    private static void LinqMethods()
    {
        //linq methodları 
        //linq query

        string[] names = new string[2];
        names[0] = "Taner Saydam";
        names[1] = "Ahmet Yılmaz";

        List<string> nameList = new();
        nameList.Add("Taner Saydam");
        nameList.Add("Ahmet Yılmaz");
        nameList.Add("Elif Tuba");
        nameList.Remove("Elif Tuba");
        nameList[0] = "Toprak Saydam";
        int index = nameList.FindIndex(p => p == "Taner Saydam");
        nameList[index] = "Tahir Saydam";
        nameList.Where(p => p == "Taner Saydam").ToList();
        string n = nameList.OrderBy(p => p).Last();


        HashSet<string> nameHashSets = new()
        {
            "Taner",
            "Ahmet",
            "Taner"
        };

        nameHashSets.Add("Seval");
        nameHashSets.Remove("Taner");

        nameList.ToHashSet();

        //Linq Methodları
        /*
        - Add => ekleme işlemi
        - Remove => silme işlemi
        - find => lambda expression ile bir kaydı bulur, bulamazsa null döner
        - first => labda expression ile ilk kaydı bulur, bulamazsa hata verir
        - firstOrDefault => lambda expression ile ilk kaydı bulur, bulamazsa null döner
        - single => lambda expiression ile ilk kaydı bulur. Aynı kayıttan birden fazla varsa hata verir. Eğer kadı bulamazsa hata verir
        - singleOrDefault => lambda expiression ile ilk kaydı bulur. Aynı kayıttan birden fazla varsa hata verir. Eğer kaydı bulamazsa null döner
        - where => lambda expression ile arama yapar kaydı bulamazsa boş döner. kaydı bulursa bizden o kaydı geri labmda query ile kullanmamızı bekler çevirmemizi bekler
        - last => Lambda expiression ile son kaydı eğer sıralama yapılmışsa getirir. Kaydı bulamazsa hata fırlatır
        - lastOdefault => labmda expression ile son kaydı eğer sıralama yapılmışsa getirir. Kaydı bulamazsa nul döner
        - findIndex => lambda expiression ile kaydı bulur ve index numarasını döner. Bulamazsa -1 döner
        - ToList => kaydı listeye çevirir
        - ToHashSet => kaydı HashSet'e çevirir
        - OrderBy => lambda expression ile listeyi küçükten büyüğe sıralar
        - OrderByDesc => lambda expression ile listeyi büyükten küçüğe sıralar
         */

        IEnumerable<string> nameEnumerable = new string[3];


        ICollection<string> nameCollections = new string[2];
        Console.WriteLine("Hello, World!");
    }
}

internal abstract class Test
{
    public abstract void Connect();

    public virtual void Connect2()
    {
        Console.WriteLine("Test connect is started...");
    }
}

internal class Test2: Test
{
    public override void Connect()
    {
        Console.WriteLine("asdasd");
    }

    public override void Connect2()
    {
        base.Connect2();
        Console.WriteLine("Test2 connect is started");
    }
}

internal class User
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
