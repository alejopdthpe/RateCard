using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RateCard.Entities;
using RateCard.ApiCore.Dao;

namespace RateCard.ApiCore.Mappers
{
    public class TechnologyMapper : EntityMapper, IObjectMapper
    {
        private const string DbColDescription = "DESCRIPTION";
        private const string DbColMonitoring = "MONITORING";
        private const string DbColIncidents = "INCIDENTS";
        private const string DbColServices = "SERVICES";
        private const string DbColType = "TYPE";
        private const string DbColUnitOfMeasure = "UNIT_MEASURE";
        private const string DbColAvgTBs = "AVERAGE_TBS";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Technology = new Technology()
            {
                Description = GetStringValue(row, DbColDescription),
                Monitoring = GetDoubleValue(row, DbColMonitoring),
                Incidents = GetDoubleValue(row, DbColIncidents),
                Services = GetDoubleValue(row, DbColServices),
                Type = GetStringValue(row, DbColType),
                Unit_Of_Measure = GetStringValue(row,DbColUnitOfMeasure),
                Avg_TBs = GetDoubleValue(row,DbColAvgTBs)
            };

            return Technology;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            List<BaseEntity> list = new List<BaseEntity>();
            foreach (Dictionary<string, object> dictionary in lstRows)
            {
                var Technology = (Technology)BuildObject(dictionary);
                list.Add(Technology);
            }
            return list;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "RET_ALL_TECH_BY_MINUTE"
            };
            return operation;
        }
    }
}
