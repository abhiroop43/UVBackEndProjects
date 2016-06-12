using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.BusinessDTO
{
    public class DepartmentDTO
    {
        [Required]
        public Guid DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public List<UsersDTO> Instructors { get; set; }

        public List<UsersDTO> AddNewInstructors { get; set; }

        public List<UsersDTO> RemoveCurrentInstructors { get; set; }

        public UsersDTO CurrentHod { get; set; }
    }
}
