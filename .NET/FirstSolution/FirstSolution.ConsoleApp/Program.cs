namespace FirstSolution.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        //ctrl + k + d => kodları düzeltiyor

        #region Değişkenler

        //string, int, bool, decimal, double, float, object, DateTime, DateOny, TimeOnly, short, long, char

        string name = "Taner Saydam";

        string message = $"Hello \n{name}";

        int num = 0; //tam sayı değerleri alıyor +2146000 -2146000
        bool isActive = false;
        decimal decimalNum = 0; //küsüratlı sayıları tutabiliyor 18 karaktere çıkabiliyor
        double doubleNum = 0; //küsürat hanesi default 2 tanedir. 
        float floatNum = 0; //decimal ve double'dan daha büyük bir rakam ihtiyacınız varsa
        object firstName = "Taner";

        object x = 0;
        object y = 1;

        var z = (int)x + (int)y;

        DateTime date = Convert.ToDateTime("20.04.2024 22:05:03"); //DateTime.Now; //20.04.2024 => 20.04.2024 00:00:00
        DateOnly dateOnly = DateOnly.FromDateTime(date); //=> 20.04.2024
        TimeOnly timeOnly = TimeOnly.FromDateTime(date); //=> 22:05:03;
        dateOnly.AddDays(1);
        dateOnly.AddMonths(1);


        short shortNum = 0; // -32.768 +32.767
        long longNum = 0; //-9.223.372.036.854.775.808 - 9.223.372.036.854.775.807

        string ageString = "10";
        int ageNumber = int.Parse(ageString);
        int.TryParse(ageString, out ageNumber);



        string dateString = "20.04.2024 22:05:03";
        DateTime date2;
        bool dateConvertResult = DateTime.TryParse(dateString, out date2);
        DateTime.TryParse(dateString, out DateTime date3);
        //Date türlerinin default değeri 01.01.0001 00:00:00
        #endregion

        #region Şart Blogları
        if(name == "Taner Saydam")
        {
            //bu işlemi yap
        }

        if(ageNumber > 20)
        {
            //bunu yap
        }else if(ageNumber < 30)
        {
            //bunu yap
        }
        else //üstteki şartların sağlanmadığı durumda çalışır. örnek kodumuza göre ageNumber == 20 ise çalışır
        {
            //bunu yap
        }

        if(ageNumber >= 18)
        {
            Console.WriteLine("Girişinize izin verildi");
        }
        else
        {
            Console.WriteLine("18 yaşından küçükler giremez!");
        }


        //ternary operatörü => single if line
        //şart ? //doğruysa bunu yap : //yanlış ise bunu yap
        var result = (ageNumber >= 18 ? ageNumber++ : ageNumber--);


        switch (ageNumber)
        {
            case <=10:
                Console.WriteLine("Girişinize izin verildi");
                break;
            case 20:
                Console.WriteLine("18 yaşından küçükler giremez!");
                break;
            default:
                Console.WriteLine("Default seçenek seçildi");
                break;
        }


        if (name.StartsWith("T"))
        {

        }
        else
        {

        }
        #endregion

        #region Döngüler
        //for foreach while

        //for belirbir bir sayıda dönderiyor
        for(int i = 0; i < 10; i++)
        {
            //if(i == 5)
            //{
            //    continue;
            //}

            if(i % 2 != 0)
            {
                continue; //sonraki döngüye geç ve alttaki kodları çalıştırma
            }
             Console.WriteLine(i);            
        }

        List<string> names = new() { "Taner", "Ahmet", "Ayşe" };
        string[] namesArray = ["Taner", "Ahmet", "Ayşe"];
        //foreach listenin tamamını dönderir
        foreach(string element in names)
        {
            if(element == "Taner")
            {
                break; //döngüyü kır ve döngüden çık
            }
        }

        //while şart sağlandığı sürece //sonsuz döngü
        while(ageNumber > 18)
        {
            ageNumber--;
        }



    #endregion

        #region Goto
        tekrar:;
            int a = 0;
            int b = 1;

            int c = a + b;
            if (c > 3) goto tekrar;


            while(c > 3) c = a + b;//şart blogları ya da döngülerde eğer tek satır kod yazıyorsak içerisine süslü paraneteze illa gerek yok
        #endregion

        Console.WriteLine((string)firstName);
    }
}
