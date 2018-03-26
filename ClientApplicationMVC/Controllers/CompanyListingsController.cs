using ClientApplicationMVC.Models;

using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;

using System;
using System.Text;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClientApplicationMVC.Controllers
{
    /// <summary>
    /// This class contains the functions responsible for handling requests routed to *Hostname*/CompanyListings/*
    /// </summary>
    public class CompanyListingsController : Controller
    {
        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings
        /// </summary>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult Index()
        {
            if (Globals.isLoggedIn())
            {
                ViewBag.Companylist = null;
                return View("Index");
            }
            return RedirectToAction("Index", "Authentication");
        }

        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings/Search
        /// </summary>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult Search(string textCompanyName)
        {

            if (Globals.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if(connection == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            CompanySearchRequest request = new CompanySearchRequest(textCompanyName);

            CompanySearchResponse response = connection.searchCompanyByName(request);
            if (response.result == false)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ViewBag.Companylist = response.list;

            return View("Index");
        }

        /// <summary>
        /// This function is called when the client navigates to *hostname*/CompanyListings/DisplayCompany/*info*
        /// </summary>
        /// <param name="id">The name of the company whos info is to be displayed</param>
        /// <returns>A view to be sent to the client</returns>
        public ActionResult DisplayCompany(string id)
        {
            if (Globals.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Authentication");
            }
            if ("".Equals(id))
            {
                return View("Index");
            }

            ServiceBusConnection connection = ConnectionManager.getConnectionObject(Globals.getUser());
            if (connection == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ViewBag.CompanyName = id;
            ViewData["companyID"] = id;

            GetCompanyInfoRequest infoRequest = new GetCompanyInfoRequest(new CompanyInstance(id));
            GetCompanyInfoResponse infoResponse = connection.getCompanyInfo(infoRequest);
            ViewBag.CompanyInfo = infoResponse.companyInfo;

            return View("DisplayCompany");
        }

        public ActionResult SaveReview()
        {
            String review = Request.Form["reviewData"];
            String company = Request.Form["companyName"];
            TimeSpan time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            String rating = Request.Form["rating"];

            var httpPostRequest = new HttpClient();
            string uri = "http://104.197.187.198/api/Review/PostReview";
            string json = "{review:{companyName:\"" + company + "\"," + "username:\"" + Globals.getUser() + "\","
                              + "review:\"" + review + "\"," + "stars:" + rating + "," + "timestamp:" + time.TotalSeconds + "}}";
            System.Diagnostics.Debug.WriteLine("\n\n\n" + json + "\n\n\n");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = httpPostRequest.PostAsync(uri, stringContent);

            string message = "Successfully saved review for company: " + company;

            Response.Write("<script>alert('" + message + "')</script>");

            return View("Index");
        }
    }
}