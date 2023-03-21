using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using mvc1.Models;

namespace mvc1.Controllers;

public class HomeController : Controller
{
    private IRepository repository;
    private readonly IHttpContextAccessor? _httpContextAccessor;
    private string message;
    public HomeController(IRepository repo,
     IHttpContextAccessor httpContextAccessor)
    {
        repository = repo;
        _httpContextAccessor = httpContextAccessor;
        var hostname = _httpContextAccessor?.HttpContext?.Request.Host.Value;
        var name = Dns.GetHostName(); // get container id
        var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
        Console.WriteLine(Environment.MachineName);
        message = $"Docker - ({hostname}-{ip})";
    }
    public IActionResult Index()
    {
        ViewBag.Message = message;
        return View(repository.Produtos);
    }
}
