using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using AcmeFrontEnd.Models;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;
using System.Net.Http.Headers;

namespace AcmeFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        [System.Web.Mvc.HttpGet]
      
        public ActionResult Index()
        {
            ViewBag.Message = "";


            
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["webapiurl"].ToString());
            Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("api/ShowEmployee").Result;
            if (response.IsSuccessStatusCode)
            {
                var EmployeeDetails = response.Content.ReadAsAsync<IEnumerable<PersonModel>>().Result;
                return View(EmployeeDetails);
            }

            else
            {
                return View();
            }
            
        }


        [System.Web.Mvc.HttpGet]
        public ActionResult ErrorPage()
        {
            ViewBag.Message = "";

            return View();
        }


    }
}
