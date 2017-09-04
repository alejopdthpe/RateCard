using RateCard.ApiCore.Dao;
using RateCard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RateCard.ApiCore.Mappers
{
    public class SLAMapper : EntityMapper, IObjectMapper
    {
        private const string DbColDescription = "DESCRIPTION";
        private const string DbColValueAmnt = "VALUE_AMNT";
        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            List<BaseEntity> list = new List<BaseEntity>();
            foreach (Dictionary<string, object> dictionary in lstRows)
            {
                var Sla = (SLA)BuildObject(dictionary);
                list.Add(Sla);
            }
            return list;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Sla = new SLA()
            {
                Description = GetStringValue(row, DbColDescription),
                Value_Amnt = GetDoubleValue(row, DbColValueAmnt)
            };

            return Sla;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "RET_ALL_SLA"
            };
            return operation;
        }
    }
}
