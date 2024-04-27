﻿using Catalog.Data;
using Catalog.Entities;
using Catalog.Exeptions;
using Catalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories;

public class ScreeningRepository : IScreeningRepository
{
    private readonly CatalogContext _context;

    public ScreeningRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<Screening> GetById(Guid id)
    {
        return await _context.Screenings.FindAsync(id)
               ?? throw new EntityNotFoundException($"Screening with Id {id} not found.");
    }

    public async Task<IEnumerable<Screening>> GetAll()
    {
        return await _context.Screenings.ToListAsync();
    }

    public async Task Create(Screening screening)
    {
        _context.Screenings.Add(screening);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Screening screening)
    {
        _context.Screenings.Update(screening);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var screening = await _context.Screenings.FindAsync(id);
        if (screening != null)
        {
            _context.Screenings.Remove(screening);
            await _context.SaveChangesAsync();
        }
    }
}