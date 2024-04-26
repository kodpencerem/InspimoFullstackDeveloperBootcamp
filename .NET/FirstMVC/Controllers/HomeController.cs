using Microsoft.AspNetCore.Mvc;

namespace FirstMVC.Controllers;
public class HomeController : Controller
{
    public static List<string> Todos = new();
    public static string UpdateTodo = string.Empty;
    public static int UpdateIndex = 0;
    public IActionResult Index()
    {        
        return View(Todos);
    }
    //CRUD Operations => Create Read Update Delete
    [HttpGet]
    public IActionResult Save(string work)
    {
       
        Todos.Add(work);

        TempData["Message"] = "Todo create is successful";
        TempData["Type"] = "success";

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DeleteByIndex(int index)
    {
        index = index - 1;
        Todos.Remove(Todos[index]);

        TempData["Message"] = "Todo delete is successful";
        TempData["Type"] = "danger";

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Get(int index)
    {
        UpdateIndex = index - 1;
        UpdateTodo = Todos[UpdateIndex];

        TempData["UpdateWork"] = UpdateTodo;

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(string updateWork)
    {
        Todos[UpdateIndex] = updateWork;
        TempData["UpdateWork"] = string.Empty;

        TempData["Message"] = "Todo update is successful";
        TempData["Type"] = "info";

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Cancel()
    {
        TempData["UpdateWork"] = string.Empty;

        return RedirectToAction("Index");
    }

    public IActionResult Contact()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Send(string name, string email, string subject, string content)
    {
        //mail gönder

        return RedirectToAction("Contact", "Home");
    }
}
