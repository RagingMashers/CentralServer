using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace CentralServer.api
{
    public class AuthenticationController : ApiController
    {
        [HttpGet]
        public object Login([FromUri]string username = null, [FromUri]string password = null)
        {
            object result;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                result = new {loggedin = false, token = "", errorCode = 10, errorText="Missing parameters"};
            }
            else
            {
                result = new { loggedin = false, token = "", errorCode = -1, errorText = "Not yet implemented" };
            }
            return result;
        }

        [HttpGet]
        public object Logout([FromUri]string username = null, [FromUri]string token = null)
        {
            object result;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token))
            {
                result = new {success = false, errorCode = 10, errorText = "Missing parameters"};
            }else if (false)//token or username not found or not matching
            {
                result = new { success = false, errorCode = 1, errorText = "Username and token do not match or do not exist" };
            }
            else{
                result = new { success = false, errorCode = -1, errorText = "Not yet implemented" };
            }
            return result;
        }
    }
}