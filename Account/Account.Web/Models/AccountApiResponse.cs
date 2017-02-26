using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Account.Web.Models
{
    public class AccountApiResponse
    {
        public AccountApiResponse()
        {

        }

        public AccountApiResponse(bool issuccess, string message)
        {
            IsSuccess = issuccess;
            Message = message;
            Errors = new List<string>();
        }



        /// <summary>
        /// boolen property define the status of the request
        /// </summary>
        public bool IsSuccess { get; set; }


        /// <summary>
        /// string property for message if success or failure of any request
        /// </summary>

        public string Message { get; set; }


        public List<string> Errors { get; set; }

        /// <summary>
        /// data property for passing actual data of the request if any
        /// </summary>

        public object Data { get; set; }
    }
}