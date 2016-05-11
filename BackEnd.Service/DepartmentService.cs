using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.BusinessDTO;
using BackEnd.DataModel;

namespace BackEnd.Service
{
    public class DepartmentService
    {
        public List<DepartmentDTO> GetAllDepartments()
        {
            using (BITS_UVEntities dbcontext = new BITS_UVEntities())
            {
                var query = (from dept in dbcontext.Departments
                                 join deptIns in dbcontext.DepartmentInstructors on dept.Id equals deptIns.DepartmentId
                             where dept.IsActive
                             select dept).ToList();

                if (query.Any())
                {
                    List<DepartmentDTO> departmentList = new List<DepartmentDTO>();
                    foreach (Department dept in query)
                    {
                        DepartmentDTO deptToList = new DepartmentDTO()
                        {
                            DepartmentId = Guid.Parse(dept.Id),
                            CreatedOn = dept.CreatedOn,
                            CreatedBy = Guid.Parse(dept.CreatedBy),
                            UpdatedOn = dept.UpdatedOn,
                            UpdatedBy = !string.IsNullOrEmpty(dept.UpdatedBy) ? Guid.Parse(dept.UpdatedBy) : Guid.Empty,
                            DepartmentName = dept.DepartmentName,
                            IsActive = dept.IsActive
                        };
                        departmentList.Add(deptToList);
                    }
                    return departmentList;
                }
                return null;
            }
        }

        public DepartmentDTO GetDepartmentById(Guid deptId)
        {
            using (BITS_UVEntities dbcontext = new BITS_UVEntities())
            {
                //var query = (from dept in dbcontext.Departments
                //    join deptIns in dbcontext.DepartmentInstructors on dept.Id equals deptIns.DepartmentId
                //    where dept.IsActive
                //    select dept);
                //if (query.Any())
                //{
                //    DepartmentDTO newDept = new DepartmentDTO
                //    {
                //        DepartmentId = Guid.Parse(query.FirstOrDefault().Id),
                //        CreatedOn = query.FirstOrDefault().CreatedOn,
                //        CreatedBy = Guid.Parse(query.FirstOrDefault().CreatedBy),
                //        DepartmentName = query.FirstOrDefault().DepartmentName,
                //        IsActive = query.FirstOrDefault().IsActive,
                //        UpdatedBy = !string.IsNullOrEmpty(query.FirstOrDefault().UpdatedBy) ? Guid.Parse(query.FirstOrDefault().UpdatedBy) : Guid.Empty,
                //        UpdatedOn = query.FirstOrDefault().UpdatedOn
                //    };
                //    return newDept;
                //}
                var query = from dept in dbcontext.Departments
                             from deptIns in dbcontext.DepartmentInstructors
                             from users in dbcontext.AspNetUsers
                             where dept.IsActive && (dept.Id == deptIns.DepartmentId) && (deptIns.UserId == users.Id)
                             select new {dept, deptIns, users};

                if (query.Any())
                {
                    DepartmentDTO newDept = new DepartmentDTO
                    {
                        DepartmentId = Guid.Parse(query.FirstOrDefault().dept.Id),
                        CreatedOn = query.FirstOrDefault().dept.CreatedOn,
                        CreatedBy = Guid.Parse(query.FirstOrDefault().dept.CreatedBy),
                        DepartmentName = query.FirstOrDefault().dept.DepartmentName,
                        IsActive = query.FirstOrDefault().dept.IsActive,
                        UpdatedBy = !string.IsNullOrEmpty(query.FirstOrDefault().dept.UpdatedBy) ? Guid.Parse(query.FirstOrDefault().dept.UpdatedBy) : Guid.Empty,
                        UpdatedOn = query.FirstOrDefault().dept.UpdatedOn
                    };
                    List<UsersDTO> instructors = new List<UsersDTO>();
                    foreach (var insQuery in query.ToList())
                    {
                        UsersDTO instructor = new UsersDTO
                        {
                            UserId = Guid.Parse(insQuery.deptIns.UserId),
                            DepartmentId = Guid.Parse(insQuery.dept.Id),
                            Email = insQuery.users.Email,
                            Firstname = insQuery.users.FirstName,
                            Lastname = insQuery.users.LastName,
                            IsHod = insQuery.deptIns.IsHOD,
                            JoiningDate = insQuery.users.JoinDate,
                            Username = insQuery.users.UserName
                        };
                        instructors.Add(instructor);
                    }
                    newDept.Instructors = instructors;
                    return newDept;
                }
                
                return null;
            }
        }

        public bool AddNewDepartment(DepartmentDTO newDept)
        {
            using (BITS_UVEntities dbcontext = new BITS_UVEntities())
            {
                Department newDepartment = new Department()
                {
                    //Id = newDept.DepartmentId.ToString(),
                    Id = Guid.NewGuid().ToString(),
                    CreatedOn = DateTime.Now,
                    CreatedBy = newDept.CreatedBy.ToString(),
                    DepartmentName = newDept.DepartmentName,
                    IsActive = true
                };
                dbcontext.Departments.Add(newDepartment);
                if (dbcontext.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool UpdateDepartment(Guid deptId, DepartmentDTO updatedDept)
        {
            using (BITS_UVEntities dbcontext = new BITS_UVEntities())
            {
                Department currentDept = dbcontext.Departments.Find(deptId.ToString());
                currentDept.DepartmentName = updatedDept.DepartmentName;
                currentDept.UpdatedOn = DateTime.Now;
                currentDept.UpdatedBy = updatedDept.UpdatedBy.ToString();
                if (updatedDept.AddNewInstructors != null && updatedDept.AddNewInstructors.Any())
                {
                    //add new instructors to the dept//
                    foreach (UsersDTO instructor in updatedDept.AddNewInstructors)
                    {
                        DepartmentInstructor newInstructor = new DepartmentInstructor();
                        newInstructor.DeptInstructorId = Guid.NewGuid().ToString();
                        newInstructor.DepartmentId = deptId.ToString();
                        newInstructor.IsHOD = instructor.IsHod;
                        newInstructor.UserId = instructor.UserId.ToString();
                        newInstructor.IsActive = true;
                        dbcontext.DepartmentInstructors.Add(newInstructor);
                    }
                }
                if (updatedDept.RemoveCurrentInstructors != null && updatedDept.RemoveCurrentInstructors.Any())
                {
                    //remove current instructors from the dept//
                    foreach (UsersDTO instructor in updatedDept.RemoveCurrentInstructors)
                    {
                        DepartmentInstructor currentInstructor =
                            dbcontext.DepartmentInstructors
                                .FirstOrDefault(user => user.UserId == instructor.UserId.ToString());
                        if (currentInstructor != null)
                        {
                            currentInstructor.IsActive = false;
                            dbcontext.Entry(currentInstructor).State = EntityState.Modified;
                        }
                    }
                }
                dbcontext.Entry(currentDept).State = EntityState.Modified;
                var t = dbcontext.GetValidationErrors();
                foreach (var eve in t)
                {
                    var a = eve.Entry.Entity.GetType().Name;
                    var b = eve.Entry.State;
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var c = ve.PropertyName;
                        var d = ve.ErrorMessage;
                    }
                }
                if (dbcontext.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool DeleteDepartment(Guid deptId)
        {
            using (BITS_UVEntities dbcontext = new BITS_UVEntities())
            {
                Department currentDept = dbcontext.Departments.Find(deptId);
                currentDept.IsActive = false;
                dbcontext.Entry(currentDept).State = EntityState.Modified;
                if (dbcontext.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
