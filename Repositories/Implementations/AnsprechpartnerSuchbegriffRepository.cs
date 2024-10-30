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
    public class AnsprechpartnerSuchbegriffRepository :iAnsprechpartnersuchbegriffinterface<DtoAnsprechpartnersuchbegriff>
    {
        private readonly AppDbContext _appDbContext;

        public AnsprechpartnerSuchbegriffRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<DtoAnsprechpartnersuchbegriff>> GetAll(int mandant)
        {
            return await _appDbContext.Ansprechpartner
                 .FromSqlRaw($"Select mandant, pos,suchbegriff from ansprechpartner where mandant = {mandant}")
                                .Select(p => new DtoAnsprechpartnersuchbegriff
                                {
                                    Mandant = p.Mandant,
                                    Pos = p.Pos,
                                    Suchbegriff = p.Suchbegriff,
                                }).ToListAsync();
        }
        public async Task<List<DtoAnsprechpartnersuchbegriff>> GetbySuchbegriff(int mandant, string suchbegriff)
        {
            return await _appDbContext.Ansprechpartner
                .FromSqlRaw("SELECT mandant, pos, suchbegriff FROM ansprechpartner WHERE mandant = {0} AND suchbegriff LIKE {1}", mandant, $"%{suchbegriff}%")
                .Select(p => new DtoAnsprechpartnersuchbegriff
                {
                    Mandant = p.Mandant,
                    Pos = p.Pos,
                    Suchbegriff = p.Suchbegriff
                })
                .ToListAsync();
        }

    }

}
