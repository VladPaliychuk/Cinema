using Catalog.Data;
using Catalog.Entities;
using Catalog.Exeptions;
using Catalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories;

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