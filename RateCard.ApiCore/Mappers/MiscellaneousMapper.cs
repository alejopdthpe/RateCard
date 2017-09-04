using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RateCard.Entities;
using RateCard.ApiCore.Dao;

namespace RateCard.ApiCore.Mappers
{
    public class MiscellaneousMapper : EntityMapper
    {
        private const string DbColDetail = "DETAIL";
        private const string DbColValueAmnt = "VALUE_AMNT";       

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Misc = new Miscellaneous()
            {
                Detail = GetStringValue(row, DbColDetail),
                ValueAmount = GetDoubleValue(row,DbColValueAmnt)
            };
            return Misc;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            List<BaseEntity> list = new List<BaseEntity>();
            foreach (Dictionary<string, object> dictionary in lstRows)
            {
                var Misc = (Miscellaneous)BuildObject(dictionary);
                list.Add(Misc);
            }
            return list;
        }

        public SqlOperation GetRetrieveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "RET_MISC"
            };
            var misc = (Miscellaneous)entity;
            operation.AddVarcharParam(DbColDetail, misc.Detail);
            return operation;
          
        }
              
    }
}
