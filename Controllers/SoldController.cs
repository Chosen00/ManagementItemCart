using Microsoft.AspNetCore.Mvc;
using CustomerCart.Models;
using System;
using Microsoft.AspNetCore.Identity;


namespace CustomerCart.Controllers
{
    public class SoldController : Controller
    {
        private readonly SoldContext _soldContext;

        public SoldController(SoldContext soldContext)
        {
            _soldContext = soldContext;
        }

        //Create
        [HttpGet]
        public IActionResult SoldForm()
        {
            var sold = new SoldModel();
            return View(sold);
        }

        [HttpPost]
        public IActionResult SoldForm(SoldModel sold)
        {
            try
            {
                bool isSuccess = _soldContext.InsertSold(sold);
                if (isSuccess)  
                {
                    TempData["SuccessMessage"] = "Sold Added Successfully!";
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
            return View(sold);
        }


        [HttpGet]
        public IActionResult UpdateSoldForm(int soldId)
        {
            var soldIds = _soldContext.GetSoldById(soldId);
            return View(soldIds);
        }

        [HttpPost]
        public IActionResult UpdateSoldForm(SoldModel updatedsold)
        {
            try
            {
                int soldId = updatedsold.SoldItemID;
                bool isSuccess = _soldContext.UpdateSold(updatedsold);

                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Sold information updated successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = soldId;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessageCatch"] = $"Error: {ex.Message}";
            }

            return View(updatedsold);
        }
    }
}
