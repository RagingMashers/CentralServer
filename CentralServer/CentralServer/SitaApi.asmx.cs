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
        public int AddIncident(string token, int id, int amountVictims, int amountWounded, double longitude, double latitude, int dangerlevel, int radius, string description)
        {
            databaseConnection = new DatabaseConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@id", id));
            parameters.Add(new MySqlParameter("@description", description));
            parameters.Add(new MySqlParameter("@amountVictims", amountVictims));
            parameters.Add(new MySqlParameter("@amountWounded", amountWounded));
            parameters.Add(new MySqlParameter("@longitude", longitude));
            parameters.Add(new MySqlParameter("@latitude", latitude));
            parameters.Add(new MySqlParameter("@radius", radius));
            parameters.Add(new MySqlParameter("@dangerlevel", dangerlevel));

            int affectedRowsInsert = databaseConnection.ExecuteNonQuery("INSERT INTO Incident VALUES (@id, @description, @amountVictims ,@amountWounded, @longitude, @latitude, @radius, @danger)", parameters);
            databaseConnection.Close();

            if (affectedRowsInsert == 0)
                return -1;
            else
                return id;
        }

        [WebMethod]
        public bool EditIncident(string token, int id, int amountVictims, int amountWounded, double longitude, double latitude, int radius, int dangerlevel, string description)
        {
            databaseConnection = new DatabaseConnection();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@id", id));
            parameters.Add(new MySqlParameter("@amountVictims", amountVictims));
            parameters.Add(new MySqlParameter("@amountWounded", amountWounded));
            parameters.Add(new MySqlParameter("@longitude", longitude));
            parameters.Add(new MySqlParameter("@latitude", latitude));
            parameters.Add(new MySqlParameter("@radius", radius));
            parameters.Add(new MySqlParameter("@dangerlevel", dangerlevel));
            parameters.Add(new MySqlParameter("@description", description));

            int affectedRowsInsert = databaseConnection.ExecuteNonQuery("UPDATE Incident SET amountVictims = @amountVictims, amountWounded = @amountWounded, longitude = @longitude, latitude = @latitude, latitude = @latitude, dangerlevel = @dangerlevel, description = @description WHERE id = @id", parameters);
            databaseConnection.Close();

            return 1 == affectedRowsInsert;
        }

        [WebMethod]
        public Incident[] GetIncidents(string token, int start = 0, int limit = 20)
        {
            databaseConnection = new DatabaseConnection();
            string[] columnNames = new string[8];
            columnNames[0] = "id";
            columnNames[1] = "amountVictims";
            columnNames[2] = "amountWounded";
            columnNames[3] = "longitude";
            columnNames[4] = "latitude";
            columnNames[5] = "radius";
            columnNames[6] = "dangerLevel";
            columnNames[7] = "description";

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idStart", start));
            parameters.Add(new MySqlParameter("@idEnd", start + limit));

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Incident WHERE id > @idStart AND id < @idEnd", parameters, columnNames);
            Incident[] incidents = new Incident[limit];

            //int id, int amountVictims, int amountWounded, double longitude, double latitude, int dangerlevel, string description
            for(int i = 0; i < dataSet.Count; i++)
            {
                string[] row = dataSet[i];
                incidents[i] = (new Incident(int.Parse(row[0]), int.Parse(row[1]), int.Parse(row[2]), double.Parse(row[3]), double.Parse(row[4]), int.Parse(row[5]), int.Parse(row[6]), row[7]));
            }

            databaseConnection.Close();

            return incidents;
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
