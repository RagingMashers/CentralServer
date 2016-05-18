using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralServer.sita
{
    public class ActionPlan
    {
        #region fields and properties
        private readonly int id;
        public int Id
        {
            get { return id; }
            set { throw new AccessViolationException("Id can not be set!"); }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private List<Task> tasks;
        public List<Task> Tasks
        {
            get { return tasks; }
            set { throw new AccessViolationException("MediaItems can not be set!"); }
        }
        #endregion

        /// <summary>
        /// DO NOT USE! FOR SERIALISATION ONLY!
        /// </summary>
	    public ActionPlan()
        {

        }

        public ActionPlan(int id, string name)
        {
            this.id = id;
            this.name = name;
            tasks = new List<Task>();
        }

        public void AddTasks(List<Task> tasks)
        {
            Tasks.AddRange(tasks);
        }
    }
}