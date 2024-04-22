using FirstSolution.RentACar.ConsoleApp.Models;
List<Car> cars = SeedCarData();
List<Customer> customers = new();
string? select = string.Empty;

SeedCustomerData();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("TS Rent a Car'a hoşgeldiniz...");
Console.ForegroundColor = ConsoleColor.White;

while (true)
{
    Console.WriteLine("------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Devam edebilmek için giriş yapmalısınız.");
    Console.WriteLine("Lütfen giriş bilgilerinizi doldurun");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("------------------------------------------------");
    Console.WriteLine("Kullanıcı Adı:");
    string? userName = Console.ReadLine();
    Console.WriteLine("Şifre:");
    string? password = Console.ReadLine();
    Console.WriteLine("------------------------------------------------");

    if (userName == "admin" && password == "1")
    {
        ShowAdminPanel();
    }
    else
    {
        bool isCustomerExists = customers.Any(p => p.UserName == userName && p.Password == password);

        if (isCustomerExists)
        {
            ShowCustomerPanel();
        }
    }

    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Kullanıcı adı ya da şifre bilgisini yanlış girdiniz.");

    Console.ForegroundColor = ConsoleColor.White;
}



void ShowCustomerPanel()
{

}

void ShowAdminPanel()
{
    

    while (select == string.Empty || Convert.ToInt16(select) > 4)
    {
        ShowMenu();

        if (select == "1")
        {
            ShowCarList();
        }
        else if (select == "2")
        {
            CreateCar();
        }
        else if (select == "3")
        {
            ShowCustomerList();
        }
        else if (select == "4")
        {
            CreateCustomer();
        }
    }
}

List<Car> SeedCarData()
{
    return new()
            {
                new Car()
                {
                    Brand = "Mercedes",
                    Model = "E Serisi",
                    ProductionYear = 2024,
                    KM = 100,
                    DailyPrice = 4500,
                    IsAvailable = true,
                },
                new Car()
                {
                    Brand = "BMW",
                    Model = "M Serisi",
                    ProductionYear = 2023,
                    KM = 18000,
                    DailyPrice = 3500,
                    IsAvailable = true,
                }
            };
}

void SeedCustomerData()
{
    Customer customer1 = new()
    {
        UserName = "taner",
        Password = "1",
        Name = "Taner Saydam",
        TaxDepartment = "Mimarsinan",
        TaxNumber = "11111111111",
        Address = "Kayseri / Kocasinan"
    };

    Customer customer2 = new()
    {
        UserName = "toprak",
        Password = "1",
        Name = "Toprak Saydam",
        TaxDepartment = "Sarıoğlan",
        TaxNumber = "22222222222",
        Address = "Kayseri / Sarıoğlan"
    };


    customers.AddRange(new List<Customer> { customer1, customer2 });
}

void ShowMenu()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Admin panele hoş geldiniz...");
    Console.WriteLine("Ben AI. Size nasıl yardımcı olabilirim?");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("------------------------------------------------");
    Console.WriteLine("1. Araç listesini görüntüle");
    Console.WriteLine("2. Araç ekle");
    Console.WriteLine("3. Müşteri listesini görüntüle");
    Console.WriteLine("4. Müşteri ekle");
    Console.WriteLine("------------------------------------------------");
    Console.WriteLine("e. Çıkış yap");

    select = Console.ReadLine();
    if (select is null || select == string.Empty || Convert.ToInt16(select) > 4)
    {
        Console.WriteLine("Lütfen listedeki seçeneklerden birini seçin!");
        Console.WriteLine("");
    }
}

void ShowCarList()
{
    Console.Clear();
    Console.WriteLine("Araç listesi:");
    Console.WriteLine("------------------------------------------------");
    Console.WriteLine("# | Araç Makası | Araç Modeli | Araç Üretim Yılı | Araç KM  | Araç Günlük Fiyatı | Araç Müsait Mi? ");
    Console.WriteLine("------------------------------------------------");

    for (int i = 0; i < cars.Count; i++)
    {
        var item = cars[i];
        Console.WriteLine($"{i + 1} | {item.Brand} | {item.Model} | {item.ProductionYear} | {item.KM} | {item.DailyPrice} | {item.IsAvailable} ");
    }

    Console.WriteLine("------------------------------------------------");
    Console.WriteLine("Ana menüye dönmek için [enter] tuşuna basın");
    select = string.Empty;
    Console.Read();
}

