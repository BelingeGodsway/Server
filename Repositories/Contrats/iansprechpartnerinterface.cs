using Microsoft.AspNetCore.Mvc;
using Shared.Project.Entities;
using Shared.Project.Responses;
namespace Server.Repositories.Contracts;


public interface iansprechpartnerinterface<T>
{
    Task<List<T>> GetAll(int mandant);

    Task<T> GetById(int mandant, int pos);
    Task<List<T>> GetBySearch(int mandant, string Suchbegriff);

    Task<GeneralResponse> Update(T item);

    //Task<GeneralResponse> DeleteById(int mandant, string adresse);
    //Task<GeneralResponse> Insert(T item);
}
