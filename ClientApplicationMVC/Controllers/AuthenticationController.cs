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

                string username;
                string password;
                string address;
                string email;
                string pnumber;
                string type;

                using (var reader = new System.IO.StreamReader(Request.InputStream))
                {
                    input = reader.ReadToEnd();
                }

                Debug.WriteLine("Bill Luu is suuuper fat -------------------------------------\n" + input);
                string[] substrings = input.Split('=','&');
                username = substrings[1];
                password = substrings[3];
                address = substrings[5];
                email = substrings[7];
                pnumber = substrings[9];
                type = substrings[11];

                CreateAccount account = new CreateAccount();
                account.username = username;
                account.password = password;
                account.address = address;
                account.email = email;
                account.phonenumber = pnumber;
                account.type = (AccountType)System.Enum.Parse(typeof(AccountType), type);

                CreateAccountRequest CAR = new CreateAccountRequest(account);
                
            }




            return View("CreateAccount");
            
        }

        //This class is incomplete and should be completed by the students in milestone 2
        //Hint: You will need to make use of the ServiceBusConnection class. See EchoController.cs for an example.
    }
}