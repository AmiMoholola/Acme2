using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using AcmeFrontEnd.Filters;
using AcmeFrontEnd.Models;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace AcmeFrontEnd.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        

        
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(PersonModel model)
        {
            if (ModelState.IsValid)
            {
                
                try
                {

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["webapiurl"].ToString());

                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("/api/values",model,new JsonMediaTypeFormatter())
                           // .Result
                           // .Content
                          //  .ReadAsAsync<int>()
                            .Result;

                    if (response.IsSuccessStatusCode)
                    {    
                        return RedirectToAction("../home/index");
                    }
                        
                    else
                    {
                        return RedirectToAction("../home/errorpage");
                    }
                
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }


            return RedirectToAction("../home/errorpage");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DeletePerson(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["webapiurl"].ToString());

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/DeleteEmployee/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("../home/index");
            }

            else
            {
                return RedirectToAction("../home/errorpage");
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowSinglePerson(int id)
        {

            PersonModel Objemployee = new PersonModel();          
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["webapiurl"].ToString());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/ShowSingleEmployee/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var EmployeeDetails = response.Content.ReadAsAsync<IEnumerable<PersonModel>>().Result;

                foreach (var item in EmployeeDetails)
                {  
                   Objemployee.Firstname = item.Firstname;
                   Objemployee.Lastname = item.Lastname;
                   Objemployee.Birthdate = item.Birthdate;
                   Objemployee.Personid = item.Personid;
                }

                return View(Objemployee);
            }

            else
            {
                return RedirectToAction("../home/errorpage");
            }

        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ShowSinglePerson(PersonModel model)
        {

            if (ModelState.IsValid)
            {
                
                try
                {
                
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["webapiurl"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("/api/UpdateEmployee",model,new JsonMediaTypeFormatter()).Result;    

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("../home/index");
                    }

                    else
                    {
                        return RedirectToAction("../home/errorpage");
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }


            return RedirectToAction("../home/errorpage");
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult Terminate(int id)
        {
            EmployeeModel EmpObject = new EmployeeModel();
            EmpObject.Personid = id;
            return View(EmpObject);
         }




        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Terminate(EmployeeModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["webapiurl"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync("/api/Terminate/",model,new JsonMediaTypeFormatter()).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("../home/index");
                    }

                    else
                    {
                        return RedirectToAction("../home/errorpage");
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            return RedirectToAction("../home/errorpage");
        }


   



        
        

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
