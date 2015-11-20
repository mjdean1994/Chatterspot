using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChatterSpot.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string DisplayName { get; set; }
        public string Body { get; set; }
        public DateTime SendDate { get; set; }
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}