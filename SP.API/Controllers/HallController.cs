using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP.Repository.Entities;
using SP.Repository.Interfaces;
using SP.Repository.Models;

namespace Prøve.API.Controllers
{
    [Route("api/halls")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly HallRepository _hallRepo;

        public HallController(HallRepository hallRepository)
        {
            _hallRepo = hallRepository;
        }

        // Get seats in a hall by hallId
        [HttpGet("{hallId}/Seats")]
        public async Task<ActionResult<List<Seat>>> GetSeatsInHall(int hallId)
        {
            try
            {
                var seats = await _hallRepo.readSeatsInHall(hallId);
                return Ok(seats);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get reserved seats in a hall by hallId
        [HttpGet("{hallId}/reservedSeats")]
        public async Task<ActionResult<List<Reserved>>> GetReservedSeatsInHall(int hallId)
        {
            try
            {
                var reservedSeats = await _hallRepo.readReservedSeats(hallId);
                return Ok(reservedSeats);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete seats based on choice (even or odd)
        [HttpDelete("deleteSeats")]
        public async Task<IActionResult> DeleteSeats(bool choice)
        {
            try
            {
                await _hallRepo.delete(choice);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete halls based on choice (even or odd) and hallId
        [HttpDelete("deleteHalls")]
        public async Task<IActionResult> DeleteHalls(bool choice, int hallId)
        {
            try
            {
                await _hallRepo.delete(choice, hallId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Create a new hall with specified row and column counts
        [HttpPost("createHall")]
        public async Task<ActionResult<Hall>> CreateHall(int rowCount, int colCount)
        {
            try
            {
                var hall = await _hallRepo.create(rowCount, colCount);
                return CreatedAtAction(nameof(GetHall), new { hallId = hall.Id }, hall);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Create reserved seats in a hall by hallId (Not implemented yet)
        [HttpPost("CreateReserved")]
        public async Task<IActionResult> CreateReserved(int hallId)
        {
            throw new NotImplementedException();
        }

        // Get hall by hallId
        [HttpGet("{hallId}")]
        public async Task<ActionResult<Hall>> GetHall(int hallId)
        {
            try
            {
                var hall = await _hallRepo.GetHallById(hallId);

                if (hall == null)
                {
                    return NotFound();
                }

                return Ok(hall);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}