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
    public class UpdateEmployeeController : ApiController
    {

         readonly AcmeEntities db;

        public UpdateEmployeeController()
        {
            db = new AcmeEntities();
        }

        [HttpPost]

        public IHttpActionResult UpdateEmployee(Person model)
        {


            var UpdatePerson = (from PersonUpdate in db.People
                                where PersonUpdate.PersonId == model.PersonId
                                select PersonUpdate).FirstOrDefault();
            if (UpdatePerson != null)
            {

                UpdatePerson.FirstName = model.FirstName;
                UpdatePerson.LastName = model.LastName;
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
