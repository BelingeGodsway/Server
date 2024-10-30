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
    public class LeistungRepository : ILeistungInterface<Leistungen>
    {
        private readonly AppDbContext _appDbContext;

        public LeistungRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

      
        public async Task<List<Leistungen>> GetAll(int mandant)
        {
            return await _appDbContext.Leistungens
                       .FromSqlRaw($"Select mandant,leistung,artikeltext,baum1,baum2,einheit,suchbegriff,zuschlag1,zuschlag2,notiz,vk1,Kalkekpreis from leistung where mandant = {mandant}")
                                .Select(p => new Leistungen
                                {
                                    Mandant = p.Mandant,
                                    Leistung = p.Leistung,
                                    Artikeltext = p.Artikeltext,
                                    Baum1 = p.Baum1,
                                    Baum2 = p.Baum2,
                                    Einheit = p.Einheit,
                                    Zuschlag1 = p.Zuschlag1,
                                    Zuschlag2 = p.Zuschlag2,
                                    Kalkekpreis = p.Kalkekpreis,
                                    Notiz = p.Notiz,
                                    VK1 = p.VK1,

                                    Suchbegriff = p.Suchbegriff
                                }).ToListAsync();
        }

        public async Task<Leistungen> GetById(int mandant, string Leistungnr)
        {
            return await _appDbContext.Leistungens
                      .FromSqlRaw($"Select mandant,leistung,artikeltext,baum1,baum2,einheit,suchbegriff,zuschlag1,zuschlag2,notiz,vk1,Kalkekpreis from leistung where leistung='{Leistungnr}' and mandant = {mandant}")
                                .Select(p => new Leistungen
                                {
                                    Mandant = p.Mandant,
                                    Leistung = p.Leistung,
                                    Suchbegriff = p.Suchbegriff,
                                    Artikeltext = p.Artikeltext,
                                    Baum1 = p.Baum1,
                                    Baum2 = p.Baum2,
                                    Einheit = p.Einheit,
                                    Zuschlag1 = p.Zuschlag1,
                                    Zuschlag2 = p.Zuschlag2,
                                    Kalkekpreis = p.Kalkekpreis,
                                    Notiz = p.Notiz,
                                    VK1 = p.VK1
                                }).FirstOrDefaultAsync();
        }

        public async Task<List<Leistungen>> GetBySearch(int mandant, string suchbegriff)
        {
            return await _appDbContext.Leistungens
                                  .FromSqlRaw($"Select mandant,leistung,artikeltext,baum1,baum2,einheit,suchbegriff,zuschlag1,zuschlag2,notiz,vk1,Kalkekpreis from leistung where suchbegriff like'%{suchbegriff}%' and mandant = {mandant}")
                                .Select(p => new Leistungen
                                {
                                    Mandant = p.Mandant,
                                    Leistung = p.Leistung,
                                    Artikeltext = p.Artikeltext,
                                    Baum1 = p.Baum1,
                                    Baum2 = p.Baum2,
                                    Einheit = p.Einheit,
                                    Zuschlag1 = p.Zuschlag1,
                                    Zuschlag2 = p.Zuschlag2,
                                    Kalkekpreis = p.Kalkekpreis,
                                    Notiz = p.Notiz,
                                    VK1 = p.VK1,

                                    Suchbegriff = p.Suchbegriff
                                }).ToListAsync();
        }
        private static GeneralResponse NotFound()
        {
            return new GeneralResponse(false, "Leistung not found");
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
