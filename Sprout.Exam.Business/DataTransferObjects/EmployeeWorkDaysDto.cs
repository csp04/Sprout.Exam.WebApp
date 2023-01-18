using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeeWorkDaysDto
    {
        [Required]
        public decimal WorkedDays { get; set; }

        [Required]
        public decimal AbsentDays {  get; set; }
    }
}
