using SP.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Repository.Interfaces
{
    public interface ISeatRepository
    {
        public Task<Hall> readSeatsInHall(int HallId);
        public Task<List<Reserved>> readReservedSeats();
        public Task delete(bool choise);
        public Task delete(bool chouse, int hallId);
        public Task create(int rowCount, int colCount);
        public void createReserved(Hall hall);
        public Task<List<Reserved>> reserved(DateTime date);
    }
}
