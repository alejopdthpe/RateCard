﻿using RateCard.ApiCore.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RateCard.Entities;
using RateCard.ApiCore.Dao;

namespace RateCard.ApiCore.Crud
{
    public class SLACrud : CrudFactory
    {
        private SLAMapper _Mapper;
        
        public SLACrud()
        {
            _Mapper = new SLAMapper();
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
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var operation = _Mapper.GetRetrieveAllStatement();
            var instance = SqlDao.GetInstance();
            var listResult = instance.ExecuteQueryProcedure(operation);
            List<BaseEntity> list = _Mapper.BuildObjects(listResult);

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
