using Account.Web.Infrastructure.Helpers;
using Account.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Account.Web.Services
{
    public abstract class BaseService
    {
       // string apival = "/necapi";
        string apival = AccountConfig.ApiEndPoint;

        /// <summary>
        /// get method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        public async Task<AccountApiResponse> Get(string url)
        {
            var apiResponse = new AccountApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AccountConfig.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.GetAsync(apival + url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Current.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<AccountApiResponse>();

                return apiResponse;
            }
        }


        /// <summary>
        /// post method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        /// <param name="formdata">post data in dictionary format</param>
        /// <returns></returns>
        protected virtual async Task<string> Post(string url, Dictionary<string, string> formdata)
        {

            if (formdata == null)
                return string.Empty;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AccountConfig.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                var postData = new List<KeyValuePair<string, string>>();

                foreach (var param in formdata)
                    postData.Add(new KeyValuePair<string, string>(param.Key, param.Value));

                HttpContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync(apival + url, content);

                string apiResponse = string.Empty;
                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsStringAsync();

                return apiResponse;
            }
        }


        /// <summary>
        /// post method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        /// <param name="formdata">generic form data to post</param>
        /// <returns>AccountApiResponse</returns>
        protected virtual async Task<AccountApiResponse> Post<T>(string url, T formdata)
        {
            if (formdata == null)
                throw new ArgumentException("cannot create post request without formdata");

            var apiResponse = new AccountApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(AccountConfig.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.PostAsJsonAsync(apival + url, formdata);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Current.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<AccountApiResponse>();

                return apiResponse;
            }
        }


        /// <summary>
        /// put method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        /// <param name="formdata">generic form data to update</param>
        /// <returns>AccountApiResponse</returns>
        protected virtual async Task<AccountApiResponse> Put<T>(string url, T formdata)
        {
            if (formdata == null)
                throw new ArgumentException("cannot create post request without formdata");

            var apiResponse = new AccountApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AccountConfig.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.PutAsJsonAsync(apival + url, formdata);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Current.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<AccountApiResponse>();

                return apiResponse;
            }
        }


        /// <summary>
        /// Delete method request to api
        /// </summary>
        /// <param name="url">relative url of the resource</param>
        /// <param name="formdata">generic form data to update</param>
        /// <returns>AccountApiResponse</returns>
        protected virtual async Task<AccountApiResponse> Delete(string url)
        {
            var apiResponse = new AccountApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AccountConfig.ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.DeleteAsync(apival + url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Current.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<AccountApiResponse>();

                return apiResponse;
            }
        }

        // Handy helper method to set the access token for each request:
        void SetClientAuthentication(HttpClient client)
        {
            var token = HttpContext.Current.User.Identity.GetToken();

            if (string.IsNullOrWhiteSpace(token))
                return;

            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", token);
        }

        protected async Task<T> DeserializeAsync<T>(string value)
        {
            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(value));
            return result;
        }


        public bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}