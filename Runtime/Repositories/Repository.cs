using System.Collections.Generic;
using System.Threading.Tasks;

namespace JasonStorey 
{
    public interface Repository<T, in TId>
    {
        Task Create(T item);
        
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(TId id);
        
        Task Update(T item);

        Task<bool> Delete();
    }
}