namespace eCommerce.MVC.Models;

public sealed class Product
{
    public string ImageUrl { get; set; } = string.Empty; //""
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
}