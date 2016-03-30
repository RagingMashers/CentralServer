using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CentralServer.sita;

namespace CentralServer
{
    /// <summary>
    /// Summary description for SitaApi
    /// </summary>
    [WebService(Namespace = "http://cims.nl/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SitaApi : System.Web.Services.WebService
    {
        [WebMethod]
        public string Login(string username, string password)
        {
            return null;
        }

        [WebMethod]
        public bool Logout()
        {
            return true;
        }

        [WebMethod]
        public Toxication[] GetToxications(string token)
        {
            return null;
        }

        [WebMethod]
        public bool AddToxication(string token, string name, string description, string chemicalCompound,
            int dangerLevel, double volatility)
        {
            return true;
        }

        [WebMethod]
        public bool EditIncident(string token, int id, string name, string description, string chemicalCompound,
            int dangerLevel, double volatility)
        {
            return true;
        }

        [WebMethod]
        public Incident[] GetIncidents(string token, int start = 0, int limit = 20)
        {
            return null;
        }

        [WebMethod]
        public Media[] GetMediaOfIncident(string token, int incident, int start = 0, int limit = 20)
        {
            return null;
        }

        [WebMethod]
        public Media[] GetMediaOfIncidentF(string token,int incident, string filter, int start = 0, int limit = 20)
        {
            return null;
        }

        [WebMethod]
        public Media[] GetMedia(string token, int start = 0, int limit = 20)
        {
            return null;
        }

        [WebMethod]
        public Media[] GetMediaF(string token, string filter, int start = 0, int limit = 20)
        {
            return null;
        }

        [WebMethod]
        public bool SendMessage(string token, int team, string message)
        {
            return true;
        }

        [WebMethod]
        public bool SendMessageWithMedia(string token,int team, string message, int mediaId)
        {
            return true;
        }
    }
}
