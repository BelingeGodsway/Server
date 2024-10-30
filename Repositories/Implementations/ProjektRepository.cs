using Microsoft.EntityFrameworkCore;
using Shared.Project.Responses;
using Server.Data;
using Server.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Project.Entities;

namespace Server.Repositories.Implementations
{
    public class ProjektRepository : Iprojektinterface<Projekt>
    {
        private readonly AppDbContext _appDbContext;

        public ProjektRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        

        public async Task<List<Projekt>> GetAll(int mandant)
        {
            return await _appDbContext.Projekt
                    .Where(p => p.Mandant.Equals(mandant)).ToListAsync();
        }

        public async Task<Projekt> GetById(int mandant, string projektnr)
        {
            return await _appDbContext.Projekt
                  .FirstOrDefaultAsync(x => x.Mandant == mandant && x.Projektnr.ToLower() == projektnr.ToLower());
        }

        public async Task<List<Projekt>> GetBySearch(int mandant, string suchbegriff)
        {
            return await _appDbContext.Projekt
                                  .Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
                                  .ToListAsync();
        }

        public async Task<GeneralResponse> Update(Projekt item)
        {
            var ad= await _appDbContext.Projekt.FindAsync(item.Mandant, item.Projektnr);
            if (ad == null)
                return NotFound();

            ad.Suchbegriff = item.Suchbegriff;
            ad.Projektbezeichnung1 = item.Projektbezeichnung1;
            ad.Projektbezeichnung2 = item.Projektbezeichnung2;
            ad.Projektname1 = item.Projektname1;
            ad.Projektname2 = item.Projektname2;
            ad.Projektstrasse = item.Projektstrasse;
            ad.Projektintraland = item.Projektintraland;
            ad.Projektplz= item.Projektplz;
            ad.Projektort = item.Projektort;
            ad.Niederlassung = item.Niederlassung;
            ad.Verantwortlicher = item.Verantwortlicher;
            ad.Vertrieb = item.Vertrieb;
            ad.Adresse = item.Adresse;
            ad.Ansprechpartner1 = item.Ansprechpartner1;
            ad.Ansprechpartner2 = item.Ansprechpartner2;
            ad.Telefon = item.Telefon;
            ad.Mail = item.Mail;
            ad.Status = item.Status;
            ad.Mobil = item.Mobil;
            ad.Projektgruppe1 = item.Projektgruppe1;
            ad.Beginn = item.Beginn;
            ad.Ende = item.Ende;
            ad.Notiz = item.Notiz;

            try
            {
                await Commit(); // Call to SaveChangesAsync
            }
            catch (DbUpdateException ex)
            {

                return new GeneralResponse(false, "An error occurred while updating the address.");
            }

            return Success();
        }

        private async Task Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }
        private async Task<bool> CheckProjekt(int mandant, string projektnr)
        {
            var item = await _appDbContext.Projekt
                .FirstOrDefaultAsync(x => x.Mandant == mandant && x.Projektnr.ToLower() == projektnr.ToLower());

            return item == null;
        }

        private static GeneralResponse NotFound()
        {
            return new GeneralResponse(false, "Projekt not found");
        }

        private static GeneralResponse Success()
        {
            return new GeneralResponse(true, "Operation successful");
        }
              
    }
}
