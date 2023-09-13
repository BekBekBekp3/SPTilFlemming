using SP.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Repository.Interfaces
{
    public interface IHallRepository
    {
        public Task<List<Seat>> readSeatsInHall(int HallId);
        public Task<List<Reserved>> readReservedSeats(int hallId);
        public Task delete(bool choice);
        public Task delete(bool choice, int hallId);
        public Task<Hall> create(int rowCount, int colCount);
        public void createReserved(Hall hall);
        public Task<List<Reserved>> reserved(DateTime dateNow);
    }
}
