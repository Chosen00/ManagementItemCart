using Microsoft.AspNetCore.Mvc;
using CustomerCart.Models;
using System;
using Microsoft.AspNetCore.Identity;


namespace CustomerCart.Controllers
{
    public class OrderedController : Controller
    {
        private readonly OrderedContext _orderedContext;

        public OrderedController(OrderedContext orderedContext)
        {
            _orderedContext = orderedContext;
        }

        //Create
        [HttpGet]
        public IActionResult OrderedForm()
        {
            var ordered = new OrderedModel();
            return View(ordered);
        }

        [HttpPost]
        public IActionResult OrderedForm(OrderedModel ordered)
        {
            try
            {
                bool isSuccess = _orderedContext.InsertOrdered(ordered);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Order Added Successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error occurred while saving data.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessageCatch"] = $"Error: {ex.Message}";
            }
            return View(ordered);
        }


        [HttpGet]
        public IActionResult UpdateOrderedForm(int orderedId)
        {
            var orderedIds = _orderedContext.GetOrderedById(orderedId);
            return View(orderedIds);
        }

        [HttpPost]
        public IActionResult UpdateOrderedForm(OrderedModel updatedordered)
        {
            try
            {
                int orderedId = updatedordered.OrderedItemId;
                bool isSuccess = _orderedContext.UpdateOrdered(updatedordered);

                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Ordered information updated successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = orderedId;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessageCatch"] = $"Error: {ex.Message}";
            }

            return View(updatedordered);
        }

        [HttpPost]
        public IActionResult JoinTableForm(int orderedItemId, int soldItemId)
        {
            try
            {
                bool isOrderedDeleted = _orderedContext.DeleteOrdered(orderedItemId);
                bool isSoldDeleted = _orderedContext.DeleteSold(soldItemId);

                if (isOrderedDeleted && isSoldDeleted)
                {
                    TempData["SuccessMessage"] = "Ordered and Sold items deleted successfully!";
                }
                else if (isOrderedDeleted)
                {
                    TempData["SuccessMessage"] = "Ordered item deleted successfully!";
                    TempData["ErrorMessage"] = "Error deleting sold item.";
                }
                else if (isSoldDeleted)
                {
                    TempData["SuccessMessage"] = "Sold item deleted successfully!";
                    TempData["ErrorMessage"] = "Error deleting ordered item.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error deleting both ordered and sold items.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
            }
            return RedirectToAction("JoinTableForm");
        }




        // GET: /Ordered/JoinTableForm
        [HttpGet]
        public IActionResult JoinTableForm()
        {
            var orderedList = _orderedContext.GetOrderedList();
            var soldList = _orderedContext.GetSoldList();

            var model = new JoinModel
            {
                OrderedItems = orderedList,
                SoldItems = soldList
            };

            return View(model);
        }
        private List<OrderedModel> GetOrderedList()
        {
            List<OrderedModel> orderedList = new List<OrderedModel>();           
            return orderedList;
        }

        private List<SoldModel> GetSoldList()
        {
            List<SoldModel> soldList = new List<SoldModel>();
            return soldList;
        }
    }
}
