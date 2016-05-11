using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BusinessDTO
{
    public class UsersDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Guid DepartmentId { get; set; }
        public bool IsHod { get; set; }
    }
}
