using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BusinessDTO
{
    public class DepartmentDTO
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public List<UsersDTO> Instructors { get; set; }
        public List<UsersDTO> AddNewInstructors { get; set; }
        public List<UsersDTO> RemoveCurrentInstructors { get; set; }
    }
}
