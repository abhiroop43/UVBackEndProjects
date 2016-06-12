using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.BusinessDTO
{
    public class UsersDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime JoiningDate { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }


        public Guid DepartmentId { get; set; }

        [Required]
        public bool IsHod { get; set; }
    }
}
