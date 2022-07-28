using System.ComponentModel.DataAnnotations;

namespace EmployeePayroll_Ajax.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Emp_Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]

        public string Department { get; set; }
        public string Notes { get; set; }

    }
}
