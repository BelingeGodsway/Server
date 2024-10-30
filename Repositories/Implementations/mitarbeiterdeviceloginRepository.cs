
using Shared.Project.DTOs;
using Shared.Project.Entities;
using Shared.Project.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Server.Helpers;
using Server.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Repositories.Implementations
{
    public class mitarbeiterdeviceloginRepository : Imitarbeiterdevicelogininterface
    {
        private readonly IOptions<JwtSection> _config;
        private readonly AppDbContext _appDbContext;

        public mitarbeiterdeviceloginRepository(IOptions<JwtSection> config, AppDbContext appDbContext)
        {
            _config = config;
            _appDbContext = appDbContext;
        }

        // Get the next Mandant value alternating between 0 and 1
        private int currentMandantValue = -1; // Initialize with a value outside your expected range

        private async Task<int> GetNextMandantValue()
        {
            if (currentMandantValue == -1) // First call, determine based on existing data
            {
                var lastMitarbeiter = await _appDbContext.mitarbeiterdevice
                                                          .OrderByDescending(m => m.Bezeichnung)
                                                          .FirstOrDefaultAsync();

                currentMandantValue = lastMitarbeiter?.Mandant == 0 ? 1 : 0;
            }
            else // Alternate between 0 and 1
            {
                currentMandantValue = currentMandantValue == 0 ? 1 : 0;
            }

            return currentMandantValue;
        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if (user == null) return new LoginResponse(false, "Model is empty");

            var mitarbeiterdevice= await FindUserByName(user.Mandant,user.Bezeichnung);
            if (mitarbeiterdevice == null) return new LoginResponse(false, "User not found");
            if (user.Passwort != mitarbeiterdevice.Passwort)
                return new LoginResponse(false, "Name/Password/Mandant not valid");


            string jwtToken = GenerateJwtToken(mitarbeiterdevice);

            return new LoginResponse(true, "Login successfully", jwtToken);
        }

        private string GenerateJwtToken(Mitarbeiterdevice user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.Key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Mandant.ToString()),
                new Claim(ClaimTypes.Name, user.Bezeichnung!)
            };
            var token = new JwtSecurityToken(
                issuer: _config.Value.Issuer,
                audience: _config.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<Mitarbeiterdevice> FindUserByName(int mandant, string bezeichnung) =>
            await _appDbContext.mitarbeiterdevice.FirstOrDefaultAsync(u => u.Bezeichnung!.ToLower() == bezeichnung.ToLower()&& u.Mandant.Equals(mandant));

        private async Task<T> AddToDatabase<T>(T model) where T : class
        {
            var result = _appDbContext.Add(model);
            await _appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }

        public async Task<bool> UpdateProfile(UserProfile profile)
        {
            var user = await _appDbContext.mitarbeiterdevice.FirstOrDefaultAsync(u => u.Mandant == int.Parse(profile.Mandant));
            user.Bezeichnung = profile.Bezeichnung;
            //user.Image = profile.Image;
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
