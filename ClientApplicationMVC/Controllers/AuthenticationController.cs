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
            System.Diagnostics.Debug.WriteLine("u:" + username + " p:" + password);
            //string username = String.Format("{0}", Request.Form["uname"]);
            //string password = String.Format("{0}", Request.Form["psw"]);

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
            ViewData["response"] = response.response;
            return View("Index");
        }

        /// <summary>
        /// The default method for this controller
        /// </summary>
        /// <returns>The login page</returns>
        public ActionResult Index()
        {
            if (Request.HttpMethod == "POST")
            {
                string username = String.Format("{0}", Request.Form["uname"]);
                string password = String.Format("{0}", Request.Form["psw"]);

                LogInRequest LR = new LogInRequest(username, password);
                ServiceBusResponse response;

                //Chantal, this is the section that makes it throw an error. 
                //I wonder if it has to do with how i don't have: if (Request.HttpMethod == "POST")? 

                ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());

                if (connection == null)
                {
                    response = ConnectionManager.sendLogIn(LR);
                }
                else
                {
                    response = connection.sendLogIn(LR);
                }
                ViewData["response"] = response.response;
            }            
            return View("Index");
        }

        public ActionResult CreateAccount()
        {
            if (Request.HttpMethod == "POST")
            {
                //Get form data from HTML web page
                //string input;
                CreateAccount accountInfo = new CreateAccount();
                ServiceBusResponse SBR;
                ServiceBusConnection SBC = ConnectionManager.getConnectionObject(Globals.getUser());
                //using (var reader = new System.IO.StreamReader(Request.InputStream))
                //{
                //    input = reader.ReadToEnd();
                //}

                //Parse string
                //string[] substrings = input.Split('=', '&');
                accountInfo.username = Request.Form["uname"];// substrings[1];
                accountInfo.password = Request.Form["psw"];// substrings[3];
                accountInfo.address = Request.Form["addr"];// substrings[5];
                accountInfo.email = Request.Form["email"];// substrings[7];
                accountInfo.phonenumber = Request.Form["pnum"]; // substrings[9];
                accountInfo.type = (AccountType)System.Enum.Parse(typeof(AccountType), Request.Form["accountType"]);// substrings[11]);

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
                if (SBR != null)
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

            return View("CreateAccount");
        }

        //This class is incomplete and should be completed by the students in milestone 2
        //Hint: You will need to make use of the ServiceBusConnection class. See EchoController.cs for an example.
    }
}