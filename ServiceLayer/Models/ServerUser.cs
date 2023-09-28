using System;

namespace ServiceLayer.Models
{
    public class ServerUser
    {
        public Guid? ServerUserID { get; set; }

        public string Username { get; set; }

        public string ImageSource { get; set; }

        public DateTime ActiveSince { get; set; }

        public bool IsSelected { get; set; }
    }
}
