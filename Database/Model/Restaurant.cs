using System.Collections.Generic;

namespace Database.Model
{
    public class Restaurant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Table> Tables { get; set; }
    }
}
