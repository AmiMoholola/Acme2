using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Acme.Models;

namespace Acme.Controllers
{
    public class TerminateController : ApiController
    {
        readonly AcmeEntities db;

        public TerminateController()
        {
            db = new AcmeEntities();
        }

        [HttpPost]

        public IHttpActionResult TerminateEmployee(Employee model)
       
        {
            var UpdateEmployee = (from emp in db.Employees
                                  where emp.PersonId == model.PersonId                                  
                                  select emp).FirstOrDefault();

            if (UpdateEmployee != null)
            {

                UpdateEmployee.TerminatedDate = model.TerminatedDate;
                
                db.SaveChanges();

                return Ok();
            }

            else
            {
                return NotFound();
            }

        }
    }
}