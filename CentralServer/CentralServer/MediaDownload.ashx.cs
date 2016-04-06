using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CentralServer.Database;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace CentralServer
{
    /// <summary>
    /// Summary description for MediaDownload
    /// </summary>
    public class MediaDownload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //todo authentication
            //get input and send errors if necesery
            if (context.Request.HttpMethod != "GET")//Method must be GET
            {
                WriteText(context,"Error, method mus be GET!");
                return;
            }
            string ids;
            if ((ids = context.Request.Params["id"]) == null)//parameter id must be given
            {
                WriteText(context,"Error, parameter id not given!");
                return;
            }
            
            int id;
            if (!int.TryParse(ids, out id))//id must be an int
            {
                WriteText(context,"Error, id is not a vallid integer!");
                return;
            }

            //set up the parameters
            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", id));

            //create the query
            //get 1 item from the media table where the id is equal to the given id
            var conn = new DatabaseConnection();
            var reader = conn.ExecuteQueryReader("SELECT mimeType, content FROM media WHERE id = @id LIMIT 1", parameters);

            //check if there are any results
            if (reader == null || !reader.HasRows)
            {
                WriteText(context,"Error, id is not found!");
                reader?.Close();
                conn.Close();
                return;
            }

            //get the data
            reader.Read();

            //get the mimetype and file extension
            var mimeType = reader.GetString("mimeType");
            context.Response.ContentType  = mimeType;
            context.Response.AddHeader("Content-Disposition",
                $"attachment; filename={id}{GetDefaultExtension(mimeType)}");//results in filename=id.extension

            //write the file by 100 byte blocks
            long index = 0;
            long bytesRead;
            var buffer = new byte[100];
            
            do
            {
                //read a buffer length from the database
                bytesRead = reader.GetBytes(reader.GetOrdinal("content"), index, buffer, 0, buffer.Length);
                index += bytesRead;
                //write the buffer to the output stream
                context.Response.OutputStream.Write(buffer,0,(int)bytesRead);
            } while (bytesRead == buffer.Length);//when less then a 100 bytes are read, then the file is completely received
            reader.Close();
            conn.Close();
        }

        /// <summary>
        /// Write text to the given context
        /// </summary>
        /// <param name="context">The context to write to</param>
        /// <param name="text">The text to write</param>
        private static void WriteText(HttpContext context, string text)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(text);
        }

        public bool IsReusable => true;//content stay's the same after upload

        /// <summary>
        /// Get the string extension of a given MimeType
        /// </summary>
        /// <param name="mimeType">The MimeType from which to get the extension. Like "image/jpeg"</param>
        /// <returns>The extension from the MimeType. Like ".jpg"</returns>
        public static string GetDefaultExtension(string mimeType)
        {
            var key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
            var value = key?.GetValue("Extension", null);
            var result = value?.ToString() ?? string.Empty;

            return result;
        }
    }
}