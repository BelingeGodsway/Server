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
    public class LeistungsuchbegriffRepository : ILeistungsuchbegriffInterface<DtoLeistunguchbegriff>
    {
        private readonly AppDbContext _appDbContext;

        public LeistungsuchbegriffRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<DtoLeistunguchbegriff>> GetAll(int mandant)
        {
            return await _appDbContext.Leistungens
                 .FromSqlRaw($"Select leistung, Leistung,suchbegriff,baum1,baum2,einheit from leistung where mandant = {mandant}")
                                .Select(p => new DtoLeistunguchbegriff
                                {
                                    Mandant = p.Mandant,
                                    Leistung = p.Leistung,
                                    Baum1 = p.Baum1,
                                    Baum2 = p.Baum2,
                                    Einheit = p.Einheit,
                                    Suchbegriff = p.Suchbegriff
                                }).ToListAsync();
        }
        public async Task<List<DtoLeistunguchbegriff>> GetbySuchbegriff(int mandant, string suchbegriff)
        {
            //Console.WriteLine($"Number of records found: {result.Count}");
            //.Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
            return await _appDbContext.Leistungens
    .FromSqlRaw($"Select mandant,leistung,suchbegriff,baum1,baum2,einheit from leistung where mandant = {mandant} and suchbegriff like '%{suchbegriff}%'")
                    .Select(p => new DtoLeistunguchbegriff
                    {
                        Mandant = p.Mandant,
                        Leistung = p.Leistung,
                        Baum1 = p.Baum1,
                        Baum2 = p.Baum2,
                        Einheit = p.Einheit,
                        Suchbegriff = p.Suchbegriff
                    }).ToListAsync();
        }

    }

}
