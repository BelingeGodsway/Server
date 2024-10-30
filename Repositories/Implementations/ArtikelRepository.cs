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
    public class ArtikelRepository : IArtikelinterface<Artikel>
    {
        private readonly AppDbContext _appDbContext;

        public ArtikelRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int mandant, string artikelnummer)
        {
            var ad = await _appDbContext.Artikel.FindAsync(mandant, artikelnummer);
            if (ad is null)
                return NotFound();

            _appDbContext.Artikel.Remove(ad);
            await Commit();
            return Success();
        }


        public async Task<List<Artikel>> GetAll(int mandant)
        {
            return await _appDbContext.Artikel
                        .Where(p => p.Mandant.Equals(mandant)).ToListAsync();
        }

        public async Task<Artikel> GetById(int mandant, string Artikelnummer)
        {
            return await _appDbContext.Artikel
                    .FirstOrDefaultAsync(p => p.Mandant.Equals(mandant) && p.Artikelnummer.Equals(Artikelnummer));
        }

        public async Task<List<Artikel>> GetBySearch(int mandant, string suchbegriff)
        {
            return await _appDbContext.Artikel
                                  .Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
                                  .ToListAsync();
        }
        private static GeneralResponse NotFound()
        {
            return new GeneralResponse(false, "Address not found");
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
