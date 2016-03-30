using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CentralServer.sita;
using CentralServer.Database;
using MySql.Data.MySqlClient;

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
        DatabaseConnection databaseConnection; 

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
            databaseConnection = new DatabaseConnection();

            MySqlParameter param = new MySqlParameter();

            string[] columnNames = new string[6];
            columnNames[0] = "id";
            columnNames[1] = "name";
            columnNames[2] = "description";
            columnNames[3] = "chemicalCompound";
            columnNames[4] = "dangerLevel";
            columnNames[5] = "volatility";

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT id, name, description, chemicalCompound, dangerLevel, volatility FROM toxication", param, columnNames);

            databaseConnection.Close();

            int rowCount = dataSet.Count;
            Toxication[] toxications = new Toxication[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                toxications[i] = new Toxication(Int32.Parse(dataSet[i][0]), dataSet[i][1], dataSet[i][2], dataSet[i][3], Int32.Parse(dataSet[i][4]), Double.Parse(dataSet[i][5]));
            }
            return toxications;
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
