using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.GenericService
{
    public interface IGenericService<TEntity, FEntity, UEntity, IEntity>
        where TEntity : class
        where FEntity : class
        where UEntity : class
        where IEntity : class
    {
        FEntity Find(int id);

        Task DeleteAsync(int id);

        Task UpdateAsync(UEntity dtoToUpdate);

        Task InsertAsync(IEntity dtoToInsert);
    }
}
