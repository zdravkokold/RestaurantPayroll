namespace RestaurantPayroll.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal TotalSalary { get; set; }
        public DateTime DateSalaryRecieved { get; set; }
    }
}