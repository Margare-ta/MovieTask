using CinemaReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaReservation.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly string emailFormat = @"^[A-Za-z0-9]*$";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string name, string email, int guestsNumber)
    {
        if (string.IsNullOrEmpty(name) || !name.Contains(' '))
        {
            ViewBag.Message = "Name is incorrect!";
            ViewBag.Email = email;
            ViewBag.GuestsNumber = guestsNumber;
            return View();
        }
        if (email.Contains('@'))
        {
            string[] emailParts = email.Split('@');
            if (emailParts[0].Contains(emailFormat) || emailParts[1].Contains(emailFormat))
            {
                ViewBag.Message = "Email is incorrect!";
                ViewBag.Name = name;
                ViewBag.GuestsNumber = guestsNumber;
                return View();
            }
        }
        else
        {
            ViewBag.Message = "Email is incorrect!";
            return View();
        }


        if (guestsNumber < 1 || guestsNumber > 10)
        {
            ViewBag.Message = "There must be at least one guest or maximum 10!";
            ViewBag.Name = name;
            ViewBag.Email = email;
            return View();
        }
        ViewBag.Message = "";

        return Redirect("https://localhost:7070/Home/Privacy");

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
