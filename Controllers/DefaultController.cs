using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project_Management_System.Controllers
{
    //This controller retirs the index page.
    public class DefaultController : Controller
    {

        // GET: Default
        //Returns the index page 
        public ActionResult Index()
        {
            return View();
        }

    }
}