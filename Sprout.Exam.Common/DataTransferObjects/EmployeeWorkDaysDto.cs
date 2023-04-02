using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.Common.DataTransferObjects
{
    public class EmployeeWorkDaysDto
    {
        [Required]
        public decimal WorkedDays { get; set; }

        [Required]
        public decimal AbsentDays {  get; set; }
    }
}
