/* SalariedWorker.cs
*      Program Title: IncInc Payroll (Hourly)
* Last Modified Date: October 14, 2020
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
using System.Text;
using System.Windows;

namespace HourlyWorkerMvc.Classes
{
    class SalariedWorker : HourlyWorker
    {

        #region "Variable Declarations"

        // Instance variables
        private decimal employeeSalary;

        private const int AnnualWorkingDays = 261;
        private const decimal DailyWorkingHours = 7.5M;
        internal const decimal MinimumSalary = 15M * DailyWorkingHours * AnnualWorkingDays;

        #endregion

        #region "Constructors"

        /// <summary>
        /// SalariedWorker constructor: accepts a worker's name, salary, and
        /// hours worked, then sets and calculates values as appropriate.
        /// </summary>
        /// <param name="nameValue">the worker's name</param>
        /// <param name="salaryValue">the worker's salary</param>
        /// <param name="hoursValue">the worker's number of hours worked</param>
        internal SalariedWorker(string nameValue, string salaryValue, string hoursValue)
        {
            this.Name = nameValue;
            this.Salary = salaryValue;
            this.Hours = hoursValue;
            FindPay();
        }

        /// <summary>
        /// SalariedWorker default constructor: empty constructor used strictly for inheritance and instantiation
        /// </summary>
        internal SalariedWorker()
        {

        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Currently called in the constructor, the findPay() method is
        /// used to calculate a worker's pay using their pay rate and hours
        /// worked. This also updates all summary values.
        /// </summary>
        protected override void FindPay()
        {
            const decimal OvertimeThreshold = 44M;
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
        /// Gets and sets the worker's hourly pay rate
        /// </summary>
        /// <returns>an employee's hourly pay rate</returns>
        internal string Rate
        {
            get
            {
                return employeeRate.ToString();
            }
            set
            {
                const int AnnualWorkingDays = 261;
                const decimal DailyWorkingHours = 7.5M;
                employeeRate = employeeSalary / (AnnualWorkingDays * DailyWorkingHours);
            }
        }

        /// <summary>
        /// Gets and sets the worker's salary
        /// </summary>
        /// <returns>an employee's salary</returns>
        internal string Salary
        {
            get
            {
                return employeeSalary.ToString();
            }
            set
            {
                // If the salary is not a decimal value, the worker is not valid
                if (!decimal.TryParse(value, out employeeSalary))
                {
                    throw new ArgumentException("Please enter the worker's salary as a numeric value.", "Salary");
                }
                // If the salary is out of range, the worker is not valid
                else if (employeeSalary < MinimumSalary)
                {
                    throw new ArgumentOutOfRangeException("Salary", "Please enter the worker's salary of at least " + MinimumSalary.ToString("c") + ".");
                }

                // If the salary parses and is in range, it's already stored in employeeSalary
                employeeRate = employeeSalary / (AnnualWorkingDays * DailyWorkingHours);

            }
        }

        #endregion

    }
    

}
