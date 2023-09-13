using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Repository.Models
{
    public class Seat
    {
        public int Id { get; set; } = 0;
        public string SeatRow { get; set; }
        public string SeatCol { get; set; }
        public Hall Hall { get; set; }
    }
}
