using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Core.Framework
{
    /// <summary>
    /// deafult api response model for communication between clients and api
    /// </summary>
    public class AccountApiResponse
    {

        /// <summary>
        /// boolen property define the status of the request
        /// </summary>
        public bool IsSuccess { get; set; }


        /// <summary>
        /// string property for message if success or failure of any request
        /// </summary>

        public string Message { get; set; }


        /// <summary>
        /// list of string property for Errors if failure of any request
        /// </summary>

        public List<string> Errors { get; set; }

        /// <summary>
        /// data property for passing actual data of the request if any
        /// </summary>

        public object Data { get; set; }
    }
}
