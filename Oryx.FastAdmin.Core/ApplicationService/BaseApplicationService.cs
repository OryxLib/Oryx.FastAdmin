using Oryx.FastAdmin.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Oryx.FastAdmin.Core.ApplicationService
{
    public class BaseApplicationService<T>
        where T : BaseModel, new()
    {
        SqlSugarClient dbClient;
        //ModelMapper modelMapper;
        public BaseApplicationService(SqlSugarClient _dbClient)
        {
            dbClient = _dbClient;
            //modelMapper = new ModelMapper(dbClient);
        }

        public List<T> Query(Expression<Func<T, bool>> expression)
        {
            return dbClient.Queryable<T>().Where(expression).ToList();
        }

        public List<T> Query(Expression<Func<T, bool>> expression, int index, int size)
        {
            return dbClient.Queryable<T>().Where(expression).ToPageList(index, size);
        }
    }
}
