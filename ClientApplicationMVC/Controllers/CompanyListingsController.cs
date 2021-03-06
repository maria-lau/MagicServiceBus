﻿using ClientApplicationMVC.Models;

using Messages.DataTypes.Database.CompanyDirectory;
using Messages.ServiceBusRequest.CompanyDirectory.Responses;
using Messages.ServiceBusRequest.CompanyDirectory.Requests;
using Messages.ServiceBusRequest.CompanyReview.Requests;

using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net.Http;
using System.Web.Script.Serialization;
using Messages.ServiceBusRequest.CompanyReview.Responses;
using Messages.ServiceBusRequest;
using Messages.ServiceBusRequest.Weather.Requests;
using Messages.ServiceBusRequest.Weather.Responses;
using Messages.DataTypes.Database.Weather;

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

            GetWeatherRequest weatherRequest = new GetWeatherRequest(infoResponse.companyInfo.city, infoResponse.companyInfo.province);
            GetWeatherResponse weatherResponse = connection.getWeather(weatherRequest);
            ViewBag.foundWeather = weatherResponse.result;
            if (weatherResponse.result)
            {
                ViewBag.currentTemp = weatherResponse.weather.Temperature.Metric.Value;
                ViewBag.feelTemp = weatherResponse.weather.RealFeelTemperature.Metric.Value;
                ViewBag.weatherText = weatherResponse.weather.WeatherText;
                WeatherIcon url = new WeatherIcon();
                ViewBag.weatherIconURL = url.weatherURL[weatherResponse.weather.WeatherIcon];
            }
            else
            {
                ViewBag.currentTemp = "N/A";
                ViewBag.feelTemp = "N/A";
                ViewBag.weatherText = "N/A";
            }

            string company = ViewBag.CompanyName;
            GetReviewRequest reviewRequest = new GetReviewRequest(company);
            GetReviewResponse reviewResponse = connection.getCompanyReviews(reviewRequest);

            ViewBag.companyReviews = reviewResponse.reviews;

            return View("DisplayCompany");
        }

        public ActionResult SaveReview()
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

            String review = Request.Form["reviewData"];
            String company = Request.Form["companyName"];
            int time = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
            String rating = Request.Form["star"];     
            string json = "{review:{companyName:\"" + company + "\"," + "username:\"" + Globals.getUser() + "\","
                              + "review:\"" + review + "\"," + "stars:" + rating + "," + "timestamp:" + time + "}}";

            SaveReviewRequest srRequest = new SaveReviewRequest(company, json);
            ServiceBusResponse response = connection.saveCompanyReview(srRequest);
            

            Response.Write("<script>alert('" + response.response + "')</script>");

            return View("Index");
        }
    }
}