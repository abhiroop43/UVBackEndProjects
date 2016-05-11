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
        [HttpGet]
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
        [HttpGet]
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
        [HttpPost]
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

        [Route("updateDept/{id:guid}", Name = "UpdateDept")]
        [HttpPut]
        public IHttpActionResult Put(Guid id, [FromBody]DepartmentDTO updatedDept)
        {
            try
            {
                bool isUpdated = deptService.UpdateDepartment(id, updatedDept);
                return Ok("Department updated successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("deleteDept/{id:guid}", Name = "DeleteDept")]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                bool isDeleted = deptService.DeleteDepartment(id);
                return Ok("Department deleted successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
