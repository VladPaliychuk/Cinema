using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Exeptions;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly CatalogContext _context;

    public SeatRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<Seat> GetById(Guid id)
    {
        return await _context.Seats.FindAsync(id)
               ?? throw new EntityNotFoundException($"Seat with Id {id} not found.");
    }

    public async Task<IEnumerable<Seat>> GetAll()
    {
        return await _context.Seats.ToListAsync();
    }

    public async Task<Seat> GetSeatWithScreeningAsync(Guid seatId)
    {
        return await _context.Seats
            .Include(seat => seat.Screening)
            .ThenInclude(screening => screening.Product)
            .FirstOrDefaultAsync(seat => seat.Id == seatId);
    }
    
    public async Task Create(Seat seat)
    {
        _context.Seats.Add(seat);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Seat seat)
    {
        _context.Seats.Update(seat);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var seat = await _context.Seats.FindAsync(id);
        if (seat != null)
        {
            _context.Seats.Remove(seat);
            await _context.SaveChangesAsync();
        }
    }
}