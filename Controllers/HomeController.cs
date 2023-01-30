using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {

        private IRepository repository;

        public HomeController( IRepository repo)
        {  
            this.repository = repo;
            
        }

        public IActionResult Index([FromServices] ProductSum prodcuctSum)
        {
            // ViewBag.Total = productSum.Total;
            ViewBag.HomeControllerGuid = repository.ToString();
            ViewBag.TotalGuid = prodcuctSum.Repository.ToString();
            return View(repository.Products);
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
}