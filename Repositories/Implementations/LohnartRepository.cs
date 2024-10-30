using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Contracts;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;


namespace Server.Repositories.Implementations
{
    public class LohnartRepository : ILohnartInterface<Lohnarten>
    {
        private readonly AppDbContext _appDbContext;

        public LohnartRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        
        public async Task<List<Lohnarten>> GetAll(int mandant)
        {
            return await _appDbContext.Lohnartens
                       .FromSqlRaw("SELECT * FROM Lohnart WHERE Mandant = {mandant}")
                     .Select(p => new Lohnarten
                     {
                         Mandant = p.Mandant,
                         Lohnart = p.Lohnart,
                         Einheit = p.Einheit,
                         Suchbegriff = p.Suchbegriff,
                         Zuschlag1 = p.Zuschlag1,
                         Zuschlag2 = p.Zuschlag2,
                         EKPreis = p.EKPreis,
                         Notiz = p.Notiz

                     }).ToListAsync();
        }

        public async Task<Lohnarten> GetById(int mandant, string lohnart)
        {
            return await _appDbContext.Lohnartens
                   .FromSqlRaw("SELECT * FROM Lohnart WHERE lohnart = '{lohnart}' AND Mandant = {mandant}")
                     .Select(p => new Lohnarten
                     {
                         Mandant = p.Mandant,
                         Lohnart = p.Lohnart,
                         Einheit = p.Einheit,
                         Suchbegriff = p.Suchbegriff,
                         Zuschlag1 = p.Zuschlag1,
                         Zuschlag2 = p.Zuschlag2,
                         EKPreis = p.EKPreis,
                         Notiz = p.Notiz

                     }).FirstOrDefaultAsync();
        }

        public async Task<List<Lohnarten>> GetBySearch(int mandant, string suchbegriff)
        {
            return await _appDbContext.Lohnartens
                                   .FromSqlRaw("SELECT * FROM Lohnart WHERE Suchbegriff LIKE '%{suchbegriff}%' AND Mandant = {mandant}")
                                 .Select(p => new Lohnarten
                                 {
                                     Mandant = p.Mandant,
                                     Lohnart = p.Lohnart,
                                     Einheit = p.Einheit,
                                     Suchbegriff = p.Suchbegriff,
                                     Zuschlag1 = p.Zuschlag1,
                                     Zuschlag2 = p.Zuschlag2,
                                     EKPreis = p.EKPreis,
                                     Notiz = p.Notiz

                                 }).ToListAsync();
                                 
        }
        private static GeneralResponse NotFound()
        {
            return new GeneralResponse(false, "fr not found");
        }

        private static GeneralResponse Success()
        {
            return new GeneralResponse(true, "Operation successful");
        }

        private async Task Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }

    }
}
