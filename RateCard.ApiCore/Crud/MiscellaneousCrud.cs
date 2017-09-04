using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RateCard.Entities;
using RateCard.ApiCore.Mappers;
using RateCard.ApiCore.Dao;

namespace RateCard.ApiCore.Crud
{
    public class MiscellaneousCrud : CrudFactory
    {
        private MiscellaneousMapper _Misc_Mapper;

        public MiscellaneousCrud()
        {

            _Misc_Mapper = new MiscellaneousMapper();
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
            var operation = _Misc_Mapper.GetRetrieveStatement(entity);
            var instance = SqlDao.GetInstance();
            var list = instance.ExecuteQueryProcedure(operation);

            foreach(var dictionary in list)
            {
                entity = _Misc_Mapper.BuildObject(dictionary);
            }

            return entity as T;

        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
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
