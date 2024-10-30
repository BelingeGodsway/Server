using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Contracts;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;



namespace Server.Repositories.Implementations
{
    public class AdressenRepository : Iadresseninterface<Adressen>
    {
        private readonly AppDbContext _appDbContext;
    


        public AdressenRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
         

        }
        public async Task<List<Adressen>> GetAll(int mandant)
        {
            return await _appDbContext.adressen
                        .Where(p=> p.Mandant.Equals(mandant)).ToListAsync();
        }

        public async Task<Adressen> GetById(int mandant, string adresse)
        {
            return await _appDbContext.adressen
                    .FirstOrDefaultAsync(p => p.Mandant.Equals(mandant) && p.Adresse.Equals(adresse));      
        }

        public async Task<List<Adressen>> GetBySearch(int mandant, string suchbegriff)
        {
            return await _appDbContext.adressen
                                  .Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
                                  .ToListAsync();
        }


        public async Task<GeneralResponse> Update(Adressen item)
        {
            var ad = await _appDbContext.adressen.FindAsync(item.Mandant, item.Adresse);
            if (ad == null)
                return NotFound();


            ad.Suchbegriff = item.Suchbegriff;
            ad.Anrede = item.Anrede;
            ad.Titel = item.Titel;
            ad.Vorname = item.Vorname;
            ad.Nachname = item.Nachname;
            ad.Firma = item.Firma;
            ad.Namenszusatz = item.Namenszusatz;
            ad.Strasse = item.Strasse;
            ad.PLZ = item.PLZ;
            ad.Ort = item.Ort;
            ad.Intraland = item.Intraland;
            ad.Telefon1 = item.Telefon1;
            ad.Telefon2 = item.Telefon2;
            ad.Mail = item.Mail;
            ad.Mobil = item.Mobil;
            ad.Internet = item.Internet;
            ad.Prioritaet = item.Prioritaet;
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

        private async Task<bool> CheckAddress(int mandant, string adresse)
        {
            var item = await _appDbContext.adressen
                .FirstOrDefaultAsync(x => x.Mandant == mandant && x.Adresse.ToLower() == adresse.ToLower());

            return item == null;
        }

        private static GeneralResponse NotFound()
        {
            return new GeneralResponse(false, "Address not found");
        }

        private static GeneralResponse Success()
        {
            return new GeneralResponse(true, "Operation successful");
        }

        
    }
}
