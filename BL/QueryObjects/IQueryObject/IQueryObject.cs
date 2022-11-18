using BL.DTOs;

namespace BL.QueryObjects.IQueryObject
{
    public interface IQueryObject<TEntity, OEntity> where TEntity : class where OEntity : class
    {
        QueryResultDto<OEntity> ExecuteQuery(TEntity filter);
    }
}
