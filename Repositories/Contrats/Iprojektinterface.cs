using Shared.Project.Responses;
namespace Server.Repositories.Contracts;

public interface Iprojektinterface<T>
    {
    Task<List<T>> GetAll(int mandant);
    Task<List<T>> GetBySearch(int mandant, string Suchbegriff);
    Task<T> GetById(int mandant, string projektnr);
    Task<GeneralResponse> Update(T item);

   

}
