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
    public class ValuesController : ApiController
    {
        

        readonly AcmeEntities db;


        public ValuesController()
        {
            db = new AcmeEntities();
        }

        [HttpPost]
        

        public IHttpActionResult AddEmployee(Person model)
        
        {
                       
            db.People.Add(new Person()
             
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                BirthDate = model.BirthDate,
            }); 


            db.SaveChanges();

            var lastId = db.People
                .Select(s => new { s.PersonId })
                .ToList()
                .Last();


            

            db.Employees.Add(new Employee() 
            {

                PersonId = lastId.PersonId,
                EmployeeNum = GenerateEmployeeNo().ToString(),
               EmployedDate = DateTime.Now
            });
            
             db.SaveChanges();

            return Ok();
        }


        private int GenerateEmployeeNo()
        {
            
            return new Random().Next(1000, 9999);
            
        }

    }
}