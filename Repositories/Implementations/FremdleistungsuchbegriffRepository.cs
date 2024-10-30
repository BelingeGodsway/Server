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
    public class FremdleistungsuchbegriffRepository : IFremdleistungsuchbegriffInterface<DtoFremdleistungSuchbegriff>
    {
        private readonly AppDbContext _appDbContext;

        public FremdleistungsuchbegriffRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<DtoFremdleistungSuchbegriff>> GetAll(int mandant)
        {
            return await _appDbContext.Fremdleistungens
                 .FromSqlRaw($"Select mandant, nummer,suchbegriff,baum1,baum2,einheit from fremdleistung where mandant = {mandant}")
                                .Select(p => new DtoFremdleistungSuchbegriff
                                {
                                    Mandant = p.Mandant,
                                    Nummer = p.Nummer,
                                    Baum1 = p.Baum1,
                                    Baum2 = p.Baum2,
                                    Einheit = p.Einheit,
                                    Suchbegriff = p.Suchbegriff
                                }).ToListAsync();
        }
        public async Task<List<DtoFremdleistungSuchbegriff>> GetbySuchbegriff(int mandant, string suchbegriff)
        {
            //Console.WriteLine($"Number of records found: {result.Count}");
            //.Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
            return await _appDbContext.Fremdleistungens
    .FromSqlRaw($"Select mandant,nummer,suchbegriff,baum1,baum2,einheit from fremdleistung where mandant = {mandant} and suchbegriff like '%{suchbegriff}%'")
                    .Select(p => new DtoFremdleistungSuchbegriff
                    {
                        Mandant = p.Mandant,
                        Nummer = p.Nummer,
                        Baum1 = p.Baum1,
                        Baum2 = p.Baum2,
                        Einheit = p.Einheit,
                        Suchbegriff = p.Suchbegriff
                    }).ToListAsync();
        }

    }

}
