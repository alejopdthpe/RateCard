using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RateCard.Entities;
using RateCard.ApiCore.Dao;

namespace RateCard.ApiCore.Mappers
{
    public class RateCardRevisedMapper : EntityMapper, IObjectMapper
    {

        private const string DbColPartNumber = "PART_NUMBER";
        private const string DbColDescription = "DESCRIPTION";
        private const string DbColListPrice = "LIST_PRICE";
        private const string DbColMouDiscRate = "MOU_DISC_RATE";
        private const string DbColYears = "YEARS"; 
        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            List<BaseEntity> list = new List<BaseEntity>();
            foreach (Dictionary<string, object> dictionary in lstRows)
            {
                var RateCardRevised = (RateCardRevised)BuildObject(dictionary);
                list.Add(RateCardRevised);
            }
            return list;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var RateCardRevised = new RateCardRevised()
            {
                Part_Number = GetStringValue(row, DbColPartNumber),
                Description = GetStringValue(row,DbColDescription),
                List_Price = GetDoubleValue(row,DbColListPrice),
                Mou_Disc_Rate = GetStringValue(row,DbColMouDiscRate),
                Years = GetIntValue(row, DbColYears)
            };

            return RateCardRevised;
        }
 
        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation()
            {
                ProcedureName = "RET_ALL_TS_OSS_HPE_SW_RATECARD"
            };
            return operation;
        }
    }
}
