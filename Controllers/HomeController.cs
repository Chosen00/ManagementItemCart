using CustomerCart.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomerCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OrderedContext _orderedContext;

        public HomeController(ILogger<HomeController> logger, OrderedContext orderedContext)
        {
            _logger = logger;
            _orderedContext = orderedContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DashboardForm()
        {
            return View();
        }


        // GET: /SoldOrdered/HistoryTable
        [HttpGet]
        public IActionResult HistoryTable()
        {
            // Retrieve the sold and ordered item IDs
            var soldItemIDs = _orderedContext.GetSoldItemIDs();
            var orderedItemIDs = _orderedContext.GetOrderedItemIDs();

            // Insert the retrieved IDs into the sold_ordered_table
            bool success = _orderedContext.InsertIntoSoldOrderedTable(soldItemIDs, orderedItemIDs);

            if (success)
            {
                // If insertion is successful, retrieve the list of sold ordered items and display them
                var soldOrderedList = _orderedContext.GetSoldOrderedList();
                return View(soldOrderedList);
            }
            else
            {
                // If insertion fails, display an error message
                ViewData["ErrorMessage"] = "Failed to insert into sold_ordered_table.";
                return View("Error");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
