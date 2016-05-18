using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralServer.sita
{
    public class Task
    {
        #region fields and properties
        private readonly int id;
        public int Id
        {
            get { return id; }
            set { throw new AccessViolationException("Id can not be set!"); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        /// <summary>
        /// DO NOT USE! FOR SERIALISATION ONLY!
        /// </summary>
	    public Task()
        {

        }

        public Task(int id, string description)
        {
            this.id = id;
            this.description = description;
        }
    }
}