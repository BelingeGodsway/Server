using Microsoft.AspNetCore.Mvc;
using Shared.Project.Entities;
using Shared.Project.Responses;
namespace Server.Repositories.Contracts;


public interface Iadresseninterface<T>
{
    Task<List<T>> GetAll(int mandant);

    Task<T> GetById(int mandant, string adresse);
    Task<List<T>> GetBySearch(int mandant, string Suchbegriff);

    Task<GeneralResponse> Update(T item);

}
