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
    public class DeleteEmployeeController : ApiController
    {

        readonly AcmeEntities db;


        public DeleteEmployeeController()
        {
            db = new AcmeEntities();
        }

        [HttpGet]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var RemovePerson = (from PersonDelete in db.People
                       where PersonDelete.PersonId == id
                       select PersonDelete).FirstOrDefault();

            var RemoveEmployee = (from EmployeeDelete in db.Employees
                                  where EmployeeDelete.PersonId == id
                                  select EmployeeDelete).FirstOrDefault();

            if (RemovePerson != null)
            {

                db.People.Remove(RemovePerson);
                db.Employees.Remove(RemoveEmployee);
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
