using Microsoft.AspNetCore.Mvc;
using Shared.Project.Entities;
using Shared.Project.Responses;
namespace Server.Repositories.Contracts;


public interface ILeistungInterface<T>
{
    Task<List<T>> GetAll(int mandant);

    Task<T> GetById(int mandant, string Leistung);
    Task<List<T>> GetBySearch(int mandant, string Suchbegriff);


}
