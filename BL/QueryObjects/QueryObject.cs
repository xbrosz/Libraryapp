using BL.DTOs.Reservation;
using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.QueryObjects
{
    public interface QueryObject<TEntity, OEntity> where TEntity : class where OEntity : class
    {
        QueryResultDto<OEntity> ExecuteQuery(TEntity filter);
    }
}
