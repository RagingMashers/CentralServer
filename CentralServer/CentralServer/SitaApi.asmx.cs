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
            if(databaseConnection == null)
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
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@name", name));
            parameters.Add(new MySqlParameter("@description", description));
            parameters.Add(new MySqlParameter("@chemicalCompound", chemicalCompound));
            parameters.Add(new MySqlParameter("@dangerLevel", dangerLevel));
            parameters.Add(new MySqlParameter("@volatility", volatility));

            int rowsAffected = databaseConnection.ExecuteNonQuery("INSERT INTO Toxication (name, description, chemicalCompound, dangerLevel, volatility) VALUES (@name, @description, @chemicalCompound, @dangerLevel, @volatility)", parameters);
            if (rowsAffected == 1)
            {
                return true;
            }

            return false;
        }

        [WebMethod]
        public bool AddIncident(string token, int amountVictims, int amountWounded, double longitude, double latitude, int dangerlevel, int radius, string description)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@description", description));
            parameters.Add(new MySqlParameter("@amountVictims", amountVictims));
            parameters.Add(new MySqlParameter("@amountWounded", amountWounded));
            parameters.Add(new MySqlParameter("@longitude", longitude));
            parameters.Add(new MySqlParameter("@latitude", latitude));
            parameters.Add(new MySqlParameter("@radius", radius));
            parameters.Add(new MySqlParameter("@dangerlevel", dangerlevel));

            int affectedRowsInsert = databaseConnection.ExecuteNonQuery("INSERT INTO Incident (description, amountVictims , amountWounded, longitude, latitude, radius, dangerlevel) VALUES (@description, @amountVictims ,@amountWounded, @longitude, @latitude, @radius, @dangerlevel)", parameters);
            databaseConnection.Close();

            return affectedRowsInsert == 1;
        }

        [WebMethod]
        public bool EditIncident(string token, int id, int amountVictims, int amountWounded, double longitude, double latitude, int radius, int dangerlevel, string description)
        {
            if (databaseConnection == null)
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
        public bool DeleteIncident(string token, int id)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@id", id));

            int affectedRowsInsert = databaseConnection.ExecuteNonQuery("DELETE FROM Incident WHERE id = @id", parameters);
            databaseConnection.Close();

            return 1 == affectedRowsInsert;
        }

        [WebMethod]
        public Incident[] GetIncidents(string token, int start = 0, int limit = 20)
        {
            if (databaseConnection == null)
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

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Incident WHERE id >= @idStart AND id <= @idEnd", parameters, columnNames);
            Incident[] incidents = new Incident[limit + 1];

            //int id, int amountVictims, int amountWounded, double longitude, double latitude, int dangerlevel, string description
            for(int i = 0; i < dataSet.Count; i++)
            {
                
                string[] row = dataSet[i];
                incidents[i] = new Incident(int.Parse(row[0]), int.Parse(row[1]), int.Parse(row[2]), double.Parse(row[3]), double.Parse(row[4]), int.Parse(row[5]), int.Parse(row[6]), row[7]);
            }
            databaseConnection.Close();

            return incidents;
        }

        [WebMethod]
        public object[] GetMediaOfIncident(string token, int incident, int start = 0, int limit = 20)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            var columnNames = new string[9];
            columnNames[0] = "id";
            columnNames[1] = "incidentId";
            columnNames[2] = "content";
            columnNames[3] = "mimeType";
            columnNames[4] = "date";
            columnNames[5] = "source";
            columnNames[6] = "accepted";
            columnNames[7] = "suggestion";
            columnNames[8] = "importance";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idStart", start));
            parameters.Add(new MySqlParameter("@idEnd", start + limit));
            parameters.Add(new MySqlParameter("@incidentId", incident));

            var dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Media WHERE id >= @idStart AND id <= @idEnd AND IncidentId = @incidentId", parameters, columnNames);
            var media = new Media[limit + 1];

            columnNames = new string[1];
            columnNames[0] = "Categoryid";

            for (var i = 0; i < dataSet.Count; i++)
            {
                var row = dataSet[i];
                parameters.Clear();
                parameters.Add(new MySqlParameter("@mediaId", int.Parse(row[0])));
                var categoryId = int.Parse(databaseConnection.ExecuteScalar("SELECT Categoryid FROM media_category WHERE Mediaid = @mediaId", parameters).ToString());
                media[i] = (new Media(int.Parse(row[0]), new byte[0], row[2], DateTime.Parse(row[4]), row[5], (MediaAccepted)int.Parse(row[6]), row[7], (Importance)int.Parse(row[8]), int.Parse(row[1]), categoryId));
            }
            databaseConnection.Close();

            return media;
        }

        [WebMethod]
        public object[] GetMediaOfIncidentF(string token,int incident, string filter, int start = 0, int limit = 20)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            string[] columnNames = new string[9];
            columnNames[0] = "id";
            columnNames[1] = "incidentId";
            columnNames[2] = "content";
            columnNames[3] = "mimeType";
            columnNames[4] = "date";
            columnNames[5] = "source";
            columnNames[6] = "accepted";
            columnNames[7] = "suggestion";
            columnNames[8] = "importance";

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idStart", start));
            parameters.Add(new MySqlParameter("@idEnd", start + limit));
            parameters.Add(new MySqlParameter("@incidentId", incident));
            parameters.Add(new MySqlParameter("@filter", filter));

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Media WHERE id >= @idStart AND id <= @idEnd AND IncidentId = @incidentId", parameters, columnNames);
            Media[] media = new Media[limit + 1];

            columnNames = new string[1];
            columnNames[0] = "Categoryid";

            for (int i = 0; i < dataSet.Count; i++)
            {
                string[] row = dataSet[i];
                parameters.Clear();
                parameters.Add(new MySqlParameter("@mediaId", int.Parse(row[0])));
                int categoryId = int.Parse(databaseConnection.ExecuteQuery("SELECT Categoryid FROM media_category WHERE Mediaid = @mediaId", parameters, columnNames)[0][0]);
                media[i] = (new Media(int.Parse(row[0]), new byte[0], row[2], DateTime.Parse(row[4]), row[5], (MediaAccepted)int.Parse(row[6]), row[7], (Importance)int.Parse(row[8]), int.Parse(row[1]), categoryId));
            }
            databaseConnection.Close();

            return media;
        }

        [WebMethod]
        public object[] GetMedia(string token, int start = 0, int limit = 20)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            string[] columnNames = new string[9];
            columnNames[0] = "id";
            columnNames[1] = "incidentId";
            columnNames[2] = "content";
            columnNames[3] = "mimeType";
            columnNames[4] = "date";
            columnNames[5] = "source";
            columnNames[6] = "accepted";
            columnNames[7] = "suggestion";
            columnNames[8] = "importance";

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idStart", start));
            parameters.Add(new MySqlParameter("@idEnd", start + limit));

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Media WHERE id >= @idStart AND id <= @idEnd", parameters, columnNames);
            Media[] media = new Media[limit + 1];

            columnNames = new string[1];
            columnNames[0] = "Categoryid";

            for (int i = 0; i < dataSet.Count; i++)
            {
                string[] row = dataSet[i];
                parameters.Clear();
                parameters.Add(new MySqlParameter("@mediaId", int.Parse(row[0])));
                int categoryId = int.Parse(databaseConnection.ExecuteQuery("SELECT Categoryid FROM media_category WHERE Mediaid = @mediaId", parameters, columnNames)[0][0]);
                media[i] = (new Media(int.Parse(row[0]), new byte[0], row[2], DateTime.Parse(row[4]), row[5], (MediaAccepted)int.Parse(row[6]), row[7], (Importance)int.Parse(row[8]), int.Parse(row[1]), categoryId));
            }
            databaseConnection.Close();

            return media;
        }

        [WebMethod]
        public object[] GetMediaF(string token, string filter, int start = 0, int limit = 20)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            string[] columnNames = new string[9];
            columnNames[0] = "id";
            columnNames[1] = "incidentId";
            columnNames[2] = "content";
            columnNames[3] = "mimeType";
            columnNames[4] = "date";
            columnNames[5] = "source";
            columnNames[6] = "accepted";
            columnNames[7] = "suggestion";
            columnNames[8] = "importance";

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idStart", start));
            parameters.Add(new MySqlParameter("@idEnd", start + limit));
            parameters.Add(new MySqlParameter("@filter", filter));

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Media WHERE id >= @idStart AND id <= @idEnd", parameters, columnNames);
            Media[] media = new Media[limit + 1];

            columnNames = new string[1];
            columnNames[0] = "Categoryid";

            for (int i = 0; i < dataSet.Count; i++)
            {
                string[] row = dataSet[i];
                parameters.Clear();
                parameters.Add(new MySqlParameter("@mediaId", int.Parse(row[0])));
                int categoryId = int.Parse(databaseConnection.ExecuteQuery("SELECT Categoryid FROM media_category WHERE Mediaid = @mediaId", parameters, columnNames)[0][0]);
                media[i] = (new Media(int.Parse(row[0]), new byte[0], row[2], DateTime.Parse(row[4]), row[5], (MediaAccepted)int.Parse(row[6]), row[7], (Importance)int.Parse(row[8]), int.Parse(row[1]), categoryId));
            }
            databaseConnection.Close();

            return media;
        }

        [WebMethod]
        public bool SendMessage(string token, int teamId, string description)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@teamId", teamId));
            parameters.Add(new MySqlParameter("@description", description));

            int affectedRows = databaseConnection.ExecuteNonQuery("INSERT INTO message (Teamid, description) VALUES (@teamId, @description)", parameters);
            if (affectedRows == 1)
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        public bool SendMessageWithMedia(string token,int teamId, string description, int mediaId)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@teamId", teamId));
            parameters.Add(new MySqlParameter("@description", description));
            parameters.Add(new MySqlParameter("@mediaId", mediaId));

            int affectedRows = databaseConnection.ExecuteNonQuery("INSERT INTO message (Teamid, description, mediaId) VALUES (@teamId, @description, @mediaId)", parameters);
            if (affectedRows == 1)
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        public Team[] GetTeamsNearIncident(double longitude, double latitude, int radius)
        {
            databaseConnection = new DatabaseConnection();

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@long", longitude));
            parameters.Add(new MySqlParameter("@lat", latitude));
            parameters.Add(new MySqlParameter("@radius", radius));

            var columnNames = new string[6];
            columnNames[0] = "id";
            columnNames[1] = "type";
            columnNames[2] = "startDate";
            columnNames[3] = "endDate";
            columnNames[4] = "longitude";
            columnNames[5] = "latitude";

            var dataSet = databaseConnection.ExecuteQuery(
                "SELECT id, type, startDate, endDate, longitude, latitude FROM team WHERE endDate IS NULL HAVING GETDISTANCE(@lat, @long, latitude, longitude) < @radius", parameters, columnNames
                );

            var amountOfRows = dataSet.Count;
            var teams = new Team[amountOfRows];

            //int id, int type, DateTime startDate, DateTime endDate, double longitude, double latitude
            for (var i = 0; i < dataSet.Count; i++)
            {

                var row = dataSet[i];
                teams[i] = new Team(int.Parse(row[0]), (ServiceType)int.Parse(row[1]), Double.Parse(row[4]), Double.Parse(row[5]), Convert.ToDateTime(row[2]), default(DateTime));
            }
            databaseConnection.Close();

            return teams;
        }
    }
}
