using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackEnd.BusinessDTO;
using BackEnd.Service;

namespace BackEnd.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/departments")]
    public class DepartmentsController : ApiController
    {
        DepartmentService deptService = new DepartmentService();

        [Route("getAll")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(deptService.GetAllDepartments());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getDeptById/{id:guid}", Name = "GetDeptById")]
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                DepartmentDTO dept = deptService.GetDepartmentById(id);
                if (dept == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(dept);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        [Route("create")]
        public IHttpActionResult Post([FromBody]DepartmentDTO newDept)
        {
            try
            {
                bool isAdded = deptService.AddNewDepartment(newDept);
                return Ok("Department created successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Departments/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Departments/5
        public void Delete(int id)
        {
        }
    }
}
