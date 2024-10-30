using Shared.Project.Responses;
namespace Server.Repositories.Contracts;
    public interface Imitarbeiterdeviceinterface<T>
{
        Task<T> GetById(string Bezeichnung);
   

}

