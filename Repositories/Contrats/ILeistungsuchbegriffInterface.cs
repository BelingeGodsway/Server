using Microsoft.AspNetCore.Mvc;
using Shared.Project.Entities;
using Shared.Project.Responses;
namespace Server.Repositories.Contracts;


public interface ILeistungsuchbegriffInterface<T>
{
    Task<List<T>> GetAll(int Mandant);

    Task<List<T>> GetbySuchbegriff(int mandant, string suchbegriff);
}
