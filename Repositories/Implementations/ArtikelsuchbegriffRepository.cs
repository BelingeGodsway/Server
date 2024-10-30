using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Contracts;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories.Contrats;
namespace Server.Repositories.Implementations
{
    public class ArtikelsuchbegriffRepository : IArtikelsuchbegriffinterface<DtoArtikelsuchbegriff>
    {
        private readonly AppDbContext _appDbContext;

        public ArtikelsuchbegriffRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<DtoArtikelsuchbegriff>> GetAll(int mandant)
        {
            return await _appDbContext.Artikel
                 .FromSqlRaw($"Select mandant, artikelnummer,suchbegriff,baum1,baum2,einheit from artikel where mandant = {mandant}")
                                .Select(p => new DtoArtikelsuchbegriff
                                {
                                    Mandant = p.Mandant,
                                    Artikelnummer = p.Artikelnummer,
                                    Baum1 = p.Baum1,
                                    Baum2 = p.Baum2,
                                    Einheit = p.Einheit,
                                    Suchbegriff = p.Suchbegriff
                                }).ToListAsync();
        }
        public async Task<List<DtoArtikelsuchbegriff>> GetbySuchbegriff(int mandant, string suchbegriff)
        {
            //Console.WriteLine($"Number of records found: {result.Count}");
            //.Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
            return await _appDbContext.Artikel
    .FromSqlRaw($"Select mandant,artikelnummer,suchbegriff,baum1,baum2,einheit from artikel where mandant = {mandant} and suchbegriff like '%{suchbegriff}%'")
                    .Select(p => new DtoArtikelsuchbegriff
                    {
                        Mandant = p.Mandant,
                        Artikelnummer = p.Artikelnummer,
                        Baum1 = p.Baum1,
                        Baum2 = p.Baum2,
                        Einheit = p.Einheit,
                        Suchbegriff = p.Suchbegriff
                    }).ToListAsync();
        }

    }

}
