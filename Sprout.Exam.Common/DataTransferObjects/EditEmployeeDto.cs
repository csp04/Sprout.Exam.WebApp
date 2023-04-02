using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.Common.DataTransferObjects
{
    public class EditEmployeeDto: BaseSaveEmployeeDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
