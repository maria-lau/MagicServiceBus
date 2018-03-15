using ClientApplicationMVC.Models;

using Messages.NServiceBus.Commands;
using Messages.DataTypes;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Authentication.Requests;
using System.Web.Mvc;
using System;

namespace ClientApplicationMVC.Controllers
{
    /// <summary>
    /// This class contains the functions responsible for handling requests routed to *Hostname*/Authentication/*
    /// </summary>
    public class AuthenticationController : Controller
    {
        // This method is called as a direct result of pressing the submit button on the 
        // HTML page
        [HttpPost]
        public ActionResult Login(string usernameData, string passwordData)
        {
            string username = usernameData;
            string password = passwordData;

            LogInRequest LR = new LogInRequest(username, password);
            ServiceBusResponse response;

            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());

            if (connection == null)
            {
                response = ConnectionManager.sendLogIn(LR);
            }
            else
            {
                response = connection.sendLogIn(LR);
            }
            //ViewData["response"] = response.response;


            Response.Write("<script>alert('" + response.response + "')</script>");

            return View("../Home/Index");
        }

        [HttpPost]
        public ActionResult CreateAccountPost(string usernameData, string passwordData, 
            string addressData, string emailData, string phoneData)
        {
            //Get form data from HTML web page
            CreateAccount accountInfo = new CreateAccount();
            ServiceBusResponse SBR;
            ServiceBusConnection SBC = ConnectionManager.getConnectionObject(Globals.getUser());

            accountInfo.username = usernameData;
            accountInfo.password = passwordData;
            accountInfo.address = addressData;
            accountInfo.email = emailData;
            accountInfo.phonenumber = phoneData;
            accountInfo.type = (AccountType)System.Enum.Parse(typeof(AccountType), Request.Form["accountType"]);

            //Send account info to bus
            CreateAccountRequest CAR = new CreateAccountRequest(accountInfo);

            if (SBC == null)
            {
                SBR = ConnectionManager.sendNewAccountInfo(CAR);
            }
            else
            {
                SBR = SBC.sendNewAccountInfo(CAR);
            }

            //Check if account created successfull
            string message = "Account created successfully.";
            if (SBR.result == true)
            {
               Response.Write("<script>alert('" + message + "')</script>");
                return View("Index");
            }
            else
            {
                message = "Failed to create account.";
                Response.Write("<script>alert('" + message + "')</script>");
                return View("CreateAccount");
            }
        }

        /// <summary>
        /// The default method for this controller
        /// </summary>
        /// <returns>The login page</returns>
        public ActionResult Index()
        {         
            return View("Index");
        }

        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        //This class is incomplete and should be completed by the students in milestone 2
        //Hint: You will need to make use of the ServiceBusConnection class. See EchoController.cs for an example.
    }
}