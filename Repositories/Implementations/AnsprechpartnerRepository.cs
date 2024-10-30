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
    public class AnsprechpartnerRepository : iansprechpartnerinterface<Ansprechpartner>
    {
        private readonly AppDbContext _appDbContext;

        public AnsprechpartnerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GeneralResponse> DeleteById(int mandant, string adresse)
        {
            var ad = await _appDbContext.adressen.FindAsync(mandant, adresse);
            if (ad is null)
                return NotFound();

            _appDbContext.adressen.Remove(ad);
            await Commit();
            return Success();
        }


        public async Task<List<Ansprechpartner>> GetAll(int mandant)
        {
            return await _appDbContext.Ansprechpartner
                        .Where(p => p.Mandant.Equals(mandant)).ToListAsync();
        }

        public async Task<Ansprechpartner> GetById(int mandant, int pos)
        {
            return await _appDbContext.Ansprechpartner
                    .FirstOrDefaultAsync(p => p.Mandant.Equals(mandant) && p.Pos.Equals(pos));
        }

        public async Task<List<Ansprechpartner>> GetBySearch(int mandant, string suchbegriff)
        {
            return await _appDbContext.Ansprechpartner
                                  .Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
                                  .ToListAsync();
        }

        public async Task<GeneralResponse> Update(Ansprechpartner item)
        {
            var ad = await _appDbContext.Ansprechpartner.FindAsync(item.Mandant, item.Pos);
            if (ad == null)
                return NotFound();

            ad.Adresse = item.Adresse;
            ad.Suchbegriff = item.Suchbegriff;
            ad.Anrede = item.Anrede;
            ad.Titel = item.Titel;
            ad.Vorname = item.Vorname;
            ad.Nachname = item.Nachname;
            ad.Telefon1 = item.Telefon1;
            ad.Telefon2 = item.Telefon2;
            ad.Mail = item.Mail;
            ad.Mobil = item.Mobil;
            ad.Notiz = item.Notiz;
            ad.Funktion = item.Funktion;
            ad.Abteilung = item.Abteilung;

            await Commit();
            return Success();
        }



        private async Task Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }

        private async Task<bool> CheckAnsprechpartner(int mandant, int pos)
        {
            var item = await _appDbContext.Ansprechpartner
                 //.FirstOrDefaultAsync(x => x.Mandant == mandant && x.Pos.ToLower() == pos.ToLower());
                 .FirstOrDefaultAsync(x => x.Mandant == mandant && x.Pos == pos);

            return item == null;
        }

        private static GeneralResponse NotFound()
        {
            return new GeneralResponse(false, "Ansprechpartner not found");
        }

        private static GeneralResponse Success()
        {
            return new GeneralResponse(true, "Operation successful");
        }


    }
}
