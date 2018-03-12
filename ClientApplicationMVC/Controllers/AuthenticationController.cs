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
        public ActionResult CreateAccount()
        {
<<<<<<< HEAD
            if (Request.HttpMethod == "POST")
            {
                //Get form data from HTML web page
                string input;
                CreateAccount accountInfo = new CreateAccount();
                using (var reader = new System.IO.StreamReader(Request.InputStream))
=======



                //string output = ViewBag.Result;
                //System.Diagnostics.Debug.WriteLine(output + "Bill Luu is really fat");
                if (Request.HttpMethod == "POST")
>>>>>>> bf539a332211dacb948c3479cf34245cfa212518
                {
                    string input;

                    string username;
                    string password;
                    string address;
                    string email;
                    string pnumber;
                    string type;
                    //create connectons
                    ServiceBusResponse SBR;
                    ServiceBusConnection SBC = ConnectionManager.getConnectionObject(Globals.getUser());

<<<<<<< HEAD
                //Parse string
                string[] substrings = input.Split('=', '&');
                accountInfo.username = substrings[1];
                accountInfo.password = substrings[3];
                accountInfo.address = substrings[5];
                accountInfo.email = substrings[7];
                accountInfo.phonenumber = substrings[9];
                accountInfo.type = (AccountType)System.Enum.Parse(typeof(AccountType), substrings[11]);

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

=======
                    //read input from View
                    using (var reader = new System.IO.StreamReader(Request.InputStream))
                    {
                        input = reader.ReadToEnd();
                    }

                    //parse input
                    string[] substrings = input.Split('=', '&');
                    username = substrings[1];
                    password = substrings[3];
                    address = substrings[5];
                    email = substrings[7];
                    pnumber = substrings[9];
                    type = substrings[11];

                    //create account object
                    CreateAccount account = new CreateAccount();
                    account.username = username;
                    account.password = password;
                    account.address = address;
                    account.email = email;
                    account.phonenumber = pnumber;
                    account.type = (AccountType)System.Enum.Parse(typeof(AccountType), type);
                    CreateAccountRequest CAR = new CreateAccountRequest(account);

                    if (SBC == null)
                    {
                        SBR = ConnectionManager.sendNewAccountInfo(CAR);
                    }
                    else
                    {
                        SBR = SBC.sendNewAccountInfo(CAR);
                    }

                }
>>>>>>> bf539a332211dacb948c3479cf34245cfa212518
            return View("CreateAccount");
        }

        //This class is incomplete and should be completed by the students in milestone 2
        //Hint: You will need to make use of the ServiceBusConnection class. See EchoController.cs for an example.
    }
}