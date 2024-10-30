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
    public class AdressenSuchbegriffRepository:IAdressensuchbegriffInterface<DtoAdressensuchbegriff>
    {
        private readonly AppDbContext _appDbContext;

        public AdressenSuchbegriffRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<DtoAdressensuchbegriff>> GetAll(int mandant)
        {
            return await _appDbContext.adressen
                 .FromSqlRaw($"Select mandant, adresse,suchbegriff from adressen where mandant = {mandant}")
                                .Select(p => new DtoAdressensuchbegriff
                                {
                                    Mandant = p.Mandant,
                                    Adresse = p.Adresse,
                                    Suchbegriff = p.Suchbegriff
                                }).ToListAsync();
        }
        public async Task<List<DtoAdressensuchbegriff>> GetbySuchbegriff(int mandant, string suchbegriff)
        {
            //Console.WriteLine($"Number of records found: {result.Count}");
            //.Where(p => p.Suchbegriff.Contains(suchbegriff) && p.Mandant.Equals(mandant))
                        return await _appDbContext.adressen
                .FromSqlRaw($"Select mandant, adresse,suchbegriff from adressen where mandant = {mandant} and suchbegriff like '%{suchbegriff}%'")
                                .Select(p => new DtoAdressensuchbegriff
                    {
                    Mandant = p.Mandant,
                    Adresse = p.Adresse,
                    Suchbegriff = p.Suchbegriff
                }).ToListAsync();
        }

    }

}
