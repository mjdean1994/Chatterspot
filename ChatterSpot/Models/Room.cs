using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChatterSpot.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public Guid RoomIdentifier { get; set; }
        public string Title { get; set; }
        public string Password { get; set; }
        public string AdministrationPassword { get; set; }
        public DateTime LastAccess { get; set; }
    }
}