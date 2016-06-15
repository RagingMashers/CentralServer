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

            var columnNames = new[] { "id","name","description","chemicalCompound","dangerLevel", "volatility"};

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT id, name, description, chemicalCompound, dangerLevel, volatility FROM toxication", param, columnNames);

            databaseConnection.Close();

            int rowCount = (dataSet?.Count??0);
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

            var columnNames = new[] { "id","amountVictims","amountWounded","longitude","latitude", "radius", "dangerLevel", "description"};

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idStart", start));
            parameters.Add(new MySqlParameter("@idEnd", start + limit));

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Incident LIMIT " + limit, parameters, columnNames);
            Incident[] incidents = new Incident[limit + 1];

            //int id, int amountVictims, int amountWounded, double longitude, double latitude, int dangerlevel, string description
            for(int i = 0; i < (dataSet?.Count??0); i++)
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

            var columnNames = new[] { "id","incidentId","content","mimeType","data", "source", "accepted", "suggestion", "importance"};

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idStart", start));
            parameters.Add(new MySqlParameter("@idEnd", start + limit));
            parameters.Add(new MySqlParameter("@incidentId", incident));

            var dataSet =
                databaseConnection.ExecuteQuery(
                    "SELECT * FROM Media WHERE IncidentId = @incidentId LIMIT " + limit, parameters,
                    columnNames);
            var media = new Media[limit + 1];

            columnNames = new string[1];
            columnNames[0] = "Categoryid";

            for (var i = 0; i < (dataSet?.Count??0); i++)
            {
                var row = dataSet[i];
                parameters.Clear();
                parameters.Add(new MySqlParameter("@mediaId", int.Parse(row[0])));
                var categoryId =
                    int.Parse(
                        databaseConnection.ExecuteScalar(
                            "SELECT Categoryid FROM media_category WHERE Mediaid = @mediaId", parameters).ToString());
                media[i] =
                    (new Media(int.Parse(row[0]), new byte[0], row[3], DateTime.Parse(row[4]), row[5],
                        (MediaAccepted) int.Parse(row[6]), row[7], (Importance) int.Parse(row[8]), int.Parse(row[1]),
                        categoryId));
            }
            databaseConnection.Close();

            return media;
        }

        [WebMethod]
        public object[] GetMedia(string token, int start = 0, int limit = 20)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            var columnNames = new[] { "id","incidentId","content","mimeType","data", "source", "accepted", "suggestion", "importance"};

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@idStart", start));
            parameters.Add(new MySqlParameter("@idEnd", start + limit));

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM Media LIMIT " + limit, parameters, columnNames);
            Media[] media = new Media[limit + 1];

            columnNames = new string[1];
            columnNames[0] = "Categoryid";

            for (int i = 0; i < (dataSet?.Count??0); i++)
            {
                string[] row = dataSet[i];
                parameters.Clear();
                parameters.Add(new MySqlParameter("@mediaId", int.Parse(row[0])));
                int categoryId = int.Parse(databaseConnection.ExecuteQuery("SELECT Categoryid FROM media_category WHERE Mediaid = @mediaId", parameters, columnNames)[0][0]);
                media[i] = (new Media(int.Parse(row[0]), new byte[0], row[3], DateTime.Parse(row[4]), row[5], (MediaAccepted)int.Parse(row[6]), row[7], (Importance)int.Parse(row[8]), int.Parse(row[1]), categoryId));
            }
            databaseConnection.Close();

            return media;
        }
        
        [WebMethod]
        public bool SendMessage(string token, int teamId, string description, string title)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@teamId", teamId));
            parameters.Add(new MySqlParameter("@description", description));
            parameters.Add(new MySqlParameter("@title", title));

            int affectedRows = databaseConnection.ExecuteNonQuery("INSERT INTO message (Teamid, description, title) VALUES (@teamId, @description, @title)", parameters);
            
            databaseConnection.Close();
            if (affectedRows == 1)
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        public bool SendMessageWithMedia(string token,int teamId, string description, string title, int[] mediaIds)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            var succes = true;

            parameters.Add(new MySqlParameter("@teamId", teamId));
            parameters.Add(new MySqlParameter("@description", description));
            parameters.Add(new MySqlParameter("@title", title));
            parameters.Add(new MySqlParameter("@mediaId", "default"));
            int affectedRowsMessage = databaseConnection.ExecuteNonQuery("INSERT INTO message (Teamid, description, title) VALUES (@teamId, @description, @title)", parameters);

            foreach (int mediaId in mediaIds)
            {
                parameters[3] = new MySqlParameter("@mediaId", mediaId);

                int affectedRowsMedia_Message = databaseConnection.ExecuteNonQuery("INSERT INTO media_message (Mediaid, Messageid) VALUES (@mediaId, (SELECT MAX(id) FROM Message WHERE description = @description))", parameters);
                if (affectedRowsMessage != 1 || affectedRowsMedia_Message != 1)
                {
                    succes = false;
                }
            }

            databaseConnection.Close();
            return succes;
        }

        [WebMethod]
        public Message[] GetMessagesOfIncident(int incident, Message.DirectionType directionOfMessages)
        {
            databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@direction", directionOfMessages.ToString()));
            parameters.Add(new MySqlParameter("@incident", incident));

            var columnNames = new[] { "id","teamid","description","title","direction, incidentId" };

            var dataSetMessages = databaseConnection.ExecuteQuery(
                "SELECT id, teamid, description, title, direction, incidentId FROM Message WHERE incidentId = @incident AND direction = @direction ORDER BY id DESC LIMIT 30", parameters, columnNames
                );
            
            var amountOfMessages = dataSetMessages?.Count??0;
            var messages = new Message[amountOfMessages];
            for(var i = 0; i < amountOfMessages; i++)
            {
                var messageRow = dataSetMessages[i];
                messages[i] = new Message(int.Parse(messageRow[0]), int.Parse(messageRow[1]), messageRow[2], messageRow[3], (Message.DirectionType)Enum.Parse(typeof(Message.DirectionType), messageRow[4]), int.Parse(messageRow[5]));

                parameters = new List<MySqlParameter>();
                parameters.Add(new MySqlParameter("@messageId", messageRow[0]));

                columnNames = new[] { "id","mimeType","source","importance","date", "accepted", "suggestion", "Messageid"};

                databaseConnection = new DatabaseConnection();
                var dataSetMedia = databaseConnection.ExecuteQuery(
                    "SELECT media.id, media.mimeType, media.source, media.importance, media.date, media.accepted, media.suggestion, media_message.Messageid FROM media, media_message WHERE media.id = media_message.Mediaid AND media_message.Messageid = @messageId", parameters, columnNames
                    );
                
                var amountOfMedia = dataSetMedia?.Count??0;
                for(var j = 0; j < amountOfMedia; j++)
                {        
                    var mediaRow = dataSetMedia[j];
                    Media tempMedia = new Media(int.Parse(mediaRow[0]), new byte[0], mediaRow[1], DateTime.Parse(mediaRow[4]), mediaRow[2], (MediaAccepted)Enum.Parse(typeof(MediaAccepted), mediaRow[5]), mediaRow[6], (Importance)Enum.Parse(typeof(Importance), mediaRow[3]), incident, 0);
                    messages[i].AddMedia(tempMedia);
                }
            }

            databaseConnection.Close();

            return messages;
        }

        [WebMethod]
        public Team[] GetTeamsNearIncident(string token, double longitude, double latitude, int radius)
        {
            databaseConnection = new DatabaseConnection();

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@long", longitude));
            parameters.Add(new MySqlParameter("@lat", latitude));
            parameters.Add(new MySqlParameter("@radius", radius));

            var columnNames = new[] { "id","type","startDate","endDate","longitude", "latitude"};

            var dataSet = databaseConnection.ExecuteQuery(
                "SELECT id, type, startDate, endDate, longitude, latitude FROM team WHERE endDate IS NULL HAVING GETDISTANCE(@lat, @long, latitude, longitude) < @radius", parameters, columnNames
                );

            var amountOfRows = dataSet?.Count??0;
            var teams = new Team[amountOfRows];

            //int id, int type, DateTime startDate, DateTime endDate, double longitude, double latitude
            for (var i = 0; i < (dataSet?.Count??0); i++)
            {

                var row = dataSet[i];
                teams[i] = new Team(int.Parse(row[0]), (ServiceType)int.Parse(row[1]), Double.Parse(row[4]), Double.Parse(row[5]), Convert.ToDateTime(row[2]), default(DateTime));
            }
            databaseConnection.Close();

            return teams;
        }

        [WebMethod]
        public ActionPlan[] GetActionPlans(string token)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            MySqlParameter param = new MySqlParameter();

            string[] columnNames = new string[2];
            columnNames[0] = "id";
            columnNames[1] = "name";

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT id, name FROM actionplan ORDER BY id", param, columnNames);

            databaseConnection.Close();

            int rowCount = (dataSet?.Count ?? 0);
            ActionPlan[] actionPlans = new ActionPlan[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                actionPlans[i] = new ActionPlan(Int32.Parse(dataSet[i][0]), dataSet[i][1]);
                actionPlans[i].AddTasks(GetTasksFromActionPlan(token, actionPlans[i].Id).ToList());
            }
            return actionPlans;
        }

        [WebMethod]
        public Task[] GetTasks(string token)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            MySqlParameter param = new MySqlParameter();

            string[] columnNames = new string[2];
            columnNames[0] = "id";
            columnNames[1] = "description";

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT id, description FROM task", param, columnNames);

            databaseConnection.Close();

            int rowCount = (dataSet?.Count ?? 0);
            Task[] tasks = new Task[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                tasks[i] = new Task(Int32.Parse(dataSet[i][0]), dataSet[i][1]);
            }
            return tasks;
        }

        [WebMethod]
        public Task[] GetTasksFromActionPlan(string token, int actionPlanId)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            MySqlParameter parameter = new MySqlParameter();
            parameter  = new MySqlParameter("@actionPlanId", actionPlanId);

            string[] columnNames = new string[2];
            columnNames[0] = "id";
            columnNames[1] = "description";

            List<string[]> dataSet = databaseConnection.ExecuteQuery("SELECT * FROM TASK WHERE id IN (SELECT Taskid FROM actionplan_task WHERE ActionPlanid = @actionPlanId ORDER BY sequenceNumber)", parameter, columnNames);

            databaseConnection.Close();

            int rowCount = (dataSet?.Count ?? 0);
            Task[] tasks = new Task[rowCount];

            for (int i = 0; i < rowCount; i++)
            {
                tasks[i] = new Task(Int32.Parse(dataSet[i][0]), dataSet[i][1]);
            }
            return tasks;
        }

        [WebMethod]
        public bool AddTask(string token, string description)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@description", description));

            int rowsAffected = databaseConnection.ExecuteNonQuery("INSERT INTO task (description) VALUES (@description)", parameters);
            if (rowsAffected == 1)
            {
                return true;
            }

            return false;
        }


        [WebMethod]
        public bool AddActionPlan(string token, string name, int[] taskIds)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@name", name));

            int rowsAffected = databaseConnection.ExecuteNonQuery("INSERT INTO actionplan (name) VALUES (@name)", parameters);

            int count = 0;

            if (rowsAffected == 1 && taskIds != null)
            {
                foreach (int taskId in taskIds)
                {
                    databaseConnection.ExecuteNonQuery("INSERT INTO actionplan_task (ActionPlanid, TaskId, sequenceNumber) VALUES ((SELECT MAX(id) FROM actionPlan), @taskId, " + (count + 1) + ")", new MySqlParameter("@taskId", taskIds[count]));
                    count++;
                }

                return true;
            }

            return false;
        }

        [WebMethod]
        public bool EditActionPlan(string token, string name, int[] taskIds)
        {
            if (databaseConnection == null)
                databaseConnection = new DatabaseConnection();

            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@name", name));

            databaseConnection.ExecuteNonQuery("DELETE FROM actionplan_task WHERE ActionPlanId IN (SELECT id FROM actionplan WHERE name = @name)", new MySqlParameter("@name", name));
            int id = (Int32.Parse(databaseConnection.ExecuteScalar("SELECT id FROM actionplan WHERE name = @name", parameters).ToString()));
            int affectedRowsDelete = databaseConnection.ExecuteNonQuery("DELETE FROM actionplan WHERE name = @name", new MySqlParameter("@name", name));

            if (affectedRowsDelete == 1)
            {
                parameters.Add(new MySqlParameter("@id", id));

                int rowsAffected = databaseConnection.ExecuteNonQuery("INSERT INTO actionplan (id, name) VALUES (@id, @name)", parameters);
                int count = 0;

                if (rowsAffected == 1)
                {
                    foreach (int taskId in taskIds)
                    {
                        databaseConnection.ExecuteNonQuery("INSERT INTO actionplan_task (ActionPlanid, TaskId, sequenceNumber) VALUES ((SELECT MAX(id) FROM actionPlan), @taskId, " + (count + 1) + ")", new MySqlParameter("@taskId", taskIds[count]));
                        count++;
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
