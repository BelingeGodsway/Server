using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repositories.Contracts;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Server.Repositories.Implementations
{
    public class MitarbeiterdeviceRepository:Imitarbeiterdeviceinterface<Mitarbeiterdevice>

    {
        private readonly AppDbContext _appDbContext;

        public MitarbeiterdeviceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Mitarbeiterdevice> GetById(string Bezeichnung)
        {
            return await _appDbContext.mitarbeiterdevice
                    .FirstOrDefaultAsync(p => p.Bezeichnung.Equals(Bezeichnung));
        }

      
    }
}
