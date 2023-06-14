/* HourlyWorkerModel.cs
*      Program Title: IncInc Payroll (Hourly)
* Last Modified Date: November 12, 2020
*         Written By: Kyle Chapman
* 
* Description:
* This is the model for the data required by the form.
* It effectively holds and validates all the user input so that
* a worker object can be created in the controller.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HourlyWorkerMvc.Classes;
using Microsoft.VisualBasic;

namespace HourlyWorkerMvc.Models
{
    public class HourlyWorkerModel
    {
        #region "Properties"

        /// <summary>
        /// The worker's first name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The worker must have a first name")]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        /// <summary>
        /// The worker's last name.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The worker must have a last name")]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        /// <summary>
        /// True if the worker is a salaried worker, False if not.
        /// </summary>
        [Required]
        [Display(Name = "Worker Type:")]
        public bool IsSalaried { get; set; }

        /// <summary>
        /// The worker's pay rate (if hourly) or salary (if salaried).
        /// </summary>
        [Required(ErrorMessage = "You must enter a pay rate or salary")]
        [Range((double)HourlyWorker.MinimumRate, double.MaxValue,
     ErrorMessage = "The pay rate or salary must be at least 15")]
        [Display(Name = "Pay Rate or Salary:")]
        public decimal Rate { get; set; }

        /// <summary>
        /// The worker's hours worked.
        /// </summary>
        [Required(ErrorMessage = "You must enter the number of hours worked")]
        [Range((double)HourlyWorker.MinimumHours, (double)HourlyWorker.MaximumHours,
            ErrorMessage = "The hours worked must be temporally possible")]
        [Display(Name = "Hours Worked:")]
        public decimal Hours { get; set; }

        /// <summary>
        /// Last worker's pay.
        /// </summary>
        public decimal Pay { get; set; }

        /// <summary>
        /// The total number of workers.
        /// </summary>
        public decimal TotalWorkers { get; set; }

        /// <summary>
        /// The total pay among all workers.
        /// </summary>
        public decimal TotalPay { get; set; }

        /// <summary>
        /// The average pay among all workers.
        /// </summary>
        public decimal AveragePay { get; set; }

        /// <summary>
        /// The total hours worked among all workers.
        /// </summary>
        public decimal TotalHours { get; set; }

        /// <summary>
        /// The total overtime hours worked among all workers.
        /// </summary>
        public decimal TotalOvertime { get; set; }

        /// <summary>
        /// An error message for the worker.
        /// Note that you do NOT need this for your Lab 4.
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion
    }
}
