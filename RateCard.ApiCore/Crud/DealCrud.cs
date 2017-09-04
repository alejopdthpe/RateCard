using RateCard.ApiCore.Dao;
using RateCard.ApiCore.Mappers;
using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace RateCard.ApiCore.Crud
{
    public class DealCrud : CrudFactory
    {
        private LocationCostMapper _Mapper_Location_Cost;
        private MiscellaneousMapper _Mapper_Misc;
        public DealCrud()
        {
            _Mapper_Location_Cost = new LocationCostMapper();
            _Mapper_Misc = new MiscellaneousMapper();
        }
 
        public override bool Create(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T RCreate<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {

            var operation = _Mapper_Misc.GetRetrieveStatement(entity);
            var instance = SqlDao.GetInstance();
            var list = instance.ExecuteQueryProcedure(operation);

            foreach (var dictionary in list)
            {
                entity = _Mapper_Misc.BuildObject(dictionary);
            }

            return entity as T;
        }

        public override List<T> RetrieveAll<T>()
        {
            var operation = _Mapper_Location_Cost.GetRetrieveAllStatement();
            var instance = SqlDao.GetInstance();
            var listResult = instance.ExecuteQueryProcedure(operation);
            List<BaseEntity> list = _Mapper_Location_Cost.BuildObjects(listResult);

            return list as List<T>;
        }

        public override T RUpdate<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override bool Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
