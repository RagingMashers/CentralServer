using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using CentralServer.Database;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace CentralServer
{
    /// <summary>
    /// Summary description for MediaUpload
    /// </summary>
    public class MediaUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //pre checks
            //method
            if (context.Request.HttpMethod != "POST")
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var result = new { succes = false, mediaId = -1, errorCode = 2, errorText = "Method must be POST" };
                context.Response.Write(JsonConvert.SerializeObject(result));
                return;
            }

            //param
            if (!context.Request.QueryString.AllKeys.Contains("id"))
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var result = new { succes = false, mediaId = -1, errorCode = 5, errorText = "id is not set!" };
                context.Response.Write(JsonConvert.SerializeObject(result));
                return;
            }

            //file info
            if (context.Request.Files.Count != 1)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var result = new {succes = false, mediaId = -1, errorCode = 1, errorText = "One and only file can be uploaded at a time! " + context.Request.Files.Count};
                context.Response.Write(JsonConvert.SerializeObject(result));
                return;
            }

            var file = context.Request.Files[0];
            if (file.ContentLength <= 0)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var result = new { succes = false, mediaId = -1, errorCode = 3, errorText = "File is empty!" };
                context.Response.Write(JsonConvert.SerializeObject(result));
                return;
            }

            var filename = Path.GetFileName(file.FileName);
            if (string.IsNullOrEmpty(filename))
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var result = new { succes = false, mediaId = -1, errorCode = 4, errorText = "File name invallid!" };
                context.Response.Write(JsonConvert.SerializeObject(result));
                return;
            }

            //get id
            int id;
            if (!int.TryParse(context.Request.QueryString["id"], out id))
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                var result = new { succes = false, mediaId = -1, errorCode = 6, errorText = "id is not an integer!" };
                context.Response.Write(JsonConvert.SerializeObject(result));
                return;
            }

            //get mime type, if invallid search by extension
            var mime = file.ContentType;
            if (string.IsNullOrEmpty(mime) || mime == "application/octet-stream")
            {
                mime = LookupMime(file.FileName.Split('.').Last());
                if (mime == null)
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    var result = new { succes = false, mediaId = -1, errorCode = 8, errorText = "Unkown file extension or MimeType!" };
                    context.Response.Write(JsonConvert.SerializeObject(result));
                    return;
                }
            }

            //get file
            List<byte> data;
            if (file.ContentLength != null && file.ContentLength != 0)
                data = new List<byte>(file.ContentLength);
            else
                data = new List<byte>();

            var buffer = new byte[1024];
            int bytes;
            while ((bytes = file.InputStream.Read(buffer, 0, buffer.Length))!=0)
            {
                if (bytes == buffer.Length)
                {
                    data.AddRange(buffer);
                }
                else
                {
                    var sub = new byte[bytes];
                    Array.Copy(buffer,sub,bytes);
                    data.AddRange(sub);
                }
            }

            //insert
            var con = new DatabaseConnection();
            
            var parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@iid", id),
                new MySqlParameter("@content", data.ToArray()),
                new MySqlParameter("@mimeType", mime)
            };

            //insert data
            var rows = con.ExecuteNonQuery("INSERT INTO media (Incidentid, content, mimeType, date, source, accepted, importance) VALUES (@iid, @content, @mimeType, now(), \"web\", 2, 1)", parameters);
            //get new id
            var mido = con.ExecuteScalar("SELECT MAX(id) FROM media", new List<MySqlParameter>());

            //no idea, somthing went wrong?
            //if (!(mido is ulong))
            //{
            //    context.Response.StatusCode = 400;
            //    context.Response.ContentType = "application/json";
            //    var result = new { succes = false, mediaId = -1, errorCode = 7, errorText = "Somthing went wrong while inserting!"/*, debug=new {mido, size = data.Count(), type = mido.GetType().ToString()}*/ };
            //    context.Response.Write(JsonConvert.SerializeObject(result));
            //    return;
            //}

            //whohoe everything is uploaded!
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            var fresult = new { succes = true, mediaId = mido, errorCode = 0, errorText = "OK!", debug=new {filename, length = file.ContentLength, acutallength = data.Count, /*hash=GetMd5Hash(file.InputStream),*/ mime = file.ContentType, mime2 = mime, rows}};
            context.Response.Write(JsonConvert.SerializeObject(fresult));
            return;

        }

        public bool IsReusable => false;
        
        /// <summary>
        /// Get the default mimetype of an extension
        /// </summary>
        /// <param name="extension">The extension of the file (no dot(.))</param>
        /// <returns>The </returns>
        private static string LookupMime(string extension)
        {
            var key = Registry.ClassesRoot.OpenSubKey(@"." + extension, false);
            return (string) key?.GetValue("Content Type");
            //registry HKCR/.<ext>/@Content Type
        }
    }
}