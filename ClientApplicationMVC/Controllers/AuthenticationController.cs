using ClientApplicationMVC.Models;

using Messages.NServiceBus.Commands;
using Messages.DataTypes;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Authentication.Requests;

using System.Web.Mvc;
using System.Diagnostics;
using Messages.DataTypes;

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
            return View("Index");
        }



        public ActionResult CreateAccount()
        {
            if (Request.HttpMethod == "POST")
            {
                //Get form data from HTML web page
                string input;
                CreateAccount accountInfo = new CreateAccount();
                ServiceBusResponse SBR;
                ServiceBusConnection SBC = ConnectionManager.getConnectionObject(Globals.getUser());
                using (var reader = new System.IO.StreamReader(Request.InputStream))
                {
                    input = reader.ReadToEnd();
                }
                
                //Parse string
                string[] substrings = input.Split('=', '&');
                accountInfo.username = substrings[1];
                accountInfo.password = substrings[3];
                accountInfo.address = substrings[5];
                accountInfo.email = substrings[7];
                accountInfo.phonenumber = substrings[9];
                accountInfo.type = (AccountType)System.Enum.Parse(typeof(AccountType), substrings[11]);

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

                //Check if username/email not already registered
                bool notRegistered = true;
                string message = "Account created successfully.";
                if (notRegistered == true)
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