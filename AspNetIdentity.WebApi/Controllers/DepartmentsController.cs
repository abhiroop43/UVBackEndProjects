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
            return Ok(deptService.GetAllDepartments());
        }

        // GET: api/Departments/5
        public IHttpActionResult Get(Guid id)
        {
            return StatusCode(HttpStatusCode.PaymentRequired);
        }

        [Route("create")]
        public void Post([FromBody]DepartmentDTO newDept)
        {
            bool isAdded = deptService.AddNewDepartment(newDept);
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
