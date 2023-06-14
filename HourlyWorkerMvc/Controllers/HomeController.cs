/* HomeController.cs
*      Program Title: IncInc Payroll (Hourly)
* Last Modified Date: November 12, 2020
*         Written By: Kyle Chapman
* 
* Description:
* This is the controller for the data required by the form.
* Its main jobs are to determine the type of worker and instantiate
* that worker object, and then to assign the values from the worker
* object to the model so they can be displayed.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HourlyWorkerMvc.Models;
using HourlyWorkerMvc.Classes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace HourlyWorkerMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Default action for Index. Displays summary values from the HourlyWorker class.
        /// </summary>
        /// <returns>A view using the HourlyWorkerModel</returns>
        public IActionResult Index()
        {
            ViewBag.SubTitle = "Incorporeal Incorporated Payroll";

            HourlyWorkerModel model = new HourlyWorkerModel();
            model.TotalWorkers = HourlyWorker.TotalWorkers;
            model.TotalHours = HourlyWorker.TotalHours;
            model.TotalOvertime = HourlyWorker.TotalOvertime;
            model.TotalPay = HourlyWorker.TotalPay;
            model.AveragePay = HourlyWorker.AveragePay;

            return View(model);
        }

        /// <summary>
        /// HttpPost action for Index. Validates entries. If entries are valid, calculates the worker's pay and displays summary values from the HourlyWorker class.
        /// </summary>
        /// <returns>A view using the HourlyWorkerModel</returns>
        [HttpPost]
        public IActionResult Index(HourlyWorkerModel workerModel)
        {
            ViewBag.SubTitle = "Incorporeal Incorporated Payroll";

            // If all fields are valid
            if (ModelState.IsValid)
            {
                // Declare (but do not instantiate) a worker object.
                HourlyWorker worker;

                // This starts a try block. This is being done to facilitate some exception
                // handling caused by the different validation rules between HourlyWorkers
                // and SalariedWorkers. You should not need any exception handling to complete
                // your Lab 4, and thus you don't need a try block!
                try
                {
                    // This part you DO need. Check what kind of worker, and then create that
                    // worker object.
                    if (workerModel.IsSalaried)
                    {
                        worker = new SalariedWorker(workerModel.FirstName, workerModel.Rate.ToString(), workerModel.Hours.ToString());
                        workerModel.Pay = worker.Pay;
                    }
                    else
                    {
                        worker = new HourlyWorker(workerModel.FirstName, workerModel.Rate.ToString(), workerModel.Hours.ToString());
                        workerModel.Pay = worker.Pay;
                    }
                }
                // Catch statements for additional validation related to pay rate and salary.
                // You should not need this in Lab 4.
                catch (ArgumentException ex)
                {
                    var errorMessage = "";

                    if (ex is ArgumentOutOfRangeException)
                    {
                        errorMessage += "Entry Out-of-Range! ";
                    }
                    else
                    {
                        errorMessage += "Entry Error! ";
                    }

                    // Writes an error message to the ErrorMessage property of the worker model.
                    workerModel.ErrorMessage = errorMessage + ex.Message;
                }
                // Catch the general exception. You don't need to do this either.
                catch (Exception ex)
                {
                    workerModel.ErrorMessage = "Critical error! Please contact the webmaster and provide the following information:\n\nType: " + ex.GetType() + "\nSource: " + ex.Source + "\nMessage: " + ex.Message + "\n\n" + ex.StackTrace;
                }

                // This you will need! Take the properties of the class and write them to the model.
                workerModel.TotalWorkers = HourlyWorker.TotalWorkers;
                workerModel.TotalHours = HourlyWorker.TotalHours;
                workerModel.TotalOvertime = HourlyWorker.TotalOvertime;
                workerModel.TotalPay = HourlyWorker.TotalPay;
                workerModel.AveragePay = HourlyWorker.AveragePay;
            }

            return View(workerModel);
        }

        /// <summary>
        /// Privacy controller. Default.
        /// </summary>
        /// <returns>Default Privacy View.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// About controller. Added but very boring.
        /// </summary>
        /// <returns>Default About View.</returns>
        public IActionResult About()
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
