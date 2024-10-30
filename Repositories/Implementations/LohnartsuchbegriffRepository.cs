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
    public class LohnartsuchbegriffRepository : ILohnartsuchbegriffInterface<DtoLohnartSuchbegriff>
    {
        private readonly AppDbContext _appDbContext;

        public LohnartsuchbegriffRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<DtoLohnartSuchbegriff>> GetAll(int mandant)
        {
            return await _appDbContext.Lohnartens
                 .FromSqlRaw($"Select mandant, lohnart,suchbegriff,einheit from lohnart where mandant = {mandant}")
                                .Select(p => new DtoLohnartSuchbegriff
                                {
                                    Mandant = p.Mandant,
                                    Lohnart = p.Lohnart,
                                    Einheit = p.Einheit,
                                    Suchbegriff = p.Suchbegriff
                                }).ToListAsync();
        }
        public async Task<List<DtoLohnartSuchbegriff>> GetbySuchbegriff(int mandant, string suchbegriff)
        {
            //Console.WriteLine($"Number of records found: {result.Count}");
            //.Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
            return await _appDbContext.Lohnartens
    .FromSqlRaw($"Select  mandant, lohnart,suchbegriff,einheit from lohnart where mandant = {mandant} and suchbegriff like '%{suchbegriff}%'")
                    .Select(p => new DtoLohnartSuchbegriff
                    {
                        Mandant = p.Mandant,
                        Lohnart = p.Lohnart,
                        Einheit = p.Einheit,
                        Suchbegriff = p.Suchbegriff
                    }).ToListAsync();
        }

    }

}
