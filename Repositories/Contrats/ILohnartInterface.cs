using Microsoft.AspNetCore.Mvc;
using Shared.Project.Entities;
using Shared.Project.Responses;
namespace Server.Repositories.Contracts;


public interface ILohnartInterface<T>
{
    Task<List<T>> GetAll(int mandant);

    Task<T> GetById(int mandant, string Lohnart);
    Task<List<T>> GetBySearch(int mandant, string Suchbegriff);


}
