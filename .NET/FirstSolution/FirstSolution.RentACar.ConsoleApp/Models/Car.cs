namespace FirstSolution.RentACar.ConsoleApp.Models;

public class Car
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public short ProductionYear { get; set; } = 2000;
    public int KM { get; set; }
    public decimal DailyPrice { get; set; }
    public bool IsAvailable { get; set; } = true;
 }

//string name => değişken
//string Name {get; set;} => property


//default değerler
//int => 0
//string => null
//DateTime => 01.01.0001 00:00:00
//bool => false
//float,decimal,double,int,short,bit => 0
//object => null