using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RateCard.Entities;
using RateCard.ApiCore.Dao;

namespace RateCard.ApiCore.Mappers
{
    public class TSSOfferPriceMapper : EntityMapper, IObjectMapper
    {
        private const string DbColAnnualTotalListCostMin = "ANNUAL_TOTAL_LIST_COST_MIN";
        private const string DbColSAW = "SAW";
        private const string DbColSAAS = "SAAS";
        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            List<BaseEntity> list = new List<BaseEntity>();
            foreach (Dictionary<string, object> dictionary in lstRows)
            {
                var TSSOffer = (TSSOfferPrice)BuildObject(dictionary);
                list.Add(TSSOffer);
            }
            return list;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var TSSOffer = new TSSOfferPrice()
            {
                ANNUAL_TOTAL_LIST_COST_MIN = GetDoubleValue(row, DbColAnnualTotalListCostMin),
                SAW = GetDoubleValue(row, DbColSAW),
                SAAS = GetDoubleValue(row, DbColSAAS)
            };

            return TSSOffer;
        }
        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "RET_ALL_TSS_OSS_OFFERING"
            };
            return operation;
        }
    }
}
