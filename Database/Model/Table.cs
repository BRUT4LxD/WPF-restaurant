using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Model
{
    public class Table
    {
        public int Id { get; set; }

        public int AvailablePlaces { get; set; }
    }
}
