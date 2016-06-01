using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CentralServer.Database;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace CentralServer.api
{
    public class MessagesController : ApiController
    {
        private static DatabaseConnection dbConnection;
        private static bool connected = false;

        static MessagesController()
        {
            dbConnection = new DatabaseConnection();
            if (!dbConnection.Connect())
            {
                Console.WriteLine("Can't open database!");
                return;
            }
            connected = true;
        }

        [HttpGet]
        public object GetMessages([FromUri]string username, [FromUri]string token)
        {
            var succes = false;
            var messages = new List<object>();
            var errorText = "";
            var errorCode = 0;
            if (!connected)
            {
                errorCode = -1;
                errorText = "Error while initialising!";
                var result = new {succes,messages,errorCode,errorMessage = errorText};
                return result;
            }
            if (!validateCred(username, token))
            {
                errorCode = 11;
                errorText = "Invallid credentials!";
                var result = new {succes, messages, errorCode, errorMessage = errorText};
                return result;
            }
            else
            {
                var result = new {succes = true, messages = new List<object>()};
                var columns1 = new[] { "id","teamId","description","title","direction" };
                var messagesResult =
                    dbConnection.ExecuteQuery("SELECT id,teamId,description,title,direction FROM message;",
                        new List<MySqlParameter>(), columns1);
                var columns2 =
                    "mid,mimeType,source,importance,date,Messageid".Split(',');
                var mediaResult =
                    dbConnection.ExecuteQuery(
                        "SELECT media.id mid,media.mimeType mimeType,media.source source,media.importance importance,media.date,media_message.Messageid Messageid FROM media, media_message WHERE media.id = media_message.Mediaid",
                        new List<MySqlParameter>(), columns2);

                

                foreach (var singleMessage in messagesResult)
                {
                    var message = new {
                        message = singleMessage[2],
                        title = singleMessage[3],
                        team = int.Parse(singleMessage[1]),
                        direction = singleMessage[4],
                        media = new List<object>()
                    };
                    foreach (var mediaItem in mediaResult.Where((i) => i[5] == singleMessage[0]))
                    {
                        message.media.Add(new
                        {
                            id = int.Parse(mediaItem[0]),
                            mimetype = mediaItem[1],
                            url =
                            $"http://{Request.RequestUri.Host + ":" + Request.RequestUri.Port}/MediaDownload.ashx?id={mediaItem[0]}",
                            source = mediaItem[2],
                            //suggestion = "I have no clue",
                            importance = int.Parse(mediaItem[3]),
                            //category = "TestCat",
                            date = DateTime.Parse(mediaItem[4])
                        });
                    }
                    result.messages.Add(message);
                }

                return result;
            }
        }

        [HttpPost]
        public object PostMessage([FromBody]JObject body, [FromUri]string username, [FromUri]string token)
        {
            //todo use body
            return new {succes=true};
        }

        private bool validateCred(string username, string token)
        {
            return username == "testUser" && token == "testToken";
        }
    }
}