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
            var teamId = -1;
            var token = "";
            var loggedin = false;
            var errorCode = 0;
            var errorText = "";
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                errorCode = 10;
                errorText = "Missing parameters";
                result = new {loggedin, username, token, teamId, errorCode, errorText};
            }
            else if (username == "testUser" && password == "testPass")
            {
                loggedin = true;
                token = "testToken";
                teamId = 2;
                errorText = "OK!";
                result = new { loggedin, username, token, teamId, errorCode, errorText };
            }
            else
            {
                errorCode = -1;
                errorText = "Not yet implemented";
                result = new { loggedin, username, token, teamId, errorCode, errorText};
            }
            return result;
        }

        [HttpGet]
        public object Logout([FromUri]string username = null, [FromUri]string token = null)
        {
            object result;
            var succes = false;
            var errorCode = 0;
            var errorText = "";
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token))
            {
                errorCode = 10;
                errorText = "Missing parameters";
                result = new {succes, errorCode, errorText};
            }else if (username == "testUser" && token == "testToken")//token or username not found or not matching
            {
                succes = true;
                errorText = "OK!";
                result = new { succes, errorCode, errorText };//Username and token do not match or do not exist = 1
            }
            else
            {
                errorCode = -1;
                errorText = "Not yet implemented";
                result = new { succes, errorCode, errorText };
            }
            return result;
        }
    }
}