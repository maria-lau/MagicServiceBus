using ClientApplicationMVC.Models;

using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;

using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net.Http;
using System.Web.Script.Serialization;

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
            if (connection == null)
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

            GetCompanyInfoRequest infoRequest = new GetCompanyInfoRequest(new CompanyInstance(id));
            GetCompanyInfoResponse infoResponse = connection.getCompanyInfo(infoRequest);
            ViewBag.CompanyInfo = infoResponse.companyInfo;

            // Call API to retrieve company reviews
            string company = ViewBag.CompanyName;
            string apiurl = "http://35.188.169.187/api/review/getreview/{companyName:\"" + company + "\"}";
            //System.Diagnostics.Debug.WriteLine("\n\n\n" + apiurl + "\n\n\n");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiurl).Result;
            HttpContent content = response.Content;
            JavaScriptSerializer js = new JavaScriptSerializer();
            ReviewInfo[] reviews = js.Deserialize<ReviewInfo[]>(content.ReadAsStringAsync().Result);
            String stringReviews = "";
            for(int i = 0; i < reviews.Length; i++)
            {
                stringReviews = stringReviews + reviews[i].username + "<br />Wrote a review for <a style=color:#1185f9>" + reviews[i].companyName 
                                + "</a><br />Rating: " + reviews[i].stars + "<br />" + reviews[i].review + "<br />Time: " + reviews[i].timestamp + "<br /><br /><br />";
            }

            ViewBag.companyReviews = stringReviews; 

            //string test = ViewBag.companyReviews;
            //System.Diagnostics.Debug.WriteLine("\n\n\n" + test + "\n\n\n");

            return View("DisplayCompany");
        }

        public ActionResult SaveReview(string starRating)
        {
            String review = Request.Form["reviewData"];
            String company = Request.Form["companyName"];
            TimeSpan time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            String rating = "";
            if (Request.Form["star"] != null)
            {
                rating = Request.Form["star"].ToString();
            }

            var httpPostRequest = new HttpClient();
            string uri = "http://104.197.187.198/api/Review/PostReview";
            string json = "{review:{companyName:\"" + company + "\"," + "username:\"" + Globals.getUser() + "\","
                              + "review:\"" + review + "\"," + "stars:" + rating + "," + "timestamp:" + time.TotalSeconds + "}}";
            System.Diagnostics.Debug.WriteLine("\n\n\n" + json + "\n\n\n");
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = httpPostRequest.PostAsync(uri, stringContent);

            //string message = "Successfully saved review for company: " + company;
            string message = json;
            Response.Write("<script>alert('" + message + "')</script>");

            return View("Index");
        }
    }
}