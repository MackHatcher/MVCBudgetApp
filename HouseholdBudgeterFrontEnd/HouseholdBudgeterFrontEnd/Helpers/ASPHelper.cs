using HouseholdBudgeterFrontEnd.Models;
using HouseholdBudgeterFrontEnd.Models.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HouseholdBudgeterFrontEnd.Helpers
{
    public class ASPHelper
    {
        public HttpResponseMessage ASPHelperPost(string url,  List<ReqParameters> ReqParameters)
        {
            var httpClient = new HttpClient();

            var parameters = new List<KeyValuePair<string, string>>();
            
            foreach (var paras in ReqParameters){
                parameters.Add(new KeyValuePair<string, string>(paras.Name, paras.Value));
            }

            var formEncodedValues = new FormUrlEncodedContent(parameters);
            var cookie = HttpContext.Current.Request.Cookies["token"];
            

            if (cookie != null)
            {
                var accessToken = cookie.Value;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
            }
            
                var result = httpClient.PostAsync(url,
                formEncodedValues).Result;


            return result;
            }

        public HttpResponseMessage ASPHelperGet(string url)
        {

            var parameters = new List<KeyValuePair<string, string>>();
            var formEncodedValues = new FormUrlEncodedContent(parameters);
            var httpClient = new HttpClient();
            var cookie = HttpContext.Current.Request.Cookies["token"];
            if (cookie != null)
            {
                var accessToken = cookie.Value;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var result = httpClient.GetAsync(url).Result;
            
            return result;
        }

        public HttpResponseMessage ASPHelperDelete(string url)
        {
            var httpClient = new HttpClient();
            
            var result = httpClient.DeleteAsync(url).Result;

            var parameters = new List<KeyValuePair<string, string>>();
            var formEncodedValues = new FormUrlEncodedContent(parameters);
            var cookie = HttpContext.Current.Request.Cookies["token"];


            if (cookie != null)
            {
                var accessToken = cookie.Value;
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return result;
        }
    }
    }
