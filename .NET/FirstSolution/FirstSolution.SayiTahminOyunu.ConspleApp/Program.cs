Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Merhaba. Sayı tahmin oyununa hoş geldin. Ben NumberAI");

int number = new Random().Next(1,100); //static => dynamic
int answerNumber = 0;

while (number != answerNumber)
{
    Console.WriteLine("1-100 arasında bir sayı tuttum. Bunu tahmin edebilir misin?");
    Console.WriteLine("Sayı girin:");
    Console.ForegroundColor = ConsoleColor.White;
    string? answer = Console.ReadLine();

    int.TryParse(answer, out answerNumber);

    if (number == answerNumber)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Tekbrikler tuttuğum sayıyı bildin!");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Üzgünüm bilemedin. Tekrar denemek ister misin?");

    }
}

