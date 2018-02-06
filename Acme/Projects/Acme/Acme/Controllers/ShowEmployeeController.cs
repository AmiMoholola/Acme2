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
    public class ShowEmployeeController : ApiController
    {
        
        readonly AcmeEntities db;


        public ShowEmployeeController()
        {
            db = new AcmeEntities();
        }

        [HttpGet]
        public IHttpActionResult  ShowEmployee()
        {


            var showperson = db.People
                .ToList();


            if (showperson != null)
            {

                return Ok(showperson);
            }

            else
            {
                return NotFound();
            }
            
        }



    }
}
