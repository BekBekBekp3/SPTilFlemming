using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SP.Repository.Database;
using SP.Repository.Interfaces;
using SP.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Repository.Entities
{
    public class HallRepository : IHallRepository
    {
        DatabaseContext DatabaseContext { get; set; }
        public HallRepository(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<List<Seat>> readSeatsInHall(int HallId)
        {
            if (HallId <= 0)
            {
                throw new ArgumentException("HallId must be a positive integer.");
            }

            // linq
            var seatsInHall = await DatabaseContext.Seat
                .Where(seat => seat.Hall.Id == HallId)
                .ToListAsync();

            return seatsInHall;
        }

        public async Task<List<Reserved>> readReservedSeats(int hallId)
        {
            if (hallId <= 0)
            {
                throw new ArgumentException("HallId must be a positive integer.");
            }

            var reservedSeatsInHall = await DatabaseContext.Reserved
                .Where(reserved => reserved.Hallobj.Id == hallId && reserved.booked)
                .ToListAsync();

            return reservedSeatsInHall;
        }

        public async Task delete(bool choice)
        {
            var seatsToDelete = choice
                ? await DatabaseContext.Seat.Where(seat => seat.Id % 2 == 0).ToListAsync() 
                : await DatabaseContext.Seat.Where(seat => seat.Id % 2 != 0).ToListAsync();

            DatabaseContext.Seat.RemoveRange(seatsToDelete);

            await DatabaseContext.SaveChangesAsync();
        }

        public async Task delete(bool choice, int hallId)
        {
            if (hallId <= 0)
            {
                throw new ArgumentException("HallId must be a positive integer.");
            }

            var hallsToDelete = choice
                ? await DatabaseContext.Hall.Where(hall => hall.Id % 2 == 0 && hall.Id != hallId).ToListAsync()
                : await DatabaseContext.Hall.Where(hall => hall.Id % 2 != 0 && hall.Id != hallId).ToListAsync();

            foreach (var hall in hallsToDelete)
            {
                var seatsToDelete = await DatabaseContext.Seat.Where(seat => seat.Hall.Id == hall.Id).ToListAsync();
                DatabaseContext.Seat.RemoveRange(seatsToDelete);
                DatabaseContext.Hall.Remove(hall);
            }

            await DatabaseContext.SaveChangesAsync();
        }

        public async Task<Hall> create(int rowCount, int colCount)
        {
            if (rowCount <= 0 || colCount <= 0)
            {
                throw new ArgumentException("Row and column counts must be positive integers.");
            }

            var hall = new Hall
            {
                Name = "HallTest"
            };

            CreateSeatsForHall(hall, rowCount, colCount);

            DatabaseContext.Hall.Add(hall);
            await DatabaseContext.SaveChangesAsync();

            return hall;
        }

        public void CreateSeatsForHall(Hall hall, int rowCount, int colCount)
        {
            for (int row = 1; row <= rowCount; row++)
            {
                for (int col = 1; col <= colCount; col++)
                {
                    var seat = new Seat
                    {
                        SeatRow = row.ToString(),
                        SeatCol = col.ToString(),
                        Hall = hall
                    };

                    DatabaseContext.Seat.Add(seat);
                }
            }
        }

        public async Task<List<Reserved>> reserved(DateTime date)
        {
            //linq
            var reservationsForDate = await DatabaseContext.Reserved
                .Where(reserved => reserved.Date.Date == date.Date && reserved.booked)
                .ToListAsync();

            return reservationsForDate;
        }

        public async Task<Hall> GetHallById(int hallId)
        {
            var test = await DatabaseContext.Hall.FirstOrDefaultAsync(hall => hall.Id == hallId);
            return test;
        }

        public void createReserved(Hall hall)
        {
            throw new NotImplementedException();
        }
    }
}