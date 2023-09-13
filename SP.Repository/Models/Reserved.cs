using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Repository.Models
{
    public class Reserved
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Hall? Hallobj { get; set; }
        public int seatId { get; set; }
        public bool booked { get; set; }
    }
}
