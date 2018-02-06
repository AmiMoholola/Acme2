using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Acme.Controllers
{
    public class ShowSingleEmployeeController : ApiController
    {
        readonly AcmeEntities db;


        public ShowSingleEmployeeController()
        {
            db = new AcmeEntities();
        }

        [HttpGet]
        public IHttpActionResult ShowSingleEmployee(int id)
        {

            var ShowSinglePerson = db.People
                .Where(emp => emp.PersonId == id)
                .ToList();

            if (ShowSinglePerson.Count > 0)
            {

                return Ok(ShowSinglePerson);
            }

            else
            {
                return NotFound();
            }

        }
    }
    
}
