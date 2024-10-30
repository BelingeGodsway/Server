using Microsoft.AspNetCore.Mvc;
using Shared.Project.Entities;
using Shared.Project.Responses;
namespace Server.Repositories.Contracts;


public interface IFremdleistungInterface<T>
{
    Task<List<T>> GetAll(int mandant);

    Task<T> GetById(int mandant, string Nummer);
    Task<List<T>> GetBySearch(int mandant, string Suchbegriff);


}
