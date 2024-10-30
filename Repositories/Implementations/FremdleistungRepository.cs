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
    public class FremdleistungRepository : IFremdleistungInterface<Fremdleistung>
    {
        private readonly AppDbContext _appDbContext;

        public FremdleistungRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int mandant, string nummer)
        {
            var ad = await _appDbContext.Fremdleistungens.FindAsync(mandant, nummer);
            if (ad is null)
                return NotFound();

            _appDbContext.Fremdleistungens.Remove(ad);
            await Commit();
            return Success();
        }


        public async Task<List<Fremdleistung>> GetAll(int mandant)
        {
            return await _appDbContext.Fremdleistungens
                        .Where(p => p.Mandant.Equals(mandant)).ToListAsync();
        }

        public async Task<Fremdleistung> GetById(int mandant, string nummer)
        {
            return await _appDbContext.Fremdleistungens
                    .FirstOrDefaultAsync(p => p.Mandant.Equals(mandant) && p.Nummer.Equals(nummer));
        }

        public async Task<List<Fremdleistung>> GetBySearch(int mandant, string suchbegriff)
        {
            return await _appDbContext.Fremdleistungens
                                  .Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
                                  .ToListAsync();
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
