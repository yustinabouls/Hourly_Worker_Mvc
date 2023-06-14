/* HourlyWorker.cs
*      Program Title: IncInc Payroll (Hourly)
* Last Modified Date: October 13, 2020
*         Written By: Kyle Chapman
* 
* Description:
* This is a class representing individual worker objects. Each stores
* their own name, pay rate and hours worked and the class methods allow
* for calculation of the worker's pay and for updating of shared summary
* values. Name, pay rate and hours worked are received as strings.
* This is being used as part of a hourly payroll application.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HourlyWorkerMvc.Classes
{
    class HourlyWorker
    {

        #region "Variable Declarations"

        // Instance variables
        protected decimal employeeRate;
        protected decimal employeeHours;

        internal const decimal MinimumRate = 15M;
        internal const decimal MaximumRate = 120M;
        internal const decimal MinimumHours = 0M;
        internal const decimal MaximumHours = 24M * 14M;

        #endregion

        #region "Constructors"

        /// <summary>
        /// HourlyWorker constructor: accepts a worker's name, pay rate, and
        /// hours worked, then sets and calculates values as appropriate.
        /// </summary>
        /// <param name="nameValue">the worker's name</param>
        /// <param name="rateValue">the worker's pay rate</param>
        /// <param name="hoursValue">the worker's number of hours worked</param>
        internal HourlyWorker(string nameValue, string rateValue, string hoursValue)
        {
            this.Name = nameValue;
            this.Rate = rateValue;
            this.Hours = hoursValue;
            FindPay();
        }

        /// <summary>
        /// HourlyWorker default constructor: empty constructor used strictly for inheritance and instantiation
        /// </summary>
        internal HourlyWorker()
        {

        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Currently called in the constructor, the findPay() method is
        /// used to calculate a worker's pay using their pay rate and hours
        /// worked. This also updates all summary values.
        /// </summary>
        protected virtual void FindPay()
        {
            const decimal OvertimeThreshold = 40M;
            const decimal OvertimePayRate = 1.5M;

            // If the employee's hours exceed the overtime threshold
            if (employeeHours > OvertimeThreshold)
            {
                // Calculate employee's pay for 40 normal hours, plus overtime pay
                Pay = employeeRate * OvertimeThreshold + employeeRate * OvertimePayRate * (employeeHours - OvertimeThreshold);
                // Increment the overall overtime hours
                TotalOvertime += employeeHours - OvertimeThreshold;

                // throw new InsufficientMemoryException(); // This is left in for testing and fun
            }
            // If the employee did not do overtime hours, calculate pay normally
            else
            {
                Pay = employeeRate * employeeHours;
            }

            // Increment all shared summary values
            TotalWorkers++;
            TotalHours += employeeHours;
            TotalPay += Pay;
        }

        #endregion

        #region "Properties"

        /// <summary>
        /// Gets and sets a worker's name
        /// </summary>
        /// <returns>an employee's name</returns>
        protected internal string Name { get; set; }
        // This uses a default property which is a nice feature of classes in C#.
        // However, one significant drawback here is that it stops us from writing validation in the class itself,
        // which means you won't actually be able to use this on Lab 1.
        // It's just here for the sake of demonstration.

        /// <summary>
        /// Gets and sets the worker's hourly pay rate
        /// </summary>
        /// <returns>an employee's hourly pay rate</returns>
        private string Rate
        {
            get
            {
                return employeeRate.ToString();
            }
            set
            {
                // If the pay rate is not a decimal value, the worker is not valid
                if (!decimal.TryParse(value, out employeeRate))
                {
                    throw new ArgumentException("Please enter the worker's pay rate as a numeric value.", "Rate");
                }
                // If the pay rate is out of range, the worker is not valid
                else if (employeeRate < MinimumRate || employeeRate > MaximumRate)
                {
                    throw new ArgumentOutOfRangeException("Rate", "Please enter the worker's pay rate between " + MinimumRate.ToString("c") + " and " + MaximumRate.ToString("c") + ".");
                }

                // If the pay rate parses and is in range, it's already stored in employeeRate
            }
        }

        /// <summary>
        /// Gets and sets the hours worked by a worker
        /// </summary>
        /// <returns>an employee's hours worked</returns>
        protected internal string Hours
        {
            get
            {
                return employeeHours.ToString();
            }
            set
            {
                // If the hours worked are not a decimal value, the worker is not valid
                if (!decimal.TryParse(value, out employeeHours))
                {
                    throw new ArgumentException("Please enter the worker's hours worked as a numeric value.", "Hours");
                }
                // If the hours worked are out of range, the worker is not valid
                else if (employeeHours < MinimumHours || employeeHours > MaximumHours)
                {
                    throw new ArgumentOutOfRangeException("Hours", "Please enter the worker's hours worked between " + MinimumHours.ToString() + " and " + MaximumHours.ToString() + ".");
                }

                // If the hours worked parse and are in range, it's already stored in employeeHours
            }
        }

        /// <summary>
        /// Gets the worker's pay
        /// </summary>
        /// <returns>a worker's pay</returns>
        protected internal decimal Pay { get; set; }

        /// <summary>
        /// Gets the overall pay among all workers
        /// </summary>
        /// <returns>the overall pay total among all workers</returns>
        protected internal static decimal TotalPay { get; protected set; }

        /// <summary>
        /// Gets the overall number of hours worked
        /// </summary>
        /// <returns>the overall number of hours worked among all workers</returns>
        protected internal static decimal TotalHours { get; protected set; } = 0M;

        /// <summary>
        /// Gets the overall number of overtime hours worked
        /// </summary>
        /// <returns>the overall number of overtime hours among all workers</returns>
        protected internal static decimal TotalOvertime { get; protected set; } = 0M;

        /// <summary>
        /// Gets the overall number of workers
        /// </summary>
        /// <returns>the overall number of workers</returns>
        protected internal static int TotalWorkers { get; protected set; } = 0;

        /// <summary>
        /// Calculates and returns an average pay among all workers
        /// </summary>
        /// <returns>the average pay among all workers</returns>
        protected internal static decimal AveragePay
        {
            get
            {
                if (TotalWorkers == 0)
                {
                    return 0;
                }
                else
                {
                    return TotalPay / TotalWorkers;
                }
            }
        }

        #endregion

    }
}
