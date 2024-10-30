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
    public class ProjektSuchbegriffRepository : iprojektsuchbegriffinterface<DtoProjektsuchbegriff>
    {
        private readonly AppDbContext _appDbContext;

        public ProjektSuchbegriffRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<DtoProjektsuchbegriff>> GetAll(int mandant)
        {
            return await _appDbContext.Projekt
                .FromSqlRaw($"SELECT mandant, projektnr, suchbegriff FROM projekt WHERE mandant = {mandant}")
                .Select(p => new DtoProjektsuchbegriff
                {
                    Mandant = p.Mandant,
                    Projektnr = p.Projektnr,
                    Suchbegriff = p.Suchbegriff,
                }).ToListAsync();
        }

        public async Task<List<DtoProjektsuchbegriff>> GetbySuchbegriff(int mandant, string suchbegriff)
        {
            return await _appDbContext.Projekt
                .FromSqlRaw($"SELECT mandant, projektnr, suchbegriff FROM projekt WHERE mandant = {mandant} AND suchbegriff LIKE '%{suchbegriff}%'")
                .Select(p => new DtoProjektsuchbegriff
                {
                    Mandant = p.Mandant,
                    Projektnr = p.Projektnr,
                    Suchbegriff = p.Suchbegriff,
                }).ToListAsync();
        }


    }

}
