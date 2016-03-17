using System;
using System.Collections.Generic;
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
            try
            {
                using (BITS_UVEntities dbcontext = new BITS_UVEntities())
                {
                    var query = (from dept in dbcontext.Departments
                                     //join deptIns in dbcontext.DepartmentInstructors on dept.Id equals deptIns.DepartmentId
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
                                UpdatedBy = dept.UpdatedBy != string.Empty ? Guid.Parse(dept.UpdatedBy) : Guid.Empty,
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
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool AddNewDepartment(DepartmentDTO newDept)
        {
            try
            {
                using (BITS_UVEntities dbcontext = new BITS_UVEntities())
                {
                    Department newDepartment = new Department()
                    {
                        Id = newDept.DepartmentId.ToString(),
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
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool DeleteDepartment(Guid deptId)
        {
            return true;
        }
    }
}
