using System;

namespace Database.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; } = DateTime.MaxValue;

        public int TableId { get; set; }

        public int RestaurantId { get; set; }

        public override string ToString()
            => "Id: " + Id.ToString() + "\t Name:" + Name + " \t Time: " + StartTime.ToString("HH:mm:ss:fff") + "\t Table: " + (TableId + 1).ToString();
    }
}
