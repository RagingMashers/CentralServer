using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace CentralServer.api
{
    public class MessagesController : ApiController
    {
        public object GetMessages([FromUri]string username, [FromUri]string token)
        {
            var result =  new {succes = true, messages = new List<object>()};
            var testMedia = new {id = 1, mimetype = "image/jpeg", url =$"http://{Request.RequestUri.Host+":"+Request.RequestUri.Port}/MediaDownload.ashx?id={1}", source = "Facebook", suggestion = "I have no clue", importance = 2, category = "TestCat"};
            result.messages.Add(new { message = "Test1", title = "Test1T", team = 1, direction = "T", media = new List<object>() {testMedia} });
            result.messages.Add(new { message = "Test2", title = "Test2T", team = 2, direction = "T", media = new List<object>() });
            result.messages.Add(new { message = "Test3", title = "Test3T", team = 3, direction = "T", media = new List<object>()});
            return result;
        }

        public object PostMessage([FromBody]JObject body, [FromUri]string username, [FromUri]string token)
        {
            //todo use body
            return new {succes=true};
        }
    }
}