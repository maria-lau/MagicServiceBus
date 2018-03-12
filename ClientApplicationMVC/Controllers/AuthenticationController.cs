using ClientApplicationMVC.Models;

using Messages.NServiceBus.Commands;
using Messages.DataTypes;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Authentication.Requests;

using System.Web.Mvc;
using System.Diagnostics;

namespace ClientApplicationMVC.Controllers
{
    /// <summary>
    /// This class contains the functions responsible for handling requests routed to *Hostname*/Authentication/*
    /// </summary>
    public class AuthenticationController : Controller
    {
        /// <summary>
        /// The default method for this controller
        /// </summary>
        /// <returns>The login page</returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Please enter your username and password.";
            return View("Index");
        }

        public ActionResult CreateAccount()
        {
            //string output = ViewBag.Result;
            //System.Diagnostics.Debug.WriteLine(output + "Bill Luu is really fat");
            if(Request.HttpMethod == "POST")
            {
                string input;
                using (var reader = new System.IO.StreamReader(Request.InputStream))
                {
                    input = reader.ReadToEnd();
                }

                Debug.WriteLine("Bill Luu is suuuper fat -------------------------------------\n" + input);
            }
            return View("CreateAccount");
            
        }

        //This class is incomplete and should be completed by the students in milestone 2
        //Hint: You will need to make use of the ServiceBusConnection class. See EchoController.cs for an example.
    }
}