void CreateCar()
{
    Console.Clear();
    string? response = "y";

    while (response == "y")
    {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Araç eklemek için aşağıdaki bilgileri doldurun");
        Console.WriteLine("------------------------------------------------");
        Car car = new(); //instance türetme > örnek oluşturma

        Console.WriteLine("Aracın markası:");
        car.Brand = Console.ReadLine() ?? "";

        Console.WriteLine("Aracın modeli");
        car.Model = Console.ReadLine() ?? "";

        Console.WriteLine("Aracın üretim yılı");
        car.ProductionYear = Convert.ToInt16(Console.ReadLine());

        Console.WriteLine("Aracın KM'si");
        car.KM = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Aracın günlük ücreti");
        car.DailyPrice = Convert.ToDecimal(Console.ReadLine());

        cars.Add(car);

        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Araç başarıyla eklendi.");
        Console.WriteLine("------------------------------------------------");

        Console.WriteLine("Yeni bir araç eklemek istiyor musunuz? (y/n)");
        response = Console.ReadLine();
        if (response == "") response = "y";

        Console.Clear();
        //ternary operatörü / single if line
    }


    select = string.Empty;
    //ctrl k + d kodların yapısını düzeltir
}

void ShowCustomerList()
{
    Console.Clear();
    Console.WriteLine("Müşteri listesi:");
    Console.WriteLine("------------------------------------------------");
    Console.WriteLine("# | Müşteri Adı | Vergi Dairesi | Vergi Numarası | Adres | K.Adı | Şifre ");
    Console.WriteLine("------------------------------------------------");

    for (int i = 0; i < customers.Count; i++)
    {
        var item = customers[i];
        Console.WriteLine($"{i + 1} | {item.Name} | {item.TaxDepartment} | {item.TaxNumber} | {item.Address} | {item.UserName} | {item.Password} ");
    }

    Console.WriteLine("------------------------------------------------");
    Console.WriteLine("Ana menüye dönmek için [enter] tuşuna basın");
    select = string.Empty;
    Console.Read();
}

void CreateCustomer()
{
    Console.Clear();
    string? response = "y";

    while (response == "y")
    {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Müşteri eklemek için aşağıdaki bilgileri doldurun");
        Console.WriteLine("------------------------------------------------");
        Customer customer = new();

        Console.WriteLine("Müşteri adı:");
        customer.Name = Console.ReadLine() ?? "";

        Console.WriteLine("Vergi dairesi");
        customer.TaxDepartment = Console.ReadLine() ?? "";

        Console.WriteLine("Vergi numarası");
        customer.TaxNumber = Console.ReadLine() ?? "";

        Console.WriteLine("Adres");
        customer.Address = Console.ReadLine() ?? "";

        Console.WriteLine("Kullanıcı Adı");
        customer.UserName = Console.ReadLine() ?? "";

        Console.WriteLine("Şifre");
        customer.Password = Console.ReadLine() ?? "";

        customers.Add(customer);

        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Müşteri başarıyla eklendi.");
        Console.WriteLine("------------------------------------------------");

        Console.WriteLine("Yeni bir müşteri eklemek istiyor musunuz? (y/n)");
        response = Console.ReadLine();
        if (response == "") response = "y";

        Console.Clear();
    }


    select = string.Empty;
    //ctrl k + d kodların yapısını düzeltir
}

void Examples() //metotlar çağrılana kadar çalışmazlar
{
    //araç kaydebileceğiz
    //Müşteri kaydedebileceğiz
    //giriş yapabileceğiz
    //araç kiralayabileceğiz
    //iş analizi


    //araç => marka | model | km | günlük kiralama ücreti | araç müsait mi


    //classlar newlenmeden kullanılamaz!!!



    var firstCar = new Car();
    firstCar.Brand = "Mercedes";
    firstCar.Model = "E Serisi";
    firstCar.ProductionYear = 2024;
    firstCar.KM = 100;
    firstCar.DailyPrice = 3500;
    //firstCar.IsAvailable = true;

    var secondCard = new Car()
    {
        Brand = "BMW",
        Model = "Example",
        ProductionYear = 2023,
        KM = 0,
        DailyPrice = 3000,
    };//ctrl + boşluk

    cars.Add(firstCar);
    cars.Add(secondCard);
}