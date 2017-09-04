using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RateCard.ApiCore.Dao;
using RateCard.Entities;

namespace RateCard.ApiCore.Mappers
{
    public class LocationCostMapper : EntityMapper, IObjectMapper
    {

        private const string DbColCountry = "COUNTRY";
        private const string DbColL1 = "L1";
        private const string DbColL2 = "L2";
        private const string DbColL3 = "L3";
        private const string DbColL4 = "L4";
        private const string DbColInflationRate = "INFLATION_RATE";
        private const string DbColEfficiency = "EFFICIENCY";
        private const string DbColIncrementalRate = "INCREMENTAL_ RATE";
        private const string DbColDecreaseRate = "DECREASE_RATE";
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Location_Cost = new LocationCost()
            {
                Country = GetStringValue(row, DbColCountry),
                L1 = GetDoubleValue(row, DbColL1),
                L2 = GetDoubleValue(row, DbColL2),
                L3 = GetDoubleValue(row, DbColL3),
                L4 = GetDoubleValue(row, DbColL4),
                Inflation_Rate = GetDoubleValue(row, DbColInflationRate),
                Efficiency = GetDoubleValue(row, DbColEfficiency),
                Incremental_Rate = GetDoubleValue(row, DbColIncrementalRate),
                Decrease_Rate = GetDoubleValue(row, DbColDecreaseRate)
            };
            return Location_Cost;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            List<BaseEntity> list = new List<BaseEntity>();
           foreach(Dictionary<string, object> dictionary in lstRows)
            {
                var Location_Cost = (LocationCost)BuildObject(dictionary);
                list.Add(Location_Cost);
            }
            return list;
        }
        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "RET_ALL_LOC_COSTS"
            };
            return operation;
        }


    }
}
