using Microsoft.AspNetCore.Mvc;
using Shared.Project.Entities;
using Shared.Project.Responses;
namespace Server.Repositories.Contracts;


public interface IArtikelinterface<T>
{
    Task<List<T>> GetAll(int mandant);

    Task<T> GetById(int mandant, string Artikelnummer);
    Task<List<T>> GetBySearch(int mandant, string Suchbegriff);
  

}
