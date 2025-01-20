using Microsoft.EntityFrameworkCore;
using RestaurantPayroll.Entities;

namespace RestaurantPayroll.Services
{
    public class RestaurantService
    {
        private readonly RestaurantPayrollDbContext context;

        public RestaurantService(RestaurantPayrollDbContext context)
        {
            this.context = context;
        }

        public void AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public List<Employee> GetAllEmployees() => context.Employees.ToList();

        public Employee GetEmployeeById(int id)
        {
            return context.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void UpdateEmployee(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            Employee employee = context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
        }

        public void AddSalary(Salary salary)
        {
            context.Salaries.Add(salary);
            context.SaveChanges();
        }

        public List<Salary> GetAllSalaries() => context.Salaries.Include(s => s.Employee).ToList();

        public List<Salary> GetSalariesByEmployeeId(int employeeId)
        {
            return context.Salaries.Where(s => s.EmployeeId == employeeId).ToList();
        }

        public void UpdateSalary(Salary salary)
        {
            context.Salaries.Update(salary);
            context.SaveChanges();
        }

        public void DeleteSalary(int id)
        {
            Salary salary = context.Salaries.FirstOrDefault(s => s.Id == id);
            if (salary != null)
            {
                context.Salaries.Remove(salary);
                context.SaveChanges();
            }
        }

        public Salary CalculateSalary(int salaryId, int hoursWorked, decimal hourlyRate, decimal bonus, decimal deductions)
        {            
            Salary salary = context.Salaries.FirstOrDefault(e => e.Id == salaryId);
            if (salary == null)
            {
                throw new Exception("Salary not found.");
            }

            decimal baseSalary = hourlyRate * hoursWorked;
            decimal totalSalary = baseSalary + bonus - deductions;
                        
            salary.BaseSalary = baseSalary;
            salary.Bonus = bonus;
            salary.Deductions = deductions;
            salary.TotalSalary = totalSalary;

            return salary;           
        }

        public List<Salary> GetSalariesForPeriod(DateTime startDate, DateTime endDate)
        {
            return context.Salaries
                .Include(s => s.Employee)
                .Where(s => s.DateSalaryRecieved >= startDate && s.DateSalaryRecieved <= endDate)
                .ToList();
        }

        public decimal GetTotalExpensesForPeriod(DateTime startDate, DateTime endDate)
        {            
            return context.Salaries
                .Where(s => s.DateSalaryRecieved >= startDate && s.DateSalaryRecieved <= endDate)
                .Sum(s => s.TotalSalary);            
        }
    }
}