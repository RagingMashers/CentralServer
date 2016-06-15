using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralServer.sita
{
    public class Message
    {
        #region fields and properties
        public enum DirectionType
        {
            T,
            E
        }
        private readonly int id;
        public int Id
        {
            get { return id; }
            set { throw new AccessViolationException("Id can not be set!"); }
        }

        private int team;
        public int Team 
        {
            get { return team; }
            set { team = value; }
        }

        private string description;
        public string Description 
        {
            get { return description;}
            set { description = value;} 
        }

        private string title;
        public string Title 
        {
            get { return title; }
            set { title = value; } 
        }

        private DirectionType directionOfMessage;
        public DirectionType DirectionOfMessage
        {
            get { return directionOfMessage; }
            set { directionOfMessage = value; }
        }

        private int incident;
        public int Incident 
        {
            get { return incident; }
            set { incident = value; } 
        }

        private readonly List<Media> media;
        public List<Media> Media 
        {
            get { return media;}
            set { throw new AccessViolationException("media can not be set!"); }
        }


        #endregion 

        /// <summary>
        /// DO NOT USE! FOR SERIALISATION ONLY!
        /// </summary>
	    public Message()
	    {
	        
	    }

        /// <summary>
        /// The constructor for the message class
        /// </summary>
        /// <param name="id">The indentifier of a message</param>
        /// <param name="team">The identifier of the team sending/receiving the message</param>
        /// <param name="description">The content of the message</param>
        /// <param name="title">The title/subject of the message</param>
        /// <param name="directionOfMessage">Indicates if the message is send to -or- send from a team.</param>
        /// /// <param name="incident">The identifier of the incident to which te message belongs.</param>
        public Message(int id, int team, string description, string title, DirectionType directionOfMessage, int incident)
        {
            this.id = id;
            this.team = team;
            this.description = description;
            this.title = title;
            this.directionOfMessage = directionOfMessage;
            this.incident = incident;
            this.media = new List<Media>();
        }

        /// <summary>
        /// Add a media object to the message.
        /// </summary>
        /// <param name="mediaObject">The media object to add.</param>
        /// <returns>returns true if succesfully added the media object to the message</returns>
        public bool AddMedia(Media mediaObject)
        {
            if (mediaObject == null) throw new NullReferenceException();
            if (media.Contains(mediaObject)) return false;
            media.Add(mediaObject);
            return true;
        }

    }
}