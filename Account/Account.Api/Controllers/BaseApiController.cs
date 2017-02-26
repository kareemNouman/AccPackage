using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Account.Api.Controllers
{
    public abstract class BaseApiController : ApiController
    {

        public BaseApiController()
        {

        }
        //public BaseApiController(ILogExceptionService logexception)
        //{
        //    this._logexception = logexception;

        //}


        [NonAction]
        public IHttpActionResult RunInSafe(Func<IHttpActionResult> fn)
        {
            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                throw;
            }

        }


        protected void ExceptionLog(Exception ex)
        {
            // exception logging
            //var logger = Request.GetDependencyScope().GetService(typeof(ILogExceptionService)) as ILogExceptionService;
            //LogException log = new LogException();
            //log.ExceptionMessage = ex.Message;
            //log.InnerException = ex.InnerException == null ? string.Empty : ex.InnerException.ToString();
            //log.StackTrace = ex.StackTrace;
            //log.CreatedOn = DateTime.Now;
            //logger.AddException(log);
        }

    }
}
