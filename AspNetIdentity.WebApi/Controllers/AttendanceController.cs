using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEnd.WebApi.Controllers
{
    public class AttendanceController : ApiController
    {
        // GET: api/Attendance
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Attendance/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Attendance
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Attendance/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Attendance/5
        public void Delete(int id)
        {
        }
    }
}
