using System;

namespace Database.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; } = DateTime.MaxValue;

        public Table Table { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
